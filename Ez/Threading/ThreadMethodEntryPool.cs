// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Collections.Pools;
using System;

namespace Ez.Threading
{
    internal static class ThreadMethodEntryPool
    {
        private readonly static ObjectPool<ThreadMethodEntry, ThreadMethodEntrySpec> _objectPool;

        static ThreadMethodEntryPool()
        {
            _objectPool = new ObjectPool<ThreadMethodEntry, ThreadMethodEntrySpec>(new ThreadMethodEntryAssistant());
        }

        public static ThreadMethodEntry Get() => _objectPool.GetT();

        public static void Return(in ThreadMethodEntry entry) => _objectPool.Return(entry);

        private struct ThreadMethodEntrySpec 
        {
        }

        private class ThreadMethodEntryAssistant : IObjectPoolAssistant<ThreadMethodEntry, ThreadMethodEntrySpec>
        {
            private readonly long CountMax = 128;
            private long count;
            public ThreadMethodEntry Create(in ThreadMethodEntrySpec specs) => new ThreadMethodEntry();

            public bool IsClear() =>
                count >= CountMax;

            public bool MeetsExpectation(in ThreadMethodEntry item, in ThreadMethodEntrySpec specs, int currentTolerance)
            {
                return true;
            }

            public void RegisterGet(in ThreadMethodEntry item)
            {
                lock (this) { count--; }
            }

            public void RegisterReturn(in ThreadMethodEntry item)
            {
                lock (this) { count++; }
            }
        }
    }
}
