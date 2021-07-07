// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Represents which triangles are going to be culling.
    /// </summary>
    public enum CullMode
    {
        /// <summary>
        /// Specifies that no triangles are discarded.
        /// </summary>
        None,
        /// <summary>
        /// Specifies that front-facing triangles are discarded.
        /// </summary>
        Back,
        /// <summary>
        /// Specifies that back-facing triangles are discarded.
        /// </summary>
        Front,
        /// <summary>
        /// Specifies that all triangles are discarded.
        /// </summary>
        FrontAndBack
    }
}
