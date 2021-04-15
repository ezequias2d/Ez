// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections
{
    /// <summary>
    /// A List that implements synchronization using ISynchronizable interface.
    /// </summary>
    /// <typeparam name="T">Type of elements using in <see cref="IList{T}"/> interface.</typeparam>
    public interface ISynchronizedList<T> : IList<T>, ISynchronizable
    {
    }
}
