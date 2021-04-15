// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections
{
    /// <summary>
    /// Represents a strongly typed unsafe read-only list that can be accessed by index.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe class ReadOnlyUnsafeList<T> : IReadOnlyList<T> where T : unmanaged
    {
        private T* _ptr;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyUnsafeList{T}"/> class that 
        /// uses a specific fixed area of the memory.
        /// </summary>
        /// <param name="ptr">Pointer to the fixed memory.</param>
        /// <param name="count">Number of T elements in the <see cref="ReadOnlyUnsafeList{T}"/>.</param>
        public ReadOnlyUnsafeList(IntPtr ptr, int count)
        {
            _ptr = (T*)ptr;
            Count = count;
        }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The element of the specified index.</returns>
        public T this[int index] 
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                return _ptr[index];
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="ReadOnlyUnsafeList{T}"/>.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="ReadOnlyUnsafeList{T}"/>.
        /// </summary>
        /// <returns>A <see cref="IEnumerator{T}"/> for the <see cref="ReadOnlyUnsafeList{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (uint i = 0; i < Count; i++)
                yield return this[(int)i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
