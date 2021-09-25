// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;
using Silk.NET.Vulkan;
using System;
using System.Buffers;
using System.Numerics;
using System.Threading.Tasks;

using BufferCreateInfo = Ez.Graphics.API.CreateInfos.BufferCreateInfo;
using VkBuffer = Silk.NET.Vulkan.Buffer;
using VkImage = Silk.NET.Vulkan.Image;
namespace Ez.Graphics.API.Vulkan.Core.Allocator
{
    internal class NaiveAllocator : DeviceResource, IAllocator
    {
        public NaiveAllocator(Device device) : base(device)
        {
            Device.Vk.GetPhysicalDeviceMemoryProperties(Device.PhysicalDevice, out _memoryProperties);
            Device.Vk.GetPhysicalDeviceProperties(Device.PhysicalDevice, out _deviceProperties);

            _isIntegratedGpu = _deviceProperties.DeviceType == PhysicalDeviceType.IntegratedGpu;
        }

        private readonly bool _isIntegratedGpu;
        private readonly PhysicalDeviceProperties _deviceProperties;
        private readonly PhysicalDeviceMemoryProperties _memoryProperties;

        public async Task<IAllocation> CreateBufferAsync(VkBuffer buffer, BufferCreateInfo bufferCreateInfo, bool isDedicatedAllocation) =>
            await Task.Run(() => CreateBuffer(buffer, bufferCreateInfo, isDedicatedAllocation));

        public async Task<IAllocation> CreateTextureAsync(VkImage image, TextureCreateInfo textureCreateInfo, bool isDedicatedAllocation) =>
            await Task.Run(() => CreateTexture(image, textureCreateInfo, isDedicatedAllocation));

        protected override void ManagedDispose()
        {
        }

        protected override void UnmanagedDispose()
        {
        }

        public unsafe IAllocation CreateBuffer(VkBuffer buffer, BufferCreateInfo bufferCreateInfo, bool isDedicatedAllocation)
        {
            Device.Vk.GetBufferMemoryRequirements(Device, buffer, out var memoryRequirements);
            return Create(bufferCreateInfo.MemoryUsage, memoryRequirements);
        }

        public unsafe IAllocation CreateTexture(VkImage image, TextureCreateInfo textureCreateInfo, bool isDedicatedAllocation)
        {
            Device.Vk.GetImageMemoryRequirements(Device, image, out var memoryRequirements);
            return Create(textureCreateInfo.MemoryMode, memoryRequirements);
        }

        private unsafe IAllocation Create(MemoryUsage mode, in MemoryRequirements requirements)
        {
            var allocInfo = new MemoryAllocateInfo
            {
                SType = StructureType.MemoryAllocateInfo,
                AllocationSize = requirements.Size,
                MemoryTypeIndex = FindProperties(requirements.MemoryTypeBits, GetPropertyFlags(mode)),
            };

            var result = Device.Vk.AllocateMemory(Device, allocInfo, null, out var memory);
            result.CheckResult();

            return new NaiveAllocation(Device, memory, requirements.MemoryTypeBits, 0, requirements.Size);
        }

        private uint FindProperties(in uint memoryTypeBitsRequirement,
            in (MemoryPropertyFlags required, MemoryPropertyFlags preferred) flags)
        {
            // get all compatible memories 
            var supportedMemoryTypes = FindRequiredProperties(memoryTypeBitsRequirement, flags.required);

            var maxPreferredCount = BitOperations.PopCount((uint)flags.preferred);

            var preferredCount = -1;
            var current = -1;

            // search by best compatible memory
            foreach (var memoryIndex in supportedMemoryTypes)
            {
                var properties = _memoryProperties.MemoryTypes[memoryIndex].PropertyFlags;
                var count = BitOperations.PopCount((uint)(properties & flags.preferred));

                if (count > preferredCount)
                {
                    preferredCount = count;
                    current = memoryIndex;

                    // stop search, biggest found 
                    if (preferredCount == maxPreferredCount)
                        break;
                }
            }

            if (current == -1)
                throw new VkException("Failed to find suitable memory type!");
            return (uint)current;
        }

        private ReadOnlySpan<int> FindRequiredProperties(in uint memoryTypeBitsRequirement, MemoryPropertyFlags required)
        {
            var memoryCount = _memoryProperties.MemoryTypeCount;
            var supportedCount = 0;
            var supported = ArrayPool<int>.Shared.Rent((int)memoryCount);

            for (var memoryIndex = 0; memoryIndex < memoryCount; memoryIndex++)
            {
                var memoryTypeBits = (1 << memoryIndex);
                var isRequiredMemoryType = (memoryTypeBitsRequirement & memoryTypeBits) != 0;

                var properties = _memoryProperties.MemoryTypes[memoryIndex].PropertyFlags;
                var hasRequiredProperties = (properties & required) == required;

                if (isRequiredMemoryType && hasRequiredProperties)
                    supported[supportedCount++] = memoryIndex;
            }

            return new Span<int>(supported, 0, supportedCount);
        }

        private (MemoryPropertyFlags Required, MemoryPropertyFlags Preferred) GetPropertyFlags(MemoryUsage mode)
        {
            var required = default(MemoryPropertyFlags);
            var preferred = default(MemoryPropertyFlags);

            switch (mode)
            {
                case MemoryUsage.CpuOnly:
                    required |= MemoryPropertyFlags.MemoryPropertyHostVisibleBit | MemoryPropertyFlags.MemoryPropertyHostCoherentBit;
                    break;
                case MemoryUsage.CpuToGpu:
                    required |= MemoryPropertyFlags.MemoryPropertyHostVisibleBit;
                    if (_isIntegratedGpu)
                        preferred |= MemoryPropertyFlags.MemoryPropertyDeviceLocalBit;
                    break;
                case MemoryUsage.GpuOnly:
                    if (!_isIntegratedGpu)
                        preferred |= MemoryPropertyFlags.MemoryPropertyDeviceLocalBit;
                    break;
                case MemoryUsage.GpuToCpu:
                    required |= MemoryPropertyFlags.MemoryPropertyHostVisibleBit;
                    preferred |= MemoryPropertyFlags.MemoryPropertyHostCachedBit;
                    break;
            }

            return (required, preferred);
        }

        private class NaiveAllocation : Disposable, IAllocation
        {
            public NaiveAllocation(Device device, DeviceMemory deviceMemory, uint memoryType, ulong offset, ulong size)
            {
                Device = device;
                Handle = deviceMemory;
                MemoryType = memoryType;
                Offset = offset;
                Size = size;
                IsMapped = false;
            }

            private Device Device { get; }
            public bool IsMapped { get; private set; }
            public DeviceMemory Handle { get; }

            public uint MemoryType { get; }

            public ulong Offset { get; }

            public ulong Size { get; }

            public unsafe IntPtr MapMemory()
            {
                void* ptr = null;
                var result = Device.Vk.MapMemory(Device, Handle, Offset, Size, 0, ref ptr);
                result.CheckResult("Unable to map memory.");
                IsMapped = true;
                return new IntPtr(ptr);
            }

            public void UnmapMemory()
            {
                if (IsMapped)
                {
                    IsMapped = false;
                    Device.Vk.UnmapMemory(Device, Handle);
                }
                else
                    throw new VkException("The memory is not mapped.");
            }

            protected unsafe override void UnmanagedDispose()
            {
                Device.Vk.FreeMemory(Device, Handle, null);
            }

            protected override void ManagedDispose()
            {

            }
        }
    }
}
