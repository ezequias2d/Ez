// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// Represents a swapchain resource.
    /// </summary>
    public interface ISwapchain : IResource
    {
        /// <summary>
        /// Gets or sets the size of swapchain.
        /// (framebuffers and swapchain should not be in use)
        /// </summary>
        Size Size { get; set; }

        /// <summary>
        /// The framebuffer of the swapchain resource.
        /// </summary>
        IReadOnlyList<IFramebuffer> Framebuffers { get; }

        /// <summary>
        /// Gets the current framebuffer of the swapchain.
        /// </summary>
        int CurrentIndex { get; }

        /// <summary>
        /// Indicates whether presentation of the <see cref="ISwapchain"/> will be synchronized to 
        /// the window system's vertical refresh rate.
        /// </summary>
        bool VSync { get; set; }

        /// <summary>
        /// Presents the swapchain to the screen.
        /// </summary>
        /// <param name="waitSemaphore">The semaphore to wait before issuing this request. (PS.: can be null if not use)</param>
        /// <param name="signalSemaphore">The semaphore to signal when the presentation ends. (PS.: can be null if not use)(</param>
        /// <param name="fence">The fence to signal when the presentation ends. (PS.: can be null if not use)</param>
        void Present(ISemaphore waitSemaphore, ISemaphore signalSemaphore, IFence fence);

        /// <summary>
        /// Presents the swapchain to the screen.
        /// <param name="waitSemaphores">The semaphores to wait before issuing this request. (PS.: can be null if not use)</param>
        /// <param name="signalSemaphore">The semaphore to signal when the presentation ends. (PS.: can be null if not use)(</param>
        /// <param name="fence">The fence to signal when the presentation ends. (PS.: can be null if not use)</param>
        void Present(ReadOnlySpan<ISemaphore> waitSemaphores, ISemaphore signalSemaphore, IFence fence);
    }
}