// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;
using Ez.Graphics.API.Vulkan.Core.Cached.Framebuffers;
using System;
using System.Collections.Generic;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class Factory : DeviceResource, IFactory
    {
        public Factory(Device device) : base(device)
        {

        }

        public IEnumerable<IResource> Resources => throw new NotImplementedException();

        IDevice IFactory.Device => Device;

        public IBuffer CreateBuffer(BufferCreateInfo bufferCreateInfo) => new Buffer(Device, bufferCreateInfo);

        public ICommandBuffer CreateCommandBuffer() =>
            new CommandBuffer(Device, Device.CommandPool);

        public IFence CreateFence() => new Fence(Device);

        public IFramebuffer CreateFramebuffer(in FramebufferCreateInfo createInfo) =>
            new Framebuffer(Device, createInfo, false);

        public IPipeline CreatePipeline(in PipelineCreateInfo createInfo) =>
            new WeakPipeline(Device, createInfo);

        public ISampler CreateSampler(in SamplerCreateInfo samplerCreateInfo) =>
            new Sampler(Device, samplerCreateInfo);

        public ISemaphore CreateSemaphore() => new Semaphore(Device);

        public IShader CreateShader(in ShaderCreateInfo createInfo) => new Shader(Device, createInfo);

        public ISwapchain CreateSwapchain(in SwapchainCreateInfo createInfo) => new Swapchain(Device, createInfo);

        public ITexture CreateTexture(in TextureCreateInfo textureCreateInfo) => new Texture(Device, textureCreateInfo);

        public ITextureView CreateTextureView(in TextureViewCreateInfo textureViewCreateInfo)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool TryGetResource<T>(string name, out T resource) where T : IResource
        {
            throw new NotImplementedException();
        }

        protected override void ManagedDispose()
        {
            throw new NotImplementedException();
        }

        protected override void UnmanagedDispose()
        {
            throw new NotImplementedException();
        }
    }
}
