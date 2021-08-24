// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace Ez.Collections.Pools
{
    /// <summary>
    /// An object that contains a value and is saved in an object pool.
    /// </summary>
    /// <typeparam name="T">Type of value into pool.</typeparam>
    /// <typeparam name="TSpecs">Type of TSpec in <see cref="PooledObject{T, TSpecs}.Source"/>.</typeparam>
    internal sealed class PooledObject<T, TSpecs> : PooledObject<T>
    {
        internal PooledObject(ObjectPool<T, TSpecs> objectPool) : base()
        {
            Source = objectPool;
            IsTemporaryUse = true;
        }

        /// <summary>
        /// The source <see cref="ObjectPool{T, TSpecs}"/> of this <see cref="PooledObject{T, TSpecs}"/>.
        /// </summary>
        public ObjectPool<T, TSpecs> Source { get; }

        protected override void ManagedDispose()
        {
            Source.Return(this);
        }

        protected override void UnmanagedDispose()
        {
            T aux = _value;
            
            if (IsTemporaryUse && !aux.Equals(default(T)))
                Source.Return(aux);

            _value = default;
        }

        internal override void Undispose()
        {
            IsDisposed = false;
            GC.ReRegisterForFinalize(this);
        }
    }

    /// <summary>
    /// An object that contains a value and is saved in an object pool.
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    public abstract class PooledObject<T> : Disposable, IResettable
    {
        private protected T _value;        

        internal PooledObject()
        {
            IsTemporaryUse = true;
        }

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

        internal void UpdateValue(in T value)
        {
            _value = value;
        }

        internal abstract void Undispose();
    }
}
