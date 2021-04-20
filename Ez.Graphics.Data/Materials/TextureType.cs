// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Graphics.Data.Materials
{
    /// <summary>
    /// The type of texture in a <see cref="MaterialProperty"/>.
    /// </summary>
    public enum TextureType : int
    {
        /// <summary>
        /// None texture(used for non-<see cref="MaterialPropertyType.Texture"/> property).
        /// </summary>
        None,
        /// <summary>
        /// Diffuse texture.
        /// </summary>
        Diffuse,
        /// <summary>
        /// Specular texture.
        /// </summary>
        Specular,
        /// <summary>
        /// Ambient texture.
        /// </summary>
        Ambient,
        /// <summary>
        /// Emissive texture.
        /// </summary>
        Emissive,
        /// <summary>
        /// Height texture.
        /// </summary>
        Height,
        /// <summary>
        /// Normal texture.
        /// </summary>
        Normals,
        /// <summary>
        /// Shininess texture.
        /// </summary>
        Shininess,
        /// <summary>
        /// Opacity texture.
        /// </summary>
        Opacity,
        /// <summary>
        /// Displacement texture.
        /// </summary>
        Displacement,
        /// <summary>
        /// Lightmap texture.
        /// </summary>
        Lightmap,
        /// <summary>
        /// Reflection texture.
        /// </summary>
        Reflection,
        /// <summary>
        /// Unknown texture.
        /// </summary>
        Unknown
    }
}
