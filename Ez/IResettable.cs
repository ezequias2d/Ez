// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
namespace Ez
{
    /// <summary>
    /// Describes an object that can be reset.
    /// </summary>
    public interface IResettable
    {
        /// <summary>
        /// Resets the object to a state that can be reused or destroyed.
        /// </summary>
        void Reset();

        /// <summary>
        /// Set an object just before being used.
        /// </summary>
        void Set();
    }
}
