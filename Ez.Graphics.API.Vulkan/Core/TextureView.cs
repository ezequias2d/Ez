// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;
using Ez.Numerics;
using Silk.NET.Vulkan;
using System;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class TextureView : BaseTexture, ITextureView
    {
        public TextureView(Device device, TextureViewCreateInfo ci) : base(device, ci.Texture.MemoryUsage)
        {
            if (ci.Texture == null || ci.Texture is not Texture texture)
                throw new VkException();

            Texture = ci.Texture;
            Range = ci.SubresourceRange;
            Format = ci.Format ?? Texture.Format;

            var aspectFlags = ImageAspectFlags.ImageAspectColorBit;
            if ((ci.Texture.Format & PixelFormat.Depth) != default)
                aspectFlags |= ImageAspectFlags.ImageAspectDepthBit;

            var vkci = new ImageViewCreateInfo
            {
                SType = StructureType.ImageViewCreateInfo,
                Image = texture.Image,
                ViewType = Texture.Usage.HasFlag(TextureUsage.Cubemap) ?
                    ci.SubresourceRange.ArrayLayerCount / 6 == 1 ? ImageViewType.Cube : ImageViewType.CubeArray :
                        (ci.SubresourceRange.ArrayLayerCount == 1) ?
                            Texture.Type switch
                            {
                                TextureType.Texture1D => ImageViewType.ImageViewType1D,
                                TextureType.Texture2D => ImageViewType.ImageViewType2D,
                                TextureType.Texture3D => ImageViewType.ImageViewType3D,
                                _ => throw new VkException($"Unsupported ArrayLayerCount '{ci.SubresourceRange.ArrayLayerCount}' with TextureType {Texture.Type}")
                            } :
                            Texture.Type switch
                            {
                                TextureType.Texture1D => ImageViewType.ImageViewType1DArray,
                                TextureType.Texture2D => ImageViewType.ImageViewType2DArray,
                                _ => throw new VkException($"Unsupported ArrayLayerCount '{ci.SubresourceRange.ArrayLayerCount}' with TextureType {Texture.Type}")
                            },
                Format = Format.ToVk(),
                Components = new ComponentMapping(ComponentSwizzle.Identity, ComponentSwizzle.Identity, ComponentSwizzle.Identity, ComponentSwizzle.Identity),
                SubresourceRange = new ImageSubresourceRange
                {
                    AspectMask = aspectFlags,
                    BaseArrayLayer = ci.SubresourceRange.BaseArrayLayer,
                    BaseMipLevel = ci.SubresourceRange.BaseMipmapLevel,
                    LayerCount = ci.SubresourceRange.ArrayLayerCount,
                    LevelCount = ci.SubresourceRange.MipmapLevelCount,
                }
            };

            unsafe
            {
                if (Device.Vk.CreateImageView(Device.Handle, vkci, null, out var pView) != Result.Success)
                    throw new VkException("Failed to create image view!");
                ImageView = pView;
            }

            Size = GraphicsApiHelper.GetMipmapDimensions(Texture, ci.SubresourceRange.BaseMipmapLevel);
        }

        public ImageView ImageView { get; }

        public ITexture Texture { get; }

        public TextureSubresourceRange Range { get; }

        public override PixelFormat Format { get; }

        public override uint MipmapLevels => Range.MipmapLevelCount;

        public override uint ArrayLayers => Range.BaseArrayLayer;

        public override TextureUsage Usage => Texture.Usage;

        public override TextureType Type => Texture.Type;

        public override SampleCount SampleCount => Texture.SampleCount;

        public override Size3 Size { get; }

        public override TextureTiling Tiling => Texture.Tiling;

        public override bool Equals(IResource other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(ITexture other)
        {
            throw new NotImplementedException();
        }

        public override TextureView GetView() => this;

        public override (IntPtr Ptr, long Length) Map()
        {
            throw new NotImplementedException();
        }

        public override void Unmap()
        {
            throw new NotImplementedException();
        }

        protected override void ManagedDispose()
        {

        }

        unsafe protected override void UnmanagedDispose()
        {
            Device.Vk.DestroyImageView(Device.Handle, ImageView, null);
        }

        public static implicit operator ImageView(TextureView view) =>
            view.ImageView;
    }
}
