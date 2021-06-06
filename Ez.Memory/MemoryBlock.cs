// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Threading;

namespace Ez.Memory
{
    /// <summary>
    /// Represents an unmanaged memory block.
    /// </summary>
    public class MemoryBlock : IMemoryBlock
    {
        /// <summary>
        /// returns an empty <see cref="MemoryBlock"/> object.
        /// </summary>
        public static MemoryBlock Empty => new();

        /// <summary>
        /// The default size of a <see cref="MemoryBlock"/>.
        /// </summary>
        public const long DefaultStorageBlockSize = 65536;

        // The end of suballocated memory.
        private long _end;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of <see cref="MemoryBlock"/> class.
        /// </summary>
        /// <param name="storageBlockSize">The size of memory in <see cref="MemoryBlock"/>. <seealso cref="MemUtil.MaxAllocSize"/></param>
        public MemoryBlock(long storageBlockSize = DefaultStorageBlockSize)
        {
            Ptr = MemUtil.Alloc(storageBlockSize);
            TotalSize = storageBlockSize;
            _end = 0;
            _disposed = false;
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        internal MemoryBlock()
        {
            Ptr = IntPtr.Zero;
            TotalSize = 0;
            _end = 0;
            _disposed = true;
        }

        /// <summary>
        /// Destroys a instance of <see cref="MemoryBlock"/> class.
        /// </summary>
        ~MemoryBlock() => Dispose(false);

        /// <summary>
        /// Gets the total bytes not sub-allocated in <see cref="MemoryBlock"/>.
        /// </summary>
        public long RemainingSize => TotalSize - TotalUsed;

        /// <summary>
        /// Gets the size of <see cref="MemoryBlock"/> in bytes.
        /// </summary>
        public long TotalSize { get; }

        /// <summary>
        /// Gets the total bytes sub-allocated in <see cref="MemoryBlock"/>.
        /// </summary>
        public long TotalUsed => _end;

        /// <summary>
        /// The base pointer to the memory allocated by <see cref="MemoryBlock"/>.
        /// </summary>
        public IntPtr Ptr { get; }

        /// <summary>
        /// Releases all allocated memory.
        /// </summary>
        public void Dispose() => Dispose(true);

        private unsafe void Dispose(bool _)
        {
            if (!_disposed)
            {
                _disposed = true;
                MemUtil.Free(Ptr);
            }
        }

        /// <inheritdoc/>
        public bool TryAlloc(long size, out IntPtr ptr)
        {
            CheckSize(size);

            if (size <= RemainingSize)
            {
                ptr = new IntPtr(Ptr.ToInt64() + _end);
                _end += size;
                return true;
            }
            ptr = IntPtr.Zero;
            return false;
        }

        /// <inheritdoc/>
        public IntPtr AllocIntPtr(long size)
        {
            if (TryAlloc(size, out var ptr))
                return ptr;
            throw new ArgumentOutOfRangeException(nameof(size));
        }

        /// <inheritdoc/>
        public PinnedMemory<T> AllocPinnedMemory<T>(int length) where T : unmanaged
        {
            if (TryAllocPinnedMemory<T>(length, out var memory))
                return memory;
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        /// <inheritdoc/>
        public bool TryAllocPinnedMemory<T>(int length, out PinnedMemory<T> memory) where T : unmanaged
        {
            CheckSize(length);

            var size = MemUtil.SizeOf<T>() * length;

            if (TryAlloc(size, out var ptr))
            {
                memory = new PinnedMemory<T>(ptr, length, new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion));
                return true;
            }
            memory = default;
            return false;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _end = 0;
        }

        /// <inheritdoc/>
        public void Set()
        {
            MemUtil.Set(Ptr, 0, TotalSize);
        }

        private void CheckSize(long size)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException();
        }
    }
}
