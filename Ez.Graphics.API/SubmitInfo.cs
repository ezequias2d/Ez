// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Structure specifying a submit operation
    /// </summary>
    public ref struct SubmitInfo
    {
        /// <summary>
        /// Semaphores upon which to wait before the command buffers for 
        /// this batch begin execution.
        /// <br/>
        /// If semaphores to wait on are provided, they define a semaphore
        /// wait operation.
        /// </summary>
        public ReadOnlySpan<ISemaphore> WaitSemaphores { get; set; }

        /// <summary>
        /// The command buffers to execute in the batch.
        /// </summary>
        public ReadOnlySpan<ICommandBuffer> CommandBuffers { get; set; }

        /// <summary>
        /// Semaphores which will be signaled when the command buffers for 
        /// this batch have completed execution. If semaphores to be signaled 
        /// are provided, they define a semaphore signal operation.
        /// </summary>
        public ReadOnlySpan<ISemaphore> SignalSemaphores { get; set; }
    }
}
