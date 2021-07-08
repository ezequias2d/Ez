// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes the properties that are supported for a particular combination of <see cref="PixelFormat"/>,
    /// <see cref="TextureType"/>, and <see cref="TextureUsage"/>.
    /// </summary>
    public struct PixelFormatProperties
    {
        /// <summary>
        /// The maximum supported width.
        /// </summary>
        public readonly uint MaxWidth;

        /// <summary>
        /// The maximum supported height.
        /// </summary>
        public readonly uint MaxHeight;

        /// <summary>
        /// The maximum supported depth.
        /// </summary>
        public readonly uint MaxDepth;

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
    }
}
