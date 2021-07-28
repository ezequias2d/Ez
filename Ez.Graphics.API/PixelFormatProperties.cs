// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Numerics;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes the properties that are supported for a particular combination of <see cref="PixelFormat"/>,
    /// <see cref="TextureType"/>, and <see cref="TextureUsage"/>.
    /// </summary>
    public readonly struct PixelFormatProperties : IEquatable<PixelFormatProperties>
    {
        /// <summary>
        /// The maximum supported size.
        /// </summary>
        public readonly Size3 MaxSize;

        /// <summary>
        /// The maximum supported number of mipmap levels.
        /// </summary>
        public readonly uint MaxMipmapLevels;

        /// <summary>
        /// The maximum supported number of array layers.
        /// </summary>
        public readonly uint MaxArrayLayers;

        /// <summary>
        /// The maximum supported sample count.
        /// </summary>
        public readonly SampleCount MaxSampleCount;

        /// <summary>
        /// Creates a new instance of <see cref="PixelFormatProperties"/> structure.
        /// </summary>
        /// <param name="maxSize">The maximum supported size.</param>
        /// <param name="maxMipmapLevels">The maxiimum supported number of mipmap levels.</param>
        /// <param name="maxArrayLayers">The maximum supported number of array layers.</param>
        /// <param name="maxSampleCount">The maximum supported sample count.</param>
        public PixelFormatProperties(Size3 maxSize, uint maxMipmapLevels, uint maxArrayLayers, SampleCount maxSampleCount)
        {
            MaxSize = maxSize;
            MaxMipmapLevels = maxMipmapLevels;
            MaxArrayLayers = maxArrayLayers;
            MaxSampleCount = maxSampleCount;
        }

        /// <inheritdoc/>
        public bool Equals(PixelFormatProperties other) =>
            MaxSize == other.MaxSize &&
            MaxMipmapLevels == other.MaxMipmapLevels &&
            MaxArrayLayers == other.MaxArrayLayers &&
            MaxSampleCount == other.MaxSampleCount;

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj != null && obj is PixelFormatProperties pfp && Equals(pfp);

        /// <inheritdoc/>
        public readonly override int GetHashCode() =>
            HashHelper<PixelFormatProperties>.Combine(MaxSize, MaxMipmapLevels, MaxArrayLayers, MaxSampleCount);

        /// <inheritdoc/>
        public override string ToString() =>
            $"(PixelFormatProperties {MaxSize},{MaxMipmapLevels},{MaxArrayLayers},{MaxSampleCount})";


        /// <summary>
        /// Compare two <see cref="PixelFormatProperties"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(PixelFormatProperties left, PixelFormatProperties right) => left.Equals(right);


        /// <summary>
        /// Compare two <see cref="PixelFormatProperties"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(PixelFormatProperties left, PixelFormatProperties right) =>
            !(left == right);
    }
}
