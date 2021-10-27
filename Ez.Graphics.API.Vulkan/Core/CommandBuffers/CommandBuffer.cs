using Ez.Graphics.API.Resources;
using Ez.Graphics.API.Vulkan.Core.Cached;
using Microsoft.Extensions.Logging;
using Silk.NET.Vulkan;
using System;
using System.Collections.Generic;
using System.Drawing;
using CollectionExtensions = Ez.Collections.CollectionExtensions;
using Framebuffer = Ez.Graphics.API.Vulkan.Core.Cached.Framebuffers.Framebuffer;
using VkBuffer = Silk.NET.Vulkan.Buffer;
using VkBufferCopy = Silk.NET.Vulkan.BufferCopy;
using VkCommandBuffer = Silk.NET.Vulkan.CommandBuffer;
using VkViewport = Silk.NET.Vulkan.Viewport;
using VkClearAttachment = Silk.NET.Vulkan.ClearAttachment;
using System.Diagnostics;
using Ez.Graphics.API.Vulkan.Core.Textures;
using System.Threading;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class CommandBuffer : DeviceResource, ICommandBuffer
    {
        
        private PipelineState _pipelineState;
        private VkViewport[] _viewports;
        private Rect2D[] _scissors;
        private readonly IList<RenderPassBeginInfo> _renderPasses;
        private readonly IList<PipelineState> _pipelineStates;
        private readonly Stack<DescriptorSet> _descriptorSets;
        private readonly ResourceBindings _resourceBindings;
        private readonly ImageLayoutManager _layoutManager;

        public CommandBuffer(Device device) : base(device)
        {
            _renderPasses = new List<RenderPassBeginInfo>();
            _pipelineStates = new List<PipelineState>();
            _descriptorSets = new Stack<DescriptorSet>();
            _resourceBindings = new ResourceBindings();

            ObjectType = ObjectType.CommandBuffer;
            Set();
            _layoutManager = new();
        }

        public CommandPool CommandPool { get; private set; }
        public VkCommandBuffer VkCommandBuffer { get; private set; }

        public static implicit operator VkCommandBuffer(CommandBuffer cb) =>
            cb.VkCommandBuffer;

        protected override void UnmanagedDispose()
        {
            Reset();
        }

        protected override void ManagedDispose()
        {
        }

        private protected override void SetName(string name)
        {
            if(CommandPool != null)
                base.SetName(name);
        }

        public void Begin()
        {
            this.CheckThreadId();

            var beginInfo = new CommandBufferBeginInfo
            {
                SType = StructureType.CommandBufferBeginInfo,
                Flags = CommandBufferUsageFlags.CommandBufferUsageSimultaneousUseBit,
            };

            var result = Device.Vk.BeginCommandBuffer(this, beginInfo);
            result.CheckResult("Failed to begin recording command buffer!");
        }

        public unsafe void BeginRenderPass(in RenderPassBeginInfo beginInfo)
        {
            this.CheckThreadId();

            var renderPass = Device.RenderPassCache.Get(beginInfo);
            _renderPasses.Add(beginInfo);

            var framebuffer = (Framebuffer)beginInfo.Framebuffer;
            var framebufferAttachments = framebuffer.Attachments;
            var attachments = beginInfo.Attachments.Span;

            var clearValues = stackalloc Silk.NET.Vulkan.ClearValue[attachments.Length];

            for (var i = 0; i < beginInfo.Attachments.Length; i++)
            {
                clearValues[i] = attachments[i].ClearValue.ToVk(framebufferAttachments[i].Target.Format);
            }

            var rpbi = new Silk.NET.Vulkan.RenderPassBeginInfo
            {
                SType = StructureType.RenderPassBeginInfo,
                RenderPass = renderPass,
                Framebuffer = framebuffer.GetHandle(renderPass),
                RenderArea = new(new(0, 0), new(framebuffer.Size.Width, framebuffer.Size.Height)),
                ClearValueCount = (uint)attachments.Length,
                PClearValues = clearValues,
            };

            Device.Vk.CmdBeginRenderPass(this, rpbi, SubpassContents.Inline);

            _pipelineState = PipelineState.Default;
            _pipelineState.RenderPass = renderPass;
            _pipelineState.Framebuffer = framebuffer;
        }

        public void BindBuffer(BufferUsage usage, IBuffer buffer, uint binding) =>
            BindBuffer(usage, new BufferSpan(buffer, 0, buffer.Size), binding);

        public void BindBuffer(BufferUsage usage, BufferSpan buffer, uint binding)
        {
            this.CheckThreadId();
            _resourceBindings.BindBuffer(usage, buffer, binding);
        }

        public void BindIndexBuffer(IBuffer buffer, IndexType format, ulong offset)
        {
            this.CheckThreadId();
            VkBuffer vkBuffer = (Buffer)buffer;
            Device.Vk.CmdBindIndexBuffer(this, vkBuffer, offset, format.ToVk());
        }

        public void BindPipeline(IPipeline pipeline)
        {
            this.CheckThreadId();
            var wpp = (WeakPipeline)pipeline;
            _pipelineState.Pipeline = wpp;
        }

        public void BindTexture(ITexture texture, ISampler sampler, uint binding) 
        {
            this.CheckThreadId();
            _resourceBindings.BindTexture((Texture)texture, (Sampler)sampler, binding);
        }

        public void BindVertexBuffer(uint index, IBuffer buffer, ulong offset)
        {
            this.CheckThreadId();
            VkBuffer vkBuffer = (Buffer)buffer;
            Device.Vk.CmdBindVertexBuffers(this, index, 1, vkBuffer, (ulong)offset);
        }

        public unsafe void ClearAttachments(ReadOnlySpan<ClearAttachment> attachments, ReadOnlySpan<ClearRectangle> regions)
        {
            this.CheckThreadId();
            var vkAttaschments = stackalloc VkClearAttachment[attachments.Length];
            for (var i = 0; i < attachments.Length; i++)
            {
                var clearAttachment = attachments[i];
                var framebufferAttachment = _pipelineState.Framebuffer.Attachments[(int)clearAttachment.ColorAttachment];
                vkAttaschments[i] = clearAttachment.ToVk(framebufferAttachment);
            }

            var vkRegions = stackalloc ClearRect[regions.Length];
            for (var i = 0; i < regions.Length; i++)
                vkRegions[i] = regions[i].ToVk();

            Device.Vk.CmdClearAttachments(this, (uint)attachments.Length, vkAttaschments, (uint)regions.Length, vkRegions);
        }

        public unsafe void ClearColorTexture(ITexture texture, in ClearColorValue color, ReadOnlySpan<TextureSubresourceRange> ranges)
        {
            this.CheckThreadId();
            if (texture is not BaseTexture baseTexture)
                throw new VkException();

            Span<ImageSubresourceRange> resolvedRanges = stackalloc ImageSubresourceRange[ranges.Length];
            var vkTexture = baseTexture.RevoluteSubresources(ranges, resolvedRanges);

            _layoutManager.TransitionImageLayout(this, vkTexture, resolvedRanges, ImageLayout.TransferDstOptimal);
            Device.Vk.CmdClearColorImage(this, vkTexture, ImageLayout.TransferDstOptimal, color.ToVk(baseTexture.Format), resolvedRanges);
        }

        public unsafe void ClearDepthStencilTexture(ITexture texture, in ClearDepthStencilValue depthStencil, ReadOnlySpan<TextureSubresourceRange> ranges)
        {
            this.CheckThreadId();

            if (texture is not BaseTexture baseTexture)
                throw new VkException();

            Span<ImageSubresourceRange> resolvedRanges = stackalloc ImageSubresourceRange[ranges.Length];
            var vkTexture = baseTexture.RevoluteSubresources(ranges, resolvedRanges);

            _layoutManager.TransitionImageLayout(this, vkTexture, resolvedRanges, ImageLayout.TransferDstOptimal);
            Device.Vk.CmdClearDepthStencilImage(this, vkTexture, ImageLayout.TransferDstOptimal, depthStencil.ToVk(), resolvedRanges);
        }

        public unsafe void CopyBuffer(IBuffer srcBuffer, IBuffer dstBuffer, ReadOnlySpan<BufferCopy> regions)
        {
            this.CheckThreadId();

            var vkRegions = stackalloc VkBufferCopy[regions.Length];

            for (var i = 0; i < regions.Length; i++)
                vkRegions[i] = new()
                {
                    SrcOffset = (ulong)regions[i].SrcOffset,
                    DstOffset = (ulong)regions[i].DstOffset,
                    Size = (ulong)regions[i].Size,
                };

            Device.Vk.CmdCopyBuffer(this, (Buffer)srcBuffer, (Buffer)dstBuffer, (uint)regions.Length, vkRegions);
        }

        public void CopyTexture(ITexture srcTexture, ITexture dstTexture, ReadOnlySpan<TextureCopy> regions)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void Dispatch(uint groupCountX, uint groupCountY, uint groupCountZ)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void DispatchIndirect(IBuffer buffer, long offset)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void Draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance)
        {
            this.CheckThreadId();

            Predraw();
            Device.Vk.CmdDraw(this, vertexCount, instanceCount, firstVertex, firstInstance);
        }

        public void DrawIndexed(uint indexCount, uint instanceCount, uint firstIndex, uint vertexOffset, uint firstInstance)
        {
            this.CheckThreadId();

            Predraw();
            Device.Vk.CmdDrawIndexed(this, indexCount, instanceCount, firstIndex, (int)vertexOffset, firstInstance);
        }

        public void DrawIndexedIndirect(IBuffer indirectBuffer, ulong offset, uint drawCount, uint stride)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void DrawIndirect(IBuffer buffer, ulong offset, uint drawCount, uint stride)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void End()
        {
            this.CheckThreadId();

            _layoutManager.Reset(this);
            Device.Vk.EndCommandBuffer(this);
        }

        public void EndRenderPass()
        {
            this.CheckThreadId();

            Device.Vk.CmdEndRenderPass(this);
        }

        public void FillBuffer(IBuffer dstBuffer, long dstOffset, long size, uint data)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void Set()
        {
            if (CommandPool != null)
                Reset();

            CommandPool = Device.CommandPoolCache.Get(Thread.CurrentThread);
            VkCommandBuffer = CommandPool.Alloc();
            ObjectHandle = (ulong)VkCommandBuffer.Handle;
        }

        public void Reset()
        {
            if(CommandPool != null)
            {
                CommandPool.Free(this);
                CommandPool = null;
            }

            foreach (var renderPass in _renderPasses)
                Device.RenderPassCache.Return(renderPass);
            _renderPasses.Clear();

            foreach (var pipelineState in _pipelineStates)
                Device.PipelineCache.Return(pipelineState);
            _pipelineStates.Clear();

            foreach (var descriptorSet in _descriptorSets)
                descriptorSet.Dispose();
            _descriptorSets.Clear();
            _resourceBindings.Reset();
        }

        public void ResolveTexture(ITexture srcTexture, ITexture dstTexture, ReadOnlySpan<TextureResolve> regions)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void SetBlendConstants(ColorSingle blendConstants)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void SetColorBlendState(in ColorBlendState state)
        {
            this.CheckThreadId();

            _pipelineState.ColorBlendState = state;
        }

        public void SetDepthBias(float depthBiasConstantFactor, float depthBiasClamp, float depthBiasSlopeFactor)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void SetDepthBounds(float minDepthBounds, float maxDepthBounds)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void SetDepthStencilState(in DepthStencilState state)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void SetInputAssemblyState(in InputAssemblyState state)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void SetLineWidth(float lineWidth)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void SetMultisampleState(in MultisampleState state)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void SetRasterizationState(in RasterizationState state)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void SetScissor(int index, in Rectangle rectangle)
        {
            this.CheckThreadId();

            _scissors[index] = rectangle.ToVk();
        }

        public void SetStencilCompareMask(StencilFaces faceMask, uint compareMask)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void SetStencilReference(StencilFaces faceMask, uint reference)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void SetStencilWriteMask(StencilFaces faceMask, uint writeMask)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void SetVertexLayoutState(uint index, VertexLayoutState layout)
        {
            this.CheckThreadId();

            CollectionExtensions.MinimumArray(ref _pipelineState.VertexLayoutStates, (int)index + 1);
            _pipelineState.VertexLayoutStates[index] = layout;
        }

        public void SetViewport(int index, in Viewport viewport)
        {
            this.CheckThreadId();

            _viewports[index] = viewport.ToVk();
        }

        public void SetViewportCount(uint viewportCount)
        {
            this.CheckThreadId();

            _pipelineState.ViewportCount = viewportCount;
            CollectionExtensions.MinimumArray(ref _viewports, (int)viewportCount);
            CollectionExtensions.MinimumArray(ref _scissors, (int)viewportCount);
        }

        public void UpdateBuffer(IBuffer dstBuffer, long dstOffset, ReadOnlySpan<byte> data)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public unsafe void CopyBufferToTexture(IBuffer srcBuffer, ITexture dstTexture, ReadOnlySpan<BufferTextureCopy> regions)
        {
            this.CheckThreadId();

            var vkDstTexture = (Texture)dstTexture;
            var vkRegions = stackalloc BufferImageCopy[regions.Length];
            Span<ImageSubresourceRange> ranges = stackalloc ImageSubresourceRange[regions.Length];
            for (var i = 0; i < regions.Length; i++)
            {
                vkRegions[i] = new()
                {
                    BufferImageHeight = regions[i].BufferTextureHeight,
                    BufferOffset = (ulong)regions[i].BufferOffset,
                    BufferRowLength = regions[i].BufferRowLength,
                    ImageExtent = regions[i].TextureExtent.ToVk(),
                    ImageOffset = regions[i].TextureOffset.ToVk(),
                    ImageSubresource = regions[i].TextureSubresource.ToVk(),
                };
                
                ranges[i] = new()
                {
                    AspectMask = vkDstTexture.ImageAspect,
                    BaseArrayLayer = vkRegions[i].ImageSubresource.BaseArrayLayer,
                    LayerCount = vkRegions[i].ImageSubresource.LayerCount,
                    BaseMipLevel = vkRegions[i].ImageSubresource.MipLevel,
                    LevelCount = 1
                };
            }

            _layoutManager.TransitionImageLayout(this, vkDstTexture, ranges, ImageLayout.TransferDstOptimal);
            Device.Vk.CmdCopyBufferToImage(this, (Buffer)srcBuffer, vkDstTexture, ImageLayout.TransferDstOptimal, (uint)regions.Length, vkRegions);
        }

        public void CopyTextureToBuffer(ITexture srcTexture, IBuffer dstBuffer, ReadOnlySpan<BufferTextureCopy> regions)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        private void Predraw()
        {
            var pp = BindPipeline();
            BindDescriptorSet();
        }

        private Cached.Pipeline BindPipeline()
        {
            var pp = Device.PipelineCache.Get(_pipelineState);
            _pipelineStates.Add(_pipelineState);

            Device.Vk.CmdBindPipeline(this, _pipelineState.Pipeline.BindPoint, pp);
            Device.Vk.CmdSetViewport(this, 0, _viewports);
            Device.Vk.CmdSetScissor(this, 0, _scissors);

            return pp;
        }

        private unsafe void BindDescriptorSet()
        {
            var pipeline = _pipelineState.Pipeline;
            if (_resourceBindings.IsDirty)
            {
                var layout = pipeline.Layout;
                var descriptorSet = layout.AllocateDescriptorSet();

                var bufferInfos = stackalloc byte[Math.Max(sizeof(DescriptorBufferInfo), sizeof(DescriptorImageInfo)) * _resourceBindings.Length];
                var descriptorWrites = stackalloc WriteDescriptorSet[_resourceBindings.Length];
                var descriptorWritesLength = 0u;

                foreach (var resource in _resourceBindings)
                {
                    // Get layout binding for given binding index.
                    var binding = resource.Binding;

                    if (!layout.TryGetLayoutBinding(binding, out var layoutBinding))
                    {
                        Device.Logger.LogWarning($"Cannot find the layout for the binding value '{resource}'.");
                        continue;
                    }

                    ref var descriptorWrite = ref descriptorWrites[descriptorWritesLength++];
                    descriptorWrite = new WriteDescriptorSet
                    {
                        SType = StructureType.WriteDescriptorSet,
                        DstSet = descriptorSet,
                        DstBinding = 0,
                        DstArrayElement = 0,

                        DescriptorType = layoutBinding.DescriptorType,
                        DescriptorCount = 1,
                    };

                    if (resource.IsBufferBinding)
                    {
                        var pBufferInfo = (DescriptorBufferInfo*)bufferInfos;
                        bufferInfos += sizeof(DescriptorBufferInfo);

                        descriptorWrite.PBufferInfo = pBufferInfo;

                        *pBufferInfo = new DescriptorBufferInfo
                        {
                            Buffer = (Buffer)resource.Buffer.Buffer,
                            Offset = (ulong)resource.Buffer.Offset,
                            Range = (ulong)resource.Buffer.Size,
                        };
                    }
                    else if (resource.IsTextureOrSamplerBinding)
                    {
                        var pImageInfo = (DescriptorImageInfo*)bufferInfos;
                        bufferInfos += sizeof(DescriptorImageInfo);

                        descriptorWrite.PImageInfo = pImageInfo;

                        *pImageInfo = default;

                        if (resource.Texture != null)
                        {
                            *pImageInfo = new()
                            {
                                ImageView = resource.Texture,
                                ImageLayout = descriptorWrite.DescriptorType switch
                                {
                                    DescriptorType.SampledImage => ImageLayout.ShaderReadOnlyOptimal,
                                    DescriptorType.CombinedImageSampler => ImageLayout.ShaderReadOnlyOptimal,
                                    DescriptorType.StorageImage => ImageLayout.General,
                                    _ => default,
                                },
                            };

                            // skip image without defined layout
                            if (pImageInfo->ImageLayout == default)
                            {
                                Device.Logger.LogWarning($"It is not possible to deduce the image layout for the '{resource}' resource. Please check the corresponding {nameof(SetLayoutBinding)}.");
                                continue;
                            }
                        }

                        if (resource.Sampler != null)
                            pImageInfo->Sampler = resource.Sampler;
                    }
                    else
                        Device.Logger.LogWarning($"The resource '{resource}' is invalid.");
                }

                Device.Vk.UpdateDescriptorSets(Device, descriptorWritesLength, descriptorWrites, 0, null);

                _descriptorSets.Push(descriptorSet);
                var vkDescriptorSet = (Silk.NET.Vulkan.DescriptorSet)descriptorSet;
                Device.Vk.CmdBindDescriptorSets(this, _pipelineState.Pipeline.BindPoint, pipeline.PipelineLayout, 0, 1, &vkDescriptorSet, 0, null);
            }
        }

        public unsafe void TransiantImageLayout(Texture texture, ReadOnlySpan<ImageSubresourceRange> ranges, ImageLayout oldLayout, ImageLayout newLayout)
        {
            this.CheckThreadId();

            if (texture == null)
                throw new ArgumentNullException(nameof(texture));

            Debug.Assert(oldLayout != newLayout);

            var srcStageFlags = oldLayout.GetSrcStageFlags(newLayout);
            var dstStageFlags = newLayout.GetDstStageFlags();

            Span<ImageMemoryBarrier> barriers = stackalloc ImageMemoryBarrier[ranges.Length];
            for(var i = 0; i < ranges.Length; i++)
            {
                var range = ranges[i];

                
                barriers[i] = new ImageMemoryBarrier()
                {
                    SType = StructureType.ImageMemoryBarrier,
                    OldLayout = oldLayout,
                    NewLayout = newLayout,
                    SrcQueueFamilyIndex = Vk.QueueFamilyIgnored,
                    DstQueueFamilyIndex = Vk.QueueFamilyIgnored,
                    Image = texture,
                    SrcAccessMask = oldLayout.GetSrcAccessMask(newLayout),
                    DstAccessMask = newLayout.GetDstAccessMask(),
                    SubresourceRange = range,
                };
            }

            Device.Vk.CmdPipelineBarrier(
                this,
                srcStageFlags, dstStageFlags,
                0, default,
                0, default,
                barriers);
        }

        public void BeginDebugLabel(string labelName, ColorSingle color)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void EndDebugLabel()
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void InsertDebugLabel(string labelName, ColorSingle color)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void DebugMarkerBegin(string markerName, ColorSingle color)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void DebugMarkerEnd()
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }

        public void DebugMessageInsert(string message)
        {
            this.CheckThreadId();

            throw new NotImplementedException();
        }
    }
}
