using Ez.Collections.Pools;

namespace Ez.Memory
{
    public static class MemoryBlockPool
    {
        private static readonly ObjectPool<MemoryBlock, MemoryBlockSpecs> _objectPool;
        private const ulong Tolerance = 268435456; // 256 MB
        static MemoryBlockPool()
        {
            _objectPool = new ObjectPool<MemoryBlock, MemoryBlockSpecs>(new MemoryBlockPoolAssistant(Tolerance));
        }

        public static void Return(MemoryBlock memoryBlock) =>
            _objectPool.Return(memoryBlock);
        
        public static MemoryBlock Get(ulong size, bool anyWithSize = false, int tolerance = 256) =>
            _objectPool.GetT(new MemoryBlockSpecs { Size = size, AnyWithSize = anyWithSize }, tolerance);

        internal struct MemoryBlockSpecs
        {
            public ulong Size;
            public bool AnyWithSize;
        }

        internal class MemoryBlockPoolAssistant : IObjectPoolAssistant<MemoryBlock, MemoryBlockSpecs>
        {
            private readonly ulong _tolerance;
            private ulong _totalUsed;
            public MemoryBlockPoolAssistant(ulong tolerance)
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

            public bool MeetsExpectation(in MemoryBlock item, in MemoryBlockSpecs specs, int currentTolerance) =>
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
