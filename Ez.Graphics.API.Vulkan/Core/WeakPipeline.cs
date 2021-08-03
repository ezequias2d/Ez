// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;
using Silk.NET.Vulkan;
using System;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class WeakPipeline : DeviceResource, IPipeline
    {
        private Shader[] _shaders;
        private ShaderModule[] _shaderModules;

        public WeakPipeline(Device device, in PipelineCreateInfo createInfo) : base(device)
        {
            var layout = Device.LayoutCache.GetAsync(createInfo);

            _shaders = new Shader[createInfo.Shaders.Length];
            _shaderModules = new ShaderModule[createInfo.Shaders.Length];
            for (var i = 0; i < _shaders.Length; i++)
                if (createInfo.Shaders[i] is Shader vkShader)
                {
                    _shaders[i] = vkShader;
                    _shaderModules[i] = vkShader.Handle;
                }
                else
                    throw new VkException("Invalid shader object.");

            IsGraphicPipeline = createInfo.IsGraphicPipeline;
            BindPoint = createInfo.IsGraphicPipeline ? PipelineBindPoint.Graphics : PipelineBindPoint.Compute;

            layout.Wait();
            Layout = layout.Result;

            PipelineLayout = CreatePipelineLayout();
        }

        public DescriptorSetLayout Layout { get; private set; }
        public PipelineBindPoint BindPoint { get; }
        public ReadOnlySpan<Shader> Shaders => _shaders;
        public ReadOnlySpan<ShaderModule> ShaderModules => _shaderModules;
        public PipelineLayout PipelineLayout { get; }

        public bool IsGraphicPipeline { get; }

        public override bool Equals(IResource other) =>
            ReferenceEquals(this, other);

        protected override void ManagedDispose()
        {
            _shaders = null;
            _shaderModules = null;
            Layout = null;
        }

        protected unsafe override void UnmanagedDispose()
        {
            Device.Vk.DestroyPipelineLayout(Device, PipelineLayout, null);
        }

        private unsafe PipelineLayout CreatePipelineLayout()
        {
            Silk.NET.Vulkan.DescriptorSetLayout layout = Layout;
            var ci = new PipelineLayoutCreateInfo
            {
                SType = StructureType.PipelineLayoutCreateInfo,
                SetLayoutCount = 1,
                PSetLayouts = &layout,
                PushConstantRangeCount = 0,
                PPushConstantRanges = null,
            };

            if (Device.Vk.CreatePipelineLayout(Device, ci, null, out var pipelineLayout) != Result.Success)
                throw new VkException("Failed to create pipeline layout!");

            return pipelineLayout;
        }

        public static implicit operator PipelineLayout(WeakPipeline pipeline) =>
            pipeline.PipelineLayout;
    }
}
