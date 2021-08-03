// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;
using Ez.Graphics.API.Vulkan.Core;
using Ez.Graphics.API.Vulkan.Core.Allocator;
using Ez.Graphics.API.Vulkan.Core.Cached.RenderPasses;
using Ez.Graphics.Contexts;
using Ez.Memory;
using Microsoft.Extensions.Logging;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using Silk.NET.Vulkan.Extensions.KHR;
using System;
using System.Collections.Generic;
using System.Linq;
using Fence = Ez.Graphics.API.Vulkan.Core.Fence;
using VkDevice = Silk.NET.Vulkan.Device;
using VkFence = Silk.NET.Vulkan.Fence;
namespace Ez.Graphics.API.Vulkan
{
    public class Device : Disposable, IDevice
    {
        private Core.Queue _presentQueue;

        public Device(ILogger logger,
            CreateInfos.DeviceCreateInfo deviceCreateInfo,
            VulkanRequiredExtensions context,
            SwapchainCreateInfo? defaultSwapchainCreateInfo,
            Func<IEnumerable<PhysicalDevice>, PhysicalDevice> selectDeviceCallback)
        {
            Logger = logger;
            Vk = Vk.GetApi();
            Extensions = new ExtensionsHelper(this);

            AddExtensions(deviceCreateInfo, context);
            Instance = new Instance(this, context, deviceCreateInfo.Debug);

            PhysicalDevice = selectDeviceCallback.Invoke(Instance.GetPhysicalDevices());
            Extensions.LoadDeviceExtensions(PhysicalDevice);

            Families = QueueFamily.LoadFamilies(this, out var totalQueues);
            Handle = CreateLogicalDevice(deviceCreateInfo, totalQueues);

            foreach (var family in Families)
                family.InitializeQueues();

            if (!TryGetExtension<KhrSwapchain>(out var khrSwapchain))
                throw new VkException();
            KhrSwapchain = khrSwapchain;

            if (!Instance.TryGetExtension<KhrSurface>(out var khrSurface))
                throw new VkException();
            KhrSurface = khrSurface;

            if (!TryGetExtension<KhrMaintenance1>(out var khrMaintenance1))
                throw new VkException();
            Maintenance1 = khrMaintenance1;

            LayoutCache = new DescriptorSetLayoutCache(this);

            Allocator = new NaiveAllocator(this);
            Factory = new Factory(this);
            GraphicsQueue = GetGraphicsQueue();

            RenderPassCache = new(this);
            PipelineCache = new(this);
            DescriptorSetLayoutCache = new(this);
            CommandPool = new(this);

            if (defaultSwapchainCreateInfo.HasValue)
                DefaultSwapchain = new Swapchain(this, defaultSwapchainCreateInfo.Value);

            NearestSampler = Factory.CreateSampler(CreateInfos.SamplerCreateInfo.Nearest);
            LinearSampler = Factory.CreateSampler(CreateInfos.SamplerCreateInfo.Linear);
            AnisotropicLinear4Sampler = Factory.CreateSampler(CreateInfos.SamplerCreateInfo.AnisotropicLinear4);
        }

        internal IReadOnlyList<QueueFamily> Families { get; }
        internal ILogger Logger { get; }
        internal VkDevice Handle { get; }
        internal IAllocator Allocator { get; }
        internal Vk Vk { get; }
        internal Instance Instance { get; }
        internal PhysicalDevice PhysicalDevice { get; }
        internal ExtensionsHelper Extensions { get; }
        internal KhrSwapchain KhrSwapchain { get; }
        internal KhrSurface KhrSurface { get; }
        internal ExtDebugUtils DebugUtils => Instance.DebugUtils;
        internal KhrMaintenance1 Maintenance1 { get; }
        internal DescriptorSetLayoutCache LayoutCache { get; }
        internal Core.CommandPool CommandPool { get; }
        internal Core.Queue GraphicsQueue { get; }
        internal RenderPassCache RenderPassCache { get; }
        internal Core.Cached.PipelineCache PipelineCache { get; }
        internal DescriptorSetLayoutCache DescriptorSetLayoutCache { get; }
        public ISampler NearestSampler { get; }

        public ISampler LinearSampler { get; }

        public ISampler AnisotropicLinear4Sampler { get; }

        public uint MinUniformBufferOffsetAlignment => throw new NotImplementedException();

        public uint MinStorageBufferOffsetAlignment => throw new NotImplementedException();

        public IFactory Factory { get; }

        public ISwapchain DefaultSwapchain { get; }

        public PhysicalDeviceFeatures Features => throw new NotImplementedException();

        private static void AddExtensions(CreateInfos.DeviceCreateInfo deviceCreateInfo, VulkanRequiredExtensions context)
        {
            if (deviceCreateInfo.Debug)
            {
                context.RequiredInstanceExtensions.Add(ExtDebugUtils.ExtensionName);
            }
            context.RequiredDeviceExtensions.Add(KhrSwapchain.ExtensionName);
            context.RequiredDeviceExtensions.Add(KhrMaintenance1.ExtensionName);
        }

        internal bool TryGetExtension<T>(out T ext) where T : NativeExtension<Vk> =>
            Vk.TryGetDeviceExtension(Instance.VkInstance, Handle, out ext);

