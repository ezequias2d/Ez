using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace Ez.Memory
{
    public struct PinnedMemory<T> : IReadOnlyList<T> where T : unmanaged
    {
        private static readonly uint TSize = MemUtil.SizeOf<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyUnsafeList{T}"/> class that 
        /// uses a specific fixed area of the memory.
        /// </summary>
        /// <param name="ptr">Pointer to the fixed memory.</param>
        /// <param name="count">Number of T elements in the <see cref="ReadOnlyUnsafeList{T}"/>.</param>
        public PinnedMemory(IntPtr ptr, int count, ReaderWriterLockSlim locker)
        {
            Ptr = ptr;
            Count = count;
            Locker = locker;
        }

        public IntPtr Ptr { get; }

        public ReaderWriterLockSlim Locker { get; }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The element of the specified index.</returns>
        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                try
                {
                    Locker?.EnterReadLock();
                    return MemUtil.Get<T>(GetPtr(index));
                }
                finally
                {
                    Locker?.ExitReadLock();
                }
            }
            set
            {
                CheckIndex(index);
                try
                {
                    Locker?.EnterWriteLock();
                    MemUtil.Set(GetPtr(index), value);
                }
                finally
                {
                    Locker?.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="Read
        public int Count { get; }

        private IntPtr GetPtr(int index) => new IntPtr(Ptr.ToInt64() + index * TSize);

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0u; i < Count; i++)
                yield return this[(int)i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static implicit operator Span<T>(PinnedMemory<T> memory) =>
            MemUtil.GetSpan<T>(memory.Ptr, memory.Count);
    }
}