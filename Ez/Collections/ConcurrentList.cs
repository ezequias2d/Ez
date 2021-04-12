// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ez.Collections
{
    /// <summary>
    /// Represents a thread-safe wrapping for <see cref="IList{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of elements using in <see cref="ISynchronizedList{T}"/> interface.</typeparam>
    public class ConcurrentList<T> : ISynchronizedList<T>
    {
        private readonly IList<T> _list;
        private readonly object _sync;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentList{T}"/> class that wraps a <see cref="IList{T}"/> instance and define an instance to be used as synchronize.
        /// </summary>
        /// <param name="list">Wrapped instance.</param>
        /// <param name="sync">Sync instance.</param>
        public ConcurrentList(IList<T> list, object sync)
        {
            _list = list;
            _sync = sync;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentList{T}"/> class that wraps a <see cref="IList{T}"/> instance.
        /// </summary>
        /// <param name="list">Wrapped instance.</param>
        public ConcurrentList(IList<T> list) : this(list, new object())
        {

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
        public T this[int index]
        {
            get
            {
                lock (_sync)
                {
                    return _list[index];
                }
            }
            set
            {
                lock (_sync)
                {
                    _list[index] = value;
                }
            }
        }

        public int Count
        {
            get
            {
                lock (_sync)
                {
                    return _list.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _list.IsReadOnly;
            }
        }

        public object Sync => _sync;

        #endregion Operators

        #region Methods
        public void Add(T item)
        {
            lock (_sync)
            {
                _list.Add(item);
            }
        }

        public void Clear()
        {
            lock (_sync)
            {
                _list.Clear();
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_sync)
            {
                _list.CopyTo(array, arrayIndex);
            }
        }

        public void Insert(int index, T item)
        {
            lock (_sync)
            {
                _list.Insert(index, item);
            }
        }

        public bool Remove(T item)
        {
            lock (_sync)
            {
                return _list.Remove(item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (_sync)
            {
                _list.RemoveAt(index);
            }
        }
        #endregion Methods

        #region Functions
        public bool Contains(T item)
        {
            lock (_sync)
            {
                return _list.Contains(item);
            }
        }

        public int IndexOf(T item)
        {
            lock (_sync)
            {
                return _list.IndexOf(item);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SafeEnumerator<T>(_list.GetEnumerator(), _sync);
        }

        public ReadOnlySpan<T> GetReadOnlySpan()
        {
            lock(_sync)
                return this.ToArray();
        }

        /// <summary>
        /// Create a instance of <see cref="ConcurrentOperationList{T}"/> for this <see cref="ConcurrentList{T}"/>.
        /// </summary>
        /// <returns>Instance of <see cref="ConcurrentOperationList{T}"/></returns>
        public ConcurrentOperationList<T> GetOperationList()
        {
            return new ConcurrentOperationList<T>(_list, _sync);
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
