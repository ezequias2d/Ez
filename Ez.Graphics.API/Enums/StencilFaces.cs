// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specifying sets of stencil state for which to update the compare mask.
    /// </summary>
    [Flags]
    public enum StencilFaces
    {
        /// <summary>
        /// Specifies that only the front set of stencil state is updated.
        /// </summary>
        Front,
        /// <summary>
        /// Specifies that only the back set of stencil state is updated.
        /// </summary>
        Back,
        /// <summary>
        /// Specifies that the front and back set of stencil state is update.
        /// </summary>
        FrontAndBack
    }
}
