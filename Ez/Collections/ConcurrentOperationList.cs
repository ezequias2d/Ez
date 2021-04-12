using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ez.Collections
{
    /// <summary>
    /// A wrapper for the list that synchronizes using a "Sync" object until it is disposed.
    /// </summary>
    /// <typeparam name="T">Type of elements using in <see cref="ISynchronizedList{T}"/> interface.</typeparam>
    public class ConcurrentOperationList<T> : ISynchronizedList<T>, IDisposable
    {
        private readonly IList<T> _list;
        private readonly object _sync;
        private bool disposed;

        #region Constructors/Destructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentOperationList{T}"/> class that wraps a <see cref="IList{T}"/> instance and define a <see cref="Sync"/> object.
        /// </summary>
        /// <param name="list">Wrapped instance.</param>
        /// <param name="sync">Sync instance.</param>
        public ConcurrentOperationList(IList<T> list, object sync)
        {
            disposed = false;
            _list = list;
            _sync = sync;
            Monitor.Enter(_sync);
        }

        ~ConcurrentOperationList()
        {
            Dispose();
        }

        #endregion Constructors/Destructors

        #region Operators
        public T this[int index]
        {
            get
            {
                if (!disposed)
                {
                    return _list[index];
                }
                throw new ObjectDisposedException(ToString());
            }
            set
            {
                if (!disposed)
                {
                    _list[index] = value;
                }
                throw new ObjectDisposedException(ToString());
            }
        }

        public int Count
        {
            get
            {
                if (!disposed)
                {
                    return _list.Count;
                }
                throw new ObjectDisposedException(ToString());
            }
        }

        public bool IsReadOnly
        {
            get
            {
                if (!disposed)
                {
                    return _list.IsReadOnly;
                }
                throw new ObjectDisposedException(ToString());
            }
        }

        public object Sync => _sync;

        #endregion Operators

        #region Methods
        public void Add(T item)
        {
            if (!disposed)
                _list.Add(item);
            else
                throw new ObjectDisposedException(ToString());
        }

        public void Clear()
        {
            if (!disposed)
                _list.Clear();
            else
                throw new ObjectDisposedException(ToString());
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (!disposed)
                _list.CopyTo(array, arrayIndex);
            else
                throw new ObjectDisposedException(ToString());
        }

        public void Insert(int index, T item)
        {
            if (!disposed)
                _list.Insert(index, item);
            else
                throw new ObjectDisposedException(ToString());
        }

        public bool Remove(T item)
        {
            if (!disposed)
                return _list.Remove(item);
            else
                throw new ObjectDisposedException(ToString());
        }

        public void RemoveAt(int index)
        {
            if (!disposed)
                _list.RemoveAt(index);
            else
                throw new ObjectDisposedException(ToString());
        }

        public void Dispose()
        {
            if (!disposed)
                Monitor.Exit(_sync);
            disposed = true;
        }
        #endregion Methods

        #region Functions

        public bool Contains(T item)
        {
            if(!disposed)
                return _list.Contains(item);
            else
                throw new ObjectDisposedException(ToString());
        }

        public IEnumerator<T> GetEnumerator()
        {
            if(!disposed)
                return _list.GetEnumerator();
            else
                throw new ObjectDisposedException(ToString());
        }

        public int IndexOf(T item)
        {
            if(!disposed)
                return _list.IndexOf(item);
            else
                throw new ObjectDisposedException(ToString());
        }

        public ReadOnlySpan<T> GetReadOnlySpan()
        {
            if (!disposed)
                return this.ToArray();
            else
                throw new ObjectDisposedException(ToString());
        }

        #endregion Functions

        #region Private Interface
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion Private Interface
    }
}
