// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Resources;
using Ez.Memory;
using Ez.Numerics;
using Silk.NET.Vulkan;
using System;
using System.Collections.Generic;
using System.Linq;

using FramebufferCreateInfo = Ez.Graphics.API.CreateInfos.FramebufferCreateInfo;
using VkFramebuffer = Silk.NET.Vulkan.Framebuffer;
using VkFramebufferCreateInfo = Silk.NET.Vulkan.FramebufferCreateInfo;
namespace Ez.Graphics.API.Vulkan.Core.Cached.Framebuffers
{
    internal class Framebuffer : DeviceResource, IFramebuffer
    {
        private Cache<VkFramebufferCreateInfo, VkFramebuffer> _cache;
        private MemoryBlock _mb;
        private PinnedMemory<ImageView> imageViews;

        public Framebuffer(Device device, in FramebufferCreateInfo createInfo, bool isPresented) : base(device)
        {
            _cache = new Cache<VkFramebufferCreateInfo, VkFramebuffer>(CreateFramebuffer);
            IsPresented = isPresented;
            Attachments = Array.AsReadOnly(createInfo.Attachments.ToArray());

            var first = Attachments.FirstOrDefault();
            Size = first.Target.Size;

            _mb = MemoryBlockPool.Get(MemUtil.SizeOf<IntPtr>() * Attachments.Count);
            imageViews = _mb.AllocPinnedMemory<ImageView>(Attachments.Count);

            for (var i = 0; i < Attachments.Count; i++)
                imageViews[i] = ((BaseTexture)Attachments[i].Target).GetView();
        }

        public bool IsPresented { get; }
        public Size3 Size { get; }

        public IReadOnlyList<FramebufferAttachment> Attachments { get; }

        protected override void ManagedDispose()
        {
            _cache.Dispose();
        }

        protected override void UnmanagedDispose()
        {

        }

        private unsafe VkFramebuffer CreateFramebuffer(in VkFramebufferCreateInfo createInfo)
        {
            var result = Device.Vk.CreateFramebuffer(Device, createInfo, null, out var framebuffer);
            result.CheckResult();
            return framebuffer;
        }

        public unsafe VkFramebuffer GetHandle(RenderPasses.RenderPass renderPass)
        {
            var ci = new VkFramebufferCreateInfo
            {
                SType = StructureType.FramebufferCreateInfo,
                RenderPass = renderPass,
                AttachmentCount = (uint)imageViews.Count,
                PAttachments = (ImageView*)imageViews.Ptr,
                Width = Size.Width,
                Height = Size.Height,
                Layers = Size.Depth,
            };

            return _cache.Create(ci);
        }
    }
}
