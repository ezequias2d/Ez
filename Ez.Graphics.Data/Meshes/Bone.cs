// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Ez.Graphics.Data.Meshes
{
    /// <summary>
    /// A single bone of a mesh.
    /// 
    /// A bone has a name by which it can be found in the frame hierarchy and
    /// by which it can be addressed by animations. In addition it has a number 
    /// of influences on vertices.
    /// </summary>
    public readonly struct Bone : IEquatable<Bone>
    {
        private readonly VertexWeight[] _weights;
        private readonly int _weightsCount;
        private readonly int _hashcode;

        /// <summary>
        /// Initializes a new instance of <see cref="Bone"/> class.
        /// </summary>
        /// <param name="name">The name of bone.</param>
        /// <param name="offsetMatrix">The offset matrix.</param>
        /// <param name="weights">The weights of bones.</param>
        public Bone(string name, Matrix4x4 offsetMatrix, ReadOnlySpan<VertexWeight> weights)
        {
            _weights = Array.Empty<VertexWeight>();
            _weightsCount = 0;

            Name = name;
            OffsetMatrix = offsetMatrix;

            _weights = default;
            _weightsCount = 0;

            GDHelper.Set(ref _weights, ref _weightsCount, weights);

            _hashcode = 0;
            _hashcode = HashHelper<Bone>.Combine(OffsetMatrix, HashHelper<VertexWeight>.Combine(Weights));
        }

        /// <summary>
        /// Bone name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Matrix that transforms from mesh space to bone space in bind pose.
        /// </summary>
        public Matrix4x4 OffsetMatrix { get; }

        /// <summary>
        /// The vertices affected by this bone and weights.
        /// </summary>
        public ReadOnlySpan<VertexWeight> Weights => _weights;

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="Bone"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="Bone"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="Bone"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Bone other) => _hashcode == other._hashcode && Weights.SequenceEqual(other.Weights) && OffsetMatrix == other.OffsetMatrix;

        /// <summary>
        /// Returns a value that indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="obj"/> are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) =>
            obj is Bone bone && Equals(bone);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => _hashcode;
    }
}
