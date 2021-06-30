// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace Ez.Graphics.Context.SwapchainSources
{
    /// <summary>
    /// A swapchain source for a UIView.
    /// </summary>
    public struct UIViewSwapchainSource
    {
        /// <summary>
        /// The pointer to an UIView.
        /// </summary>
        public readonly IntPtr UIView;

        /// <summary>
        /// Creates a new instance of <see cref="UIViewSwapchainSource"/> struct.
        /// </summary>
        /// <param name="uiView">The pointer to an UIView.</param>
        public UIViewSwapchainSource(IntPtr uiView)
        {
            UIView = uiView;
        }
    }
}
