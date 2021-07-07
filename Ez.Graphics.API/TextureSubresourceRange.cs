// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Resources;
using Ez.Numerics;
using System;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specifies a range of image sub-resources .
    /// </summary>
    public struct TextureSubresourceRange : IEquatable<TextureSubresourceRange>
    {
        /// <summary>
        /// Creates a new <see cref="TextureSubresourceRange"/> instance by texture values.
        /// </summary>
        /// <param name="target">The texture to extract values.</param>
        public TextureSubresourceRange(ITexture target)
        {
            BaseMipmapLevel = 0;
            MipmapLevelCount = target.MipmapLevels;
            BaseArrayLayer = 0;
            ArrayLayerCount = target.ArrayLayers;
        }

        /// <summary>
        /// Creates a new <see cref="TextureSubresourceRange"/> instance.
        /// </summary>
        /// <param name="baseMipLevel">The base mip level visible in the view. Must be less 
        /// than <see cref="ITexture.MipmapLevels"/>.</param>
        /// <param name="mipLevels">The number of mip levels visible in the view.</param>
        /// <param name="baseArrayLayer">The base array layer visible in the view.</param>
        /// <param name="arrayLayers">The number of array layers visible in the view.</param>
        public TextureSubresourceRange(uint baseMipLevel, uint mipLevels, uint baseArrayLayer, uint arrayLayers)
        {
            BaseMipmapLevel = baseMipLevel;
            MipmapLevelCount = mipLevels;
            BaseArrayLayer = baseArrayLayer;
            ArrayLayerCount = arrayLayers;
        }

        /// <summary>
        /// The base mip level visible in the view. Must be less than <see cref="ITexture.MipmapLevels"/>.
        /// </summary>
        public uint BaseMipmapLevel { get; set; }
        /// <summary>
        /// The number of mip levels visible in the view.
        /// </summary>
        public uint MipmapLevelCount { get; set; }

        /// <summary>
        /// The base array layer visible in the view.
        /// </summary>
        public uint BaseArrayLayer { get; set; }

        /// <summary>
        /// The number of array layers visible in the view.
        /// </summary>
        public uint ArrayLayerCount { get; set; }

        /// <inheritdoc/>
        public bool Equals(TextureSubresourceRange other) =>
            BaseMipmapLevel == other.BaseMipmapLevel &&
            MipmapLevelCount == other.MipmapLevelCount &&
            BaseArrayLayer == other.BaseArrayLayer &&
            ArrayLayerCount == other.ArrayLayerCount;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is TextureSubresourceRange tsr && Equals(tsr);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<TextureSubresourceRange>.Combine(BaseMipmapLevel, MipmapLevelCount, BaseArrayLayer, ArrayLayerCount);

        /// <inheritdoc/>
        public override string ToString() => 
            $"(BaseMipmapLevel: {BaseMipmapLevel}, MipmapLevelCount: {MipmapLevelCount}, " +
            $"BaseArrayLayer: {BaseArrayLayer}, ArrayLayerCount: {ArrayLayerCount})";

        /// <summary>
        /// Indicates whether a <see cref="TextureSubresourceRange"/> and other <see cref="TextureSubresourceRange"/>
        /// are equals.
        /// </summary>
        /// <param name="left">The first <see cref="TextureSubresourceRange"/>.</param>
        /// <param name="right">The second <see cref="TextureSubresourceRange"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> are equals to <paramref name="right"/>,
        /// otherwise <see langword="false"/>.</returns>
        public static bool operator ==(TextureSubresourceRange left, TextureSubresourceRange right) =>
            left.Equals(right);

        /// <summary>
        /// Indicates whether a <see cref="TextureSubresourceRange"/> and other <see cref="TextureSubresourceRange"/>
        /// are equals.
        /// </summary>
        /// <param name="left">The first <see cref="TextureSubresourceRange"/>.</param>
        /// <param name="right">The second <see cref="TextureSubresourceRange"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> are equals to <paramref name="right"/>,
        /// otherwise <see langword="false"/>.</returns>
        public static bool operator !=(TextureSubresourceRange left, TextureSubresourceRange right) =>
            !(left == right);
    }
}
