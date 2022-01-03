// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Collections.Pools;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ez.Memory
{
    /// <summary>
    /// An static <see cref="MemoryBlock"/> pool.
    /// </summary>
    public static class MemoryBlockPool
    {
        private static readonly ObjectPool<MemoryBlock> _objectPool;
        private const long Tolerance = 268435456; // 256 MB
        static MemoryBlockPool()
        {
            _objectPool = new ObjectPool<MemoryBlock>(new MemoryBlockPoolAssistant(Tolerance));
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
        public static MemoryBlock? Get(long size, bool anyWithSize = false, int tolerance = 256) =>
            _objectPool.GetT(new MemoryBlockSpecs { Size = size, AnyWithSize = anyWithSize }, tolerance);

        internal struct MemoryBlockSpecs
        {
            public long Size;
            public bool AnyWithSize;
        }

        internal class MemoryBlockPoolAssistant : IObjectPoolAssistant<MemoryBlock>
        {
            private readonly long _tolerance;
            private readonly IDictionary<MemoryBlock, long> _cache;
            private long _current;
            public MemoryBlockPoolAssistant(long tolerance)
            {
                _tolerance = tolerance;
                _cache = new Dictionary<MemoryBlock, long>();
            }

            public bool IsClear()
            {
                if (_current > _tolerance)
                {
                    _current = 0;
                    return false;
                }
                return true;
            }

            public bool Evaluate(in MemoryBlock item, params object[] args)
            {
                var specs = (MemoryBlockSpecs)args.First(obj => obj is MemoryBlockSpecs);

                var result = specs.Size <= item.TotalSize && (specs.AnyWithSize || (item.TotalSize / specs.Size) <= 4);
                if(result == false)
                {
                    if (_cache.ContainsKey(item))
                        _cache[item] += item.TotalSize;
                    else
                        _cache[item] = item.TotalSize;
                    _current += item.TotalSize;
                }

                return result;
            }

            public MemoryBlock Create(params object[] args) 
            {
                var specs = (MemoryBlockSpecs)args.First(obj => obj is MemoryBlockSpecs);
                return new MemoryBlock(specs.Size); 
            }

            public void RegisterReturn(in MemoryBlock item) =>            
                _current += item.TotalSize;

            public void RegisterGet(in MemoryBlock item)
            {
                _current -= item.TotalSize;
                if (_cache.ContainsKey(item))
                {
                    _current -= _cache[item];
                    _cache[item] = 0;
                }
            }
        }
    }
}
