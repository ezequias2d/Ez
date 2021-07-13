// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// Represents a synchronization resource that can be used to insert 
    /// a dependency between operations.
    /// </summary>
    public interface ISemaphore : IResource
    {
    }
}
