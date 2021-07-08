// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Structure specifying a clear depth stencil value.
    /// </summary>
    public struct ClearDepthStencilValue
    {
        /// <summary>
        /// Gets or sets the clear value for the depth aspect of the depth/stencil attachment. 
        /// It is a floating-point value which is automatically converted to the attachment’s format.
        /// </summary>
        public float Depth { get; set; }

        /// <summary>
        /// Gets or sets the clear value for the stencil aspect of the depth/stencil attachment. 
        /// It is a 32-bit integer value which is converted to the attachment’s format by taking 
        /// the appropriate number of LSBs.
        /// </summary>
        public uint Stencil { get; set; }
    }
}
