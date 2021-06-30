// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace Ez.Graphics.Context.SwapchainSources
{
    /// <summary>
    /// A swapchain source for a Xlib display and window.
    /// </summary>
    public struct XlibSwapchainSource : ISwapchainSource
    {
        /// <summary>
        /// The Xlib display handle.
        /// </summary>
        public readonly IntPtr Display;
        /// <summary>
        /// The Xlib window handle.
        /// </summary>
        public readonly IntPtr Window;

        /// <summary>
        /// Creates a new instance of <see cref="XlibSwapchainSource"/> struct.
        /// </summary>
        /// <param name="display">The Xlib display handle.</param>
        /// <param name="window">The Xlib window handle.</param>
        public XlibSwapchainSource(IntPtr display, IntPtr window)
        {
            Display = display;
            Window = window;
        }
    }
}
