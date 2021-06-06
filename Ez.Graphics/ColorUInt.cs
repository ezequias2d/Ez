using System;
using System.Drawing;

namespace Ez.Graphics
{
    /// <summary>
    /// A color struct in 32-bits unsigned integer values in RGBA format.
    /// </summary>
    public struct ColorUInt : IColor<uint>
    {
        /// <summary>
        /// Creates a new <see cref="ColorUInt"/> struct.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public ColorUInt(in uint r, in uint g, in uint b, in uint a) =>
            (R, G, B, A) = (r, g, b, a);

        /// <inheritdoc/>
        public uint R { get; }

        /// <inheritdoc/>
        public uint G { get; }

        /// <inheritdoc/>
        public uint B { get; }

        /// <inheritdoc/>
        public uint A { get; }

        /// <inheritdoc/>
        public bool Equals(IColor other) => Equals(other.GetColorInt());

        /// <inheritdoc/>
        public bool Equals(IColor<uint> other) =>
            (R, G, B, A) == (other.R, other.G, other.B, other.A);

        /// <inheritdoc/>
        public Color GetColor() =>
            Color.FromArgb(ToByte(A), ToByte(R), ToByte(G), ToByte(B));

        /// <inheritdoc/>
        public ColorByte GetColorByte() =>
            new(ToByte(R), ToByte(G), ToByte(B), ToByte(A));

        /// <inheritdoc/>
        public ColorInt GetColorInt() => new(ToInt(R), ToInt(G), ToInt(B), ToInt(A));

        /// <inheritdoc/>
        public ColorSingle GetColorSingle() =>
            new(ToSingle(R), ToSingle(G), ToSingle(B), ToSingle(A));

        /// <inheritdoc/>
        public ColorUInt GetColorUInt() => this;

        private static int ToInt(in uint value) =>
            (int)(value + int.MinValue);

        private static float ToSingle(in uint value) =>
            (float)(value * (1.0 / uint.MaxValue));

        private static byte ToByte(in uint value) => (byte)(value >> (3 * 8));
    }
}
