// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Collections.Pools;

namespace Ez.Memory
{
    /// <summary>
    /// An static <see cref="MemoryBlock"/> pool.
    /// </summary>
    public static class MemoryBlockPool
    {
        private static readonly ObjectPool<MemoryBlock, MemoryBlockSpecs> _objectPool;
        private const long Tolerance = 268435456; // 256 MB
        static MemoryBlockPool()
        {
            _objectPool = new ObjectPool<MemoryBlock, MemoryBlockSpecs>(new MemoryBlockPoolAssistant(Tolerance));
        }

        /// <summary>
        /// Returns a <see cref="MemoryBlock"/> to the pool.
        /// </summary>
        /// <param name="memoryBlock">The memory block to return.</param>
        public static void Return(MemoryBlock memoryBlock)
        {
            if(memoryBlock != null && memoryBlock != MemoryBlock.Empty)
                _objectPool.Return(memoryBlock);
        }

        /// <summary>
        /// Gets a <see cref="MemoryBlock"/> from the pool, or creates a new <see cref="MemoryBlock"/>.
        /// </summary>
        /// <param name="size">The size in bytes of <see cref="MemoryBlock"/>.</param>
        /// <param name="anyWithSize">Flag that says it can be any <see cref="MemoryBlock"/> with enough size.</param>
        /// <param name="tolerance">Number of attempts to get before creating a new memory block.</param>
        /// <returns>A <see cref="MemoryBlock"/> with at least the requested size.</returns>
        public static MemoryBlock Get(long size, bool anyWithSize = false, int tolerance = 256) =>
            _objectPool.GetT(new MemoryBlockSpecs { Size = size, AnyWithSize = anyWithSize }, tolerance);

        internal struct MemoryBlockSpecs
        {
            public long Size;
            public bool AnyWithSize;
        }

        internal class MemoryBlockPoolAssistant : IObjectPoolAssistant<MemoryBlock, MemoryBlockSpecs>
        {
            private readonly long _tolerance;
            private long _totalUsed;
            public MemoryBlockPoolAssistant(long tolerance)
            {
                _tolerance = tolerance;
            }

            public bool IsClear()
            {
                if (_totalUsed > _tolerance)
                {
                    _totalUsed = 0;
                    return false;
                }
                return true;
            }

            public bool Evaluate(in MemoryBlock item, in MemoryBlockSpecs specs, int currentTolerance) =>
                specs.Size <= item.TotalSize &&
                    (specs.AnyWithSize || currentTolerance == 0 || (item.TotalSize / specs.Size) <= 4);

            public MemoryBlock Create(in MemoryBlockSpecs specs) => new MemoryBlock(specs.Size);

            public void RegisterReturn(in MemoryBlock item) =>            
                _totalUsed += item.TotalSize;

            public void RegisterGet(in MemoryBlock item) =>
                _totalUsed -= item.TotalSize;
        }
    }
}
