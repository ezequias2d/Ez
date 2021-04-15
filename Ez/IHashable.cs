// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez
{
    /// <summary>
    /// An interface that can hash the contents of the structure or class instance. that implements it .
    /// </summary>
    public interface IHashable
    {
        /// <summary>
        /// A hashcode for the content of a instance.
        /// </summary>
        int Hashcode { get; }
    }
}
