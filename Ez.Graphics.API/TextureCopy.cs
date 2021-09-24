// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specifying an texture copy operation.
    /// </summary>
    public struct TextureCopy : IEquatable<TextureCopy>
    {
        /// <summary>
        /// Specifies  the subresource of the texture used for the source
        /// texture data.
        /// </summary>
        public TextureSubresourceLayers SrcSubresource;

        /// <summary>
        /// Specifies the initial X, Y, and Z offsets in texels of the
        /// sub-region of the source texture data.
        /// </summary>
        public Point3 SrcOffset;

        /// <summary>
        /// Specifies the subresource of the texture used for the destination
        /// texture data.
        /// </summary>
        public TextureSubresourceLayers DstSubresource;

        /// <summary>
        /// Specifies the initial X, Y, and Z offsets in texels of the
        /// sub-region of the destination texture data.
        /// </summary>
        public Point3 DstOffset;

        /// <summary>
        /// Specifies the size in texels of the texture to copy in width,
        /// height and depth.
        /// </summary>
        public Size3 Extent;

        /// <inheritdoc/>
        public bool Equals(TextureCopy other) =>
            SrcSubresource == other.SrcSubresource &&
            SrcOffset == other.SrcOffset &&
            DstSubresource == other.DstSubresource &&
            DstOffset == other.DstOffset &&
            Extent == other.Extent;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is TextureCopy tc && Equals(tc);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelper<TextureCopy>.Combine(
            SrcSubresource,
            SrcOffset,
            DstSubresource,
            DstOffset,
            Extent);

        /// <summary>
        /// Compare two <see cref="TextureCopy"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(TextureCopy left, TextureCopy right) => left.Equals(right);

        /// <summary>
        /// Compare two <see cref="TextureCopy"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(TextureCopy left, TextureCopy right) => !(left == right);
    }
}
