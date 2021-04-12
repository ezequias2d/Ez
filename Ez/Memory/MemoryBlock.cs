using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Ez.Memory
{
    public unsafe class MemoryBlock : IDisposable, IResettable
    {
        public const ulong DefaultStorageBlockSize = 40000;

        private ulong _end;        
        private bool _freed;

        public MemoryBlock(ulong storageBlockSize = DefaultStorageBlockSize, bool autofree = true)
        {
            BasePtr = (byte*)MemUtil.Alloc(storageBlockSize);
            Autofree = autofree;
            TotalSize = storageBlockSize;
            _end = 0;
            _freed = false;
        }

        ~MemoryBlock()
        {
            Dispose(false);
        }

        public ulong RemainingSize => TotalSize - _end;
        public ulong TotalSize { get; }
        public ulong TotalUsed => _end;
        public byte* BasePtr { get; }
        public bool Autofree { get; }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if ((disposing || Autofree) && !_freed)
            {
                Marshal.FreeHGlobal((IntPtr)BasePtr);
                _freed = true;
            }
        }

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

        public void Reset()
        {
            _end = 0;
            MemUtil.Set(BasePtr, 0, TotalSize);
        }

        public void Set()
        {
            
        }
    }
}
