// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System.Runtime.InteropServices;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Structure specifying a clear color value.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ClearColorValue
    {
        /// <summary>
        /// The color clear values when the <see cref="PixelFormat"/> are UNorm,
        /// SNorm, UFloat, SFloat or Srgb.
        /// </summary>
        [FieldOffset(0)]
        public ColorSingle SingleValue;

        /// <summary>
        /// The color clear values when the <see cref="PixelFormat"/> are SInt.
        /// </summary>
        [FieldOffset(0)]
        public ColorInt IntValue;

        /// <summary>
        /// The color clear values when the <see cref="PixelFormat"/> are UInt.
        /// </summary>
        [FieldOffset(0)]
        public ColorUInt UIntValue;
    }
}
