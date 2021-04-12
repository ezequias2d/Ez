using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections.Pools
{
    /// <summary>
    /// An object that contains a value and is saved in an object pool.
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
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

                if (IsTemporaryUse)
                    Source.Return(aux);

                _disposed = true;
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
        protected T _value;

        internal PooledObject()
        {
            IsTemporaryUse = true;
        }

        /// <summary>
        /// Flag indicating whether the value should be returned to the pool(true) when discarded with Dispose or not(false).
        /// </summary>
        public bool IsTemporaryUse { get; set; }

        ~PooledObject() => Dispose(false);

        public void Dispose() => Dispose(true);

        protected abstract void Dispose(bool disposing);
        internal abstract void Undispose();

        internal void UpdateValue(in T value)
        {
            _value = value;
        }

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

        public ref readonly T Value => ref _value;
    }
}
