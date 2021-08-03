using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Ez.Memory
{
    /// <summary>
    /// Provides a implementation of <see cref="IPinnedMemory{T}"/> to wraps a
    /// pinned memory pointer.
    /// </summary>
    /// <typeparam name="T">The type of elements in pinned memory.</typeparam>
    public struct PinnedMemory<T> : IPinnedMemory<T> where T : unmanaged
    {
        private static readonly uint TSize = MemUtil.SizeOf<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PinnedMemory{T}"/> struct.
        /// </summary>
        /// <param name="ptr">Pointer to the fixed memory.</param>
        /// <param name="count">Number of T elements in the <see cref="PinnedMemory{T}"/>.</param>
        /// <param name="locker">An object to control access (optional, use <see langword="null"/> to disable).</param>
        public PinnedMemory(IntPtr ptr, int count, ReaderWriterLockSlim locker)
        {
            Ptr = ptr;
            Count = count;
            ReaderWriterLock = locker;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PinnedMemory{T}"/> struct.
        /// </summary>
        /// <param name="ptr">Pointer to the fixed memory.</param>
        /// <param name="count">Number of T elements in the <see cref="PinnedMemory{T}"/>.</param>
        public PinnedMemory(IntPtr ptr, int count)
            : this(ptr, count, new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion))
        {

        }

        /// <inheritdoc/>
        public IntPtr Ptr { get; }

        /// <inheritdoc/>
        public ReaderWriterLockSlim ReaderWriterLock { get; }

        /// <inheritdoc/>
        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                try
                {
                    ReaderWriterLock?.EnterReadLock();
                    return MemUtil.GetRef<T>(GetPtr(index));
                }
                finally
                {
                    ReaderWriterLock?.ExitReadLock();
                }
            }
            set
            {
                CheckIndex(index);
                try
                {
                    ReaderWriterLock?.EnterWriteLock();
                    MemUtil.GetRef<T>(GetPtr(index)) = value;
                }
                finally
                {
                    ReaderWriterLock?.ExitWriteLock();
                }
            }
        }

        /// <inheritdoc/>
        public int Count { get; }

        /// <summary>
        /// Gets the byte size of the pinned memory.
        /// </summary>
        public long ByteSize => Count * TSize;

        private IntPtr GetPtr(int index) => new IntPtr(Ptr.ToInt64() + index * TSize);

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0u; i < Count; i++)
                yield return this[(int)i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Cast a <see cref="PinnedMemory{T}"/> to a <see cref="Span{T}"/>.
        /// </summary>
        /// <param name="memory">The <see cref="PinnedMemory{T}"/> to cast.</param>
        public static implicit operator Span<T>(PinnedMemory<T> memory) =>
            MemUtil.GetSpan<T>(memory.Ptr, memory.Count);

        /// <summary>
        /// Cast a <see cref="PinnedMemory{T}"/> to a <see cref="Memory{T}"/>.
        /// </summary>
        /// <param name="memory">The <see cref="PinnedMemory{T}"/> to cast.</param>
        public static implicit operator Memory<T>(PinnedMemory<T> memory) =>
            MemUtil.GetMemory<T>(memory.Ptr, memory.Count);

        /// <summary>
        /// Cast a <see cref="PinnedMemory{T}"/> to a <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <param name="memory">The <see cref="PinnedMemory{T}"/> to cast.</param>
        public static implicit operator ReadOnlySpan<T>(PinnedMemory<T> memory) =>
            MemUtil.GetSpan<T>(memory.Ptr, memory.Count);

        /// <summary>
        /// Cast a <see cref="PinnedMemory{T}"/> to a <see cref="ReadOnlyMemory{T}"/>.
        /// </summary>
        /// <param name="memory">The <see cref="PinnedMemory{T}"/> to cast.</param>
        public static implicit operator ReadOnlyMemory<T>(PinnedMemory<T> memory) =>
            MemUtil.GetMemory<T>(memory.Ptr, memory.Count);

        /// <summary>
        /// Cast a <see cref="PinnedMemory{T}"/> to a <see cref="ReadOnlyPinnedMemory{T}"/>.
        /// </summary>
        /// <param name="memory">The <see cref="PinnedMemory{T}"/> to cast.</param>
        public static implicit operator ReadOnlyPinnedMemory<T>(PinnedMemory<T> memory) =>
            new(memory.Ptr, memory.Count, memory.ReaderWriterLock);
    }
}