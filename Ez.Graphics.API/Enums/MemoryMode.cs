// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specifies the memory mode of a <see cref="IBuffer"/> or <see cref="ITexture"/>
    /// </summary>
    public enum MemoryMode
    {
        /// <summary>
        /// Specifies a buffer or texture memory backing 
        /// should reside in a device’s local memory only.
        /// </summary>
        GpuOnly = 0,
        /// <summary>
        /// Specifies a buffer or texture memory backing
        /// should reside in host memory.
        /// </summary>
        CpuOnly = 1,
        /// <summary>
        /// Specifies a buffer or texture memory backing 
        /// should be optimized for data transfers from
        /// the host to the device.
        /// </summary>
        CpuToGpu = 1 << 1,
        /// <summary>
        /// Specifies a buffer or texture memory backing 
        /// should be optimized for data transfers from
        /// the device to the host.
        /// </summary>
        GpuToCpu = 1 << 2,
    }
}
