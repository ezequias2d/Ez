// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Ez.Memory
{
    /// <summary>
    /// Represents an unmanaged memory block.
    /// </summary>
    public unsafe class MemoryBlock : IDisposable, IResettable
    {
        public static MemoryBlock Empty => new MemoryBlock();
        /// <summary>
        /// The default size of a <see cref="MemoryBlock"/>.
        /// </summary>
        public const ulong DefaultStorageBlockSize = 65536;

        // The end of suballocated memory.
        private ulong _end;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of <see cref="MemoryBlock"/> class.
        /// </summary>
        /// <param name="storageBlockSize">The size of memory in <see cref="MemoryBlock"/>.</param>
        public MemoryBlock(ulong storageBlockSize = DefaultStorageBlockSize)
        {
            BasePtr = (byte*)MemUtil.Alloc(storageBlockSize);
            TotalSize = storageBlockSize;
            _end = 0;
            _disposed = false;
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        internal MemoryBlock()
        {
            BasePtr = null;
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
        public ulong RemainingSize => TotalSize - TotalUsed;

        /// <summary>
        /// Gets the size of <see cref="MemoryBlock"/> in bytes.
        /// </summary>
        public ulong TotalSize { get; }

        /// <summary>
        /// Gets the total bytes sub-allocated in <see cref="MemoryBlock"/>.
        /// </summary>
        public ulong TotalUsed => _end;

        /// <summary>
        /// The base pointer to the memory allocated by <see cref="MemoryBlock"/>.
        /// </summary>
        public IntPtr BaseIntPtr => new IntPtr(BasePtr);

        /// <summary>
        /// The base pointer to the memory allocated by <see cref="MemoryBlock"/>.
        /// </summary>
        public byte* BasePtr { get; }

        /// <summary>
        /// Releases all allocated memory.
        /// </summary>
        public void Dispose() => Dispose(true);

        private void Dispose(bool _)
        {
            if (!_disposed)
            {
                _disposed = true;
                Marshal.FreeHGlobal((IntPtr)BasePtr);
            }
        }

        /// <summary>
        /// Sub-allocates a part of the <see cref="MemoryBlock"/>.
        /// </summary>
        /// <param name="size">The size in bytes of the sub-allocation.</param>
        /// <param name="ptr">Contains the pointer to the sub-allocated area, if there is enough memory, otherwise null.</param>
        /// <returns></returns>
        public bool Alloc(ulong size, out void* ptr)
        {
            if (size <= RemainingSize)
            {
                ptr = BasePtr + _end;
                _end += size;
                return true;
            }
            ptr = null;
            return false;
        }

        /// <summary>
        /// Resets the sub-allocated memory to the initial state, without sub-allocated memory.
        /// </summary>
        public void Reset()
        {
            _end = 0;
            MemUtil.Set(BasePtr, 0, TotalSize);
        }
        void IResettable.Set()
        {
            
        }
    }
}
