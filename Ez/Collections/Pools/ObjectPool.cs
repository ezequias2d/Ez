// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ez.Collections.Pools
{
    /// <summary>
    /// A pool of objects.
    /// </summary>
    /// <typeparam name="T">T object</typeparam>
    /// <typeparam name="PooledObject<T, TSpecs>">Wrapper of T object</typeparam>
    public sealed class ObjectPool<T, TSpecs>
    {
        private readonly ConcurrentQueue<PooledObject<T, TSpecs>> _objectWrapper;
        private readonly ConcurrentQueue<PooledObject<T, TSpecs>> _bag;
        private readonly IObjectPoolAssistant<T, TSpecs> _assistant;

        public ObjectPool(IObjectPoolAssistant<T, TSpecs> assistant)
        {
            _assistant = assistant;
            _bag = new ConcurrentQueue<PooledObject<T, TSpecs>>();
            _objectWrapper = new ConcurrentQueue<PooledObject<T, TSpecs>>();
        }

        public int Count => _bag.Count;

        /// <summary>
        /// Get a wrapper with a T object.
        /// </summary>
        /// <param name="specs">Object specifications.</param>
        /// <param name="tolerance">Maximum number of attempts to acquire an object with specifications.</param>
        /// <returns></returns>
        public PooledObject<T> Get(TSpecs specs = default, int tolerance = 8)
        {
            if (!TryGet(out var result, specs, tolerance))
            {
                result = GetWrapper();
                result.UpdateValue(_assistant.Create(specs));
            }

            result.Undispose();

            return result;
        }

        public T GetT(TSpecs specs = default, int tolerance = 8)
        {
            if(!TryGetT(out var result, specs, tolerance))
                result = _assistant.Create(specs);

            return result;
        }

        /// <summary>
        /// Try get a wrapper with a T object. (don't create if you can't find)
        /// </summary>
        /// <param name="pooledObject">A wrapper with a T object.</param>
        /// <returns>True if found, otherwise false.</returns>
        public bool TryGet(out PooledObject<T> pooledObject, in TSpecs specs = default, int tolerance = 8)
        {
            tolerance = Math.Min(tolerance, _bag.Count);

            // As long as tolerance does not burst and continue search
            while (tolerance > 0)
            {
                tolerance--;

                //try to catch
                if (_bag.TryDequeue(out var po))
                {
                    //check if the object is acceptable
                    if (_assistant.MeetsExpectation(po.Value, specs, tolerance))
                    {
                        pooledObject = po;
                        po.Set();
                        _assistant.RegisterGet(pooledObject.Value);
                        return true;
                    }
                    else
                        _bag.Enqueue(po);
                }
            }

            // create if you can't find.
            pooledObject = default;
            return false;
        }

        public bool TryGetT(out T value, in TSpecs specs = default, in int tolerance = 8)
        {
            {
                if(!TryGet(out var pooledObject, specs, tolerance))
                {
                    value = default;
                    return false;
                }

                using (pooledObject)
                {
                    pooledObject.IsTemporaryUse = false;
                    value = pooledObject.Value;
                }
            }

            return true;
        }

        /// <summary>
        /// Only return a T object.
        /// </summary>
        /// <param name="item">Value saved in pool.</param>
        public void Return(in T item)
        {
            PooledObject<T, TSpecs> wrapper = GetWrapper();
            wrapper.UpdateValue(item);
            wrapper.Reset();
            _bag.Enqueue(wrapper);

            _assistant.RegisterReturn(item);

            if (!_assistant.IsClear())
                 Clear();
        }

        /// <summary>
        /// Only return a wrapper for a T object.
        /// </summary>
        /// <param name="wrapper">Wrapper of a T object.</param>
        internal void Return(in PooledObject<T, TSpecs> wrapper)
        {
            wrapper.UpdateValue(default);
            _objectWrapper.Enqueue(wrapper);

            if (_objectWrapper.Count > 40000)
                ClearWrappers();
        }

        /// <summary>
        /// Remove and dispose all <see cref="PooledObject<T, TSpecs>"/> in this pool.
        /// </summary>
        public void Clear()
        {
            while (!_bag.IsEmpty)
            {
                _bag.TryDequeue(out PooledObject<T, TSpecs> pooledObject);

                if (pooledObject is IDisposable disposable)
                    disposable.Dispose();
            }
        }

        public void ClearWrappers()
        {
            while (!_objectWrapper.IsEmpty)
                _objectWrapper.TryDequeue(out PooledObject<T, TSpecs> _);
        }

        private PooledObject<T, TSpecs> GetWrapper()
        {
            if (!_objectWrapper.TryDequeue(out PooledObject<T, TSpecs> result))
                result = new PooledObject<T, TSpecs>(this);
            return result;
        }
    }
}
