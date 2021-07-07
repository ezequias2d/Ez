// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Threading.Tasks;

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// Represents a buffer resource.
    /// </summary>
    public interface IBuffer : IResource, IMappableResource
    {
        /// <summary>
        /// Size of buffer in bytes.
        /// </summary>
        long Size { get; }

        /// <summary>
        /// A bitmask indicating how this instance is permitted to be used.
        /// </summary>
        BufferUsage Usage { get; }

        /// <summary>
        /// Updates a <see cref="IBuffer"/> region with new data.
        /// </summary>
        /// <typeparam name="U">The type of data to upload.</typeparam>
        /// <param name="source">A readonly memory containing the data to upload.</param>
        /// <param name="bufferOffset">An offset, in bytes, from the beginning of the <see cref="IBuffer"/>'s storage, at which new data will be uploaded.</param>
        void SubData<U>(ReadOnlySpan<U> source, long bufferOffset) where U : unmanaged;

        /// <summary>
        /// Updates a <see cref="IBuffer"/> region with new data.
        /// </summary>
        /// <typeparam name="U">The type of data to upload.</typeparam>
        /// <param name="source">A readonly memory containing the data to upload.</param>
        /// <param name="bufferOffset">An offset, in bytes, from the beginning of the <see cref="IBuffer"/>'s storage, at which new data will be uploaded.</param>
        Task SubDataAsync<U>(ReadOnlySpan<U> source, long bufferOffset) where U : unmanaged;
    }
}