// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Ez.Collections
{
    /// <summary>
    /// Represents a thread-safe wrapping for <see cref="IList{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of elements using in <see cref="ISynchronizedList{T}"/> interface.</typeparam>
    public class ConcurrentList<T> : ISynchronizedList<T>
    {
        private readonly IList<T> _list;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentList{T}"/> class that wraps a <see cref="IList{T}"/> instance.
        /// </summary>
        /// <param name="list">Wrapped instance.</param>
        public ConcurrentList(IList<T> list) 
        {
            _list = list;
            Lock = new(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentList{T}"/> class that wraps a new instance of <see cref="List{T}"/> class.
        /// </summary>
        public ConcurrentList() : this(new List<T>())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentList{T}"/> class that wraps a new instance of <see cref="List{T}"/> class that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">Collection</param>
        public ConcurrentList(IEnumerable<T> collection) : this(new List<T>(collection))
        {

        }

        #endregion Constructors

        #region Operators 
        /// <inheritdoc/>
        public T this[int index]
        {
            get
            {
                Lock.EnterReadLock();
                try
                {
                    return _list[index];
                }
                finally
                {
                    Lock.ExitReadLock();
                }
            }
            set
            {
                Lock.EnterWriteLock();
                try
                {
                    _list[index] = value;
                }
                finally
                {
                    Lock.ExitWriteLock();
                }
            }
        }

        /// <inheritdoc/>
        public int Count
        {
            get
            {
                Lock.EnterReadLock();
                try
                {
                    return _list.Count;
                }
                finally
                {
                    Lock.ExitReadLock();
                }
            }
        }

        /// <inheritdoc/>
        public bool IsReadOnly => _list.IsReadOnly;

        /// <inheritdoc/>
        public ReaderWriterLockSlim Lock { get; }

        #endregion Operators

        #region Methods
        /// <inheritdoc/>
        public void Add(T item)
        {
            Lock.EnterWriteLock();
            try
            {
                _list.Add(item);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public void Clear()
        {
            Lock.EnterWriteLock();
            try
            {
                _list.Clear();
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Lock.EnterReadLock();
            try
            {
                _list.CopyTo(array, arrayIndex);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        /// <inheritdoc/>
        public void Insert(int index, T item)
        {
            Lock.EnterWriteLock();
            try
            {
                _list.Insert(index, item);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public bool Remove(T item)
        {
            Lock.EnterWriteLock();
            try
            {
                return _list.Remove(item);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            Lock.EnterWriteLock();
            try
            {
                _list.RemoveAt(index);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }
        #endregion Methods

        #region Functions
        /// <inheritdoc/>
        public bool Contains(T item)
        {
            Lock.EnterReadLock();
            try
            {
                return _list.Contains(item);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        /// <inheritdoc/>
        public int IndexOf(T item)
        {
            Lock.EnterReadLock();
            try
            {
                return _list.IndexOf(item);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return new SafeEnumerator<T>(_list.GetEnumerator(), Lock);
        }

        /// <summary>
        /// Create a instance of <see cref="ConcurrentOperationList{T}"/> for this <see cref="ConcurrentList{T}"/>.
        /// </summary>
        /// <returns>Instance of <see cref="ConcurrentOperationList{T}"/></returns>
        public ConcurrentOperationList<T> GetOperationList()
        {
            return new ConcurrentOperationList<T>(_list, Lock);
        }
        #endregion Functions

        #region Private Interfaces
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion Private Interfaces
    }
}
