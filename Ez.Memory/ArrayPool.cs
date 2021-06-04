// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Collections.Pools;
using System;

namespace Ez.Memory
{
    /// <summary>
    /// Represents an array pool.
    /// </summary>
    /// <typeparam name="T"></typeparam>
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

        /// <summary>
        /// Returns an array to the pool.
        /// </summary>
        /// <param name="array">The returned array.</param>
        public static void Return(T[] array)
        {
            _objectPool.Return(array);
        }

        /// <summary>
        /// Gets an array from the pool.
        /// </summary>
        /// <param name="size">The minimum size expected.</param>
        /// <param name="anyWithSize">Flag that indicates that any array with at least the <paramref name="size"/>
        /// is valid to be get.</param>
        /// <param name="tolerance">Number of attempts to obtain an object with the proposed conditions.</param>
        /// <returns></returns>
        public static T[] GetT(long size, bool anyWithSize = false, int tolerance = 256) =>
            _objectPool.GetT(new ArraySpecs { Size = size, AnyWithSize = anyWithSize }, tolerance);


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

            public bool Evaluate(in T[] item, in ArraySpecs specs, int currentTolerance) =>
                item.LongLength >= specs.Size && 
                    (currentTolerance == 0 || (item.LongLength / specs.Size <= 1) || specs.AnyWithSize);

            public T[] Create(in ArraySpecs specs) => new T[specs.Size];
        }
    }
}
