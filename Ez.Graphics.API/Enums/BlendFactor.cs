// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Represents the framebuffer blending factors.
    /// </summary>
    public enum BlendFactor
    {
        /// <summary>
        /// Factor is equal to 0.
        /// </summary>
        Zero,
        /// <summary>
        /// Factor is equal to 1.
        /// </summary>
        One,
        /// <summary>
        /// Factor is equal to the source color.
        /// </summary>
        SourceColor,
        /// <summary>
        /// Factor is equal to 1 minus the source color.
        /// </summary>
        OneMinusSourceColor,
        /// <summary>
        /// Factor is equal to destination color.
        /// </summary>
        DestinationColor,
        /// <summary>
        /// Factor is equal to 1 minus the destination color.
        /// </summary>
        OneMinusDestinationColor,
        /// <summary>
        /// Factor is equal to alpha component of the source color.
        /// </summary>
        SourceAlpha,
        /// <summary>
        /// Factor is equal to 1 - alpha component of the source color.
        /// </summary>
        OneMinusSourceAlpha,
        /// <summary>
        /// Factor is equal to alpha component of the destination color.
        /// </summary>
        DestinationAlpha,
        /// <summary>
        /// Factor is equal to 1 - alpha component of dest the destination color.
        /// </summary>
        OneMinusDestinationAlpha,
        /// <summary>
        /// Factor is equal to the constant factor color.
        /// </summary>
        ConstantColor,
        /// <summary>
        /// Factor is equal to 1 - the constant factor color.
        /// </summary>
        OneMinusConstantColor,
        /// <summary>
        /// Factor is equal to the alpha component of the constant factor color.
        /// </summary>
        ConstantAlpha,
        /// <summary>
        /// Factor is equal to 1 - alpha component of the constant factor color.
        /// </summary>
        OneMinusConstantAlpha
    }
}
