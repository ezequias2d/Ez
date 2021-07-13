// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
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
        /// Resizes the textures in this instance.
        /// </summary>
        /// <param name="size">The new size of textures.</param>
        void Resize(Size size);

        /// <summary>
        /// Presents the swapchain to the screen.
        /// </summary>
        /// <param name="waitSemaphore">The semaphores to wait before issuing this request. (PS.: can be null if not use)</param>
        /// <param name="signalSemaphore">The semaphore to signal when the presentation ends. (PS.: can be null if not use)(</param>
        /// <param name="fence">The fence to signal when the presentation ends.(PS.: can be null if not use)</param>
        void Present(ISemaphore waitSemaphore, ISemaphore signalSemaphore, IFence fence);
    }
}