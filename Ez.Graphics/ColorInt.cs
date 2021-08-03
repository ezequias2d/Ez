using System.Drawing;
using System.Runtime.InteropServices;

namespace Ez.Graphics
{
    /// <summary>
    /// A color struct in 32-bits integer values in RGBA format.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ColorInt : IColor<int>
    {
        /// <summary>
        /// Creates a new <see cref="ColorInt"/> struct.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public ColorInt(int r, int g, int b, int a) =>
            (R, G, B, A) = (r, g, b, a);

        /// <inheritdoc/>
        public int R { get; }

        /// <inheritdoc/>
        public int G { get; }

        /// <inheritdoc/>
        public int B { get; }

        /// <inheritdoc/>
        public int A { get; }

        /// <inheritdoc/>
        public bool Equals(IColor other) => Equals(other.GetColorInt());

        /// <inheritdoc/>
        public bool Equals(IColor<int> other) =>
            (R, G, B, A) == (other.R, other.G, other.B, other.A);

        /// <inheritdoc/>
        public Color GetColor() =>
            Color.FromArgb(ToByte(A), ToByte(R), ToByte(G), ToByte(B));

        /// <inheritdoc/>
        public ColorByte GetColorByte() =>
            new(ToByte(R), ToByte(G), ToByte(B), ToByte(A));

        /// <inheritdoc/>
        public ColorInt GetColorInt() => this;

        /// <inheritdoc/>
        public ColorSingle GetColorSingle() =>
            new(ToSingle(R), ToSingle(G), ToSingle(B), ToSingle(A));

        /// <inheritdoc/>
        public ColorUInt GetColorUInt() =>
            new(ToUInt(R), ToUInt(G), ToUInt(B), ToUInt(A));

        private uint ToUInt(in int value) =>
            (uint)(((long)value) - int.MinValue);

        private float ToSingle(in int value) =>
            (value * (1.0f / uint.MaxValue)) - (int.MinValue * (1.0f / uint.MaxValue));

        private byte ToByte(in int value) => (byte)(ToUInt(value) >> (3 * 8));
    }
}
