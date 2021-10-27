// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Resources;
using Ez.Numerics;
using Silk.NET.Vulkan;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class QueueFamily : DeviceResource
    {
        public static IReadOnlyList<QueueFamily> LoadFamilies(Device device, out uint totalQueues)
        {
            var families = new List<QueueFamily>();

            var index = 0u;
            totalQueues = 0;
            foreach (var p in device.PhysicalDevice.GetQueueFamilyProperties())
            {
                families.Add(new QueueFamily(device, index++, p));
                totalQueues += p.QueueCount;
            }

            return families;
        }

        public QueueFamily(Device device, uint index, QueueFamilyProperties properties) : base(device)
        {
            Index = index;
            QueueFlags = properties.QueueFlags;
            TotalCount = properties.QueueCount;
            Count = TotalCount;
            Queues = new ConcurrentBag<Queue>();

            TextureTransferGranularity = new(properties.MinImageTransferGranularity.Width,
                properties.MinImageTransferGranularity.Height,
                properties.MinImageTransferGranularity.Depth);
        }

        private ConcurrentBag<Queue> Queues { get; }

        public uint Index { get; }

        public QueueFlags QueueFlags { get; private set; }

        public uint TotalCount { get; }

        public uint Count { get; }

        public Size3 TextureTransferGranularity { get; }

        internal void InitializeQueues()
        {
            for (var i = 0u; i < TotalCount; i++)
            {
                Device.Vk.GetDeviceQueue(Device.Handle, Index, i, out var queue);
                Queues.Add(new Queue(Device, this, queue));
            }
        }

        public bool SupportsPresentation(ISwapchain swapchain)
        {
            if (swapchain is not Swapchain sc)
                return false;

            Device.KhrSurface.GetPhysicalDeviceSurfaceSupport(Device.PhysicalDevice.VkPhysicalDevice, Index, sc, out var supported);

            return supported;
        }

        public Queue GetQueue()
        {
            if (Queues.TryPeek(out var queue))
                return queue;
            throw new VkException("Unable to get a queue!");
        }

        protected override void UnmanagedDispose()
        {

        }

        protected override void ManagedDispose()
        {
            foreach (var queue in Queues)
                queue.Dispose();
            Queues.Clear();
        }
    }
}
