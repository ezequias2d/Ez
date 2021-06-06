using System;

namespace Ez.Memory
{
    /// <summary>
    /// Represents an ephemeral instance of a <see cref="MemoryBlock"/>.
    /// 
    /// It should be used to expose a block of memory without providing a reference 
    /// that suggests it is permanent.
    /// </summary>
    public ref struct EphemeralMemoryBlock
    {
        /// <summary>
        /// returns an empty <see cref="MemoryBlock"/> object.
        /// </summary>
        public static EphemeralMemoryBlock Empty => new EphemeralMemoryBlock(MemoryBlock.Empty);

        private readonly MemoryBlock _memoryBlock;

        /// <summary>
        /// Initializes a new instance of <see cref="EphemeralMemoryBlock"/> struct.
        /// </summary>
        public EphemeralMemoryBlock(MemoryBlock memoryBlock)
        {
            _memoryBlock = memoryBlock;
        }

        /// <summary>
        /// Gets the total bytes not sub-allocated in <see cref="MemoryBlock"/>.
        /// </summary>
        public long RemainingSize => _memoryBlock.RemainingSize;

        /// <summary>
        /// Gets the size of <see cref="MemoryBlock"/> in bytes.
        /// </summary>
        public long TotalSize => _memoryBlock.TotalSize;

        /// <summary>
        /// Gets the total bytes sub-allocated in <see cref="MemoryBlock"/>.
        /// </summary>
        public long TotalUsed => _memoryBlock.TotalUsed;

        /// <summary>
        /// The base pointer to the memory allocated by <see cref="MemoryBlock"/>.
        /// </summary>
        public IntPtr BaseIntPtr => _memoryBlock.Ptr;

        /// <summary>
        /// Sub-allocates a part of the <see cref="MemoryBlock"/>.
        /// </summary>
        /// <param name="size">The size in bytes of the sub-allocation.</param>
        /// <param name="ptr">Contains the pointer to the sub-allocated area, if there is enough memory, otherwise null.</param>
        /// <returns></returns>
        public unsafe bool TryAlloc(long size, out IntPtr ptr) => _memoryBlock.TryAlloc(size, out ptr);

        /// <summary>
        /// Resets the sub-allocated memory to the initial state, without sub-allocated memory.
        /// </summary>
        public void Reset() => _memoryBlock.Reset();

        /// <summary>
        /// Implicitly creates an <see cref="EphemeralMemoryBlock"/> instance.
        /// </summary>
        /// <param name="memoryBlock"></param>
        public static implicit operator EphemeralMemoryBlock(MemoryBlock memoryBlock) => 
            new EphemeralMemoryBlock(memoryBlock);
    }
}