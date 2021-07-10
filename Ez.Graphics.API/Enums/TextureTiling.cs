// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specifies the tiling arrangement of data in an texture.
    /// </summary>
    public enum TextureTiling
    {
        /// <summary>
        /// Specifies optimal tiling (texels are laid out in an implementation-dependent
        /// arrangement, for more efficient memory access).
        /// </summary>
        Optimal,

        /// <summary>
        /// Specifies linear tiling (texels are laid out in memory in row-major order, 
        /// possibly with some padding on each row).
        /// </summary>
        Linear,
    }
}
