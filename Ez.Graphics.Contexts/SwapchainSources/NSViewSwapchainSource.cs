// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace Ez.Graphics.Context.SwapchainSources
{
    /// <summary>
    /// A swapchain source for a NSView.
    /// </summary>
    public struct NSViewSwapchainSource : ISwapchainSource
    {
        /// <summary>
        /// A pointer to a NSView.
        /// </summary>
        public readonly IntPtr NSView;

        /// <summary>
        /// Creates a new instance of <see cref="NSViewSwapchainSource"/> struct.
        /// </summary>
        /// <param name="nsView">The pointer to a NSView.</param>
        public NSViewSwapchainSource(IntPtr nsView)
        {
            NSView = nsView;
        }
    }
}
