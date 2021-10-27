// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;
using Silk.NET.Vulkan;
using System;
using System.Collections.Generic;
using System.Linq;
using VkGraphicsPipelineCreateInfo = Silk.NET.Vulkan.GraphicsPipelineCreateInfo;
using VkPipelineCache = Silk.NET.Vulkan.PipelineCache;
namespace Ez.Graphics.API.Vulkan.Core.Cached
{
    internal class Pipeline : DeviceResource
    {
        private readonly VkPipelineCache _pipelineCache;

        public Pipeline(Device device, VkPipelineCache pipelineCache, PipelineState state) : base(device)
        {
            _pipelineCache = pipelineCache;
            VkPipeline = state.Pipeline.BindPoint switch
            {
                PipelineBindPoint.Graphics => CreateGraphicsPipeline(state),
                PipelineBindPoint.Compute => CreateComputePipeline(state),
                PipelineBindPoint.RayTracingKhr => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };

            ObjectHandle = VkPipeline.Handle;
            ObjectType = ObjectType.Pipeline;
        }

        private Silk.NET.Vulkan.Pipeline VkPipeline { get; }

        private unsafe Silk.NET.Vulkan.Pipeline CreateGraphicsPipeline(in PipelineState state)
        {
            var shaderStagesCount = state.Pipeline.Shaders.Length;
            var shadeStages = stackalloc PipelineShaderStageCreateInfo[shaderStagesCount];
            #region Shader stages
            var entryNames = new List<Utf8FixedString>();

            for (var i = 0; i < state.Pipeline.Shaders.Length; i++)
            {
                var shader = state.Pipeline.Shaders[i];
                var entryName = (Utf8FixedString)shader.EntryPoint;
                entryNames.Add(entryName);

                shadeStages[i] = new()
                {
                    SType = StructureType.PipelineShaderStageCreateInfo,
                    Stage = shader.VkStage,
                    Module = shader.Handle,
                    PName = entryName,
                };
            }
            #endregion

            #region Vertex Input State
            var bindingCount = state.VertexLayoutStates.Length;
            var attributeCount = state.VertexLayoutStates.Sum((state) => state.Elements.Length);

            var vertexBindings = stackalloc VertexInputBindingDescription[bindingCount];
            var vertexAttributes = stackalloc VertexInputAttributeDescription[attributeCount];

            var locationOffset = 0;
            var attributeIndex = 0;
            for (var i = 0; i < state.VertexLayoutStates.Length; i++)
            {
                ref VertexLayoutState vls = ref state.VertexLayoutStates[i];

                vertexBindings[i] = new()
                {
                    Binding = (uint)i,
                    InputRate = vls.InputRate.ToVk(),
                    Stride = vls.Stride
                };

                for (var j = 0; j < vls.Elements.Length; j++)
                {
                    ref VertexElementDescription element = ref vls.Elements.Span[j];

                    var offset = element.Offset;
                    if (offset == uint.MaxValue)
                    {
                        if (j > 0)
                            offset = vertexAttributes[attributeIndex - 1].Offset + GraphicsApiHelper.GetSizeInBytes(vls.Elements.Span[j - 1].Format);
                        else
                            offset = 0;
                    }

                    vertexAttributes[attributeIndex++] = new()
                    {
                        Binding = (uint)i,
                        Format = element.Format.ToVk(),
                        Location = (uint)(locationOffset + j),
                        Offset = offset,
                    };
                }
                locationOffset += vls.Elements.Length;
            }
            #endregion

            var vertexInputState = new PipelineVertexInputStateCreateInfo
            {
                SType = StructureType.PipelineVertexInputStateCreateInfo,
                VertexBindingDescriptionCount = (uint)bindingCount,
                PVertexBindingDescriptions = vertexBindings,
                VertexAttributeDescriptionCount = (uint)attributeCount,
                PVertexAttributeDescriptions = vertexAttributes,
            };

            var inputAssemblyState = new PipelineInputAssemblyStateCreateInfo
            {
                SType = StructureType.PipelineInputAssemblyStateCreateInfo,
                Topology = state.InputAssemblyState.Topology.ToVk(),
                PrimitiveRestartEnable = state.InputAssemblyState.PrimitiveRestartEnable,
            };

            var viewportState = new PipelineViewportStateCreateInfo
            {
                SType = StructureType.PipelineViewportStateCreateInfo,
                ViewportCount = state.ViewportCount,
                ScissorCount = state.ViewportCount,

                PViewports = null, // dynamic
                PScissors = null, // dynamic
            };

            var rasterizationState = new PipelineRasterizationStateCreateInfo
            {
                SType = StructureType.PipelineRasterizationStateCreateInfo,
                DepthClampEnable = state.RasterizationState.DepthClampEnabled,
                RasterizerDiscardEnable = state.RasterizationState.RasterizerDiscardEnabled,
                PolygonMode = state.RasterizationState.PolygonMode.ToVk(),
                LineWidth = 1f,
                CullMode = state.RasterizationState.CullMode.ToVk(),
                FrontFace = state.RasterizationState.FrontFace.ToVk(),
                DepthBiasEnable = state.RasterizationState.DepthBiasEnabled,
                DepthBiasConstantFactor = 0f,
                DepthBiasClamp = 1f,
                DepthBiasSlopeFactor = 1f,
            };

            var multisampleState = new PipelineMultisampleStateCreateInfo
            {
                SType = StructureType.PipelineMultisampleStateCreateInfo,
                RasterizationSamples = state.MultisampleState.SampleCount.ToVk(),
                SampleShadingEnable = false,
                MinSampleShading = 1f,
                PSampleMask = null,
                AlphaToCoverageEnable = state.MultisampleState.AlphaToCoverageEnabled,
                AlphaToOneEnable = false,
            };

            var colorBlendState = new PipelineColorBlendStateCreateInfo
            {
                SType = StructureType.PipelineColorBlendStateCreateInfo,
                LogicOpEnable = state.ColorBlendState.LogicOperationEnabled,
                LogicOp = state.ColorBlendState.LogicOperation.ToVk(),
            };

            #region Color Blend state
            colorBlendState.BlendConstants[0] = 1f;
            colorBlendState.BlendConstants[1] = 1f;
            colorBlendState.BlendConstants[2] = 1f;
            colorBlendState.BlendConstants[3] = 1f;


            var colorBlendAttachmentCount = state.ColorBlendState.AttachmentStates.Length;
            var colorBlendAttachments = stackalloc PipelineColorBlendAttachmentState[colorBlendAttachmentCount];
            colorBlendState.AttachmentCount = (uint)colorBlendAttachmentCount;
            colorBlendState.PAttachments = colorBlendAttachments;
            for (var i = 0; i < colorBlendAttachmentCount; i++)
            {
                ref ColorBlendAttachmentState attachment = ref state.ColorBlendState.AttachmentStates.Span[i];
                colorBlendAttachments[i] = new()
                {
                    AlphaBlendOp = attachment.AlphaBlendOperation.ToVk(),
                    BlendEnable = attachment.BlendEnabled,
                    ColorBlendOp = attachment.ColorBlendOperation.ToVk(),
                    ColorWriteMask = attachment.ColorWriteMask.ToVk(),
                    DstAlphaBlendFactor = attachment.DestinationAlphaBlendFactor.ToVk(),
                    DstColorBlendFactor = attachment.DestinationColorBlendFactor.ToVk(),
                    SrcAlphaBlendFactor = attachment.SourceAlphaBlendFactor.ToVk(),
                    SrcColorBlendFactor = attachment.SourceColorBlendFactor.ToVk(),
                };
            }
            #endregion

            var depthStencilState = new PipelineDepthStencilStateCreateInfo
            {
                SType = StructureType.PipelineDepthStencilStateCreateInfo,
                DepthTestEnable = state.DepthStencilState.DepthTestEnabled,
                DepthWriteEnable = state.DepthStencilState.DepthWriteEnabled,
                DepthCompareOp = state.DepthStencilState.DepthComparison.ToVk(),
                DepthBoundsTestEnable = state.DepthStencilState.DepthBoundsTestEnabled,
                StencilTestEnable = state.DepthStencilState.StencilTestEnabled,

                Front = state.DepthStencilState.StencilFront.ToVk(),
                Back = state.DepthStencilState.StencilBack.ToVk(),
            };

            #region Dynamic State
            var dynamicStates = stackalloc DynamicState[]
            {
                DynamicState.Viewport,
                DynamicState.Scissor,
                DynamicState.LineWidth,
                DynamicState.DepthBias,
                DynamicState.BlendConstants,
                DynamicState.DepthBounds,
                DynamicState.StencilCompareMask,
                DynamicState.StencilWriteMask,
                DynamicState.StencilReference,
            };

            #endregion
            var dynamicState = new PipelineDynamicStateCreateInfo
            {
                SType = StructureType.PipelineDynamicStateCreateInfo,
                DynamicStateCount = 9,
                PDynamicStates = dynamicStates,
            };

            var pipelineCreateInfo = new VkGraphicsPipelineCreateInfo
            {
                SType = StructureType.GraphicsPipelineCreateInfo,

                StageCount = (uint)shaderStagesCount,
                PStages = shadeStages,

                PVertexInputState = &vertexInputState,
                PInputAssemblyState = &inputAssemblyState,
                PViewportState = &viewportState,
                PRasterizationState = &rasterizationState,
                PMultisampleState = &multisampleState,
                PDepthStencilState = &depthStencilState,
                PColorBlendState = &colorBlendState,
                PDynamicState = &dynamicState,
                Layout = state.Pipeline,
                RenderPass = state.RenderPass,
                Subpass = 0,
            };

            var result = Device.Vk.CreateGraphicsPipelines(Device, _pipelineCache, 1, pipelineCreateInfo, null, out var pipeline);
            result.CheckResult("Fail to create the pipeline.");

            return pipeline;
        }

        public unsafe Silk.NET.Vulkan.Pipeline CreateComputePipeline(in PipelineState state)
        {
            var shader = state.Pipeline.Shaders[0];
            var entryPoint = (Utf8FixedString)shader.EntryPoint;

            var pipelineCreateInfo = new ComputePipelineCreateInfo
            {
                SType = StructureType.ComputePipelineCreateInfo,
                Stage = new()
                {
                    SType = StructureType.PipelineShaderStageCreateInfo,
                    Stage = shader.VkStage,
                    Module = shader.Handle,
                    PName = entryPoint,
                },
                Layout = state.Pipeline,
            };

            if (Device.Vk.CreateComputePipelines(Device, _pipelineCache, 1, pipelineCreateInfo, null, out var pipe) != Result.Success)
                throw new VkException("Fail to create the pipeline.");

            return pipe;
        }

        protected unsafe override void UnmanagedDispose()
        {
            Device.Vk.DestroyPipeline(Device, VkPipeline, null);
        }

        protected override void ManagedDispose()
        {
        }

        public static implicit operator Silk.NET.Vulkan.Pipeline(Pipeline pipeline)
        {
            pipeline.CheckDispose();
            return pipeline.VkPipeline;
        }
    }
}
