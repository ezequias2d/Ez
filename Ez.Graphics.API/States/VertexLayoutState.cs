// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Text;

using Ez.Numerics;
using Ez.Graphics.API.Resources;
using Ez.Graphics.API.CreateInfos;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describe layout of vertex data in an single <see cref="IBuffer"/> used as a vertex buffer.
    /// </summary>
    public struct VertexLayoutState : IEquatable<VertexLayoutState>
    {
        /// <summary>
        /// The number of bytes between successive elements in the <see cref="IBuffer"/>.
        /// </summary>
        public uint Stride { get; set; }

        /// <summary>
        /// A value that specifies whether vertex attribute addressing is a function of the vertex index 
        /// or of the instance index.
        /// </summary>
        public VertexInputRate InputRate { get; set; }

        /// <summary>
        /// An array of VertexElementDescription objects, each describing a single element of vertex data.
        /// </summary>
        public VertexElementDescription[] Elements;

        /// <summary>
        /// Creates a new instance of <see cref="VertexLayoutState"/> structure.
        /// </summary>
        /// <param name="stride">The number of bytes in between successive elements in the <see cref="IBuffer"/>.</param>
        /// <param name="elements">An array of <see cref="VertexElementDescription"/> objects, each describing a single element
        /// of vertex data.</param>
        public VertexLayoutState(uint stride, params VertexElementDescription[] elements)
        {
            Stride = stride;
            Elements = elements;
            InputRate = 0;
        }

        /// <summary>
        /// Creates a new instance of <see cref="VertexLayoutState"/> structure.
        /// </summary>
        /// <param name="stride">The number of bytes in between successive elements in the <see cref="IBuffer"/>.</param>
        /// <param name="elements">An array of <see cref="VertexElementDescription"/> objects, each describing a single element
        /// of vertex data.</param>
        /// <param name="inputRate">A value controlling how often data for instances is advanced for this element. For
        /// per-vertex elements, this value should be 0.</param>
        public VertexLayoutState(uint stride, VertexInputRate inputRate, params VertexElementDescription[] elements)
        {
            Stride = stride;
            Elements = elements;
            InputRate = inputRate;
        }

        /// <summary>
        /// Creates a new instance of <see cref="VertexLayoutState"/> structure.
        /// </summary>
        /// <param name="elements">An array of <see cref="VertexElementDescription"/> objects, each describing a single element
        /// of vertex data.</param>
        public VertexLayoutState(params VertexElementDescription[] elements)
        {
            Elements = elements;
            uint computedStride = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                uint elementSize = GraphicsApiHelper.GetSizeInBytes(elements[i].Format);
                if (elements[i].Offset != 0)
                    computedStride = elements[i].Offset + elementSize;
                else
                    computedStride += elementSize;
            }

            Stride = computedStride;
            InputRate = 0;
        }

        /// <inheritdoc/>
        public bool Equals(VertexLayoutState other) =>
            Stride == other.Stride &&
            InputRate == other.InputRate &&
            MemoryExtensions.SequenceEqual<VertexElementDescription>(Elements, other.Elements);

        /// <inheritdoc/>
        public override int GetHashCode() => 
            HashHelper<VertexLayoutState>.Combine(Stride, InputRate, HashHelper<VertexElementDescription>.Combine(Elements));
     
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is VertexLayoutState vlci && Equals(vlci);

        /// <inheritdoc/>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append('(');

            builder.Append("Stride: ");
            builder.Append(Stride);
            builder.Append(',');
            builder.Append("InputRate: ");
            builder.Append(InputRate);
            builder.Append(',');
            builder.Append("Elements: ");

            if (Elements == null)
                builder.Append("null");
            else
                for(var i = 0; i < Elements.Length; i++)
                {
                    builder.Append(Elements[i]);
                    if (i != Elements.Length - 1)
                        builder.Append(',');
                }

            builder.Append(')');

            return builder.ToString();
        }

        /// <summary>
        /// Compare two <see cref="VertexLayoutState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(VertexLayoutState left, VertexLayoutState right) => left.Equals(right);


        /// <summary>
        /// Compare two <see cref="VertexLayoutState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(VertexLayoutState left, VertexLayoutState right) =>
            !(left == right);
    }
}
