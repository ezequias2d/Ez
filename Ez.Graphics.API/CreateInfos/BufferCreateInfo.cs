// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;
using Ez.Numerics;

namespace Ez.Graphics.API.CreateInfos
{
    /// <summary>
    /// Describes a <see cref="IBuffer"/> object.
    /// </summary>
    public struct BufferCreateInfo : IEquatable<BufferCreateInfo>
    {
        /// <summary>
        /// The size in bytes of buffer.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Indicates how the <see cref="IBuffer"/> will be used.
        /// </summary>
        public BufferUsage Usage { get; set; }

        /// <summary>
        /// Indicates the memory usage of the buffer.
        /// </summary>
        public MemoryUsage MemoryUsage { get; }

        /// <summary>
        /// Creates a new instance of <see cref="BufferCreateInfo"/> struct.
        /// </summary>
        /// <param name="size">The size in bytes of buffer.</param>
        /// <param name="usage">The usage flag of buffer.</param>
        /// <param name="memoryMode">The memory usage of the buffer.</param>
        public BufferCreateInfo(long size, BufferUsage usage, MemoryUsage memoryMode)
        {
            Size = size;
            Usage = usage;
            MemoryUsage = memoryMode;
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<BufferCreateInfo>.Combine(Usage, Size);

        /// <inheritdoc/>
        public bool Equals(BufferCreateInfo other) => Size == other.Size && Usage == other.Usage;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is BufferCreateInfo bci && Equals(bci);

        /// <inheritdoc/>
        public override string ToString() => $"(Size: {Size}, BufferUsage: {Usage})";

        /// <summary>
        /// Compare two <see cref="BufferCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(BufferCreateInfo left, BufferCreateInfo right) =>
            left.Equals(right);


        /// <summary>
        /// Compare two <see cref="BufferCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(BufferCreateInfo left, BufferCreateInfo right) =>
            !(left == right);
    }
}
