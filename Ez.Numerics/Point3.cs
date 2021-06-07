using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ez.Numerics
{
    /// <summary>
    /// Represents a three-dimensional point.
    /// </summary>
    public struct Point3 : IEquatable<Point3>, IFormattable
    {
        /// <summary>
        /// Creates a new <see cref="Point3"/> instance whose elements have the specified values.
        /// </summary>
        /// <param name="x">The value to assign to the <see cref="X"/> field.</param>
        /// <param name="y">The value to assign to the <see cref="Y"/> field.</param>
        /// <param name="z">The value to assign to the <see cref="Z"/> field.</param>
        public Point3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Creates a new <see cref="Point3"/> instance whose three elements have the same value.
        /// </summary>
        /// <param name="value">The value to assign to all three elements.</param>
        public Point3(int value) : this(value, value, value)
        {
            
        }

        /// <summary>
        /// The X component of the point.
        /// </summary>
        public int X;

        /// <summary>
        /// The Y component of the point.
        /// </summary>
        public int Y;

        /// <summary>
        /// The Z component of the point.
        /// </summary>
        public int Z;

        /// <inheritdoc/>
        public bool Equals(Point3 other) =>
            X == other.X && Y == other.Y && Z == other.Z;

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is Point3 p && Equals(p);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<Point3>.Combine(X, Y, Z);

        /// <inheritdoc/>
        public override string ToString() => ToString("G", CultureInfo.CurrentCulture);

        /// <inheritdoc/>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            StringBuilder sb = new StringBuilder();
            string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
            sb.Append('<');
            sb.Append(X.ToString(format, formatProvider));
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(Y.ToString(format, formatProvider));
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(Z.ToString(format, formatProvider));
            sb.Append('>');
            return sb.ToString();
        }

        #region static
        #region properties
        /// <summary>
        /// Gets a point whose 3 elements are equal to one.
        /// </summary>
        public static Point3 One => new(1);

        /// <summary>
        /// Gets a point whose 3 elements are equal to zero.
        /// </summary>
        public static Point3 Zero => new(0);

        /// <summary>
        /// Gets the point (1, 0, 0).
        /// </summary>
        public static Point3 UnitX => new(1, 0, 0);

        /// <summary>
        /// Gets the point (0, 1, 0).
        /// </summary>
        public static Point3 UnitY => new(0, 1, 0);

        /// <summary>
        /// Gets the point (0, 0, 1).
        /// </summary>
        public static Point3 UnitZ => new(0, 0, 1);
        #endregion
        #region methods
        /// <summary>
        /// Returns a point whose elements are absolute values of each of the specified
        /// point's elements.
        /// </summary>
        /// <param name="value">A point.</param>
        /// <returns>The absolute value point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3 Abs(in Point3 value) =>
            new(Math.Abs(value.X), Math.Abs(value.Y), Math.Abs(value.Z));

        /// <summary>
        /// Adds two points together.
        /// </summary>
        /// <param name="left">The first point to add.</param>
        /// <param name="right">The second point to add.</param>
        /// <returns>The summed point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3 Add(Point3 left, Point3 right) =>
            new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        /// <summary>
        /// Restricts a point between a minimum and maximum value.
        /// </summary>
        /// <param name="value">The point to restrict.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The restricted vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3 Clamp(Point3 value, Point3 min, Point3 max)
        {
            var x = Clamp(value.X, min.X, max.X);
            var y = Clamp(value.Y, min.Y, max.Y);
            var z = Clamp(value.Z, min.Z, max.Z);
            return new Point3(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Clamp(in int value, in int min, in int max) =>
            Max(Min(value, max), min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Max(in int value1, in int value2) =>
            (value1 < value2) ? value2 : value1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Min(in int value1, in int value2) =>
            (value1 > value2) ? value2 : value1;

        /// <summary>
        /// Computes the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(in Point3 value1, in Point3 value2) =>
            Math.Sqrt(DistanceSquared(value1, value2));

        /// <summary>
        /// Computes the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceF(in Point3 value1, in Point3 value2) =>
            MathF.Sqrt(DistanceSquaredF(in value1, in value2));

        /// <summary>
        /// Returns the Euclidean distance squared between two specified points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance squared.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceSquared(in Point3 value1, in Point3 value2)
        {
            double dx = value1.X - value2.X;
            double dy = value1.Y - value2.Y;
            double dz = value1.Z - value2.Z;
            
            return (dx * dx) + (dy * dy) + (dz * dz);
        }

        /// <summary>
        /// Returns the Euclidean distance squared between two specified points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance squared.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquaredF(in Point3 value1, in Point3 value2)
        {
            float dx = value1.X - value2.X;
            float dy = value1.Y - value2.Y;
            float dz = value1.Z - value2.Z;

            return (dx * dx) + (dy * dy) + (dz * dz);
        }

        /// <summary>
        /// Divides the specified point by a specified scalar value.
        /// </summary>
        /// <param name="left">The point.</param>
        /// <param name="divisor">The scalar value.</param>
        /// <returns>The point that results from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3 Divide(in Point3 left, int divisor)
        {
            var x = left.X / divisor;
            var y = left.Y / divisor;
            var z = left.Z / divisor;
            return new(x, y, z);
        }

        /// <summary>
        /// Divides the first point by the second.
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>The point resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3 Divide(in Point3 left, in Point3 right)
        {
            var x = left.X / right.X;
            var y = left.Y / right.Y;
            var z = left.Z / right.Z;
            return new(x, y, z);
        }

        /// <summary>
        /// Returns a point whose elements are the maximum of each of the pairs of
        /// elements in two specified points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The maximized point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3 Max(in Point3 value1, in Point3 value2)
        {
            var x = Max(value1.X, value2.X);
            var y = Max(value1.Y, value2.Y);
            var z = Max(value1.Z, value2.Z);
            return new(x, y, z);
        }

        /// <summary>
        /// Returns a point whose elements are the manimum of each of the pairs of
        /// elements in two specified points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The maximized point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3 Min(in Point3 value1, in Point3 value2)
        {
            var x = Min(value1.X, value2.X);
            var y = Min(value1.Y, value2.Y);
            var z = Min(value1.Z, value2.Z);
            return new(x, y, z);
        }

        /// <summary>
        /// Multiplies a scalar value by a specified point.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The point to multiply.</param>
        /// <returns>The scaled point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3 Multiply(int left, Point3 right)
        {
            var x = left * right.X;
            var y = left * right.Y;
            var z = left * right.Z;
            return new(x, y, z);
        }

        /// <summary>
        /// Multiplies a point by a specified scalar.
        /// </summary>
        /// <param name="left">The point to multiply.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3 Multiply(Point3 left, int right)
        {
            var x = left.X * right;
            var y = left.Y * right;
            var z = left.Z * right;
            return new(x, y, z);
        }

        /// <summary>
        /// Multiplies the first point by the second. 
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>The point resulting from the multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3 Multiply(Point3 left, Point3 right)
        {
            var x = left.X * right.X;
            var y = left.Y * right.Y;
            var z = left.Z * right.Z;
            return new(x, y, z);
        }

        /// <summary>
        /// Negates a specified point.
        /// </summary>
        /// <param name="value">The point to negate.</param>
        /// <returns>The negated point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3 Negate(in Point3 value) =>
            new(-value.X, -value.Y, -value.Z);

        /// <summary>
        /// Subtracts the second point from the first.
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>The difference point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3 Subtract(in Point3 left, in Point3 right) =>
            new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        #endregion
        #region operators
        /// <summary>
        /// Adds two pointers together.
        /// </summary>
        /// <param name="left">The first point to add.</param>
        /// <param name="right">The second point to add.</param>
        /// <returns>The summed point.</returns>
        public static Point3 operator +(in Point3 left, in Point3 right) =>
            Add(left, right);

        /// <summary>
        /// Divides the first point by the second.
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>The point that results from dividing <paramref name="left"/> 
        /// by <paramref name="right"/>.</returns>
        public static Point3 operator /(in Point3 left, in Point3 right) =>
            Divide(left, right);

        /// <summary>
        /// Divides the specified point by a specified scalar value.
        /// </summary>
        /// <param name="left">The point.</param>
        /// <param name="divisor">The scalar value.</param>
        /// <returns>The point that results from the division.</returns>
        public static Point3 operator /(in Point3 left, int divisor) =>
            Divide(left, divisor);

        /// <summary>
        /// Returns a value that indicates whether each pair of elements in two
        /// specified pointers is equal.
        /// </summary>
        /// <param name="left">The first point to compare.</param>
        /// <param name="right">The second point to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and 
        /// <paramref name="right"/> are qual; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(in Point3 left, in Point3 right) =>
            left.Equals(right);

        /// <summary>
        /// Returns a value that indicates whether two specified points are not
        /// equal.
        /// </summary>
        /// <param name="left">The first point to compare.</param>
        /// <param name="right">The second point to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and 
        /// <paramref name="right"/> are not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(in Point3 left, in Point3 right) =>
            !(left == right);


        /// <summary>
        /// Multiplies the first point by the second. 
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>The point resulting from the multiplication.</returns>
        public static Point3 operator *(in Point3 left, in Point3 right) =>
            Multiply(left, right);

        /// <summary>
        /// Multiplies a point by a specified scalar.
        /// </summary>
        /// <param name="left">The point to multiply.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled point.</returns>
        public static Point3 operator *(in Point3 left, int right) =>
            Multiply(left, right);

        /// <summary>
        /// Multiplies a scalar value by a specified point.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The point to multiply.</param>
        /// <returns>The scaled point.</returns>
        public static Point3 operator *(int left, in Point3 right) =>
            Multiply(left, right);

        /// <summary>
        /// Subtracts the second point from the first.
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>The difference point.</returns>
        public static Point3 operator -(in Point3 left, in Point3 right) =>
            Subtract(left, right);

        /// <summary>
        /// Negates a specified point.
        /// </summary>
        /// <param name="value">The point to negate.</param>
        /// <returns>The negated point.</returns>
        public static Point3 operator -(in Point3 value) =>
            Negate(value);

        #endregion
        #endregion
    }
}
