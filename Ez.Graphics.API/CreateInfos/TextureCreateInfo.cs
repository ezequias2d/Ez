// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;
using Ez.Numerics;

namespace Ez.Graphics.API.CreateInfos
{
    /// <summary>
    /// Describes a <see cref="ITexture"/> object.
    /// </summary>
    public struct TextureCreateInfo : IEquatable<TextureCreateInfo>
    {
        /// <summary>
        /// The type of the <see cref="ITexture"/>
        /// </summary>
        public TextureType Type { get; set; }

        /// <summary>
        /// The format of the <see cref="ITexture"/>.
        /// </summary>
        public PixelFormat Format { get; set; }

        /// <summary>
        /// The mipmap levels of the <see cref="ITexture"/>.
        /// </summary>
        public uint MipLevels { get; set; }

        /// <summary>
        /// The array layers of the <see cref="ITexture"/>.
        /// </summary>
        public uint ArrayLayers { get; set; }

        /// <summary>
        /// The sample count of the <see cref="ITexture"/>.
        /// 
        /// <para>
        ///     <list type="bullet">
        ///         <item>If is not <see cref="SampleCount.Count1"/>, then <see cref="Type"/> must be
        ///         <see cref="TextureType.Texture2D"/> and <see cref="Usage"/> must not contain 
        ///         <see cref="TextureUsage.Cubemap"/>.</item>
        ///     </list>
        /// </para>
        /// </summary>
        public SampleCount Samples { get; set; }

        /// <summary>
        /// The tiling arrangement of the texel blocks in memory.
        /// </summary>
        public TextureTiling Tiling { get; set; }

        /// <summary>
        /// The texture usage of the <see cref="ITexture"/>.
        /// </summary>
        public TextureUsage Usage { get; set; }

        /// <summary>
        /// The size of the <see cref="ITexture"/>.
        /// </summary>
        public Size3 Size { get; set; }

        /// <summary>
        /// Indicates the memory usage of the <see cref="ITexture"/>.
        /// The memory mode of the <see cref="ITexture"/>.
        /// </summary>
        public MemoryUsage MemoryMode { get; set; }

        /// <summary>
        /// Creates a <see cref="TextureCreateInfo"/> struct.
        /// </summary>
        /// <param name="type">The texture type of a <see cref="ITexture"/>.</param>
        /// <param name="format">The pixel format of a <see cref="ITexture"/>.</param>
        /// <param name="mipLevels">The mipmap levels of a <see cref="ITexture"/>.</param>
        /// <param name="arrayLayers">The array layers of a <see cref="ITexture"/>.</param>
        /// <param name="samples">The sample count of a <see cref="ITexture"/>.</param>
        /// <param name="tiling">The tiling arrangement of the texel blocks in memory.</param>
        /// <param name="usage">The texture usage of a <see cref="ITexture"/>.</param>
        /// <param name="size">The texture size of a <see cref="ITexture"/>.</param>
        /// <param name="memoryMode"></param>
        public TextureCreateInfo(
            TextureType type, 
            PixelFormat format, 
            uint mipLevels, 
            uint arrayLayers, 
            SampleCount samples,
            TextureTiling tiling,
            TextureUsage usage, 
            Size3 size,
            MemoryUsage memoryMode) 
            => (Type, Format, MipLevels, ArrayLayers, Samples, Usage, Size, Tiling, MemoryMode) =
                (type, format, mipLevels, arrayLayers, samples, usage, size, tiling, memoryMode);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<TextureCreateInfo>.Combine(Type, Format, MipLevels, ArrayLayers, Samples, Usage, Size);

        /// <inheritdoc/>
        public bool Equals(TextureCreateInfo other) =>
            Type == other.Type &&
            Format == other.Format &&
            MipLevels == other.MipLevels &&
            ArrayLayers == other.ArrayLayers &&
            Samples == other.Samples &&
            Usage == other.Usage &&
            Size == other.Size;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is TextureCreateInfo tci && Equals(tci);

        /// <inheritdoc/>
        public override string ToString() => 
            $"(Type: {Type}, Format: {Format}, " +
            $"MipLevels: {MipLevels}, ArrayLayers: {ArrayLayers}, " +
            $"Samples: {Samples}, Usage: {Usage}, " +
            $"Size: {Size})";

        /// <summary>
        /// Compare two <see cref="TextureCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(TextureCreateInfo left, TextureCreateInfo right) =>
            left.Equals(right);

        /// <summary>
        /// Compare two <see cref="TextureCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(TextureCreateInfo left, TextureCreateInfo right) =>
            !(left == right);
    }
}
