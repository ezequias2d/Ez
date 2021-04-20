// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Graphics.Data.Meshes
{
    /// <summary>
    /// Represents a triangle face of a <see cref="Mesh"/>.
    /// </summary>
    public struct TriangleFace : IEquatable<TriangleFace>
    {
        /// <summary>
        /// The index of first vertex of triangle.
        /// </summary>
        public uint Vertex1;

        /// <summary>
        /// The index of second vertex of triangle.
        /// </summary>
        public uint Vertex2;

        /// <summary>
        /// The index of third vertex of triangle.
        /// </summary>
        public uint Vertex3;

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="TriangleFace"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="TriangleFace"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="TriangleFace"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(TriangleFace other) =>
            (Vertex1, Vertex2, Vertex3) == (other.Vertex1, other.Vertex2, other.Vertex3);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => HashHelper<TriangleFace>.Combine(Vertex1, Vertex2, Vertex3);
    }
}
