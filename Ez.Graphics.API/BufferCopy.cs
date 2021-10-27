// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Ez.Numerics;
using System;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specifying a buffer copy operation.
    /// </summary>
    public struct BufferCopy : IEquatable<BufferCopy>
    {
        /// <summary>
        /// Specifies the starting offset in bytes from the start of the source buffer.
        /// </summary>
        public long SrcOffset;

        /// <summary>
        /// Specifies the starting offset in bytes from the start of the destination buffer.
        /// </summary>
        public long DstOffset;

        /// <summary>
        /// Specifies the number of bytes to copy.
        /// </summary>
        public long Size;

        /// <inheritdoc/>
        public bool Equals(BufferCopy other) =>
            SrcOffset == other.SrcOffset &&
            DstOffset == other.DstOffset &&
            Size == other.Size;

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is BufferCopy bc && Equals(bc);

        /// <summary>
        /// Compare two <see cref="BufferCopy"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(BufferCopy left, BufferCopy right) => left.Equals(right);

        /// <summary>
        /// Compare two <see cref="BufferCopy"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(BufferCopy left, BufferCopy right) => !(left == right);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelper<BufferCopy>.Combine(SrcOffset, DstOffset, Size);
    }
}
