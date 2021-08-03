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

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class Texture : BaseTexture, ITexture
    {
        private readonly TextureView _view;
        private readonly ImageLayout[] _imagelayouts;
        public Texture(Device device, TextureCreateInfo ci) : base(device, ci.MemoryMode)
        {
            Image = CreateImage(ci);

            Allocation = Device.Allocator.CreateTexture(Image, ci, false);
            var result = Device.Vk.BindImageMemory(Device, Image, Allocation.Handle, Allocation.Offset);

            result.CheckResult();

            Format = ci.Format;
            Size = ci.Size;
            MipmapLevels = ci.MipLevels;
            ArrayLayers = ci.ArrayLayers;
            Usage = ci.Usage;
            Type = ci.Type;
            SampleCount = ci.Samples;
            Tiling = ci.Tiling;

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
        }

        public Texture(Device device, Image image, Format format, Extent2D extent, bool depth) : base(device, MemoryUsage.GpuOnly)
        {
            VkFormat = format;
            Format = ToPixelFormat(format);
            Size = new(extent.Width, extent.Height, 1);
            MipmapLevels = 1;
            ArrayLayers = 1;
            Usage = depth ? TextureUsage.DepthStencilAttachment : TextureUsage.ColorAttachment;
            Type = TextureType.Texture2D;
            //ViewType = ImageViewType.ImageViewType2D;
            SampleCount = SampleCount.Count1;
            Image = image;
            Tiling = TextureTiling.Optimal;
        }

        public Image Image { get; }
        public IAllocation Allocation { get; }
        public Format VkFormat { get; }

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

        private unsafe Image CreateImage(in TextureCreateInfo tci)
        {
            var families = Device.Families;
            var familyCount = families.Count;
            var queueFamilyIndices = stackalloc uint[familyCount];
            for (var i = 0; i < familyCount; i++)
                queueFamilyIndices[i] = Device.Families[i].Index;

            var ici = new ImageCreateInfo
            {
                SType = StructureType.ImageCreateInfo,
                ImageType = tci.Type.ToVk(),
                Format = tci.Format.ToVk(),
                Extent = tci.Size.ToVk(),
                MipLevels = tci.MipLevels,
                ArrayLayers = tci.ArrayLayers,
                Samples = tci.Samples.ToVk(),
                Tiling = tci.Tiling.ToVk(),
                Usage = tci.Usage.ToVk(),
                InitialLayout = ImageLayout.Undefined,
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

        public static implicit operator Image(Texture texture)
        {
            texture.CheckDispose();
            return texture.Image;
        }
    }
}
