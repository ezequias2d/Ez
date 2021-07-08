// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Structure specifying a clear value.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ClearValue
    {
        /// <summary>
        /// Gets or sets the color image clear values to use when clearing a color image 
        /// or attachment.
        /// </summary>
        [FieldOffset(0)]
        public ClearColorValue Color;

        /// <summary>
        /// Gets or sets the depth and stencil clear values to use when clearing a 
        /// depth/stencil image or attachment.
        /// </summary>
        [FieldOffset(0)]
        public ClearDepthStencilValue DepthStencil;
    }
}
