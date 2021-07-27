// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Drawing;

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// A container that stores encoded commands for a <see cref="IDevice"/>
    /// </summary>
    public interface ICommandBuffer : IResource
    {
        /// <summary>
        /// Start recording a command buffer.
        /// This function must be called before other commands can be issued.
        /// </summary>
        void Begin();

        /// <summary>
        /// Finish recording a command buffer.
        /// This function must only be called after <see cref="Begin"/> has been called.
        /// </summary>
        void End();

        /// <summary>
        /// Reset a command buffer to the initial state.
        /// </summary>
        void Reset();

        /// <summary>
        /// Begin a new render pass.
        /// </summary>
        /// <param name="beginInfo">The render pass info.</param>
        void BeginRenderPass(in RenderPassBeginInfo beginInfo);

        /// <summary>
        /// End the current render pass.
        /// </summary>
        void EndRenderPass();

        /// <summary>
        /// Binds a <see cref="IPipeline"/> used for rendering.
        /// When a new Pipeline is set, the previously-bound resources become invalidated 
        /// and must be re-bound.
        /// </summary>
        /// <param name="pipeline">The <see cref="IPipeline"/> to be bound.</param>
        void BindPipeline(IPipeline pipeline);


        /// <summary>
        /// Binds a <see cref="IBuffer"/> as a resource for the current <see cref="IPipeline"/>.
        /// </summary>
        /// <param name="usage">The usage of the buffer. Only accept <see cref="BufferUsage.StorageBuffer"/> 
        /// or <see cref="BufferUsage.UniformBuffer"/>.</param>
        /// <param name="buffer">The buffer to bind.</param>
        /// <param name="binding">The binding value.</param>
        void BindBuffer(BufferUsage usage, IBuffer buffer, uint binding);

        /// <summary>
        /// Binds a <see cref="BufferSpan"/> as a resource for the current <see cref="IPipeline"/>.
        /// </summary>
        /// <param name="usage">The usage of the buffer. Only accept <see cref="BufferUsage.StorageBuffer"/> 
        /// or <see cref="BufferUsage.UniformBuffer"/>.</param>
        /// <param name="buffer">The buffer range to bind.</param>
        /// <param name="binding">The binding value.</param>
        void BindBuffer(BufferUsage usage, BufferSpan buffer, uint binding);

        /// <summary>
        /// Binds a <see cref="ITexture"/> as a resource for the current <see cref="IPipeline"/>.
        /// </summary>
        /// <param name="texture">The texture to bind.</param>
        /// <param name="sampler">The sampler used by <paramref name="texture"/>.</param>
        /// <param name="binding">The binding value.</param>
        void BindTexture(ITexture texture, ISampler sampler, uint binding);

        /// <summary>
        /// Binds <paramref name="buffer"/> as an active vertex buffer for the given index.
        /// </summary>
        /// <param name="index">The vertex buffer index to bind.</param>
        /// <param name="buffer">The buffer to bind.</param>
        /// <param name="offset">The offset from the start of the buffer, in bytes, from which data will
        /// start to be read.</param>
        void BindVertexBuffer(uint index, IBuffer buffer, ulong offset);

        /// <summary>
        /// Binds <paramref name="buffer"/> as an active index buffer.
        /// </summary>
        /// <param name="buffer">The index buffer to bind.</param>
        /// <param name="format">The format of data in the <paramref name="buffer"/>.</param>
        /// <param name="offset">The offset from the start of the buffer, in bytes, from which data will
        /// start to be read.</param>
        void BindIndexBuffer(IBuffer buffer, IndexType format, ulong offset);

        /// <summary>
        /// Sets the vertex layout state of the vertex buffer.
        /// </summary>
        /// <param name="index">The vertex buffer index to set layout.</param>
        /// <param name="layout">The vertex layout.</param>
        void SetVertexLayoutState(uint index, VertexLayoutState layout);

        /// <summary>
        /// Sets the count of viewports
        /// </summary>
        /// <param name="viewportCount"></param>
        void SetViewportCount(uint viewportCount);

        /// <summary>
        /// Sets the input assembly state of the pipeline.
        /// </summary>
        /// <param name="state">The input assembly state to set.</param>
        void SetInputAssemblyState(in InputAssemblyState state);

        /// <summary>
        /// Sets the input assembly state of the pipeline.
        /// </summary>
        /// <param name="state">The input assembly state to set.</param>
        void SetRasterizationState(in RasterizationState state);

        /// <summary>
        /// Sets the multisample state of the pipeline.
        /// </summary>
        /// <param name="state">The multisample state to set.</param>
        void SetMultisampleState(in MultisampleState state);

        /// <summary>
        /// Sets the depth stencil state of the pipeline.
        /// </summary>
        /// <param name="state">The depth stencil state to set.</param>
        void SetDepthStencilState(in DepthStencilState state);

        /// <summary>
        /// Sets the color blend state of the pipeline.
        /// </summary>
        /// <param name="state">The color blend state to set.</param>
        void SetColorBlendState(in ColorBlendState state);

        /// <summary>
        /// Sets the <see cref="Viewport"/> at the given index.
        /// The <paramref name="index"/> given must be less than the number of color attachments in the active <see cref="IFramebuffer"/>.
        /// </summary>
        /// <param name="index">Is the index of the viewport whose parameters are updated by the command.</param>
        /// <param name="viewport">The viewport to set.</param>
        void SetViewport(int index, in Viewport viewport);

        /// <summary>
        /// Set the scissor ractangle.
        /// The index given must be less than the number of color attachments in the active <see cref="IFramebuffer"/>.
        /// </summary>
        /// <param name="index">The color target index. (if index &lt; 0 than sets all color targets)</param>
        /// <param name="rectangle">The rectangle to scissor.</param>
        void SetScissor(int index, in Rectangle rectangle);

        /// <summary>
        /// Sets the line width state.
        /// </summary>
        /// <param name="lineWidth">The line width to set.</param>
        void SetLineWidth(float lineWidth);

        /// <summary>
        /// Sets the depth bias state.
        /// </summary>
        /// <param name="depthBiasConstantFactor">The scalar factor controlling the constant depth value added to each 
        /// fragment.</param>
        /// <param name="depthBiasClamp">The maximum(or minimum) depth bias of a fragment.</param>
        /// <param name="depthBiasSlopeFactor">The scalar factor applied to a fragment's slope in depth bias calculation.</param>
        void SetDepthBias(float depthBiasConstantFactor, float depthBiasClamp, float depthBiasSlopeFactor);

        /// <summary>
        /// Sets the values of blend constants.
        /// </summary>
        /// <param name="blendConstants">The blend constant color used in blending.</param>
        void SetBlendConstants(ColorSingle blendConstants);

        /// <summary>
        /// Sets the depth bounds test values.
        /// </summary>
        /// <param name="minDepthBounds">The minimum depth bound.</param>
        /// <param name="maxDepthBounds">The maximum depth bound.</param>
        void SetDepthBounds(float minDepthBounds, float maxDepthBounds);

        /// <summary>
        /// Sets the stencil compare mask state.
        /// </summary>
        /// <param name="faceMask">The bitmask specifying the set of stencil state for which to
        /// update the compare mask.</param>
        /// <param name="compareMask">The new value to use as the stencil compare mask.</param>
        void SetStencilCompareMask(StencilFaces faceMask, uint compareMask);

        /// <summary>
        /// Sets the stencil write mask state.
        /// </summary>
        /// <param name="faceMask">The bitmask specifying the set of stencil state for which to
        /// update the write mask.</param>
        /// <param name="writeMask">The new value to use as the stencil write mask.</param>
        void SetStencilWriteMask(StencilFaces faceMask, uint writeMask);

        /// <summary>
        /// Sets the stencil reference state.
        /// </summary>
        /// <param name="faceMask">The bitmask specifying the set of stencil state for which to
        /// update the reference value.</param>
        /// <param name="reference"></param>
        void SetStencilReference(StencilFaces faceMask, uint reference);

        /// <summary>
        /// Draws primitives only from the current vertex buffer.
        /// </summary>
        /// <param name="vertexCount">The number of vertices to draw.</param>
        /// <param name="instanceCount">The number of instances to draw.</param>
        /// <param name="firstVertex">The index of the first vertex to draw.</param>
        /// <param name="firstInstance">The instance ID of the first instance to draw.</param>
        void Draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance);

        /// <summary>
        /// Draws indexed primitives from the current vertex and index buffers.
        /// </summary>
        /// <param name="indexCount">The number of vertices to draw.</param>
        /// <param name="instanceCount">The number of instances to draw.</param>
        /// <param name="firstIndex">The base index within the index buffer.</param>
        /// <param name="vertexOffset">The value added to the vertex index before indexing into the vertex buffer.</param>
        /// <param name="firstInstance">The instance ID of the first instance to draw.</param>
        void DrawIndexed(uint indexCount, uint instanceCount, uint firstIndex, uint vertexOffset, uint firstInstance);

        /// <summary>
        /// Draw primitives with indirect parameters.
        /// </summary>
        /// <param name="buffer">The buffer containing draw parameters. 
        /// Must have been created with the <see cref="BufferUsage.IndirectBuffer"/> flag.</param>
        /// <param name="offset">The byte offset into buffer where parameters begin.</param>
        /// <param name="drawCount">The number of draws to execute, and can be zero.</param>
        /// <param name="stride">The byte stride between successive sets of draw parameters.</param>
        void DrawIndirect(IBuffer buffer, ulong offset, uint drawCount, uint stride);

        /// <summary>
        /// Draw primitives with indirect parameters and indexed vertices.
        /// </summary>
        /// <param name="indirectBuffer">The buffer containing draw parameters.
        /// Must have been created with the <see cref="BufferUsage.IndirectBuffer"/> flag.</param>
        /// <param name="offset">The byte offset into buffer where parameters begin.</param>
        /// <param name="drawCount">The number of draws to execute, and can be zero.</param>
        /// <param name="stride">The byte stride between successive sets of draw parameters.</param>
        void DrawIndexedIndirect(IBuffer indirectBuffer, ulong offset, uint drawCount, uint stride);

        /// <summary>
        /// Dispatch compute work items.
        /// </summary>
        /// <param name="groupCountX">The number of local workgroups to dispatch in the X dimension.</param>
        /// <param name="groupCountY">The number of local workgroups to dispatch in the Y dimension.</param>
        /// <param name="groupCountZ">The number of local workgroups to dispatch in the Z dimension.</param>
        void Dispatch(uint groupCountX, uint groupCountY, uint groupCountZ);

        /// <summary>
        /// Dispatch compute work items with indirect parameters.
        /// </summary>
        /// <param name="buffer">The buffer containing dispatch parameters.</param>
        /// <param name="offset">The byte offset into <paramref name="buffer"/> where parameters begin.</param>
        void DispatchIndirect(IBuffer buffer, long offset);

        /// <summary>
        /// Copy data between buffer regions.
        /// </summary>
        /// <param name="srcBuffer">The source buffer.</param>
        /// <param name="dstBuffer">The destination buffer.</param>
        /// <param name="regions">The regions to copy.</param>
        void CopyBuffer(IBuffer srcBuffer, IBuffer dstBuffer, ReadOnlySpan<BufferCopy> regions);

        /// <summary>
        /// Copy data from a buffer into an texture.
        /// </summary>
        /// <param name="srcBuffer">The source buffer.</param>
        /// <param name="dstTexture">The destination texture.</param>
        /// <param name="regions">The regions to copy.</param>
        void CopyBufferToTexture(IBuffer srcBuffer, ITexture dstTexture, ReadOnlySpan<BufferTextureCopy> regions);

        /// <summary>
        /// Copy data between textures.
        /// </summary>
        /// <param name="srcTexture">The source texture.</param>
        /// <param name="dstTexture">The destination texture.</param>
        /// <param name="regions">The regions to copy.</param>
        void CopyTexture(ITexture srcTexture, ITexture dstTexture, ReadOnlySpan<TextureCopy> regions);

        /// <summary>
        /// Copy texture data into a buffer.
        /// </summary>
        /// <param name="srcTexture">The source texture.</param>
        /// <param name="dstBuffer">The destination buffer.</param>
        /// <param name="regions">The regions to copy.</param>
        void CopyTextureToBuffer(ITexture srcTexture, IBuffer dstBuffer, ReadOnlySpan<BufferTextureCopy> regions);

        /// <summary>
        /// Updates a buffer's contents from host memory.
        /// The data size must be less than or equal to 65536 bytes. For larger updates, applications can use buffer to buffer copies.
        /// <para>
        /// Note
        /// <br/>
        /// Buffer updates performed with <see cref="UpdateBuffer(IBuffer, long, ReadOnlySpan{byte})"/> first copy the data into command 
        /// buffer memory when the command is recorded(which requires additional storage and may incur an additional allocation), and
        /// then copy the data from the command buffer into dstBuffer when the command is executed on a device.
        /// </para>
        /// </summary>
        /// <param name="dstBuffer">The buffer to be updated.</param>
        /// <param name="dstOffset">The byte offset into the buffer to start updating, and must be a multiple of 4.</param>
        /// <param name="data">The source data for the buffer update, and must have a size multiple of 4.</param>
        void UpdateBuffer(IBuffer dstBuffer, long dstOffset, ReadOnlySpan<byte> data);

        /// <summary>
        /// Fill a region of a buffer with a fixed value.
        /// </summary>
        /// <param name="dstBuffer">The buffer to be filled.</param>
        /// <param name="dstOffset">The byte offset into the buffer at which to start filling, and must be a multiple of 4.</param>
        /// <param name="size">The number of bytes to fill, and must be either a multiple of 4.</param>
        /// <param name="data">The 4-byte word written repeatedly to the buffer to fill <paramref name="size"/> bytes of data.</param>
        void FillBuffer(IBuffer dstBuffer, long dstOffset, long size, uint data);

        /// <summary>
        /// Clear regions of a color texture.
        /// </summary>
        /// <param name="texture">The texture to be cleared.</param>
        /// <param name="color">The clear color value.</param>
        /// <param name="ranges">The ranges to clear.</param>
        void ClearColorTexture(ITexture texture, in ClearColorValue color, ReadOnlySpan<TextureSubresourceRange> ranges);

        /// <summary>
        /// Fill regions of a combined depth/stencil texture.
        /// </summary>
        /// <param name="texture">The texture to be cleared.</param>
        /// <param name="depthStencil">The clear depth stencil value.</param>
        /// <param name="ranges">The ranges to clear.</param>
        void ClearDepthStencilTexture(ITexture texture, in ClearDepthStencilValue depthStencil, ReadOnlySpan<TextureSubresourceRange> ranges);

        /// <summary>
        /// Clear regions within bound framebuffer attachments.
        /// </summary>
        /// <param name="attachments">The attachments to clear and the values to use.</param>
        /// <param name="regions">The regions to clear.</param>
        void ClearAttachments(ReadOnlySpan<ClearAttachment> attachments, ReadOnlySpan<ClearRectangle> regions);

        /// <summary>
        /// Resolve regions of an texture.
        /// </summary>
        /// <param name="srcTexture">The source texture.</param>
        /// <param name="dstTexture">The destination texture.</param>
        /// <param name="regions">The regions to resolve.</param>
        void ResolveTexture(ITexture srcTexture, ITexture dstTexture, ReadOnlySpan<TextureResolve> regions);

        /// <summary>
        /// Open a command buffer marker region.
        /// </summary>
        /// <param name="markerName">The name of the marker.</param>
        /// <param name="color">The optional color that can be associated with the marker.</param>
        void DebugMarkerBegin(string markerName, ColorSingle color);

        /// <summary>
        /// Close a command buffer marker region.
        /// </summary>
        void DebugMarkerEnd();

        /// <summary>
        /// Inserts a debug marker into the CommandList at the current position. This is used by graphics debuggers to identify
        /// points of interest in a command stream.
        /// </summary>
        /// <param name="message">The name of the marker. This is an opaque identifier used for display by graphics debuggers.</param>
        void DebugMessageInsert(string message);
    }
}
