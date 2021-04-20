// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Graphics.Data.Materials
{
    /// <summary>
    /// The type of a <see cref="MaterialProperty"/>.
    /// </summary>
    public enum MaterialPropertyType
    {
        /// <summary>
        /// Undefined type.
        /// </summary>
        Undefined,
        /// <summary>
        /// Texture Type.
        /// </summary>
        Texture,
        /// <summary>
        /// Color type.
        /// </summary>
        Color,
        /// <summary>
        /// Single type.
        /// </summary>
        Single,
        /// <summary>
        /// Double type.
        /// </summary>
        Double,
        /// <summary>
        /// Integer type.
        /// </summary>
        Integer
    }
}
