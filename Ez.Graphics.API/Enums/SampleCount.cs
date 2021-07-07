// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specifies the sample counts supported for a <see cref="ITexture"/> used for 
    /// storage operations.
    /// </summary>
    public enum SampleCount : byte
    {
        /// <summary>
        /// Specifies an image with one sample per pixel.
        /// </summary>
        Count1,
        /// <summary>
        /// Specifies an image with 2 samples per pixel.
        /// </summary>
        Count2,
        /// <summary>
        /// Specifies an image with 4 samples per pixel.
        /// </summary>
        Count4,
        /// <summary>
        /// Specifies an image with 8 samples per pixel.
        /// </summary>
        Count8,
        /// <summary>
        /// Specifies an image with 16 samples per pixel.
        /// </summary>
        Count16,
        /// <summary>
        /// Specifies an image with 32 samples per pixel.
        /// </summary>
        Count32
    }
}
