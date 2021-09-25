using Ez.Graphics.API.Vulkan.Core.CommandBuffers;
using Silk.NET.Vulkan;
using System.Threading;
using VkCommandPool = Silk.NET.Vulkan.CommandPool;
using VkCommandBuffer = Silk.NET.Vulkan.CommandBuffer;
using System;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class CommandPool : DeviceResource
    {
        private uint _freeAllocations;
        public CommandPool(Device device, Thread thread) : base(device)
        {
            //_cache = cache;
            var poolInfo = new CommandPoolCreateInfo
            {
                SType = StructureType.CommandPoolCreateInfo,
                QueueFamilyIndex = Device.GraphicsQueue.Family.Index,
                Flags = CommandPoolCreateFlags.CommandPoolCreateResetCommandBufferBit,
            };

            unsafe
            {
                var result = Device.Vk.CreateCommandPool(Device, poolInfo, null, out var commandPool);
                result.CheckResult("Failed to create command pool!");

                VkCommandPool = commandPool;
            }
            PoolThread = thread;
        }
        public Thread PoolThread { get; }
        private VkCommandPool VkCommandPool { get; }

        public VkCommandBuffer Alloc()
        {
            var allocInfo = new CommandBufferAllocateInfo
            {
                SType = StructureType.CommandBufferAllocateInfo,
                CommandPool = this,
                Level = CommandBufferLevel.Primary,
                CommandBufferCount = 1,
            };

            var result = Device.Vk.AllocateCommandBuffers(Device, allocInfo, out var cb);
            result.CheckResult("Failed to allocate command buffers!");
            return cb;
        }

        public void Free(VkCommandBuffer cb)
        {
            Span<VkCommandBuffer> cbs = stackalloc VkCommandBuffer[1];
            cbs[0] = cb;
            Device.Vk.FreeCommandBuffers(Device, this, cbs);
            if(++_freeAllocations == 16)
            {
                Device.Vk.TrimCommandPool(Device, this, 0);
                _freeAllocations = 0;
            }

        }

        protected override void ManagedDispose()
        {

        }

        protected unsafe override void UnmanagedDispose()
        {
            Device.Vk.DestroyCommandPool(Device, VkCommandPool, null);
        }

        public static implicit operator VkCommandPool(CommandPool command)
        {
            command.CheckDispose();
            return command.VkCommandPool;
        }
    }
}