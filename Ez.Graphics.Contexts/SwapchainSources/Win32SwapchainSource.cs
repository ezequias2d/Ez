// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace Ez.Graphics.Context.SwapchainSources
{
    /// <summary>
    /// A swapchain source for a Win32 window.
    /// </summary>
    public struct Win32SwapchainSource : ISwapchainSource
    {
        /// <summary>
        /// The Win32 window handle.
        /// </summary>
        public readonly IntPtr Hwnd;

        /// <summary>
        /// The Win32 instance handle.
        /// </summary>
        public readonly IntPtr Hinstance;

        /// <summary>
        /// Creates a new instance of <see cref="Win32SwapchainSource"/> struct.
        /// </summary>
        /// <param name="hwnd">The win32 window handle.</param>
        /// <param name="hinstance"> The win32 instance handle.</param>
        public Win32SwapchainSource(IntPtr hwnd, IntPtr hinstance)
        {
            Hwnd = hwnd;
            Hinstance = hinstance;
        }
    }
}