        private VkDevice CreateLogicalDevice(CreateInfos.DeviceCreateInfo deviceCreateInfo, uint totalQueues)
        {
            var queueCreateInfos = new DeviceQueueCreateInfo[Families.Count];
            var priorities = MemoryBlockPool.Get(totalQueues * sizeof(float));
            for (var i = 0; i < queueCreateInfos.Length; i++)
            {
                const float defaultPriority = 1.0f;
                var familyPriorities = priorities.AllocPinnedMemory<float>((int)Families[i].TotalCount);
                MemUtil.Set(familyPriorities.Ptr, defaultPriority, familyPriorities.Count);

                queueCreateInfos[i].SType = StructureType.DeviceQueueCreateInfo;
                queueCreateInfos[i].QueueFamilyIndex = (uint)i;
                queueCreateInfos[i].QueueCount = Families[i].TotalCount;
                unsafe
                {
                    queueCreateInfos[i].PQueuePriorities = (float*)familyPriorities.Ptr;
                }
            }

            var enabledFeatures = PhysicalDevice.VkFeatures;

            unsafe
            {
                List<string> unsupportedExtensions = new List<string>();
                List<Utf8FixedString> extensions = new List<Utf8FixedString>();
                byte** ptrExtensions = stackalloc byte*[Extensions.DeviceExtensions.Count()];

                var i = 0;
                void AddExtension(string name)
                {
                    if (Extensions.SupportDeviceExtension(name))
                    {
                        extensions.Add(name);
                        ptrExtensions[i] = extensions[i];
                        i++;
                    }
                    else
                        unsupportedExtensions.Add(name);
                }

                AddExtension(KhrSwapchain.ExtensionName);
                AddExtension(KhrMaintenance1.ExtensionName);
                AddExtension(KhrGetMemoryRequirements2.ExtensionName);
                AddExtension("VK_KHR_dedicated_allocation");

                if (unsupportedExtensions.Count != 0)
                {
                    Logger.LogError($"Missing extensions: {string.Join(", ", unsupportedExtensions)}");
                    // todo
                    throw new VkException("");
                }

                fixed (DeviceQueueCreateInfo* qcisPtr = queueCreateInfos)
                {
                    var dci = new Silk.NET.Vulkan.DeviceCreateInfo
                    {
                        SType = StructureType.DeviceCreateInfo,
                        PNext = default,
                        PQueueCreateInfos = qcisPtr,
                        QueueCreateInfoCount = (uint)queueCreateInfos.Length,
                        PEnabledFeatures = &enabledFeatures,
                        EnabledExtensionCount = (uint)extensions.Count,
                        PpEnabledExtensionNames = ptrExtensions,
                        EnabledLayerCount = 0,
                        PpEnabledLayerNames = null,
                    };

                    if (Vk.CreateDevice(PhysicalDevice.VkPhysicalDevice, dci, null, out var device) != Result.Success)
                        throw new VkException("Failed to create logical device!");

                    return device;
                }
            }
        }

        protected unsafe override void UnmanagedDispose()
        {
            Vk.DestroyDevice(Handle, null);
        }

        protected override void ManagedDispose()
        {

        }

        public bool TryGetPixelFormatProperties(PixelFormat format, TextureType type, TextureUsage usage, TextureTiling tiling, out PixelFormatProperties properties)
        {
            var vkFormat = format.ToVk();
            var vkType = type.ToVk();
            var vkUsage = usage.ToVk();
            var vkTiling = tiling.ToVk();


            var result = Vk.GetPhysicalDeviceImageFormatProperties(PhysicalDevice, vkFormat, vkType, vkTiling, vkUsage, default, out var ifp);
            if (result != Result.Success)
            {
                properties = default;
                return false;
            }

            properties = new(ifp.MaxExtent.ToEz(), ifp.MaxMipLevels, ifp.MaxArrayLayers, ifp.SampleCounts.ToEz());
            return true;
        }

        public bool WaitForFences(IEnumerable<IFence> fences, bool waitAll, ulong timeout)
        {
            Span<VkFence> vkFences = stackalloc VkFence[fences.Count()];
            {
                var i = 0;
                foreach (var fence in fences)
                    vkFences[i++] = (Fence)fence;
            }

            return Vk.WaitForFences(this, vkFences, waitAll, timeout) == Result.Success;
        }

        public bool WaitForFences(IEnumerable<IFence> fences, bool waitAll, TimeSpan timeout) =>
            WaitForFences(fences, waitAll, (ulong)(timeout.TotalMilliseconds * 1000000));

        public void Submit(SubmitInfo submitInfo, IFence fence) => GraphicsQueue.Submit(submitInfo, (Fence)fence);

        public void Wait()
        {
            Vk.DeviceWaitIdle(Handle);
            Vk.QueueWaitIdle(GraphicsQueue);

            if (_presentQueue != null)
                Vk.QueueWaitIdle(_presentQueue);
        }

        private Core.Queue GetGraphicsQueue()
        {
            foreach (var family in Families)
            {
                if ((family.QueueFlags & QueueFlags.QueueGraphicsBit) != 0)
                    return family.GetQueue();
            }
            throw new VkException("Unable to find a graphics queue");
        }

        internal Core.Queue GetPresentQueue(Swapchain swapchain)
        {
            if (_presentQueue == null && swapchain == null)
                throw new VkException("Unable to get queue for null swapchain!");

            if (_presentQueue == null)
            {
                foreach (var family in Families)
                    if (family.SupportsPresentation(swapchain))
                    {
                        _presentQueue = family.GetQueue();
                        break;
                    }
            }
            return _presentQueue ?? throw new VkException("Unable to present the swapchain!");
        }

        public static implicit operator VkDevice(Device d) => d.Handle;
    }
}
