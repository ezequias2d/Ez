using Ez.Collections.Pools;
using System;

namespace Ez.Memory
{
    public static class ArrayPool<T>
    {
        private static readonly ObjectPool<T[], ArraySpecs> _objectPool;
        private const long ArrayCountTolerance = 16384;
        private const long Tolerance = 16777216; // 16 MB
        private const double VMTolerance = 4096;
        static ArrayPool()
        {
            _objectPool = new ObjectPool<T[], ArraySpecs>(new MemoryObjectPoolAssistant());
        }

        public static void Return(T[] memoryBlock)
        {
            _objectPool.Return(memoryBlock);
        }

        public static T[] GetT(long size, bool anyWithSize = false, int tolerance = 256) =>
            _objectPool.GetT(new ArraySpecs { Size = size, AnyWithSize = anyWithSize }, tolerance);

        public static PooledObject<T[]> Get(long size, bool anyWithSize = false, int tolerance = 256) =>
            _objectPool.Get(new ArraySpecs { Size = size, AnyWithSize = anyWithSize }, tolerance);


        private struct ArraySpecs
        {
            public long Size;
            public bool AnyWithSize;
        }

        private class MemoryObjectPoolAssistant : IObjectPoolAssistant<T[], ArraySpecs>
        {
            private long ArrayCount;
            private long ArrayElementCount;
            private double Med;
            private double VM;
            public MemoryObjectPoolAssistant()
            {
                ArrayCount = ArrayElementCount = 0;
                Med = VM = 0;
            }

            public bool IsClear() =>
                ArrayCount >= ArrayCountTolerance ||
                ArrayElementCount >= Tolerance ||
                VM > VMTolerance;

            public void RegisterReturn(in T[] item)
            {
                lock (this)
                {
                    double div = 1d / (ArrayCount + 1);
                    Med = (Med * ArrayCount + item.LongLength) * div;
                    VM = (VM * ArrayCount + Math.Abs(Med - item.LongLength)) * div;

                    ArrayCount++;
                    ArrayElementCount += item.LongLength;
                }
            }

            public void RegisterGet(in T[] item)
            {
                lock (this)
                {
                    double div = 1d / (ArrayCount - 1);
                    VM = (VM * ArrayCount - Math.Abs(Med - item.LongLength)) * div;
                    Med = (Med * ArrayCount - item.LongLength) * div;

                    ArrayCount--;
                    ArrayElementCount -= item.LongLength;
                }
            }

            public bool MeetsExpectation(in T[] item, in ArraySpecs specs, int currentTolerance) =>
                item.LongLength >= specs.Size && 
                    (currentTolerance == 0 || (item.LongLength / specs.Size <= 1) || specs.AnyWithSize);

            public T[] Create(in ArraySpecs specs) => new T[specs.Size];
        }
    }
}
