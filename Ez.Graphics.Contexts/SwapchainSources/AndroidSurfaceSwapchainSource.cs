// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace Ez.Graphics.Context.SwapchainSources
{
    /// <summary>
    /// A swapchain source for a Android Surface.
    /// </summary>
    public struct AndroidSurfaceSwapchainSource : ISwapchainSource
    {
        /// <summary>
        /// The handlle of the android surface.
        /// </summary>
        public readonly IntPtr Surface;

        /// <summary>
        /// The java native interface environment handle.
        /// </summary>
        public readonly IntPtr JniEnv;

        /// <summary>
        /// Creates a new instance of <see cref="AndroidSurfaceSwapchainSource"/> struct.
        /// </summary>
        /// <param name="surfaceHandle">The handle of the android surface.</param>
        /// <param name="jniEnv">The java native interface environment handle.</param>
        public AndroidSurfaceSwapchainSource(IntPtr surfaceHandle, IntPtr jniEnv)
        {
            Surface = surfaceHandle;
            JniEnv = jniEnv;
        }
    }
}
