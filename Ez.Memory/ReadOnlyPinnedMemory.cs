using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Ez.Memory
{
    /// <summary>
    /// Provides a implementation of <see cref="IReadOnlyPinnedMemory{T}"/> to wraps a
    /// pinned memory pointer.
    /// </summary>
    /// <typeparam name="T">The type of elements in pinned memory.</typeparam>
    public struct ReadOnlyPinnedMemory<T> : IReadOnlyPinnedMemory<T> where T : unmanaged
    {
        private readonly PinnedMemory<T> _pm;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyPinnedMemory{T}"/> struct.
        /// </summary>
        /// <param name="ptr">Pointer to the fixed memory.</param>
        /// <param name="count">Number of T elements in the <see cref="PinnedMemory{T}"/>.</param>
        /// <param name="locker">An object to control access (optional, use <see langword="null"/> to disable).</param>
        public ReadOnlyPinnedMemory(IntPtr ptr, int count, ReaderWriterLockSlim locker)
        {
            _pm = new(ptr, count, locker);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PinnedMemory{T}"/> struct.
        /// </summary>
        /// <param name="ptr">Pointer to the fixed memory.</param>
        /// <param name="count">Number of T elements in the <see cref="PinnedMemory{T}"/>.</param>
        public ReadOnlyPinnedMemory(IntPtr ptr, int count) 
            : this(ptr, count, new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion))
        {

        }

        /// <inheritdoc/>
        public IntPtr Ptr => _pm.Ptr;

        /// <inheritdoc/>
        public ReaderWriterLockSlim ReaderWriterLock => _pm.ReaderWriterLock;

        /// <inheritdoc/>
        public T this[int index] => _pm[index];

        /// <inheritdoc/>
        public int Count => _pm.Count;

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator() => _pm.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Cast a <see cref="PinnedMemory{T}"/> to a <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <param name="memory">The <see cref="PinnedMemory{T}"/> to cast.</param>
        public static implicit operator ReadOnlySpan<T>(ReadOnlyPinnedMemory<T> memory) =>
            MemUtil.GetSpan<T>(memory.Ptr, memory.Count);

        /// <summary>
        /// Cast a <see cref="PinnedMemory{T}"/> to a <see cref="ReadOnlyMemory{T}"/>.
        /// </summary>
        /// <param name="memory">The <see cref="PinnedMemory{T}"/> to cast.</param>
        public static implicit operator ReadOnlyMemory<T>(ReadOnlyPinnedMemory<T> memory) =>
            MemUtil.GetMemory<T>(memory.Ptr, memory.Count);
    }
}