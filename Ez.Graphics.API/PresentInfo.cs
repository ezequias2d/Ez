// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Structure specifying a present operation
    /// </summary>
    public ref struct PresentInfo
    {
        /// <summary>
        /// The semaphores to wait before issuing the request.
        /// (PS.: can be empty)
        /// </summary>
        public ReadOnlySpan<ISemaphore> WaitSemaphores { get; set; }

        /// <summary>
        /// The swapchains being presented.
        /// </summary>
        public ReadOnlySpan<ISwapchain> Swapchains { get; set; }

        /// <summary>
        /// The semaphores to signal when each swapchain is presented.
        /// (PS.: can be empty or have elements with a null value)
        /// </summary>
        /// <value></value>
        public ReadOnlySpan<ISemaphore> SignalSemaphores { get; set; }
    }
}