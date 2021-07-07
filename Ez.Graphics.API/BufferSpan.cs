// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;
using Ez.Numerics;

namespace Ez.Graphics.API
{
    /// <summary>
    /// A represents a section of a <see cref="IBuffer"/>.
    /// to shaders.
    /// </summary>
    public struct BufferSpan : IEquatable<BufferSpan>
    {
        /// <summary>
        /// The <see cref="IBuffer"/> that this span refers to.
        /// </summary>
        public IBuffer Buffer;

        /// <summary>
        /// The offset, in bytes, from the beginning of the buffer that this span starts at.
        /// </summary>
        public long Offset;

        /// <summary>
        /// The total number of bytes in the span.
        /// </summary>
        public long Size;

        /// <summary>
        /// Creates a new instance of <see cref="BufferSpan"/> structure.
        /// </summary>
        /// <param name="buffer">The <see cref="IBuffer"/> that this span refers to.</param>
        /// <param name="offset">The offset, in bytes, from the beginning of the buffer that this span starts at.</param>
        /// <param name="size">The total number of bytes in the span.</param>
        public BufferSpan(IBuffer buffer, long offset, long size)
        {
            Buffer = buffer;
            Offset = offset;
            Size = size;
        }

        /// <inheritdoc/>
        public bool Equals(BufferSpan other) => 
            Buffer == other.Buffer && Offset.Equals(other.Offset) && Size.Equals(other.Size);
        
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is BufferSpan br && Equals(br);

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int bufferHash = Buffer?.GetHashCode() ?? 0;
            return HashHelper<BufferSpan>.Combine(bufferHash, Offset, Size);
        }

        /// <summary>
        /// Compare two <see cref="BufferSpan"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(BufferSpan left, BufferSpan right) => left.Equals(right);


        /// <summary>
        /// Compare two <see cref="VertexLayoutState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(BufferSpan left, BufferSpan right) =>
            !(left == right);
    }
}
