// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ez.Graphics
{
    /// <summary>
    /// Provides methods that can retrieve representations in <see cref="Color"/>, <see cref="ColorSingle"/> and <see cref="ColorByte"/>.
    /// </summary>
    public interface IColor : IEquatable<IColor>
    {
        /// <summary>
        /// Gets the representation of a <see cref="IColor"/> instance as <see cref="Color"/>.
        /// </summary>
        /// <returns>A <see cref="Color"/> representation of this <see cref="ColorSingle"/>.</returns>
        Color GetColor();

        /// <summary>
        /// Gets the representation of a <see cref="IColor"/> instance as <see cref="ColorSingle"/>.
        /// </summary>
        /// <returns>A <see cref="ColorSingle"/> representation of this <see cref="IColor"/>.</returns>
        ColorSingle GetColorSingle();

        /// <summary>
        /// Gets the representation of a <see cref="IColor"/> instance as <see cref="ColorByte"/>.
        /// </summary>
        /// <returns>A <see cref="ColorByte"/> representation of this <see cref="IColor"/>.</returns>
        ColorByte GetColorByte();
    }

    /// <summary>
    /// A color struct in T vluas in RGBA format.
    /// </summary>
    /// <typeparam name="T">The type of a color component.</typeparam>
    public interface IColor<T> : IColor, IEquatable<IColor<T>> where T : unmanaged
    {
        /// <summary>
        /// The red component.
        /// </summary>
        T R { get; }
        /// <summary>
        /// The green component.
        /// </summary>
        T G { get; }
        /// <summary>
        /// The blue component.
        /// </summary>
        T B { get; }
        /// <summary>
        /// The alpha component.
        /// </summary>
        T A { get; }
    }
}
