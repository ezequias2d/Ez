using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace Ez.Memory
{
    public class UnmanagedMemoryManager<T> : MemoryManager<T> where T : unmanaged
    {
        public UnmanagedMemoryManager(IntPtr ptr, int length)
        {
            Ptr = ptr;
            Length = length;
        }

        public IntPtr Ptr { get; }
        public int Length { get; }

        public override Span<T> GetSpan() => MemUtil.GetSpan<T>(Ptr, Length);

        public override MemoryHandle Pin(int elementIndex = 0)
        {
            if (elementIndex < 0 || elementIndex >= Length)
                throw new ArgumentOutOfRangeException(nameof(elementIndex));
            unsafe
            {
                return new MemoryHandle(((T*)Ptr) + elementIndex);
            }
        }

        public override void Unpin() { }

        protected override void Dispose(bool disposing) { }
    }
}
