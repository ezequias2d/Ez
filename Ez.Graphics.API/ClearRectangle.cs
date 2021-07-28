// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System.Drawing;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes a clear rectangle.
    /// </summary>
    public struct ClearRectangle
    {
        /// <summary>
        /// Gets or sets the region to be cleared.
        /// </summary>
        public Rectangle Rectangle;

        /// <summary>
        /// Gets or sets the first layer to be cleared.
        /// </summary>
        public uint BaseArrayLayer;

        /// <summary>
        /// Gets or sets the number of layers to clear.
        /// </summary>
        public uint ArrayLayerCount;
    }
}
