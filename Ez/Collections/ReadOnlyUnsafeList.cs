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
    public unsafe class ReadOnlyUnsafeList<T> : IReadOnlyList<T> where T : unmanaged
    {
        private T* _ptr;

        public ReadOnlyUnsafeList(T* ptr, int count)
        {
            _ptr = ptr;
            Count = count;
        }
        public T this[int index] 
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                return _ptr[index];
            }
        }

        public int Count { get; }
        public IEnumerator<T> GetEnumerator()
        {
            for (uint i = 0; i < Count; i++)
                yield return this[(int)i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
