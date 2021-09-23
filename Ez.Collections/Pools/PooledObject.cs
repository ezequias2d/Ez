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
    /// <typeparam name="T">Value type</typeparam>
    public class PooledObject<T> : IDisposable, IResettable
    {
        private protected T _value;
        private bool _disposed;

        internal PooledObject(ObjectPool<T> pool)
        {
            IsTemporaryUse = true;
            Source = pool;
        }

        /// <summary>
        /// The source <see cref="ObjectPool{T}"/> of this <see cref="PooledObject{T}"/>.
        /// </summary>
        public ObjectPool<T> Source { get; }

        /// <summary>
        /// Destroys this instance of <see cref="PooledObject{T}"/>.
        /// </summary>
        ~PooledObject() => Dispose(false);

        /// <summary>
        /// Flag indicating whether the value should be returned to the pool(true) when discarded with Dispose or not(false).
        /// </summary>
        public bool IsTemporaryUse { get; set; }

        /// <summary>
        /// Gets the value of pooled object.
        /// </summary>
        public ref readonly T Value => ref _value;

        /// <inheritdoc/>
        public void Reset()
        {
            if (_value is IResettable resettable)
                resettable.Reset();
        }

        /// <inheritdoc/>
        public void Set()
        {
            if (_value is IResettable resettable)
                resettable.Set();
        }

        /// <inheritdoc/>
        public void Dispose() => Dispose(true);

        internal void UpdateValue(in T value)
        {
            _value = value;
        }

        /// <summary>
        /// Disposes the <see cref="PooledObject{T}"/>.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
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

        internal void Undispose()
        {
            _disposed = false;
        }
    }
}
