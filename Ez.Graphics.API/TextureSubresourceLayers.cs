// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Ez.Numerics;
using System;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specifying an image subresource layers.
    /// </summary>
    public struct TextureSubresourceLayers : IEquatable<TextureSubresourceLayers>
    {
        /// <summary>
        /// Gets or sets the mipmap level to copy.
        /// </summary>
        public uint MipmapLevel;

        /// <summary>
        /// Gets or sets the starting layer to copy.
        /// </summary>
        public uint BaseArrayLayer;

        /// <summary>
        /// Gets or sets the number of layers to copy.
        /// </summary>
        public uint LayerCount;

        /// <inheritdoc/>
        public bool Equals(TextureSubresourceLayers other) =>
            MipmapLevel == other.MipmapLevel &&
            BaseArrayLayer == other.BaseArrayLayer &&
            LayerCount == other.LayerCount;

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is TextureSubresourceLayers tsl && Equals(tsl);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelper<TextureSubresourceLayers>.Combine(MipmapLevel, BaseArrayLayer, LayerCount);

        /// <summary>
        /// Compare two <see cref="TextureSubresourceLayers"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(TextureSubresourceLayers left, TextureSubresourceLayers right) =>
            left.Equals(right);

        /// <summary>
        /// Compare two <see cref="TextureSubresourceLayers"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(TextureSubresourceLayers left, TextureSubresourceLayers right) =>
            !(left == right);
    }
}
