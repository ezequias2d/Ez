// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace Ez.Graphics.Context.SwapchainSources
{
    /// <summary>
    /// A swapchain source for a Wayland surface and display.
    /// </summary>
    public struct WaylandSwapchainSource : ISwapchainSource
    {
        /// <summary>
        /// The Wayland display proxy.
        /// </summary>
        public readonly IntPtr Display;

        /// <summary>
        /// The Wayland surface proxy to map.
        /// </summary>
        public readonly IntPtr Surface;

        /// <summary>
        /// Creates a new instance of <see cref="WaylandSwapchainSource"/> struct.
        /// </summary>
        /// <param name="display">The Wayland display proxy.</param>
        /// <param name="surface">The Wayland surface proxy to map.</param>
        public WaylandSwapchainSource(IntPtr display, IntPtr surface)
        {
            Display = display;
            Surface = surface;
        }
    }
}
