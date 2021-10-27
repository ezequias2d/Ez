// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Resources;
using Silk.NET.Vulkan;
using VkRenderPass = Silk.NET.Vulkan.RenderPass;
namespace Ez.Graphics.API.Vulkan.Core.Cached.RenderPasses
{
    internal class RenderPass : DeviceResource
    {
        public RenderPass(Device device, RenderPassBeginInfo description) : base(device)
        {
            VkRenderPass = CreateRenderPass(description);

            ObjectHandle = VkRenderPass.Handle;
            ObjectType = ObjectType.RenderPass;
            this.SetDefaultName();
        }

        public VkRenderPass VkRenderPass { get; }


        public static implicit operator VkRenderPass(RenderPass pass) =>
            pass.VkRenderPass;

        private unsafe VkRenderPass CreateRenderPass(RenderPassBeginInfo bi)
        {
            var framebuffer = (Framebuffers.Framebuffer)bi.Framebuffer;
            var hasDepth = bi.DepthStencilAttachmentIndex >= 0;

            var framebufferAttachments = framebuffer.Attachments;
            var renderPassAttachments = bi.Attachments.Span;

            var attachmentsCount = renderPassAttachments.Length;
            var attachments = stackalloc AttachmentDescription[attachmentsCount];

            var colorAttachmentCount = bi.Attachments.Length - ((bi.DepthStencilAttachmentIndex < 0) ? 0 : 1);
            var inputAttachmentCount = framebufferAttachments.Count;

            var attachmentsReferences = stackalloc AttachmentReference[colorAttachmentCount];
            var attachmentsReferenceIndex = 0;
            var depthStencilAttachment = new AttachmentReference();

            for (var i = 0; i < colorAttachmentCount; i++)
            {
                var isStencil = (framebufferAttachments[i].Target.Format & PixelFormat.Stencil) != 0;
                var isDepth = (framebufferAttachments[i].Target.Format & PixelFormat.Depth) != 0;

                attachments[i] = new()
                {
                    Format = framebufferAttachments[i].Target.Format.ToVk(),
                    Samples = framebufferAttachments[i].Target.SampleCount.ToVk(),
                    LoadOp = renderPassAttachments[i].LoadOperation.ToVk(),
                    StoreOp = renderPassAttachments[i].StoreOperation.ToVk(),

                    StencilLoadOp = AttachmentLoadOp.DontCare,
                    StencilStoreOp = AttachmentStoreOp.DontCare,

                    //InitialLayout = GetImageLayout(framebuffer, framebufferAttachments[i].Target),
                    InitialLayout = ImageLayout.Undefined,
                    FinalLayout = GetFinalLayout(framebuffer, framebufferAttachments[i].Target),
                };

                if (isStencil)
                {
                    attachments[i].StencilLoadOp = attachments[i].LoadOp;
                    attachments[i].StencilStoreOp = attachments[i].StoreOp;
                }

                var attachmentReference = new AttachmentReference
                {
                    Attachment = (uint)i,
                    Layout = GetFinalLayoutReference(framebuffer, framebufferAttachments[i].Target),
                };

                if (i != bi.DepthStencilAttachmentIndex)
                    attachmentsReferences[attachmentsReferenceIndex++] = attachmentReference;
                else
                    depthStencilAttachment = attachmentReference;
            }

            var subpass = new SubpassDescription
            {
                PipelineBindPoint = PipelineBindPoint.Graphics,
                ColorAttachmentCount = (uint)colorAttachmentCount,
                PColorAttachments = attachmentsReferences,
                PDepthStencilAttachment = hasDepth ? &depthStencilAttachment : null
            };

            var rpci = new RenderPassCreateInfo
            {
                SType = StructureType.RenderPassCreateInfo,
                AttachmentCount = (uint)attachmentsCount,
                PAttachments = attachments,
                SubpassCount = 1,
                PSubpasses = &subpass,
            };

            var result = Device.Vk.CreateRenderPass(Device, rpci, null, out var renderPass);
            result.CheckResult("Failed to create render pass!");

            return renderPass;
        }

        private ImageLayout GetImageLayout(Framebuffers.Framebuffer framebuffer, ITexture texture)
        {
            if (framebuffer.IsPresented)
                return ImageLayout.PresentSrcKhr;
            else if (texture.Usage.HasFlag(TextureUsage.Sampled))
                return ImageLayout.ShaderReadOnlyOptimal;
            else
                return ImageLayout.ColorAttachmentOptimal;
        }

        private ImageLayout GetImageLayoutDepthStencil(ITexture texture)
        {
            if (texture.Usage.HasFlag(TextureUsage.Sampled))
                return ImageLayout.ShaderReadOnlyOptimal;
            else
                return ImageLayout.DepthStencilAttachmentOptimal;
        }

        private ImageLayout GetFinalLayout(Framebuffers.Framebuffer framebuffer, ITexture texture)
        {
            const PixelFormat depthStencil = PixelFormat.Depth | PixelFormat.Stencil;

            if (framebuffer.IsPresented && ((texture.Format & depthStencil) == default))
                return ImageLayout.PresentSrcKhr;
            else if ((texture.Format & depthStencil) == depthStencil)
                return ImageLayout.DepthStencilAttachmentOptimal;
            else if ((texture.Format & PixelFormat.Depth) == PixelFormat.Depth)
                return ImageLayout.DepthAttachmentOptimal;
            else if ((texture.Format & PixelFormat.Stencil) == PixelFormat.Stencil)
                return ImageLayout.StencilAttachmentOptimal;
            else
                return ImageLayout.ColorAttachmentOptimal;
        }

        private ImageLayout GetFinalLayoutReference(Framebuffers.Framebuffer framebuffer, ITexture texture)
        {
            const PixelFormat depthStencil = PixelFormat.Depth | PixelFormat.Stencil;

            if ((texture.Format & depthStencil) == depthStencil)
                return ImageLayout.DepthStencilAttachmentOptimal;
            else if ((texture.Format & PixelFormat.Depth) == PixelFormat.Depth)
                return ImageLayout.DepthAttachmentOptimal;
            else if ((texture.Format & PixelFormat.Stencil) == PixelFormat.Stencil)
                return ImageLayout.StencilAttachmentOptimal;
            else
                return ImageLayout.ColorAttachmentOptimal;
        }

        protected unsafe override void UnmanagedDispose()
        {
            Device.Vk.DestroyRenderPass(Device, VkRenderPass, null);
        }

        protected override void ManagedDispose()
        {

        }
    }
}
