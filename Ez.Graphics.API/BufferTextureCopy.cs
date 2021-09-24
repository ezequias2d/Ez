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
    public struct BufferTextureCopy : IEquatable<BufferTextureCopy>
    {
        /// <summary>
        /// Specifies the starting offset in bytes from the start of the source buffer.
        /// </summary>
        public long BufferOffset;

        /// <summary>
        /// Specifies in texels  a subregion of a larger two-dimensional
        /// texture in buffer memory, and control the addressing calculations.
        /// If it is zero, that aspect of the buffer memory is considered to be 
        /// tightly packed according to the <see cref="TextureExtent"/>.
        /// </summary>
        public uint BufferRowLength;

        /// <summary>
        /// Specifies in texels  a subregion of a larger three-dimensional
        /// texture in buffer memory, and control the addressing calculations.
        /// If it is zero, that aspect of the buffer memory is considered to be 
        /// tightly packed according to the <see cref="TextureExtent"/>.
        /// </summary>
        public uint BufferTextureHeight;

        /// <summary>
        /// Specifies the number of bytes to copy.
        /// </summary>
        public long Size;

        /// <summary>
        /// Specifies  the subresource of the texture used for the source or 
        /// destination image data.
        /// </summary>
        public TextureSubresourceLayers TextureSubresource;

        /// <summary>
        /// Specifies the initial X, Y, and Z offsets in texels of the
        /// sub-region of the source or destination image data.
        /// </summary>
        public Point3 TextureOffset;

        /// <summary>
        /// Specifies the size in texels of the texture to copy in width,
        /// height and depth.
        /// </summary>
        public Size3 TextureExtent;

        /// <inheritdoc/>
        public bool Equals(BufferTextureCopy other) =>
            BufferOffset == other.BufferOffset &&
            BufferRowLength == other.BufferRowLength &&
            BufferTextureHeight == other.BufferTextureHeight &&
            Size == other.Size &&
            TextureSubresource == other.TextureSubresource &&
            TextureOffset == other.TextureOffset &&
            TextureExtent == other.TextureExtent;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is BufferTextureCopy btc && Equals(btc);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelper<BufferTextureCopy>.Combine(
            BufferOffset,
            BufferRowLength,
            BufferTextureHeight,
            Size,
            TextureSubresource,
            TextureOffset,
            TextureExtent);

        /// <summary>
        /// Compare two <see cref="BufferTextureCopy"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(BufferTextureCopy left, BufferTextureCopy right) => left.Equals(right);

        /// <summary>
        /// Compare two <see cref="BufferTextureCopy"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(BufferTextureCopy left, BufferTextureCopy right) => !(left == right);
    }
}
