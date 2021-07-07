// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes flags that can be used to tell how a buffer can be used.
    /// </summary>
    [Flags]
    public enum BufferUsage : byte
    {
        /// <summary>
        /// Indicates that there is no flag.
        /// </summary>
        None = 0,
        /// <summary>
        /// Specifies that the buffer can be used as the source of a transfer command.
        /// </summary>
        TransferSource = 1,
        /// <summary>
        /// Specifies that the buffer can be used as the destination of a transfer command.
        /// </summary>
        TransferDestination = 1 << 1,
        /// <summary>
        /// Specifies that the <see cref="IBuffer"/> can be used as a uniform Buffer in
        /// <see cref="ICommandBuffer.BindBuffer(BufferUsage, IBuffer, uint)"/> and
        /// <see cref="ICommandBuffer.BindBuffer(BufferUsage, IBuffer, uint)"/>.
        /// </summary>
        UniformBuffer = 1 << 2,
        /// <summary>
        /// Specifies that the <see cref="IBuffer"/> can be used as storage buffer in
        /// <see cref="ICommandBuffer.BindBuffer(BufferUsage, IBuffer, uint)"/> and
        /// <see cref="ICommandBuffer.BindBuffer(BufferUsage, IBuffer, uint)"/>.
        /// </summary>
        StorageBuffer = 1 << 3,
        /// <summary>
        /// Specifies that the <see cref="IBuffer"/> can be used as index buffer in
        /// <see cref="ICommandBuffer.BindIndexBuffer(IBuffer, IndexType, uint)"/>.
        /// </summary>
        IndexBuffer = 1 << 4,
        /// <summary>
        /// Specifies that the <see cref="IBuffer"/> can be used as vertex buffer in
        /// <see cref="ICommandBuffer.BindVertexBuffer(uint, IBuffer, uint)"/>.
        /// </summary>
        VertexBuffer = 1 << 5,
        /// <summary>
        /// Specifies that the <see cref="IBuffer"/> can be used as indirect buffer in
        /// <see cref="ICommandBuffer.DrawIndirect(IBuffer, uint, uint, uint)"/>,
        /// <see cref="ICommandBuffer.DrawIndexedIndirect(IBuffer, uint, uint, uint)"/> and
        /// <see cref="ICommandBuffer.DispatchIndirect(IBuffer, long)"/>.
        /// </summary>
        IndirectBuffer = 1 << 6,
        /// <summary>
        /// Indicates that a <see cref="IBuffer"/> is host visible.<br/>        
        /// Host Buffers can use all <see cref="MapMode"/> values in
        /// <see cref="IMappableResource.Map(MapMode, uint)"/>.
        /// </summary>
        HostBuffer = 1 << 7,
    }
}
