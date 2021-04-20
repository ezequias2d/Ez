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
    /// Represents the light source of a <see cref="Light"/> structure.
    /// </summary>
    public enum LightSource : byte
    {
        /// <summary>
        /// Undefined light.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// A directional light.
        /// </summary>
        Directional = 1,
        /// <summary>
        /// A point light.
        /// </summary>
        Point = 2,
        /// <summary>
        /// A spot light.
        /// </summary>
        Spot = 3,
        /// <summary>
        /// A area light.
        /// </summary>
        Area = 4,
        /// <summary>
        /// A ambient light.
        /// </summary>
        Ambient = 5
    }
}
