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
    /// Describes a dictionary that can auto-index a type that auto-indexes.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface ISelfIndexedDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, ICollection<TValue> where TValue : ISelfIndexedElement<TKey>
    {
    }
}
