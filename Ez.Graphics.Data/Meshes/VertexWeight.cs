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
    /// Represents the influence of a bone on a vertex.
    /// </summary>
    public struct VertexWeight : IEquatable<VertexWeight>
    {
        /// <summary>
        /// Index of the vertex which is influenced by the bone.
        /// </summary>
        public uint VertexIndex;

        /// <summary>
        /// The strength of the influence in the range 0 to 1.
        /// The influence from all bones at one vertex amounts to 1.
        /// </summary>
        public float Weight;

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="VertexWeight"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="VertexWeight"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="VertexWeight"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(VertexWeight other) => 
            (VertexIndex, Weight) == (other.VertexIndex, other.Weight);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => HashHelper<VertexWeight>.Combine(VertexIndex, Weight);
    }
}
