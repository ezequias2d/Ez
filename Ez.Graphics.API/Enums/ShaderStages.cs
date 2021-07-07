// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Represents shader stages of a <see cref="IPipeline"/>.
    /// </summary>
    [Flags]
    public enum ShaderStages
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,
        /// <summary>
        /// Specifies the vertex stage.
        /// </summary>
        Vertex = 1,
        /// <summary>
        /// Specifies the fragment stage.
        /// </summary>
        Fragment = 1 << 1,
        /// <summary>
        /// Specifies the compute stage.
        /// </summary>
        Compute = 1 << 2,
        /// <summary>
        /// Specifies the geometry stage.
        /// </summary>
        Geometry = 1 << 3,
        /// <summary>
        /// Specifies the tessellation evaluation stage.
        /// </summary>
        TessellationEvaluation = 1 << 4,
        /// <summary>
        /// Specifies the tessellation control stage.
        /// </summary>
        TessellationControl = 1 << 5,
    }
}
