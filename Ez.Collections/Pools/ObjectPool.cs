// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Concurrent;

namespace Ez.Collections.Pools
{
    /// <summary>
    /// A pool of objects.
    /// </summary>
    /// <typeparam name="T">T object</typeparam>
    public sealed class ObjectPool<T>
    {
        private readonly ConcurrentQueue<PooledObject<T>> _objectWrapper;
        private readonly ConcurrentQueue<PooledObject<T>> _bag;
        private readonly IObjectPoolAssistant<T> _assistant;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPool{T}"/> class that is empty.
        /// </summary>
        /// <param name="assistant"></param>
        public ObjectPool(IObjectPoolAssistant<T> assistant)
        {
            _assistant = assistant;
            _bag = new ConcurrentQueue<PooledObject<T>>();
            _objectWrapper = new ConcurrentQueue<PooledObject<T>>();
        }

        /// <summary>
        /// Number of elements in the <see cref="ObjectPool{T}"/>.
        /// </summary>
        public int Count => _bag.Count;

        /// <summary>
        /// Get a wrapper with a T object.
        /// </summary>
        /// <param name="args">Arguments for the object taken from the pool.</param>
        /// <returns>A <see cref="PooledObject{T}"/> object that the <see cref="PooledObject{T}.Value"/> is validated with <paramref name="args"/>.</returns>
        public PooledObject<T> Get(params object[] args)
        {
            if (!TryGet(out var result, args))
            {
                result = GetWrapper();
                result.UpdateValue(_assistant.Create(args));
            }

            result.Undispose();

            return result;
        }

        /// <summary>
        /// Get a T object.
        /// </summary>
        /// <param name="args">Arguments for the object taken from the pool.</param>
        /// <returns>A T object that is validated with <paramref name="args"/>.</returns>
        public T GetT(params object[] args)
        {
            if (!TryGetT(out var result, args))
                result = _assistant.Create(args);

            return result;
        }

        /// <summary>
        /// Try get a wrapper with a T object.
        /// </summary>
        /// <param name="pooledObject">A wrapper with a T object.</param>
        /// <param name="args">Arguments for the object taken from the pool.</param>
        /// <returns>
        /// If failure to find an object whose <paramref name="args"/> are valid,
        ///  then returns <see langword="false"/> and <paramref name="pooledObject"/> = <see langword="null"/>,
        /// otherwise, returns <see langword="true"/> and a <see cref="PooledObject{T}"/> with a valid <see cref="PooledObject{T}.Value"/> 
        /// in <paramref name="pooledObject"/>.
        /// </returns>
        public bool TryGet(out PooledObject<T> pooledObject, params object[] args)
        {
            var count = _bag.Count;

            // As long as tolerance does not burst and continue search
            while (count > 0)
            {
                count--;

                //try to catch
                if (_bag.TryDequeue(out var po))
                {
                    //check if the object is acceptable
                    if (_assistant.Evaluate(po.Value, args))
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
        /// <param name="args">Arguments for the object taken from the pool.</param>
        /// <returns>If failure to find an object whose <paramref name="args"/> are valid,
        ///  then returns <see langword="false"/> and <paramref name="value"/> = <see langword="default"/>,
        /// otherwise, returns <see langword="true"/> and a valid <paramref name="value"/>.</returns>
        public bool TryGetT(out T value, params object[] args)
        {
            {
                if (!TryGet(out var pooledObject, args))
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
            PooledObject<T> wrapper = GetWrapper();
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
        internal void Return(in PooledObject<T> wrapper)
        {
            if (wrapper.Source != this)
                throw new EzException($"The {nameof(wrapper)} is not created by same ObjectPool.");

            _objectWrapper.Enqueue(wrapper);

            if (_objectWrapper.Count > 40000)
                ClearWrappers();
        }

        /// <summary>
        /// Removes and disposes all of <see cref="PooledObject{T}"/> and your values in this <see cref="ObjectPool{T}"/>.
        /// </summary>
        public void Clear()
        {
            while (!_bag.IsEmpty)
            {
                _bag.TryDequeue(out PooledObject<T> pooledObject);

                pooledObject.IsTemporaryUse = false;

                if (pooledObject.Value is IDisposable disposable)
                    disposable.Dispose();

                pooledObject.Dispose();
            }
            ClearWrappers();
        }

        /// <summary>
        /// Removes and disposes all of unused <see cref="PooledObject{T}"/> in this <see cref="ObjectPool{T}"/>
        /// </summary>
        public void ClearWrappers()
        {
            while (!_objectWrapper.IsEmpty)
                _objectWrapper.TryDequeue(out PooledObject<T> _);
        }

        private PooledObject<T> GetWrapper()
        {
            if (!_objectWrapper.TryDequeue(out PooledObject<T> result))
                result = new PooledObject<T>(this);
            return result;
        }
    }
}
