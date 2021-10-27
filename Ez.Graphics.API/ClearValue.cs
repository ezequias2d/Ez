// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Ez.Numerics;
using System;
using System.Runtime.InteropServices;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Structure specifying a clear value.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ClearValue : IEquatable<ClearValue>
    {
        /// <summary>
        /// Gets or sets the color image clear values to use when clearing a color image 
        /// or attachment.
        /// </summary>
        [FieldOffset(0)]
        public ClearColorValue Color;

        /// <summary>
        /// Gets or sets the depth and stencil clear values to use when clearing a 
        /// depth/stencil image or attachment.
        /// </summary>
        [FieldOffset(0)]
        public ClearDepthStencilValue DepthStencil;

        /// <inheritdoc/>
        public bool Equals(ClearValue other) =>
            Color.Equals(other.Color) &&
            DepthStencil.Equals(other.DepthStencil);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelper<ClearValue>.Combine(Color, DepthStencil);

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is ClearValue cv && Equals(cv);

        /// <summary>
        /// Compare two <see cref="ClearValue"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(ClearValue left, ClearValue right) => left.Equals(right);

        /// <summary>
        /// Compare two <see cref="ClearValue"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(ClearValue left, ClearValue right) => !(left == right);
    }
}
