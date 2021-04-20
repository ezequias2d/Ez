// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// Provides static methods to <see cref="ColorSingle"/> manipulation.
    /// </summary>
    public readonly partial struct ColorSingle
    {
        /// <summary>
        /// Linearly interpolates between two colors.
        /// </summary>
        /// <param name="a">The first color.</param>
        /// <param name="b">The second color.</param>
        /// <param name="t">Influence of the second color on the final result.</param>
        /// <returns><paramref name="a"/> * (1f - <paramref name="t"/>) + <paramref name="b"/> * <paramref name="t"/>.</returns>
        public static ColorSingle Lerp(in ColorSingle a, in ColorSingle b, float t) =>
            a + (b - a) * t;
    }
}
