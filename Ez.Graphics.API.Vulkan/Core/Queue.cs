// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Silk.NET.Vulkan;
using VkCommandBuffer = Silk.NET.Vulkan.CommandBuffer;
using VkFence = Silk.NET.Vulkan.Fence;
using VkQueue = Silk.NET.Vulkan.Queue;
using VkSemaphore = Silk.NET.Vulkan.Semaphore;
using VkSubmitInfo = Silk.NET.Vulkan.SubmitInfo;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class Queue : DeviceResource
    {
        public Queue(Device device, QueueFamily family, VkQueue queue) : base(device)
        {
            VkQueue = queue;
            Family = family;
        }

        public QueueFamily Family { get; }
        private VkQueue VkQueue { get; }

        public bool Equals(Queue other) =>
            other != null && VkQueue.Handle == other.VkQueue.Handle;

        public unsafe void Submit(SubmitInfo info, Fence fence)
        {
            var commandBuffers = stackalloc VkCommandBuffer[info.CommandBuffers.Length];
            var waitSemaphores = stackalloc VkSemaphore[info.WaitSemaphores.Length];
            var waitDstStageMask = stackalloc PipelineStageFlags[info.WaitSemaphores.Length];
            var vkSignalSemaphores = stackalloc VkSemaphore[info.SignalSemaphores.Length];

            for (var i = 0; i < info.CommandBuffers.Length; i++)
                //commandBuffers[i] = info.CommandBuffers[i] as NaiveCommandBuffer ??
                commandBuffers[i] = info.CommandBuffers[i] as CommandBuffer;

            for (var i = 0; i < info.WaitSemaphores.Length; i++)
            {
                waitSemaphores[i] = (Semaphore)info.WaitSemaphores[i];
                waitDstStageMask[i] = PipelineStageFlags.PipelineStageBottomOfPipeBit;
            }

            for (var i = 0; i < info.SignalSemaphores.Length; i++)
                vkSignalSemaphores[i] = (Semaphore)info.SignalSemaphores[i];

            var submitInfo = new VkSubmitInfo
            {
                SType = StructureType.SubmitInfo,

                CommandBufferCount = (uint)info.CommandBuffers.Length,
                PCommandBuffers = commandBuffers,

                SignalSemaphoreCount = (uint)info.SignalSemaphores.Length,
                PSignalSemaphores = vkSignalSemaphores,

                WaitSemaphoreCount = (uint)info.WaitSemaphores.Length,
                PWaitSemaphores = waitSemaphores,
                PWaitDstStageMask = waitDstStageMask,

            };

            var vkFence = fence ?? default(VkFence);
            lock (this)
            {
                Device.Vk.QueueSubmit(VkQueue, 1, &submitInfo, vkFence);
            }
        }

        protected override void ManagedDispose()
        {

        }

        protected override void UnmanagedDispose()
        {

        }

        public static implicit operator VkQueue(Queue queue) =>
            queue.VkQueue;
    }
}
