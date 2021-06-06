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
    /// A color struct in 32-bits floating-point values in RGBA format.
    /// </summary>
    public readonly partial struct ColorSingle : IColor<float>, IEquatable<ColorSingle>
    {
        private readonly Vector4 _literal;

        /// <summary>
        /// Creates a new <see cref="ColorSingle"/> struct.
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
        /// Creates a new <see cref="ColorSingle"/> struct.
        /// </summary>
        /// <param name="channels">The vector containing the color components.</param>
        public ColorSingle(Vector4 channels)
        {
            _literal = channels;
        }

        /// <inheritdoc/>
        public float R => _literal.X;

        /// <inheritdoc/>
        public float G => _literal.Y;

        /// <inheritdoc/>
        public float B => _literal.Z;

        /// <inheritdoc/>
        public float A => _literal.W;

        /// <summary>
        /// Converts this ColorSingle into a Vector4.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4 ToVector4()
        {
            return _literal;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ColorSingle other) =>
            _literal.Equals(other._literal);

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is ColorSingle other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(IColor other) => Equals(other.GetColorSingle());

        bool IEquatable<IColor<float>>.Equals(IColor<float> other) => Equals(other.GetColorSingle());

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashHelper<ColorSingle>.Combine(R, G, B, A);
        }

        /// <inheritdoc/>
        public override string ToString() =>  string.Format("R:{0}, G:{1}, B:{2}, A:{3}", R, G, B, A);

        /// <inheritdoc/>
        public Color GetColor() => Color.FromArgb((byte)(A * 255), (byte)(R * 255), (byte)(G * 255), (byte)(B * 255));

        /// <inheritdoc/>
        public ColorSingle GetColorSingle() => this;

        /// <inheritdoc/>
        public ColorByte GetColorByte() => new ColorByte((byte)(R * 255), (byte)(G * 255), (byte)(B * 255), (byte)(A * 255));

        /// <inheritdoc/>
        public ColorInt GetColorInt() => new(ToInt(R), ToInt(G), ToInt(B), ToInt(A));

        /// <inheritdoc/>
        public ColorUInt GetColorUInt() => new(ToUInt(R), ToUInt(G), ToUInt(B), ToUInt(A));
        
        private static uint ToUInt(in float value) => (uint)(Math.Clamp(value, 0, 1) * uint.MaxValue);
        private static int ToInt(in float value) => (int)(Math.Clamp(value, -1, 1) * (double)uint.MaxValue + int.MinValue);

        /// <summary>
        /// Element-wise equality.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(ColorSingle left, ColorSingle right) =>
            left.Equals(right);

        /// <summary>
        /// Element-wise inequality.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(ColorSingle left, ColorSingle right) => 
            !left.Equals(right);

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
