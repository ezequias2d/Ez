using Ez.Graphics.API.Resources;
using Ez.Graphics.API.Vulkan.Core.Allocator;
using Ez.Memory;

using Silk.NET.Vulkan;

using System;
using System.Threading.Tasks;

using BufferCreateInfo = Ez.Graphics.API.CreateInfos.BufferCreateInfo;
using VkBuffer = Silk.NET.Vulkan.Buffer;
using VkBufferCreateInfo = Silk.NET.Vulkan.BufferCreateInfo;
namespace Ez.Graphics.API.Vulkan.Core
{
    internal class Buffer : DeviceResource, IBuffer
    {
        private IFence _fence;
        public Buffer(Device device, in BufferCreateInfo createInfo) : base(device)
        {
            VkBuffer = CreateBuffer(createInfo);
            Allocation = Device.Allocator.CreateBuffer(VkBuffer, createInfo, false);
            Device.Vk.BindBufferMemory(Device, VkBuffer, Allocation.Handle, Allocation.Offset);
            Size = createInfo.Size;
            Usage = createInfo.Usage;
            MemoryUsage = createInfo.MemoryUsage;
        }

        private VkBuffer VkBuffer { get; }
        private IAllocation Allocation { get; }

        public long Size { get; }

        public BufferUsage Usage { get; }
        public MemoryUsage MemoryUsage { get; }

        public (IntPtr Ptr, long Length) Map()
        {
            var ptr = Allocation.MapMemory();
            return (ptr, (long)Allocation.Size);
        }

        public void SubData<U>(ReadOnlySpan<U> source, long bufferOffset) where U : unmanaged
        {
            if (!this.TrySubData(source, bufferOffset))
            {
                if (!Usage.HasFlag(BufferUsage.TransferDestination))
                    throw new VkException($"The buffer must have {BufferUsage.TransferDestination} flag to upload data when is not mappable.");
                var byteSize = source.Length * MemUtil.SizeOf<U>();
                using var staging = new Buffer(Device, new BufferCreateInfo(byteSize, BufferUsage.TransferSource, MemoryUsage.CpuToGpu));

                if (staging.TrySubData(source, 0))
                {
                    using var commandBuffer = Device.Factory.CreateCommandBuffer();
                    commandBuffer.Name = "IBuffer.SubData ICommandBuffer";

                    commandBuffer.Begin();
                    commandBuffer.CopyBuffer(staging, this, new BufferCopy[]
                    {
                        new()
                        {
                            SrcOffset = 0,
                            DstOffset = bufferOffset,
                            Size = byteSize,
                        }
                    });
                    commandBuffer.End();

                    var fence = GetFence();

                    var submitInfo = new SubmitInfo
                    {
                        CommandBuffers = new ICommandBuffer[] { commandBuffer },
                    };
                    Device.Submit(submitInfo, fence);
                    fence.Wait(ulong.MaxValue);
                }
                else
                    throw new NotImplementedException();
            }
        }

        public Task SubDataAsync<U>(ReadOnlySpan<U> source, long bufferOffset) where U : unmanaged
        {
            if (!this.TrySubData(source, bufferOffset))
            {
                if (!Usage.HasFlag(BufferUsage.TransferDestination))
                    throw new VkException($"The buffer must have {BufferUsage.TransferDestination} flag to upload data when is not mappable.");

                var byteSize = source.Length * MemUtil.SizeOf<U>();
                var staging = new Buffer(Device, new BufferCreateInfo(byteSize, BufferUsage.TransferSource, MemoryUsage.CpuToGpu));

                if (staging.TrySubData(source, 0))
                {
                    return Task.Run(() =>
                    {
                        using var commandBuffer = Device.Factory.CreateCommandBuffer();
                        commandBuffer.Name = "IBuffer.SubData ICommandBuffer";

                        commandBuffer.Begin();
                        commandBuffer.CopyBuffer(staging, this, new BufferCopy[]
                        {
                        new()
                        {
                            SrcOffset = 0,
                            DstOffset = bufferOffset,
                            Size = byteSize,
                        }
                        });
                        commandBuffer.End();

                        var fence = GetFence();

                        var submitInfo = new SubmitInfo
                        {
                            CommandBuffers = new ICommandBuffer[] { commandBuffer },
                        };
                        Device.Submit(submitInfo, fence);
                        fence.Wait(ulong.MaxValue);

                        staging.Dispose();
                    });
                }
                else
                    throw new NotImplementedException();
            }

            return Task.CompletedTask;
        }

        public void Unmap()
        {
            Allocation.UnmapMemory();
        }

        protected override void ManagedDispose()
        {
            Allocation.Dispose();
        }

        protected unsafe override void UnmanagedDispose()
        {
            Device.Vk.DestroyBuffer(Device, VkBuffer, null);
        }

        private unsafe VkBuffer CreateBuffer(in BufferCreateInfo createInfo)
        {
            var families = Device.Families;
            var familyCount = families.Count;
            var queueFamilyIndices = stackalloc uint[familyCount];
            for (var i = 0; i < familyCount; i++)
                queueFamilyIndices[i] = Device.Families[i].Index;

            var ci = new VkBufferCreateInfo
            {
                SType = StructureType.BufferCreateInfo,
                Size = (ulong)createInfo.Size,
                Usage = createInfo.Usage.ToVk(),
                SharingMode = (familyCount == 1) ?
                    SharingMode.Exclusive :
                    SharingMode.Concurrent,
                QueueFamilyIndexCount = (uint)familyCount,
                PQueueFamilyIndices = queueFamilyIndices,
            };

            var result = Device.Vk.CreateBuffer(Device, ci, null, out var buffer);
            result.CheckResult("Failed to create the buffer!");

            return buffer;
        }

        public static implicit operator VkBuffer(Buffer buffer)
        {
            buffer.CheckDispose();
            return buffer.VkBuffer;
        }

        private IFence GetFence()
        {
            if (_fence == null)
                _fence = Device.Factory.CreateFence();
            else
                _fence.Reset();
            return _fence;
        }
    }
}
