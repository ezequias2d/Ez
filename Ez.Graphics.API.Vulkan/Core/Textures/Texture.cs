// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;
using Ez.Graphics.API.Vulkan.Core.Allocator;
using Ez.Numerics;
using Silk.NET.Vulkan;
using System;

namespace Ez.Graphics.API.Vulkan.Core.Textures
{
    internal class Texture : BaseTexture, ITexture
    {
        private readonly TextureView _view;
        public Texture(Device device, TextureCreateInfo ci) : base(device, ci.MemoryMode)
        {
            Format = ci.Format;
            Size = ci.Size;
            MipmapLevels = ci.MipLevels;
            ArrayLayers = ci.ArrayLayers;
            Usage = ci.Usage;
            Type = ci.Type;
            SampleCount = ci.Samples;
            Tiling = ci.Tiling;
            ImageAspect = GetImageAspectFlags();
            DefaultImageLayout = GetDefaultImageLayout(Usage);
            Image = CreateImage();

            Allocation = Device.Allocator.CreateTexture(Image, ci, false);
            var result = Device.Vk.BindImageMemory(Device, Image, Allocation.Handle, Allocation.Offset);
            result.CheckResult();

            _view = new TextureView(Device, new TextureViewCreateInfo
            {
                Texture = this,
                Format = Format,
                SubresourceRange = new()
                {
                    BaseArrayLayer = 0,
                    BaseMipmapLevel = 0,
                    ArrayLayerCount = ArrayLayers,
                    MipmapLevelCount = MipmapLevels,
                },
            });
            
            TransiantImageLayout(GetInitialLayout(), DefaultImageLayout);
        }

        public Texture(Device device, Image image, Format format, Extent2D extent, bool depth, ImageLayout layout) : base(device, MemoryUsage.GpuOnly)
        {
            VkFormat = format;
            Format = ToPixelFormat(format);
            Size = new(extent.Width, extent.Height, 1);
            MipmapLevels = 1;
            ArrayLayers = 1;
            Usage = depth ? TextureUsage.DepthStencilAttachment : TextureUsage.ColorAttachment;
            Type = TextureType.Texture2D;
            SampleCount = SampleCount.Count1;
            Image = image;
            Tiling = TextureTiling.Optimal;
            ImageAspect = GetImageAspectFlags();
            DefaultImageLayout = layout;
        }

        public Image Image { get; }
        public IAllocation Allocation { get; }
        public Format VkFormat { get; }
        public ImageAspectFlags ImageAspect { get; }
        public ImageLayout DefaultImageLayout { get; }
        public override PixelFormat Format { get; }

        public override Size3 Size { get; }

        public override uint MipmapLevels { get; }

        public override uint ArrayLayers { get; }

        public override TextureUsage Usage { get; }

        public override TextureType Type { get; }

        public override SampleCount SampleCount { get; }

        public override TextureTiling Tiling { get; }

        private static PixelFormat ToPixelFormat(Format format) =>
            format switch
            {
                Silk.NET.Vulkan.Format.R8G8B8A8Srgb => PixelFormat.R8G8B8A8Srgb,
                Silk.NET.Vulkan.Format.R8G8B8A8Unorm => PixelFormat.R8G8B8A8SNorm,
                Silk.NET.Vulkan.Format.B8G8R8A8Srgb => PixelFormat.B8G8R8A8Srgb,
                Silk.NET.Vulkan.Format.B8G8R8A8Unorm => PixelFormat.B8G8R8A8UNorm,
                _ => throw new VkException()
            };

        private unsafe Image CreateImage()
        {
            var families = Device.Families;
            var familyCount = families.Count;
            var queueFamilyIndices = stackalloc uint[familyCount];
            for (var i = 0; i < familyCount; i++)
                queueFamilyIndices[i] = Device.Families[i].Index;

            var ici = new ImageCreateInfo
            {
                SType = StructureType.ImageCreateInfo,
                ImageType = Type.ToVk(),
                Format = Format.ToVk(),
                Extent = Size.ToVk(),
                MipLevels = MipmapLevels,
                ArrayLayers = ArrayLayers,
                Samples = SampleCount.ToVk(),
                Tiling = Tiling.ToVk(),
                Usage = Usage.ToVk(),
                InitialLayout = GetInitialLayout(),
                SharingMode = (familyCount == 1) ?
                    SharingMode.Exclusive :
                    SharingMode.Concurrent,
                QueueFamilyIndexCount = (uint)familyCount,
                PQueueFamilyIndices = queueFamilyIndices,
            };

            var result = Device.Vk.CreateImage(Device, ici, null, out var image);
            result.CheckResult("Failed to create image!");

            return image;
        }

