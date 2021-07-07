// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// Represents a synchronization resource that allows the device to notify
    /// when a command buffer completes execution.
    /// </summary>
    public interface IFence : IResource
    {
        /// <summary>
        /// Gets a value indicating whether the <see cref="IFence">fence</see> is currently
        /// signaled. A <see cref="IFence">fence</see> is signaled after a command buffer 
        /// completes execution and has been submitted with a <see cref="IFence">fence</see> 
        /// instance.
        /// </summary>
        bool Signaled { get; }

        /// <summary>
        /// Resets the <see cref="IFence"/> to the unsignaled state.
        /// </summary>
        void Reset();

        /// <summary>
        /// Wait for fence.
        /// </summary>
        /// <param name="timeout">Timeout in nanoseconds.</param>
        void Wait(in ulong timeout);
    }
}
