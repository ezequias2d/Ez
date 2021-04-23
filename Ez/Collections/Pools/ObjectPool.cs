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
    /// <typeparam name="TSpecs">Wrapper of T object</typeparam>
    public sealed class ObjectPool<T, TSpecs>
    {
        private readonly ConcurrentQueue<PooledObject<T, TSpecs>> _objectWrapper;
        private readonly ConcurrentQueue<PooledObject<T, TSpecs>> _bag;
        private readonly IObjectPoolAssistant<T, TSpecs> _assistant;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPool{T, TSpecs}"/> class that is empty.
        /// </summary>
        /// <param name="assistant"></param>
        public ObjectPool(IObjectPoolAssistant<T, TSpecs> assistant)
        {
            _assistant = assistant;
            _bag = new ConcurrentQueue<PooledObject<T, TSpecs>>();
            _objectWrapper = new ConcurrentQueue<PooledObject<T, TSpecs>>();
        }

        /// <summary>
        /// Number of elements in the <see cref="ObjectPool{T, TSpecs}"/>.
        /// </summary>
        public int Count => _bag.Count;

        /// <summary>
        /// Get a wrapper with a T object.
        /// </summary>
        /// <param name="specs">Specifications for the object taken from the pool.</param>
        /// <param name="tolerance">Maximum number of attempts to acquire an object with specifications.</param>
        /// <returns>A <see cref="PooledObject{T}"/> object that the <see cref="PooledObject{T}.Value"/> is validated with <paramref name="specs"/>.</returns>
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

        /// <summary>
        /// Get a T object.
        /// </summary>
        /// <param name="specs">Specifications for the object taken from the pool.</param>
        /// <param name="tolerance">Number of attempts to get an object corresponding to <paramref name="specs"/>.</param>
        /// <returns>A T object that is validated with <paramref name="specs"/>.</returns>
        public T GetT(TSpecs specs = default, int tolerance = 8)
        {
            if(!TryGetT(out var result, specs, tolerance))
                result = _assistant.Create(specs);

            return result;
        }

        /// <summary>
        /// Try get a wrapper with a T object.
        /// </summary>
        /// <param name="pooledObject">A wrapper with a T object.</param>
        /// <param name="specs">Specifications for the object taken from the pool.</param>
        /// <param name="tolerance">Number of attempts to get an object corresponding to <paramref name="specs"/>.</param>
        /// <returns>
        /// If tolerance &lt; 0, or
        ///  failure <paramref name="tolerance"/> times to find an object whose <paramref name="specs"/> are valid,
        ///  then returns <see langword="false"/> and <paramref name="pooledObject"/> = <see langword="null"/>,
        /// otherwise, returns <see langword="true"/> and a <see cref="PooledObject{T}"/> with a valid <see cref="PooledObject{T}.Value"/> 
        /// in <paramref name="pooledObject"/>.
        /// </returns>
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
                    if (_assistant.Evaluate(po.Value, specs, tolerance))
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

            pooledObject = default;
            return false;
        }

        /// <summary>
        /// Try get a T object.
        /// </summary>
        /// <param name="value">When this method returns <see langword="true"/>, contains the value that had got.</param>
        /// <param name="specs">Specifications for the object taken from the pool.</param>
        /// <param name="tolerance">Number of attempts to get an object corresponding to <paramref name="specs"/>.</param>
        /// <returns>If tolerance &lt; 0, or
        ///  failure <paramref name="tolerance"/> times to find an object whose <paramref name="specs"/> are valid,
        ///  then returns <see langword="false"/> and <paramref name="value"/> = <see langword="default"/>,
        /// otherwise, returns <see langword="true"/> and a valid <paramref name="value"/>.</returns>
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
            if (wrapper.Source != this)
                throw new EzException($"The {nameof(wrapper)} is not created by same ObjectPool.");

            _objectWrapper.Enqueue(wrapper);

            if (_objectWrapper.Count > 40000)
                ClearWrappers();
        }

        /// <summary>
        /// Removes and disposes all of <see cref="PooledObject{T}"/> and your values in this <see cref="ObjectPool{T, TSpecs}"/>.
        /// </summary>
        public void Clear()
        {
            while (!_bag.IsEmpty)
            {
                _bag.TryDequeue(out PooledObject<T, TSpecs> pooledObject);

                pooledObject.IsTemporaryUse = false;

                if (pooledObject.Value is IDisposable disposable)
                    disposable.Dispose();

                pooledObject.Dispose();
            }
            ClearWrappers();
        }

        /// <summary>
        /// Removes and disposes all of unused <see cref="PooledObject{T}"/> in this <see cref="ObjectPool{T, TSpecs}"/>
        /// </summary>
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
