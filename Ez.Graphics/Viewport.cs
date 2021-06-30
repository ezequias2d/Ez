// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Ez.Numerics;
using System;
using System.Text;

namespace Ez.Graphics
{
    /// <summary>
    /// Describes a 3-dimensional region.
    /// </summary>
    public struct Viewport : IEquatable<Viewport>
    {
        /// <summary>
        /// The minimum X value.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The minimum Y value.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The width.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// The height.
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// The minimum depth.
        /// </summary>
        public float MinDepth { get; set; }

        /// <summary>
        /// The maximum depth.
        /// </summary>
        public float MaxDepth { get; set; }

        /// <summary>
        /// Constructs a new Viewport.
        /// </summary>
        /// <param name="x">The minimum X value.</param>
        /// <param name="y">The minimum Y value.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="minDepth">The minimum Z(depth) value.</param>
        /// <param name="maxDepth">The maximum Z(depth) value.</param>
        public Viewport(float x, float y, float width, float height, float minDepth, float maxDepth) =>
            (X, Y, Width, Height, MinDepth, MaxDepth) = (x, y, width, height, minDepth, maxDepth);

        /// <inheritdoc/>
        public bool Equals(Viewport other) =>
            X == other.X &&
            Y == other.Y &&
            Width == other.Width &&
            Height == other.Height &&
            MinDepth == other.MinDepth &&
            MaxDepth == other.MaxDepth;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Viewport v && Equals(v);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelper<Viewport>.Combine(X, Y, Width, Height, MinDepth, MaxDepth);

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("(Viewport, X: ");
            sb.Append(X);
            sb.Append(", Y: ");
            sb.Append(Y);
            sb.Append(", Width: ");
            sb.Append(Width);
            sb.Append(", Height: ");
            sb.Append(Height);
            sb.Append(", MinDepth: ");
            sb.Append(MinDepth);
            sb.Append(", MaxDepth: ");
            sb.Append(MaxDepth);
            sb.Append(")");
            return base.ToString();
        }

        /// <summary>
        /// Returns <see langword="true"/> if its operands are equal, otherwise <see langword="false"/>.
        /// </summary>
        /// <param name="left">The first operand.</param>
        /// <param name="right">The second operand.</param>
        /// <returns><see langword="true"/> if its operands are equal, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(Viewport left, Viewport right) => left.Equals(right);

        /// <summary>
        /// Returns <see langword="true"/> if its operands are not equal, otherwise <see langword="false"/>.
        /// </summary>
        /// <param name="left">The first operand.</param>
        /// <param name="right">The second operand.</param>
        /// <returns><see langword="true"/> if its operands are not equal, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(Viewport left, Viewport right) => !(left == right);
    }
}
