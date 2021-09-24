// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Runtime.InteropServices;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Structure specifying a clear color value.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ClearColorValue : IEquatable<ClearColorValue>
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

        /// <inheritdoc/>
        public bool Equals(ClearColorValue other) =>
            SingleValue.Equals(other.SingleValue) &&
            IntValue.Equals(other.IntValue) &&
            UIntValue.Equals(other.UIntValue);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelper<ClearColorValue>.Combine(SingleValue, IntValue, UIntValue);

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is ClearColorValue ccv && Equals(ccv);

        /// <summary>
        /// Compare two <see cref="ClearColorValue"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(ClearColorValue left, ClearColorValue right) => left.Equals(right);

        /// <summary>
        /// Compare two <see cref="ClearColorValue"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(ClearColorValue left, ClearColorValue right) => !(left == right);
    }
}
