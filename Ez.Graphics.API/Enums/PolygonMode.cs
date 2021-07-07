// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Represents the polygon rasterization mode.
    /// </summary>
    public enum PolygonMode
    {
        /// <summary>
        /// Specifies that polygon vertices are drawn as points.
        /// </summary>
        Point,
        /// <summary>
        /// Specifies that polygon edges are drawn as line segments.
        /// </summary>
        Line,
        /// <summary>
        /// Specifies that polygons are rendered filled.
        /// </summary>
        Fill
    }
}
