// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// Specifying the basic dimensionality of an texture.
    /// </summary>
    public enum TextureType
    {
        /// <summary>
        /// Specifies a one-dimensional texture.
        /// </summary>
        Texture1D,
        /// <summary>
        /// Specifies a two-dimensional texture.
        /// </summary>
        Texture2D,
        /// <summary>
        /// Specifies a three-dimensional image.
        /// </summary>
        Texture3D
    }
}
