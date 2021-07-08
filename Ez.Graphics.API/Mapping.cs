// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Memory;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// Provides an object class that represents a visible mapping to 
    /// the cpu of part of the gpu's accessible memory in a managed
    /// manner.
    /// </summary>
    /// <typeparam name="T">The unmanaged type of data.</typeparam>
    public class Mapping<T> : IDisposable, IEnumerable<T> where T : unmanaged
    {
        private static readonly long TSize = MemUtil.SizeOf<T>();
        private readonly uint _subresource;

        /// <summary>
        /// Creates a new instance of <see cref="Mapping{T}"/> and map the 
        /// <paramref name="resource"/>.
        /// </summary>
        /// <param name="resource">The resource to map.</param>
        /// <param name="subresource">The subresource to map. For <see cref="ITexture"/> are 
        /// indexed first by mip slice, then by array layer.
        /// (see <see cref="GraphicsApiHelper.CalculateSubresource(ITexture, uint, uint)"/> and 
        /// <see cref="GraphicsApiHelper.GetSubresourceLevelAndLayer(ITexture, uint, out uint, out uint)"/>)</param>
        public Mapping(IMappableResource resource, uint subresource)
        {
            Resource = resource;
            _subresource = subresource;
#pragma warning disable CS0618
            (Ptr, ByteLength) = resource.Map(subresource);
#pragma warning restore CS0618

            Length = ByteLength / TSize;
            Memory = MemUtil.GetMemory<T>(Ptr, (int)Length);
        }

        /// <summary>
        /// Creates a new instance of <see cref="Mapping{T}"/> and map the <paramref name="texture"/>.
        /// </summary>
        /// <param name="texture">The texture to map.</param>
        /// <param name="mipmapLevel">The mipmap level of the map.</param>
        /// <param name="arrayLayer">The array layer of the map.</param>
        public Mapping(ITexture texture, uint mipmapLevel, uint arrayLayer)
            : this(texture, GraphicsApiHelper.CalculateSubresource(texture, mipmapLevel, arrayLayer)) { }

        /// <summary>
        /// Creates a new instance of <see cref="Mapping{T}"/> and map the <paramref name="buffer"/>.
        /// </summary>
        /// <param name="buffer">The buffer to map.</param>
        public Mapping(IBuffer buffer) : this(buffer, 0) { }

        /// <summary>
        /// Destroys a <see cref="Mapping{T}"/> instance.
        /// </summary>
        ~Mapping() => Dispose();

        /// <inheritdoc/>
        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

#pragma warning disable CS0618
            Resource.Unmap(_subresource);
#pragma warning restore CS0618
        }

        /// <summary>
        /// Gets the mapped object.
        /// </summary>
        public IMappableResource Resource { get; }

        /// <summary>
        /// Gets a value that indicate that the map is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Gets the <see langword="byte"/> length of the mapped memory.
        /// </summary>
        public long ByteLength { get; }

        /// <summary>
        /// Gets the <typeparamref name="T"/> length of the mapped memory.
        /// </summary>
        public long Length { get; }

        /// <summary>
        /// Gets the <see cref="IntPtr"/> of the mapped memory.
        /// </summary>
        public IntPtr Ptr { get; }

        /// <summary>
        /// Gets the <see cref="Memory{T}"/> of the mapping.
        /// </summary>
        public Memory<T> Memory { get; }

        /// <summary>
        /// Gets a reference for a <paramref name="index"/> of mapping.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ref T this[long index] => ref MemUtil.GetRef<T>(new IntPtr((long)Ptr + index * TSize));

        /// <summary>
        /// Returns an enumerator that iterates through a mapping.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> object that can be used to iterate through the mapping.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0ul; i < (ulong)Length; i++)
                yield return this[(long)i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
