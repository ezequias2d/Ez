// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Drawing;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes a clear rectangle.
    /// </summary>
    public struct ClearRectangle : IEquatable<ClearRectangle>
    {
        /// <summary>
        /// Gets or sets the region to be cleared.
        /// </summary>
        public Rectangle Rectangle;

        /// <summary>
        /// Gets or sets the first layer to be cleared.
        /// </summary>
        public uint BaseArrayLayer;

        /// <summary>
        /// Gets or sets the number of layers to clear.
        /// </summary>
        public uint ArrayLayerCount;

        /// <inheritdoc/>
        public bool Equals(ClearRectangle other) =>
            Rectangle == other.Rectangle &&
            BaseArrayLayer == other.BaseArrayLayer &&
            ArrayLayerCount == other.ArrayLayerCount;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is ClearRectangle cr && Equals(cr);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelper<ClearRectangle>.Combine(Rectangle, BaseArrayLayer, ArrayLayerCount);

        /// <summary>
        /// Compare two <see cref="ClearRectangle"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(ClearRectangle left, ClearRectangle right) => left.Equals(right);

        /// <summary>
        /// Compare two <see cref="ClearRectangle"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(ClearRectangle left, ClearRectangle right) => !(left == right);
    }
}
