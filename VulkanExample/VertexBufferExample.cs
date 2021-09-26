using System;
using System.Numerics;
using Ez.Graphics.API;
using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;
using Ez.Memory;
using Ez.Numerics;
using Microsoft.Extensions.Logging;
using VulkanExample.Properties;

namespace VulkanExample
{
    public class VertexBufferExample: BaseExample
    {
        private static string vertexShaderSource = @"
#version 450

layout(location = 0) in vec2 inPosition;
layout(location = 1) in vec3 inColor;

layout(location = 0) out vec3 fragColor;

layout(binding = 0) uniform UniformBufferObject {
    mat4 model;
    mat4 view;
    mat4 proj;
} ubo;

void main() {
    gl_Position = ubo.proj * ubo.view * ubo.model * vec4(inPosition, 0.0, 1.0);
    fragColor = inColor;
}";
        private static string fragmentShaderSource = @"#version 450

layout(location = 0) in vec3 fragColor;
layout(location = 0) out vec4 outColor;

void main() {
    outColor = vec4(fragColor, 1.0);
}";

        private IPipeline _pipeline;
        private IBuffer _vertexBuffer;
        private IBuffer _indexBuffer;
        private uint _indexCount;

        private IBuffer _uboBuffer;
        private ITexture _texture;

        private VertexLayoutState _vertexLayout;
        public VertexBufferExample(ILogger logger) : base(logger)
        {
            
        }
        
        public override void Load()
        {

            var vertices = new Vertex[] 
            {
                new(new(-0.5f, -0.5f), new(1.0f, 0.0f, 0.0f)),
                new(new( 0.5f, -0.5f), new(0.0f, 1.0f, 0.0f)),
                new(new( 0.5f,  0.5f), new(0.0f, 0.0f, 1.0f)),
                new(new(-0.5f,  0.5f), new(1.0f, 1.0f, 1.0f))
            };

            var indices = new ushort[] { 0, 1, 2, 2, 3, 0 };
            _indexCount = (uint)indices.Length;

            _vertexBuffer = Factory.CreateBuffer(
                new(
                    size: MemUtil.SizeOf<Vertex>(vertices),
                    bufferUsage: BufferUsage.VertexBuffer | BufferUsage.TransferDestination,
                    memoryUsage: MemoryUsage.GpuOnly
                )
            );
            _indexBuffer = Factory.CreateBuffer(
                new(
                    size: MemUtil.SizeOf<ushort>(indices),
                    bufferUsage: BufferUsage.IndexBuffer | BufferUsage.TransferDestination,
                    memoryUsage: MemoryUsage.GpuOnly
                )
            );
            _uboBuffer = Factory.CreateBuffer(
                new(
                    size: MemUtil.SizeOf<UniformBufferObject>(),
                    bufferUsage: BufferUsage.UniformBuffer | BufferUsage.TransferDestination,
                    memoryUsage: MemoryUsage.GpuOnly
                )
            );

            _vertexBuffer.SubData<Vertex>(vertices, 0);
            _indexBuffer.SubData<ushort>(indices, 0);

            _pipeline = CreatePipeline();

            _vertexLayout = new(new VertexElementDescription(VertexElementType.Single2, 0), new(VertexElementType.Single3, 1));

            
            var textureData = ResourceHelper.LoadTexture(Resources.texture);
            _texture = Factory.CreateTexture(new TextureCreateInfo 
            {
                Format = textureData.PixelFormat,
                ArrayLayers = textureData.ArrayLayers,
                MipLevels = textureData.MipmapLevels,
                Samples = SampleCount.Count1,
                Size = new(textureData.Width, textureData.Height, textureData.Depth),
                Tiling = TextureTiling.Optimal,
                Type = textureData.TextureType,
                Usage = TextureUsage.Sampled | TextureUsage.TransferDestination,
                MemoryMode = MemoryUsage.GpuOnly,
            });

            _texture.SubData(textureData.Data, 0, 0, 0, 0, textureData.Width, textureData.Height, textureData.Depth);
        }

        public readonly double start = DateTime.Now.TimeOfDay.TotalSeconds;
        public unsafe override void Render(ICommandBuffer cb, RenderPassBeginInfo swapchainRenderPassBeginInfo)
        {
            Span<UniformBufferObject> ubo = stackalloc UniformBufferObject[1];
            var now = DateTime.Now.TimeOfDay.TotalSeconds;
            var time = now - start;

            ubo[0] = new()
            {
                Model = Matrix4x4.CreateRotationZ((float)(time * EzMath.Deg2Rad * 90.0)),
                View = Matrix4x4.CreateLookAt(new(2), new(0), new(0, 0, 1)),
                Proj = Matrix4x4.CreatePerspectiveFieldOfView(EzMath.Deg2Rad * 45f, Swapchain.Size.Width / (float)Swapchain.Size.Height, 0.1f, 10f)
            };
            ubo[0].Proj.M22 *= -1;

            _uboBuffer.SubData<UniformBufferObject>(ubo, 0);




            var pAttachmentStates = stackalloc ColorBlendAttachmentState[1];
            pAttachmentStates[0] = ColorBlendAttachmentState.Default;

            cb.SetColorBlendState(new()
            {
                AttachmentStates = MemUtil.GetMemory<ColorBlendAttachmentState>((IntPtr)pAttachmentStates, 1),
                LogicOperation = LogicOperation.Set,
                LogicOperationEnabled = false,
            });

            cb.SetViewportCount(1);
            cb.SetViewport(0, new()
            {
                X = 0,
                Y = 0,
                Width = swapchainRenderPassBeginInfo.Framebuffer.Size.Width,
                Height = swapchainRenderPassBeginInfo.Framebuffer.Size.Height,
                MinDepth = 0.1f,
                MaxDepth = 1f,
            });

            cb.SetScissor(0, new()
            {
                X = 0,
                Y = 0,
                Width = (int)swapchainRenderPassBeginInfo.Framebuffer.Size.Width,
                Height = (int)swapchainRenderPassBeginInfo.Framebuffer.Size.Height,
            });

            cb.BindPipeline(_pipeline);

            cb.BindVertexBuffer(0, _vertexBuffer, 0);

            cb.BindIndexBuffer(_indexBuffer, IndexType.UShort, 0);
            cb.SetVertexLayoutState(0, _vertexLayout);

            //cb.Draw(3, 1, 0, 0);
            cb.BindBuffer(BufferUsage.UniformBuffer, _uboBuffer, 0);
            cb.DrawIndexed(_indexCount, 1, 0, 0, 0);
        }

        private IPipeline CreatePipeline()
        {
            var vertexShader = Factory.CreateShader(new ShaderCreateInfo("triangle", "main", ShaderStages.Vertex, vertexShaderSource));
            var fragmentShader = Factory.CreateShader(new ShaderCreateInfo("triangle", "main", ShaderStages.Fragment, fragmentShaderSource));

            return Factory.CreatePipeline(new PipelineCreateInfo
            {
                Bindings = new SetLayoutBinding[]
                {
                    new()
                    {
                        Binding = 0,
                        SetType = SetType.UniformBuffer,
                        ShaderStages = ShaderStages.Vertex,
                    }
                },
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