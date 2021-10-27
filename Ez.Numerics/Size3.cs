using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ez.Numerics
{
    /// <summary>
    /// Stores an ordered triad of unsigned integers, which specify a
    /// <see cref="Width"/>, <see cref="Height"/> and <see cref="Depth"/>.
    /// </summary>
    public struct Size3 : IEquatable<Size3>, IFormattable
    {
        /// <summary>
        /// Creates a new <see cref="Size3"/> instance whose elements have the specified values.
        /// </summary>
        /// <param name="width">The value to assign to the <see cref="Width"/> field.</param>
        /// <param name="height">The value to assign to the <see cref="Height"/> field.</param>
        /// <param name="depth">The value to assign to the <see cref="Depth"/> field.</param>
        public Size3(uint width, uint height, uint depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
        }

        /// <summary>
        /// Creates a new <see cref="Size3"/> instance whose three elements have the same value.
        /// </summary>
        /// <param name="value">The value to assign to all three elements.</param>
        public Size3(uint value) : this(value, value, value)
        {

        }

        /// <summary>
        /// The Width component of the size.
        /// </summary>
        public uint Width;

        /// <summary>
        /// The Height component of the size.
        /// </summary>
        public uint Height;

        /// <summary>
        /// The Depth component of the size.
        /// </summary>
        public uint Depth;

        /// <inheritdoc/>
        public bool Equals(Size3 other) =>
            Width == other.Width && Height == other.Height && Depth == other.Depth;

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is Size3 p && Equals(p);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<Size3>.Combine(Width, Height, Depth);

        /// <inheritdoc/>
        public override string ToString() => ToString("G", CultureInfo.CurrentCulture);

        /// <inheritdoc/>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            StringBuilder sb = new();
            string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
            sb.Append('<');
            sb.Append(Width.ToString(format, formatProvider));
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(Height.ToString(format, formatProvider));
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(Depth.ToString(format, formatProvider));
            sb.Append('>');
            return sb.ToString();
        }

        #region static
        #region properties

        /// <summary>
        /// Gets a size whose 3 elements are equal to one.
        /// </summary>
        public static Size3 One { get; } = new Size3(1);

        /// <summary>
        /// Gets a size whose 3 elements are equal to zero.
        /// </summary>
        public static Size3 Zero { get; } = new Size3(0);

        /// <summary>
        /// Gets the size (1, 0, 0).
        /// </summary>
        public static Size3 UnitW { get; } = new Size3(1, 0, 0);

        /// <summary>
        /// Gets the size (0, 1, 0).
        /// </summary>
        public static Size3 UnitH { get; } = new Size3(0, 1, 0);

        /// <summary>
        /// Gets the size (0, 0, 1).
        /// </summary>
        public static Size3 UnitD { get; } = new Size3(0, 0, 1);
        #endregion
        #region methods

        /// <summary>
        /// Adds two sizes together.
        /// </summary>
        /// <param name="left">The first size to add.</param>
        /// <param name="right">The second size to add.</param>
        /// <returns>The summed size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3 Add(Size3 left, Size3 right) =>
            new(left.Width + right.Width, left.Height + right.Height, left.Depth + right.Depth);

        /// <summary>
        /// Restricts a size between a minimum and maximum value.
        /// </summary>
        /// <param name="value">The size to restrict.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The restricted vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3 Clamp(Size3 value, Size3 min, Size3 max)
        {
            var x = Clamp(value.Width, min.Width, max.Width);
            var y = Clamp(value.Height, min.Height, max.Height);
            var z = Clamp(value.Depth, min.Depth, max.Depth);
            return new Size3(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Clamp(in uint value, in uint min, in uint max) =>
            Max(Min(value, max), min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Max(in uint value1, in uint value2) =>
            (value1 < value2) ? value2 : value1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Min(in uint value1, in uint value2) =>
            (value1 > value2) ? value2 : value1;

        /// <summary>
        /// Divides the specified size by a specified scalar value.
        /// </summary>
        /// <param name="left">The size.</param>
        /// <param name="divisor">The scalar value.</param>
        /// <returns>The size that results from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3 Divide(in Size3 left, uint divisor)
        {
            var x = left.Width / divisor;
            var y = left.Height / divisor;
            var z = left.Depth / divisor;
            return new(x, y, z);
        }

        /// <summary>
        /// Divides the first size by the second.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The size resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3 Divide(in Size3 left, in Size3 right)
        {
            var x = left.Width / right.Width;
            var y = left.Height / right.Height;
            var z = left.Depth / right.Depth;
            return new(x, y, z);
        }

        /// <summary>
        /// Returns a size whose elements are the maximum of each of the pairs of
        /// elements in two specified sizes.
        /// </summary>
        /// <param name="value1">The first size.</param>
        /// <param name="value2">The second size.</param>
        /// <returns>The maximized size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3 Max(in Size3 value1, in Size3 value2)
        {
            var x = Max(value1.Width, value2.Width);
            var y = Max(value1.Height, value2.Height);
            var z = Max(value1.Depth, value2.Depth);
            return new(x, y, z);
        }

        /// <summary>
        /// Returns a size whose elements are the manimum of each of the pairs of
        /// elements in two specified sizes.
        /// </summary>
        /// <param name="value1">The first size.</param>
        /// <param name="value2">The second size.</param>
        /// <returns>The maximized size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3 Min(in Size3 value1, in Size3 value2)
        {
            var x = Min(value1.Width, value2.Width);
            var y = Min(value1.Height, value2.Height);
            var z = Min(value1.Depth, value2.Depth);
            return new(x, y, z);
        }

        /// <summary>
        /// Multiplies a scalar value by a specified size.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The size to multiply.</param>
        /// <returns>The scaled size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3 Multiply(uint left, Size3 right)
        {
            var x = left * right.Width;
            var y = left * right.Height;
            var z = left * right.Depth;
            return new(x, y, z);
        }

        /// <summary>
        /// Multiplies a size by a specified scalar.
        /// </summary>
        /// <param name="left">The size to multiply.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3 Multiply(Size3 left, uint right)
        {
            var x = left.Width * right;
            var y = left.Height * right;
            var z = left.Depth * right;
            return new(x, y, z);
        }

        /// <summary>
        /// Multiplies the first size by the second. 
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The size resulting from the multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3 Multiply(Size3 left, Size3 right)
        {
            var x = left.Width * right.Width;
            var y = left.Height * right.Height;
            var z = left.Depth * right.Depth;
            return new(x, y, z);
        }

        /// <summary>
        /// Subtracts the second size from the first.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The difference size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3 Subtract(in Size3 left, in Size3 right) =>
            new(left.Width - right.Width, left.Height - right.Height, left.Depth - right.Depth);

        #endregion
        #region operators
        /// <summary>
        /// Adds two pointers together.
        /// </summary>
        /// <param name="left">The first size to add.</param>
        /// <param name="right">The second size to add.</param>
        /// <returns>The summed size.</returns>
        public static Size3 operator +(in Size3 left, in Size3 right) =>
            Add(left, right);

        /// <summary>
        /// Divides the first size by the second.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The size that results from dividing <paramref name="left"/> 
        /// by <paramref name="right"/>.</returns>
        public static Size3 operator /(in Size3 left, in Size3 right) =>
            Divide(left, right);

        /// <summary>
        /// Divides the specified size by a specified scalar value.
        /// </summary>
        /// <param name="left">The size.</param>
        /// <param name="divisor">The scalar value.</param>
        /// <returns>The size that results from the division.</returns>
        public static Size3 operator /(in Size3 left, uint divisor) =>
            Divide(left, divisor);

        /// <summary>
        /// Returns a value that indicates whether each pair of elements in two
        /// specified pointers is equal.
        /// </summary>
        /// <param name="left">The first size to compare.</param>
        /// <param name="right">The second size to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and 
        /// <paramref name="right"/> are qual; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(in Size3 left, in Size3 right) =>
            left.Equals(right);

        /// <summary>
        /// Returns a value that indicates whether two specified sizes are not
        /// equal.
        /// </summary>
        /// <param name="left">The first size to compare.</param>
        /// <param name="right">The second size to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and 
        /// <paramref name="right"/> are not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(in Size3 left, in Size3 right) =>
            !(left == right);


        /// <summary>
        /// Multiplies the first size by the second. 
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The size resulting from the multiplication.</returns>
        public static Size3 operator *(in Size3 left, in Size3 right) =>
            Multiply(left, right);

        /// <summary>
        /// Multiplies a size by a specified scalar.
        /// </summary>
        /// <param name="left">The size to multiply.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled size.</returns>
        public static Size3 operator *(in Size3 left, uint right) =>
            Multiply(left, right);

        /// <summary>
        /// Multiplies a scalar value by a specified size.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The size to multiply.</param>
        /// <returns>The scaled size.</returns>
        public static Size3 operator *(uint left, in Size3 right) =>
            Multiply(left, right);

        /// <summary>
        /// Subtracts the second size from the first.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The difference size.</returns>
        public static Size3 operator -(in Size3 left, in Size3 right) =>
            Subtract(left, right);

        #endregion

        /// <summary>
        /// Casts <see cref="Size3"/> to <see cref="Size2"/> losing depth value.
        /// </summary>
        /// <param name="size">The value to cast.</param>
        public static implicit operator Size2(Size3 size) => new(size.Width, size.Height);

        /// <summary>
        /// Casts <see cref="Size3"/> to <see cref="Size"/> losing depth value.
        /// </summary>
        /// <param name="size">The value to cast.</param>
        public static implicit operator Size(Size3 size) => new((int)size.Width, (int)size.Height);

        #endregion
    }
}
