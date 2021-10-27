using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ez.Numerics
{
    /// <summary>
    /// Stores an ordered dyad of unsigned integers, which specify a
    /// <see cref="Width"/> and <see cref="Height"/>.
    /// </summary>
    public struct Size2 : IEquatable<Size2>, IFormattable
    {
        /// <summary>
        /// Creates a new <see cref="Size2"/> instance whose elements have the specified values.
        /// </summary>
        /// <param name="width">The value to assign to the <see cref="Width"/> field.</param>
        /// <param name="height">The value to assign to the <see cref="Height"/> field.</param>
        public Size2(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Creates a new <see cref="Size2"/> instance whose three elements have the same value.
        /// </summary>
        /// <param name="value">The value to assign to all two elements.</param>
        public Size2(uint value) : this(value, value)
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

        /// <inheritdoc/>
        public bool Equals(Size2 other) =>
            Width == other.Width && Height == other.Height;

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is Size2 p && Equals(p);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<Size2>.Combine(Width, Height);

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
            sb.Append('>');
            return sb.ToString();
        }

        #region static
        #region properties

        /// <summary>
        /// Gets a size whose 2 elements are equal to one.
        /// </summary>
        public static Size2 One { get; } = new Size2(1);

        /// <summary>
        /// Gets a size whose 2 elements are equal to zero.
        /// </summary>
        public static Size2 Zero { get; } = new Size2(0);

        /// <summary>
        /// Gets the size (1, 0).
        /// </summary>
        public static Size2 UnitW { get; } = new Size2(1, 0);

        /// <summary>
        /// Gets the size (0, 1).
        /// </summary>
        public static Size2 UnitH { get; } = new Size2(0, 1);
        #endregion
        #region methods

        /// <summary>
        /// Adds two sizes together.
        /// </summary>
        /// <param name="left">The first size to add.</param>
        /// <param name="right">The second size to add.</param>
        /// <returns>The summed size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2 Add(Size2 left, Size2 right) =>
            new(left.Width + right.Width, left.Height + right.Height);

        /// <summary>
        /// Restricts a size between a minimum and maximum value.
        /// </summary>
        /// <param name="value">The size to restrict.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The restricted vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2 Clamp(Size2 value, Size2 min, Size2 max)
        {
            var x = Clamp(value.Width, min.Width, max.Width);
            var y = Clamp(value.Height, min.Height, max.Height);
            return new Size2(x, y);
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
        public static Size2 Divide(in Size2 left, uint divisor)
        {
            var x = left.Width / divisor;
            var y = left.Height / divisor;
            return new(x, y);
        }

        /// <summary>
        /// Divides the first size by the second.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The size resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2 Divide(in Size2 left, in Size2 right)
        {
            var x = left.Width / right.Width;
            var y = left.Height / right.Height;
            return new(x, y);
        }

        /// <summary>
        /// Returns a size whose elements are the maximum of each of the pairs of
        /// elements in two specified sizes.
        /// </summary>
        /// <param name="value1">The first size.</param>
        /// <param name="value2">The second size.</param>
        /// <returns>The maximized size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2 Max(in Size2 value1, in Size2 value2)
        {
            var x = Max(value1.Width, value2.Width);
            var y = Max(value1.Height, value2.Height);
            return new(x, y);
        }

        /// <summary>
        /// Returns a size whose elements are the manimum of each of the pairs of
        /// elements in two specified sizes.
        /// </summary>
        /// <param name="value1">The first size.</param>
        /// <param name="value2">The second size.</param>
        /// <returns>The maximized size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2 Min(in Size2 value1, in Size2 value2)
        {
            var x = Min(value1.Width, value2.Width);
            var y = Min(value1.Height, value2.Height);
            return new(x, y);
        }

        /// <summary>
        /// Multiplies a scalar value by a specified size.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The size to multiply.</param>
        /// <returns>The scaled size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2 Multiply(uint left, Size2 right)
        {
            var x = left * right.Width;
            var y = left * right.Height;
            return new(x, y);
        }

        /// <summary>
        /// Multiplies a size by a specified scalar.
        /// </summary>
        /// <param name="left">The size to multiply.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2 Multiply(Size2 left, uint right)
        {
            var x = left.Width * right;
            var y = left.Height * right;
            return new(x, y);
        }

        /// <summary>
        /// Multiplies the first size by the second. 
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The size resulting from the multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2 Multiply(Size2 left, Size2 right)
        {
            var x = left.Width * right.Width;
            var y = left.Height * right.Height;
            return new(x, y);
        }

        /// <summary>
        /// Subtracts the second size from the first.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The difference size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2 Subtract(in Size2 left, in Size2 right) =>
            new(left.Width - right.Width, left.Height - right.Height);

        #endregion
        #region operators
        /// <summary>
        /// Adds two pointers together.
        /// </summary>
        /// <param name="left">The first size to add.</param>
        /// <param name="right">The second size to add.</param>
        /// <returns>The summed size.</returns>
        public static Size2 operator +(in Size2 left, in Size2 right) =>
            Add(left, right);

        /// <summary>
        /// Divides the first size by the second.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The size that results from dividing <paramref name="left"/> 
        /// by <paramref name="right"/>.</returns>
        public static Size2 operator /(in Size2 left, in Size2 right) =>
            Divide(left, right);

        /// <summary>
        /// Divides the specified size by a specified scalar value.
        /// </summary>
        /// <param name="left">The size.</param>
        /// <param name="divisor">The scalar value.</param>
        /// <returns>The size that results from the division.</returns>
        public static Size2 operator /(in Size2 left, uint divisor) =>
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
        public static bool operator ==(in Size2 left, in Size2 right) =>
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
        public static bool operator !=(in Size2 left, in Size2 right) =>
            !(left == right);


        /// <summary>
        /// Multiplies the first size by the second. 
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The size resulting from the multiplication.</returns>
        public static Size2 operator *(in Size2 left, in Size2 right) =>
            Multiply(left, right);

        /// <summary>
        /// Multiplies a size by a specified scalar.
        /// </summary>
        /// <param name="left">The size to multiply.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled size.</returns>
        public static Size2 operator *(in Size2 left, uint right) =>
            Multiply(left, right);

        /// <summary>
        /// Multiplies a scalar value by a specified size.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The size to multiply.</param>
        /// <returns>The scaled size.</returns>
        public static Size2 operator *(uint left, in Size2 right) =>
            Multiply(left, right);

        /// <summary>
        /// Subtracts the second size from the first.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The difference size.</returns>
        public static Size2 operator -(in Size2 left, in Size2 right) =>
            Subtract(left, right);

        /// <summary>
        /// Casts <see cref="Size2"/> to <see cref="Size3"/> with depth 0.
        /// </summary>
        /// <param name="size">The value to cast.</param>
        public static implicit operator Size3(Size2 size) => new(size.Width, size.Height, 0);

        /// <summary>
        /// Casts <see cref="Size2"/> to <see cref="Size"/>
        /// </summary>
        /// <param name="size">The value to cast.</param>
        public static implicit operator Size(Size2 size) => new((int)size.Width, (int)size.Height);
        #endregion
        #endregion
    }
}
