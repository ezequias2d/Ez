// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Represents the topology of vertex data.
    /// </summary>
    public enum PrimitiveTopology
    {
        /// <summary>
        /// Specifies a series of separate triangle primitives.
        /// </summary>
        TriangleList,
        /// <summary>
        /// Specifies a series of connected triangle primitives with consecutive triangles sharing an edge.
        /// </summary>
        TriangleStrip,
        /// <summary>
        /// Specifies a series of separate line primitives.
        /// </summary>
        LineList,
        /// <summary>
        /// Specifies a series of connected line primitives with consecutive lines sharing a vertex.
        /// </summary>
        LineStrip,
        /// <summary>
        /// Specifies a series of separate point primitives.
        /// </summary>
        PointList
    }
}
