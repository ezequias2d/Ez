// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Numerics;

namespace Ez.Graphics.API.CreateInfos
{
    /// <summary>
    /// Describe a single element of a vertex.
    /// </summary>
    public struct VertexElementDescription : IEquatable<VertexElementDescription>
    {
        /// <summary>
        /// The format of the element.
        /// </summary>
        public VertexElementType Format;

        /// <summary>
        /// The element location in Shader.
        /// </summary>
        public uint Location;

        /// <summary>
        /// The offset in bytes from the beginning of the vertex.
        /// </summary>
        public uint Offset;

        /// <summary>
        /// Creates a new instance of <see cref="VertexElementDescription"/> struct.
        /// </summary>
        /// <param name="format">The format of the element.</param>
        /// <param name="location">The element location in shader.</param>
        public VertexElementDescription(VertexElementType format, uint location)
        {
            Format = format;
            Location = location;
            Offset = uint.MaxValue;
        }

        /// <inheritdoc/>
        public bool Equals(VertexElementDescription other) =>
            GetHashCode() == other.GetHashCode() &&
            Format == other.Format &&
            Location == other.Location &&
            Offset == other.Offset;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is VertexElementDescription ved && Equals(ved);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<VertexElementDescription>.Combine(Format, Location, Offset);

        /// <summary>
        /// Compare two <see cref="VertexElementDescription"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(VertexElementDescription left, VertexElementDescription right) => left.Equals(right);


        /// <summary>
        /// Compare two <see cref="VertexLayoutState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(VertexElementDescription left, VertexElementDescription right) =>
            !(left == right);
    }
}