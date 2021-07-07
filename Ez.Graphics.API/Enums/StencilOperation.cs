// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Stencil comparison function
    /// </summary>
    public enum StencilOperation
    {
        /// <summary>
        /// Keeps the current value.
        /// </summary>
        Keep,
        /// <summary>
        /// Sets the value to 0.
        /// </summary>
        Zero,
        /// <summary>
        /// The stencil value is replaced with the <see cref="DepthStencilState.StencilReference"/>.
        /// </summary>
        Replace,
        /// <summary>
        /// The stencil value is increased by 1 if it is lower than the maximum value.
        /// </summary>
        IncrementAndClamp,
        /// <summary>
        /// The stencil value is decreased by 1 if it is higher than the minimum value.
        /// </summary>
        DecrementAndClamp,
        /// <summary>
        /// Bitwise inverts the current stencil buffer value.
        /// </summary>
        Invert,
        /// <summary>
        /// The stencil value is increased by 1, if maximum value is exceeded wraps it back to 0.
        /// </summary>
        IncrementAndWrap,
        /// <summary>
        /// The stencil value is decreased by 1, if value is lower than 0 wraps it back to maximum value.
        /// </summary>
        DecrementAndWrap,
    }
}
