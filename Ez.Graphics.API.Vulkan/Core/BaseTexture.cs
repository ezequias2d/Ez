using Ez.Graphics.API.Resources;
using Ez.Numerics;
using Silk.NET.Vulkan;
using System;
using System.Threading.Tasks;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal abstract class BaseTexture : DeviceResource, ITexture
    {
        public BaseTexture(Device device, MemoryUsage usage) : base(device)
        {
            MemoryUsage = usage;
        }
        public abstract PixelFormat Format { get; }
        public abstract Size3 Size { get; }
        public abstract uint MipmapLevels { get; }
        public abstract uint ArrayLayers { get; }
        public abstract TextureUsage Usage { get; }
        public abstract TextureType Type { get; }
        public abstract SampleCount SampleCount { get; }
        public abstract TextureTiling Tiling { get; }
        public MemoryUsage MemoryUsage { get; }

        public void SubData<T>(ReadOnlySpan<T> source, uint mipmapLevel, int x, int y, int z, uint width, uint height, uint depth) where T : unmanaged
        {
            CheckSubDataLimits(mipmapLevel, x, y, z, width, height, depth);

            using var staging = Device.Factory.CreateBuffer(new(source.Length, BufferUsage.TransferSource, MemoryUsage.CpuToGpu));

            if (staging.TrySubData(source, 0))
                SubDataStaging(staging, mipmapLevel, x, y, z, width, height, depth);
            else
                throw new NotImplementedException();
        }

        public Task SubDataAsync<T>(ReadOnlySpan<T> source, uint mipmapLevel, int x, int y, int z, uint width, uint height, uint depth) where T : unmanaged
        {
            CheckSubDataLimits(mipmapLevel, x, y, z, width, height, depth);

            var staging = Device.Factory.CreateBuffer(new(source.Length, BufferUsage.TransferSource, MemoryUsage.CpuToGpu));

            if (staging.TrySubData(source, 0))
                return Task.Run(() =>
                {
                    SubDataStaging(staging, mipmapLevel, x, y, z, width, height, depth);
                    staging.Dispose();
                });
            else
                throw new NotImplementedException();
        }

        private void CheckSubDataLimits(uint mipmapLevel, int x, int y, int z, uint width, uint height, uint depth)
        {
            if (!(x >= 0 && x + width <= Size.Width &&
                           y >= 0 && y + height <= Size.Height &&
                           z >= 0 && x + depth <= Size.Depth))
                throw new VkException($"The portion of the {nameof(ITexture)} to be used must be completely contained within the {nameof(ITexture)}.");
        }

        private void SubDataStaging(IBuffer staging, uint mipmapLevel, int x, int y, int z, uint width, uint height, uint depth)
        {
            using var commandBuffer = Device.Factory.CreateCommandBuffer();
            commandBuffer.Name = "ITexture.SubData ICommandBuffer";

            commandBuffer.Begin();
            var btc = new BufferTextureCopy()
            {
                BufferOffset = 0,
                BufferRowLength = 0,
                BufferTextureHeight = 0,

                TextureOffset = new(x, y, z),
                TextureExtent = new(width, height, depth),
                TextureSubresource = new()
                {
                    BaseArrayLayer = 0,
                    LayerCount = 1,
                    MipmapLevel = mipmapLevel,
                },
            };

            switch (Type)
            {
                case TextureType.Texture1D:
                    btc.TextureOffset.Y = btc.TextureOffset.Z = 0;
                    btc.TextureExtent.Height = btc.TextureExtent.Depth = 1;
                    btc.TextureSubresource.BaseArrayLayer = (uint)y;
                    btc.TextureSubresource.LayerCount = height;
                    break;
                case TextureType.Texture2D:
                    btc.TextureOffset.Z = 0;
                    btc.TextureExtent.Depth = 1;
                    btc.TextureSubresource.BaseArrayLayer = (uint)z;
                    btc.TextureSubresource.LayerCount = depth;
                    break;
            }

            //Device.Vk.GetImageSubresourceLayout(Device, this)
            //    TransitionImageLayout
            commandBuffer.CopyBufferToTexture(staging, this, new BufferTextureCopy[] { btc });
            commandBuffer.End();

            var fence = Device.Factory.CreateFence();

            var submitInfo = new SubmitInfo
            {
                CommandBuffers = new ICommandBuffer[] { commandBuffer },
            };
            Device.Submit(submitInfo, fence);
            fence.Wait(ulong.MaxValue);
        }
        public abstract TextureView GetView();
        public abstract (IntPtr Ptr, long Length) Map();
        public abstract void Unmap();
        public abstract bool Equals(ITexture other);

        public static implicit operator ImageView(BaseTexture texture) => texture.GetView();
    }
}
