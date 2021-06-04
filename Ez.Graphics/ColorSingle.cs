// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ez.Graphics
{
    /// <summary>
    /// A color struct in 32-bits floating-point values in RGBA format.
    /// </summary>
    public readonly partial struct ColorSingle : IColor<float>, IEquatable<ColorSingle>
    {
        private readonly Vector4 _literal;

        /// <summary>
        /// The red component.
        /// </summary>
        public float R => _literal.X;
        /// <summary>
        /// The green component.
        /// </summary>
        public float G => _literal.Y;
        /// <summary>
        /// The blue component.
        /// </summary>
        public float B => _literal.Z;
        /// <summary>
        /// The alpha component.
        /// </summary>
        public float A => _literal.W;

        /// <summary>
        /// Constructs a new ColorSingle from the given components.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public ColorSingle(float r, float g, float b, float a)
        {
            _literal = new Vector4(r, g, b, a);
        }

        /// <summary>
        /// Constructs a new ColorSingle from the XYZW components of a vector.
        /// </summary>
        /// <param name="channels">The vector containing the color components.</param>
        public ColorSingle(Vector4 channels)
        {
            _literal = channels;
        }

        /// <summary>
        /// Converts this ColorSingle into a Vector4.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4 ToVector4()
        {
            return _literal;
        }

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="ColorSingle"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="ColorSingle"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="ColorSingle"/> are equals; otherwise, <see langword="false"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ColorSingle other) =>
            _literal.Equals(other._literal);

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj) =>
            obj is ColorSingle other && Equals(other);

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="IColor"/> have equivalent colors in the color space of the first..
        /// </summary>
        /// <param name="other">The other <see cref="IColor"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="ColorSingle"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(IColor other) => Equals(other.GetColorSingle());

        bool IEquatable<IColor<float>>.Equals(IColor<float> other) => Equals(other.GetColorSingle());

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashHelper<ColorSingle>.Combine(R, G, B, A);
        }

        /// <summary>
        /// Returns a string representation of this color.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("R:{0}, G:{1}, B:{2}, A:{3}", R, G, B, A);
        }

        /// <summary>
        /// Gets the representation of a <see cref="ColorSingle"/> instance as <see cref="Color"/>.
        /// </summary>
        /// <returns>A <see cref="Color"/> representation of this <see cref="ColorSingle"/>.</returns>
        public Color GetColor() => Color.FromArgb((byte)(A * 255), (byte)(R * 255), (byte)(G * 255), (byte)(B * 255));

        /// <summary>
        /// Gets the representation of a <see cref="ColorSingle"/> instance as <see cref="ColorSingle"/>.
        /// </summary>
        /// <returns>A <see cref="ColorSingle"/> representation of this <see cref="ColorSingle"/>.</returns>
        public ColorSingle GetColorSingle() => this;

        /// <summary>
        /// Gets the representation of a <see cref="ColorSingle"/> instance as <see cref="ColorByte"/>.
        /// </summary>
        /// <returns>A <see cref="ColorByte"/> representation of this <see cref="ColorSingle"/>.</returns>
        public ColorByte GetColorByte() => new ColorByte((byte)(R * 255), (byte)(G * 255), (byte)(B * 255), (byte)(A * 255));

        /// <summary>
        /// Element-wise equality.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(ColorSingle left, ColorSingle right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Element-wise inequality.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(ColorSingle left, ColorSingle right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Negates the specified color.
        /// </summary>
        /// <param name="color">The color to negate.</param>
        /// <returns>The negated vector.</returns>
        public static ColorSingle operator -(ColorSingle color) =>
            new ColorSingle(-color._literal);

        /// <summary>
        /// Adds two colors together.
        /// </summary>
        /// <param name="left">The first color.</param>
        /// <param name="right">The second color.</param>
        /// <returns>The summed color.</returns>
        public static ColorSingle operator +(ColorSingle left, ColorSingle right) =>
            new ColorSingle(left._literal + right._literal);

        /// <summary>
        /// Subtracts the second color from the first.
        /// </summary>
        /// <param name="left">The first color.</param>
        /// <param name="right">The second color.</param>
        /// <returns>The color that results from subracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        public static ColorSingle operator -(ColorSingle left, ColorSingle right) =>
            new ColorSingle(left._literal - right._literal);

        /// <summary>
        /// Multiplies two colors together.
        /// </summary>
        /// <param name="left">The first color.</param>
        /// <param name="right">The second color.</param>
        /// <returns>The product color.</returns>
        public static ColorSingle operator *(ColorSingle left, ColorSingle right) =>
            new ColorSingle(left._literal * right._literal);

        /// <summary>
        /// Divides the first color by the second.
        /// </summary>
        /// <param name="left">The first color.</param>
        /// <param name="right">The second color.</param>
        /// <returns>The color that results from dividing <paramref name="left"/> by <paramref name="right"/>.</returns>
        public static ColorSingle operator /(ColorSingle left, ColorSingle right) =>
            new ColorSingle(left._literal / right._literal);

        /// <summary>
        /// Multiples the specified color by the specified scalar value.
        /// </summary>
        /// <param name="left">The color.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled color.</returns>
        public static ColorSingle operator *(ColorSingle left, float right) =>
            new ColorSingle(left._literal * right);

        /// <summary>
        /// Divides the specified color by a specified scalar value.
        /// </summary>
        /// <param name="left">The color.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The result of the division.</returns>
        public static ColorSingle operator /(ColorSingle left, float right) =>
            new ColorSingle(left._literal / right);

        /// <summary>
        /// Multiples the specified color by the specified scalar value.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The color.</param>
        /// <returns>The scaled color.</returns>
        public static ColorSingle operator *(float left, ColorSingle right) =>
            new ColorSingle(right._literal * left);


        /// <summary>
        /// Red (1, 0, 0, 1)
        /// </summary>
        public static readonly ColorSingle Red = new ColorSingle(1f, 0f, 0f, 1f);
        /// <summary>
        /// Dark Red (139f / 255f, 0f, 0f, 1f)
        /// </summary>
        public static readonly ColorSingle DarkRed = new ColorSingle(139f / 255f, 0f, 0f, 1f);
        /// <summary>
        /// Green (0f, 1f, 0f, 1f)
        /// </summary>
        public static readonly ColorSingle Green = new ColorSingle(0f, 1f, 0f, 1f);
        /// <summary>
        /// Blue (0f, 0f, 1f, 1f)
        /// </summary>
        public static readonly ColorSingle Blue = new ColorSingle(0f, 0f, 1f, 1f);
        /// <summary>
        /// Yellow (1f, 1f, 0f, 1f)
        /// </summary>
        public static readonly ColorSingle Yellow = new ColorSingle(1f, 1f, 0f, 1f);
        /// <summary>
        /// Grey (128f / 255f, 128f / 255f, 128 / 255f, 1f)
        /// </summary>
        public static readonly ColorSingle Grey = new ColorSingle(128f / 255f, 128f / 255f, 128f / 255f, 1f);
        /// <summary>
        /// Light Grey (211f / 255f, 211f / 255f, 211f / 255f, 1f)
        /// </summary>
        public static readonly ColorSingle LightGrey = new ColorSingle(211f / 255f, 211f / 255f, 211f / 255f, 1f);
        /// <summary>
        /// Cyan (0f, 1f, 1f, 1f)
        /// </summary>
        public static readonly ColorSingle Cyan = new ColorSingle(0f, 1f, 1f, 1f);
        /// <summary>
        /// White (1f, 1f, 1f, 1f)
        /// </summary>
        public static readonly ColorSingle White = new ColorSingle(1f, 1f, 1f, 1f);
        /// <summary>
        /// Cornflower Blue (100f / 255f, 149f / 255f, 237f / 255f, 1f)
        /// </summary>
        public static readonly ColorSingle CornflowerBlue = new ColorSingle(100f / 255f, 149f / 255f, 237f / 255f, 1f);
        /// <summary>
        /// Clear (0f, 0f, 0f, 0f)
        /// </summary>
        public static readonly ColorSingle Clear = new ColorSingle(0f, 0f, 0f, 0f);
        /// <summary>
        /// Black (0f, 0f, 0f, 1f)
        /// </summary>
        public static readonly ColorSingle Black = new ColorSingle(0f, 0f, 0f, 1f);
        /// <summary>
        /// Pink (1f, 192f / 255f, 203f / 255f, 1f)
        /// </summary>
        public static readonly ColorSingle Pink = new ColorSingle(1f, 192f / 255f, 203f / 255f, 1f);
        /// <summary>
        /// Orange (1f, 165f / 255f, 0f, 1f)
        /// </summary>
        public static readonly ColorSingle Orange = new ColorSingle(1f, 165f / 255f, 0f, 1f);
    }
}
