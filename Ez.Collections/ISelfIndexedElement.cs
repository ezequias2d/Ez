// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections
{
    /// <summary>
    /// An element that can be auto indexing.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISelfIndexedElement<T>
    {
        /// <summary>
        /// The index for a <see cref="ISelfIndexedElement{T}"/>.
        /// </summary>
        T Key { get; }

        /// <summary>
        /// Action(T oldKey)
        /// </summary>
        event Action<T> KeyPropertyChange;
    }
}
