// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ez.Graphics.API.Vulkan.Core.Cached
{
    internal class Cache<TCreateInfo, TCached> : Disposable
    {
        public delegate TCached CreateCached(in TCreateInfo createInfo);

        private IDictionary<TCreateInfo, Reference> _cache;
        public Cache(CreateCached create)
        {
            _cache = new ConcurrentDictionary<TCreateInfo, Reference>();
            Create = create;
        }

        public virtual CreateCached Create { get; }

        public TCached Get(TCreateInfo createInfo)
        {
            if (_cache.TryGetValue(createInfo, out var reference))
            {
                reference.Increment();
                return reference.Value;
            }

            var cached = Create(createInfo);
            _cache.Add(createInfo, new Reference(cached));

            return cached;
        }

        public void Return(TCreateInfo createInfo)
        {
            if (_cache.TryGetValue(createInfo, out var reference))
            {
                reference.Decrement();
            }
        }

        public async Task<TCached> GetAsync(TCreateInfo createInfo) =>
            await Task.Run(() => Get(createInfo));

        protected override void UnmanagedDispose()
        {
        }

        protected override void ManagedDispose()
        {
            _cache = null;
        }

        private class Reference
        {
            public Reference(TCached value)
            {
                _referenceCount = 0;
                Value = value;
            }

            private ulong _referenceCount;

            public event Action<TCreateInfo> KeyPropertyChange;
            public TCached Value { get; }

            public ulong ReferenceCount
            {
                get
                {
                    lock (this)
                    {
                        return _referenceCount;
                    }
                }
            }

            public void Increment()
            {
                lock (this)
                {
                    _referenceCount++;
                }
            }

            public void Decrement()
            {
                lock (this)
                {
                    _referenceCount--;
                }
            }
        }
    }
}
