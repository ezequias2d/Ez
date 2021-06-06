using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ez.Memory
{
    /// <summary>
    /// Represents an unmanaged memory block.
    /// </summary>
    public interface IMemoryBlock : IDisposable, IResettable
    {
        /// <summary>
        /// Gets the total bytes not sub-allocated in <see cref="MemoryBlock"/>.
        /// </summary>
        long RemainingSize { get; }

        /// <summary>
        /// Gets the size of <see cref="MemoryBlock"/> in bytes.
        /// </summary>
        long TotalSize { get; }

        /// <summary>
        /// Gets the total bytes sub-allocated in <see cref="MemoryBlock"/>.
        /// </summary>
        long TotalUsed { get; }

        /// <summary>
        /// The base pointer to the memory allocated by <see cref="MemoryBlock"/>.
        /// </summary>
        IntPtr Ptr { get; }

        /// <summary>
        /// Try to sub-allocate a part of the <see cref="MemoryBlock"/>.
        /// </summary>
        /// <param name="size">The size in bytes of the sub-allocation.</param>
        /// <param name="ptr">Contains the pointer to the sub-allocated area, if there 
        /// is enough memory, otherwise <see cref="IntPtr.Zero"/>.</param>
        /// <returns><see langword="true"/> if the <paramref name="ptr"/> has a
        /// pointer to a sub-allocated area, otherwise <see langword="false"/>.</returns>
        bool TryAlloc(long size, out IntPtr ptr);

        /// <summary>
        /// Sub-allocates a part of the <see cref="MemoryBlock"/>.
        /// </summary>
        /// <param name="size">The size in bytes of the sub-allocation.</param>
        /// <returns>The pointer to the sub-allocated area, if there is 
        /// enough memory, otherwise null.</returns>
        IntPtr AllocIntPtr(long size);

        /// <summary>
        /// Sub-allocates a part of the <see cref="MemoryBlock"/> as a <see cref="PinnedMemory{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <see cref="PinnedMemory{T}"/>.</typeparam>
        /// <param name="length">The number of elements to sub-allocate to <see cref="PinnedMemory{T}"/>.</param>
        /// <returns>A <see cref="PinnedMemory{T}"/> of the sub-allocated area.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Occurs when no has memory enough or 
        /// <paramref name="length"/> is negative.</exception>
        PinnedMemory<T> AllocPinnedMemory<T>(int length) where T : unmanaged;

        /// <summary>
        /// Try to sub-allocate a part of the <see cref="MemoryBlock"/> as a <see cref="PinnedMemory{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <see cref="PinnedMemory{T}"/>.</typeparam>
        /// <param name="length">The number of elements to sub-allocate to <see cref="PinnedMemory{T}"/>.</param>
        /// <param name="memory">The sub-allocated <see cref="PinnedMemory{T}"/>, otherwise <see langword="default"/>.</param>
        /// <returns><see langword="true"/> if the <paramref name="memory"/> has a <see cref="PinnedMemory{T}"/> of
        /// the sub-allocated area, otherwise <see langword="false"/>.</returns>
        bool TryAllocPinnedMemory<T>(int length, out PinnedMemory<T> memory) where T : unmanaged;
    }
}
