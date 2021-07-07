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
        /// </summary>
        public SampleCount Samples { get; set; }

        /// <summary>
        /// The texture usage of the <see cref="ITexture"/>.
        /// </summary>
        public TextureUsage Usage { get; set; }

        /// <summary>
        /// The width of the <see cref="ITexture"/>.
        /// </summary>
        public uint Width { get; set; }

        /// <summary>
        /// The height of the <see cref="ITexture"/>.
        /// </summary>
        public uint Height { get; set; }

        /// <summary>
        /// The depth of the <see cref="ITexture"/>.
        /// </summary>
        public uint Depth { get; set; }

        /// <summary>
        /// Creates a <see cref="TextureCreateInfo"/> struct.
        /// </summary>
        /// <param name="type">The texture type of a <see cref="ITexture"/>.</param>
        /// <param name="format">The pixel format of a <see cref="ITexture"/>.</param>
        /// <param name="mipLevels">The mipmap levels of a <see cref="ITexture"/>.</param>
        /// <param name="arrayLayers">The array layers of a <see cref="ITexture"/>.</param>
        /// <param name="samples">The sample count of a <see cref="ITexture"/>.</param>
        /// <param name="usage">The texture usage of a <see cref="ITexture"/>.</param>
        /// <param name="width">The texture width of a <see cref="ITexture"/>.</param>
        /// <param name="height">The texture height of a <see cref="ITexture"/>.</param>
        /// <param name="depth">The texture depth of a <see cref="ITexture"/>.</param>
        public TextureCreateInfo(
            TextureType type, 
            PixelFormat format, 
            uint mipLevels, 
            uint arrayLayers, 
            SampleCount samples, 
            TextureUsage usage, 
            uint width, 
            uint height, 
            uint depth) 
            => (Type, Format, MipLevels, ArrayLayers, Samples, Usage, Width, Height, Depth) =
                (type, format, mipLevels, arrayLayers, samples, usage, width, height, depth);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<TextureCreateInfo>.Combine(Type, Format, MipLevels, ArrayLayers, Samples, Usage, Width, Height, Depth);

        /// <inheritdoc/>
        public bool Equals(TextureCreateInfo other) =>
            Type == other.Type &&
            Format == other.Format &&
            MipLevels == other.MipLevels &&
            ArrayLayers == other.ArrayLayers &&
            Samples == other.Samples &&
            Usage == other.Usage &&
            Width == other.Width &&
            Height == other.Height &&
            Depth == other.Depth;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is TextureCreateInfo tci && Equals(tci);

        /// <inheritdoc/>
        public override string ToString() => 
            $"(Type: {Type}, Format: {Format}, " +
            $"MipLevels: {MipLevels}, ArrayLayers: {ArrayLayers}, " +
            $"Samples: {Samples}, Usage: {Usage}, " +
            $"Width: {Width}, Height: {Height}," +
            $"Depth: {Depth})";

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
