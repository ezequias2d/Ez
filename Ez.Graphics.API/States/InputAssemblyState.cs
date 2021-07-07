// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;
using Ez.Numerics;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Defines paramaters of pipeline input assembly state.
    /// </summary>
    public struct InputAssemblyState : IEquatable<InputAssemblyState>
    {
        /// <summary>
        /// The <see cref="Topology"/> controls what a vertex stream represents when rendering.
        /// </summary>
        public PrimitiveTopology Topology { get; set; }

        /// <summary>
        /// The <see cref="PrimitiveRestartEnable"/> controls whether a special vertex index value
        /// is treated as restarting the assembly of primitives. This enable only applies to indexed
        /// draws (vkCmdDrawIndexed and vkCmdDrawIndexedIndirect), and the special index value is 
        /// either 0xFFFFFFFF when the <see cref="IndexType"/> parameter of 
        /// <see cref="ICommandBuffer.BindIndexBuffer(IBuffer, IndexType, uint)"/>
        /// is equal to <see cref="IndexType.UInt"/>, or 0xFFFF when <see cref="IndexType"/> is equal
        /// to <see cref="IndexType.UShort"/>. Primitive restart is not allowed for “list” topologies.
        /// </summary>
        public bool PrimitiveRestartEnable { get; set; }

        /// <inheritdoc/>
        public bool Equals(InputAssemblyState other) =>
            Topology == other.Topology &&
            PrimitiveRestartEnable == other.PrimitiveRestartEnable;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is InputAssemblyState ias && Equals(ias);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelper<ColorBlendAttachmentState>.Combine(Topology, PrimitiveRestartEnable);

        /// <inheritdoc/>
        public override string ToString() => $"(Topology: {Topology}, PrimitiveRestartEnable: {PrimitiveRestartEnable})";

        /// <summary>
        /// Compare two <see cref="InputAssemblyState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(InputAssemblyState left, InputAssemblyState right) => left.Equals(right);

        /// <summary>
        /// Compare two <see cref="InputAssemblyState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(InputAssemblyState left, InputAssemblyState right) =>
            !(left == right);

        /// <summary>
        /// The default input assembly state in a render pass.
        /// </summary>
        public static readonly InputAssemblyState Default = new()
        {
            PrimitiveRestartEnable = false,
            Topology = PrimitiveTopology.TriangleList,
        };
    }
}
