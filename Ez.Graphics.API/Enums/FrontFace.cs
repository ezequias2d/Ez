// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes the order that the vertices of the front face is described in the vertex buffer.
    /// </summary>
    public enum FrontFace
    {
        /// <summary>
        /// The vertices for the front face are arranged clockwise.
        /// </summary>
        Clockwise,
        /// <summary>
        /// The vertices for the front face are arranged anticlockwise.
        /// </summary>
        Anticlockwise
    }
}
