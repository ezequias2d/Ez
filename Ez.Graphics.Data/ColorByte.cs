// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.Data.Animations;
using Ez.Numerics;
using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// A color struct in 8-bits integer values in RGBA format.
    /// </summary>
    public readonly struct ColorByte : IColor<byte>, IEquatable<ColorByte>
    {
        /// <summary>
        /// Constructs a new ColorSingle from the given components.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public ColorByte(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// The red component.
        /// </summary>
        public byte R { get; }

        /// <summary>
        /// The green component.
        /// </summary>
        public byte G { get; }

        /// <summary>
        /// The blue component.
        /// </summary>
        public byte B { get; }

        /// <summary>
        /// The alpha component.
        /// </summary>
        public byte A { get; }

        /// <summary>
        /// Gets the representation of a <see cref="ColorByte"/> instance as <see cref="Color"/>.
        /// </summary>
        /// <returns>A <see cref="Color"/> representation of this <see cref="ColorSingle"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color GetColor() =>
            Color.FromArgb(A, R, G, B);

        /// <summary>
        /// Gets the representation of a <see cref="ColorByte"/> instance as <see cref="ColorByte"/>.
        /// </summary>
        /// <returns>A <see cref="ColorByte"/> representation of this <see cref="ColorSingle"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ColorByte GetColorByte() => this;

        /// <summary>
        /// Gets the representation of a <see cref="ColorByte"/> instance as <see cref="ColorSingle"/>.
        /// </summary>
        /// <returns>A <see cref="ColorSingle"/> representation of this <see cref="ColorByte"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ColorSingle GetColorSingle() =>
            new ColorSingle(new Vector4(R, G, B, A) * (1f / 255f));

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() =>
            HashHelper<ColorByte>.Combine(R, G, B, A);

        /// <summary>
        /// Returns the string representation of the current <see cref="Vector3Key"/> instance.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString() =>
            $"R:{R}, G:{G}, B:{B}, A:{A}";

        /// <summary>
        /// Returns a value that indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="obj"/> are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if(obj is IColor color)
            {
                if (color is IColor<byte> colorByte)
                    return Equals(colorByte);
                else
                    return Equals(color);
            }
            return false;
        }

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="ColorByte"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="ColorByte"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="ColorByte"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(ColorByte other) => Equals((IColor<byte>)other);

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="IColor"/> have equivalent colors in the color space of the first..
        /// </summary>
        /// <param name="other">The other <see cref="IColor"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="ColorSingle"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(IColor other) =>
           Equals(other.GetColorByte());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool IEquatable<IColor<byte>>.Equals(IColor<byte> other) =>
            R == other.R && G == other.G && B == other.B && A == other.A;

        /// <summary>
        /// Element-wise equality.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(ColorByte left, ColorByte right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Element-wise inequality.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(ColorByte left, ColorByte right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Element-wise equality.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(ColorByte left, IColor right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Element-wise inequality.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(ColorByte left, IColor right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Red (255, 0, 0, 255)
        /// </summary>
        public static readonly ColorByte Red = new ColorByte(255, 0, 0, 255);
        /// <summary>
        /// Dark Red (139, 0, 0, 255)
        /// </summary>
        public static readonly ColorByte DarkRed = new ColorByte(139, 0, 0, 255);
        /// <summary>
        /// Green (0, 255, 0, 255)
        /// </summary>
        public static readonly ColorByte Green = new ColorByte(0, 255, 0, 255);
        /// <summary>
        /// Blue (0, 0, 255, 255)
        /// </summary>
        public static readonly ColorByte Blue = new ColorByte(0, 0, 255, 255);
        /// <summary>
        /// Yellow (255, 255, 0, 255)
        /// </summary>
        public static readonly ColorByte Yellow = new ColorByte(255, 255, 0, 255);
        /// <summary>
        /// Grey (128, 128, 128, 255)
        /// </summary>
        public static readonly ColorByte Grey = new ColorByte(128, 128, 128, 255);
        /// <summary>
        /// Light Grey (211, 211, 211, 255)
        /// </summary>
        public static readonly ColorByte LightGrey = new ColorByte(211, 211, 211, 255);
        /// <summary>
        /// Cyan (0, 255, 255, 255)
        /// </summary>
        public static readonly ColorByte Cyan = new ColorByte(0, 255, 255, 255);
        /// <summary>
        /// White (255, 255, 255, 255)
        /// </summary>
        public static readonly ColorByte White = new ColorByte(255, 255, 255, 255);
        /// <summary>
        /// Cornflower Blue (100, 149, 237, 255)
        /// </summary>
        public static readonly ColorByte CornflowerBlue = new ColorByte(100, 149, 237, 255);
        /// <summary>
        /// Clear (0, 0, 0, 0)
        /// </summary>
        public static readonly ColorByte Clear = new ColorByte(0, 0, 0, 0);
        /// <summary>
        /// Black (0, 0, 0, 255)
        /// </summary>
        public static readonly ColorByte Black = new ColorByte(0, 0, 0, 255);
        /// <summary>
        /// Pink (255, 192, 203, 255)
        /// </summary>
        public static readonly ColorByte Pink = new ColorByte(255, 192, 203, 255);
        /// <summary>
        /// Orange (255, 165, 0, 255)
        /// </summary>
        public static readonly ColorByte Orange = new ColorByte(255, 165, 0, 255);
    }
}
