// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specify rate at which vertex attributes are pulled from buffers.
    /// </summary>
    public enum VertexInputRate : uint
    {
        /// <summary>
        /// Specifies that vertex attribute addressing is a function of the vertex index.
        /// </summary>
        Vertex = 0,
        /// <summary>
        /// Specifies that vertex attribute addressing is a function of the instance index.
        /// </summary>
        Instance = 1,
    }
}
