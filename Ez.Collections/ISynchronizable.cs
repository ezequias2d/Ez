// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System.Threading;

namespace Ez.Collections
{
    /// <summary>
    /// Interface that specifies that an object of the class can be synced using the object provided by Sync.
    /// </summary>
    public interface ISynchronizable
    {
        /// <summary>
        /// Object used for sync.
        /// </summary>
        ReaderWriterLockSlim Lock { get; }
    }
}
