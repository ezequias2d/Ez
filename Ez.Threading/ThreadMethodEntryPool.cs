// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Collections.Pools;
using System;

namespace Ez.Threading
{
    internal class ThreadMethodEntryPool
    {
        private readonly ObjectPool<ThreadMethodEntry> _objectPool;

        public ThreadMethodEntryPool(ThreadMethodExecutor executor)
        {
            _objectPool = new ObjectPool<ThreadMethodEntry>(new ThreadMethodEntryAssistant(executor));
        }

        public ThreadMethodEntry Get() => _objectPool.GetT();

        public void Return(in ThreadMethodEntry entry) => _objectPool.Return(entry);

        private class ThreadMethodEntryAssistant : IObjectPoolAssistant<ThreadMethodEntry>
        {
            private readonly long CountMax = 128;
            private long count;
            private readonly ThreadMethodExecutor _executor;

            public ThreadMethodEntryAssistant(ThreadMethodExecutor executor)
            {
                _executor = executor;
            }

            public ThreadMethodEntry Create(params object[] args) => new(_executor);

            public bool IsClear() =>
                count <= CountMax;

            public bool Evaluate(in ThreadMethodEntry item, params object[] args)
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
