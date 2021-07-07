// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Represents the type of index data used in a <see cref="IBuffer"/>.
    /// </summary>
    public enum IndexType
    {
        /// <summary>
        /// Each index is a 16-bit unsigned integer (System.UInt16).
        /// </summary>
        UShort,
        /// <summary>
        /// Each index is a 32-bit unsigned integer (System.UInt32).
        /// </summary>
        UInt
    }
}