        public override bool Equals(ITexture other)
        {
            throw new NotImplementedException();
        }

        public override (IntPtr Ptr, long Length) Map()
        {
            throw new NotImplementedException();
        }

        public override void Unmap()
        {
            throw new NotImplementedException();
        }

        protected unsafe override void UnmanagedDispose()
        {
            Device.Vk.DestroyImage(Device, Image, null);
        }

        protected override void ManagedDispose()
        {
            Allocation.Dispose();
        }

        public override TextureView GetView() => _view;

        internal SubresourceLayout GetSubresourceLayout(uint mipLevel, uint arrayLayer)
        {
            var mipSize = Size.GetMipmapDimensions(mipLevel);
            var sampleCount = (uint)SampleCount;

            var layout = new SubresourceLayout()
            {
                RowPitch = Format.GetRowPitch(mipSize.Width),
                DepthPitch = Format.GetDepthPitch(mipSize),
                ArrayPitch = Format.GetLayerSize(Size, sampleCount, MipmapLevels),
                Size = Format.GetMultiSampledLevelSize(Size, sampleCount, mipLevel),
                Offset = Format.GetMemoryOffset(Size, mipLevel, arrayLayer)
            };
            
            return layout;
        }

        internal ImageLayout GetInitialLayout() =>
            Tiling == TextureTiling.Linear ? ImageLayout.Preinitialized : ImageLayout.Undefined;

        private ImageAspectFlags GetImageAspectFlags()
        {
            if (Usage.HasFlag(TextureUsage.DepthStencilAttachment))
            {
                ImageAspectFlags result = default;

                if (Format.HasFlag(PixelFormat.Stencil))
                    result |= ImageAspectFlags.ImageAspectStencilBit;

                if (Format.HasFlag(PixelFormat.Depth))
                    result |= ImageAspectFlags.ImageAspectDepthBit;
            }
            return ImageAspectFlags.ImageAspectColorBit;
        }

        private ImageLayout GetDefaultImageLayout(TextureUsage usage)
        {
            if (usage.HasFlag(TextureUsage.ColorAttachment))
                return ImageLayout.ColorAttachmentOptimal;
            else if (usage.HasFlag(TextureUsage.DepthStencilAttachment))
                return ImageLayout.DepthStencilAttachmentOptimal;
            else if (usage.HasFlag(TextureUsage.Storage))
                return ImageLayout.General;
            else if (usage.HasFlag(TextureUsage.Sampled))
                return ImageLayout.ShaderReadOnlyOptimal;
            else if (usage.HasFlag(TextureUsage.TransferDestination))
                return ImageLayout.TransferDstOptimal;
            else if (usage.HasFlag(TextureUsage.TransferDestination))
                return ImageLayout.TransferSrcOptimal;
            
            return ImageLayout.Undefined;
        }

        private unsafe void TransiantImageLayout(ImageLayout oldLayout, ImageLayout newLayout)
        {
            var cb = (CommandBuffer)Device.Factory.CreateCommandBuffer();
            cb.Begin();
            Span<ImageSubresourceRange> range = stackalloc ImageSubresourceRange[1];
            range[0] = new()
            {
                AspectMask = ImageAspect,
                BaseArrayLayer = 0,
                BaseMipLevel = 0,
                LayerCount = ArrayLayers,
                LevelCount = MipmapLevels,
            };
            cb.TransiantImageLayout(this, range, oldLayout, newLayout);
            cb.End();

            var fence = Device.Factory.CreateFence();

            var submitInfo = new SubmitInfo()
            {
                CommandBuffers = new[] { cb },
                SignalSemaphores = Array.Empty<ISemaphore>(),
                WaitSemaphores = Array.Empty<ISemaphore>(),
            };
            Device.Submit(submitInfo, fence);
            fence.Wait(ulong.MaxValue);
        }

        public static implicit operator Image(Texture texture)
        {
            texture.CheckDispose();
            return texture.Image;
        }
    }
}
