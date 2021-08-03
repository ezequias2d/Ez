// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Silk.NET.Vulkan;
using System;
using System.Collections.Generic;

using VkDescriptorPool = Silk.NET.Vulkan.DescriptorPool;
using VkDescriptorSetLayout = Silk.NET.Vulkan.DescriptorSetLayout;
namespace Ez.Graphics.API.Vulkan.Core
{
    internal class DescriptorPoolNode : DeviceResource
    {
        private readonly VkDescriptorPool _handle;
        private readonly DescriptorSetLayout _layout;
        private readonly IList<DescriptorSet> _allocatedDescriptorSets;

        public DescriptorPoolNode(Device device, DescriptorSetLayout layout, ReadOnlySpan<DescriptorPoolSize> poolSizes, uint maxSets) : base(device)
        {
            Max = maxSets;
            Allocated = 0;
            _allocatedDescriptorSets = new List<DescriptorSet>();
            _layout = layout;

            unsafe
            {
                fixed (DescriptorPoolSize* pPoolSizes = poolSizes)
                {
                    var createInfo = new DescriptorPoolCreateInfo
                    {
                        SType = StructureType.DescriptorPoolCreateInfo,
                        Flags = DescriptorPoolCreateFlags.DescriptorPoolCreateFreeDescriptorSetBit,
                        PoolSizeCount = (uint)poolSizes.Length,
                        PPoolSizes = pPoolSizes,
                        MaxSets = Max,
                    };

                    var result = Device.Vk.CreateDescriptorPool(Device, createInfo, null, out _handle);
                    result.CheckResult();
                }
            }
        }

        public uint Allocated { get; private set; }

        public uint Max { get; }

        public uint Available => Max - Allocated;

        public DescriptorSet AllocateDescriptorSet()
        {
            if (Available <= 0)
                throw new VkException("The allocation limit for descriptor pool node has been reached.");

            Allocated++;

            DescriptorSet descriptorSet;
            unsafe
            {
                var setLayout = (VkDescriptorSetLayout)_layout;
                var allocInfo = new DescriptorSetAllocateInfo
                {
                    SType = StructureType.DescriptorSetAllocateInfo,
                    DescriptorPool = this,
                    DescriptorSetCount = 1,
                    PSetLayouts = &setLayout
                };

                var result = Device.Vk.AllocateDescriptorSets(Device, allocInfo, out var handle);
                result.CheckResult();
                descriptorSet = new DescriptorSet(Device, handle, this, _layout);
            }

            _allocatedDescriptorSets.Add(descriptorSet);
            return descriptorSet;
        }

        internal void FreeDescriptorSet(DescriptorSet set)
        {
            if (set.Pool != this)
                throw new VkException("To dispose a  descriptor set, use the pool for which the part was created.");

            lock (this)
            {
                if (_allocatedDescriptorSets.Contains(set))
                {
                    _allocatedDescriptorSets.Remove(set);
                    var result = Device.Vk.FreeDescriptorSets(Device, this, 1, set);
                    result.CheckResult();
                    Allocated--;
                }
            }
        }

        protected override void ManagedDispose()
        {

        }

        protected unsafe override void UnmanagedDispose()
        {
            Device.Vk.DestroyDescriptorPool(Device, this, null);
        }

        public static implicit operator VkDescriptorPool(DescriptorPoolNode node) =>
            node._handle;
    }
}
