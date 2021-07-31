// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes the behavior of sampling outside an <see cref="ITexture"/>.
    /// </summary>
    public enum EdgeSample
    {
        /// <summary>
        /// The texture coordinate wraps around the texture.
        /// So a texture coordinate of -0.2 becomes the equivalent of 0.8.
        /// </summary>
        Repeat,
        /// <summary>
        /// The texture coordinate wraps around like a mirror.
        /// So a texture coordinate of -0.2 becomes 0.2 and -1.2 becomes 0.8.
        /// </summary>
        MirrorRepeat,
        /// <summary>
        /// The texture coordinate is clamped to the [0, 1] range.
        /// </summary>
        ClampToEdge,
    }
}
