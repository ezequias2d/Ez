// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Graphics.Data.Cameras
{
    /// <summary>
    /// Represents a camera mode of <see cref="Camera"/> instance.
    /// </summary>
    public enum CameraMode : byte
    {
        /// <summary>
        /// Indicates that it is undefined.
        /// </summary>
        Undefined,
        /// <summary>
        /// Indicates a camera in orthographic mode.
        /// </summary>
        Orthographic,
        /// <summary>
        /// Indicates a camera in perspective mode.
        /// </summary>
        Perspective
    }
}
