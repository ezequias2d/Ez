// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// The type of an action performed on the scene .
    /// </summary>
    public enum SceneAction
    {
        /// <summary>
        /// Indicates that the affected object has been added.
        /// </summary>
        Add,
        /// <summary>
        /// Indicates that the affected object has been removed.
        /// </summary>
        Remove,
        /// <summary>
        /// Indicates that the affected object has been modified.
        /// </summary>
        Update,
        /// <summary>
        /// Indicates that the affected object has been moved from index.
        /// </summary>
        Move,
    }
}
