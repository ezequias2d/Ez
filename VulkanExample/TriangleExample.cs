using System;
using System.Drawing;
using System.Threading;
using Ez.Graphics;
using Ez.Graphics.API;
using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;
using Ez.Graphics.API.Vulkan;
using Ez.Memory;
using Ez.Windowing;
using Ez.Windowing.GLFW;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace VulkanExample
{
    public class TriangleExample : BaseExample
    {
        private static string vertexShaderSource = @"
#version 450

layout(location = 0) out vec3 fragColor;

vec2 positions[3] = vec2[](
    vec2(0.0, -0.5),
    vec2(0.5, 0.5),
    vec2(-0.5, 0.5)
);

vec3 colors[3] = vec3[](
    vec3(1.0, 0.0, 0.0),
    vec3(0.0, 1.0, 0.0),
    vec3(0.0, 0.0, 1.0)
);

void main() {
    gl_Position = vec4(positions[gl_VertexIndex], 0.0, 1.0);
    fragColor = colors[gl_VertexIndex];
}";
        private static string fragmentShaderSource = @"#version 450

layout(location = 0) in vec3 fragColor;
layout(location = 0) out vec4 outColor;

void main() {
    outColor = vec4(fragColor, 1.0);
}";

        private IPipeline _pipeline;
        public TriangleExample(ILogger logger) : base(logger)
        {
            
        }

        public override void Load()
        {
            _pipeline = CreatePipeline();
        }

        public unsafe override void Render(ICommandBuffer commandBuffer, RenderPassBeginInfo swapchainRenderPassBeginInfo)
        {
            var pAttachmentStates = stackalloc ColorBlendAttachmentState[1];
            pAttachmentStates[0] = ColorBlendAttachmentState.Default;

            commandBuffer.SetColorBlendState(new()
            {
                AttachmentStates = MemUtil.GetMemory<ColorBlendAttachmentState>((IntPtr)pAttachmentStates, 1),
                LogicOperation = LogicOperation.Set,
                LogicOperationEnabled = false,
            });

            commandBuffer.SetViewportCount(1);
            commandBuffer.SetViewport(0, new()
            {
                X = 0,
                Y = 0,
                Width = swapchainRenderPassBeginInfo.Framebuffer.Size.Width,
                Height = swapchainRenderPassBeginInfo.Framebuffer.Size.Height,
                MinDepth = 0.1f,
                MaxDepth = 1f,
            });

            commandBuffer.SetScissor(0, new()
            {
                X = 0,
                Y = 0,
                Width = (int)swapchainRenderPassBeginInfo.Framebuffer.Size.Width,
                Height = (int)swapchainRenderPassBeginInfo.Framebuffer.Size.Height,
            });

            commandBuffer.BindPipeline(_pipeline);

            commandBuffer.Draw(3, 1, 0, 0);
        }

        private IPipeline CreatePipeline()
        {
            var vertexShader = Factory.CreateShader(new ShaderCreateInfo("triangle", "main", ShaderStages.Vertex, vertexShaderSource));
            var fragmentShader = Factory.CreateShader(new ShaderCreateInfo("triangle", "main", ShaderStages.Fragment, fragmentShaderSource));

            return Factory.CreatePipeline(new PipelineCreateInfo
            {
                Bindings = Array.Empty<SetLayoutBinding>(),
                IsGraphicPipeline = true,
                Shaders = new IShader[]
                {
                    vertexShader,
                    fragmentShader
                },
            });
        }
    }
}