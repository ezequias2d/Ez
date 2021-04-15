// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections.Pools
{
    /// <summary>
    /// An object that contains a value and is saved in an object pool.
    /// </summary>
    /// <typeparam name="T">Type of value into pool.</typeparam>
    /// <typeparam name="TSpecs">Type of TSpec in <see cref="PooledObject{T, TSpecs}.Source"/>.</typeparam>
    internal sealed class PooledObject<T, TSpecs> : PooledObject<T>
    {
        private bool _disposed;

        internal PooledObject(ObjectPool<T, TSpecs> objectPool) : base()
        {
            Source = objectPool;
            IsTemporaryUse = true;
        }

        /// <summary>
        /// The source <see cref="ObjectPool{T, TSpecs}"/> of this <see cref="PooledObject{T, TSpecs}"/>.
        /// </summary>
        public ObjectPool<T, TSpecs> Source { get; }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                T aux = _value;

                if (disposing)
                    Source.Return(this);

                if (IsTemporaryUse && !aux.Equals(default))
                    Source.Return(aux);

                _disposed = true;
                _value = default;
            }
        }

        internal override void Undispose()
        {
            _disposed = false;
        }
    }

    /// <summary>
    /// An object that contains a value and is saved in an object pool.
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    public abstract class PooledObject<T> : IDisposable, IResettable
    {
        private protected T _value;        

        internal PooledObject()
        {
            IsTemporaryUse = true;
        }

        /// <summary>
        /// Destroys this instance of <see cref="PooledObject{T}"/>.
        /// </summary>
        ~PooledObject() => Dispose(false);

        /// <summary>
        /// Flag indicating whether the value should be returned to the pool(true) when discarded with Dispose or not(false).
        /// </summary>
        public bool IsTemporaryUse { get; set; }

        /// <summary>
        /// Gets
        /// </summary>
        public ref readonly T Value => ref _value;

        /// <summary>
        /// Reset the <see cref="Value"/> if it implements IResettable.
        /// </summary>
        public void Reset()
        {
            if (_value is IResettable resettable)
                resettable.Reset();
        }

        /// <summary>
        /// Set the <see cref="Value"/> if it implements IResettable.
        /// </summary>
        public void Set()
        {
            if (_value is IResettable resettable)
                resettable.Set();
        }

        /// <summary>
        /// Releases
        /// </summary>
        public void Dispose() => Dispose(true);

        internal void UpdateValue(in T value)
        {
            _value = value;
        }

        /// <summary>
        /// Disposes the <see cref="PooledObject{T}"/>.
        /// </summary>
        /// <param name="disposing"></param>
        protected abstract void Dispose(bool disposing);
        internal abstract void Undispose();
    }
}
