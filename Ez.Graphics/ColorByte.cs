// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Ez.Graphics
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

        /// <inheritdoc/>
        public byte R { get; }

        /// <inheritdoc/>
        public byte G { get; }

        /// <inheritdoc/>
        public byte B { get; }

        /// <inheritdoc/>
        public byte A { get; }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color GetColor() =>
            Color.FromArgb(A, R, G, B);

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ColorByte GetColorByte() => this;

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ColorSingle GetColorSingle() =>
            new ColorSingle(new Vector4(R, G, B, A) * (1f / 255f));

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() =>
            HashHelper<ColorByte>.Combine(R, G, B, A);

        /// <inheritdoc/>
        public override string ToString() =>
            $"R:{R}, G:{G}, B:{B}, A:{A}";

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public bool Equals(ColorByte other) => Equals((IColor<byte>)other);

        /// <inheritdoc/>
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
