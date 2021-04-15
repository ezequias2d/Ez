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

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentList{T}"/> class that wraps a <see cref="IList{T}"/> instance and define an instance to be used as synchronize.
        /// </summary>
        /// <param name="list">Wrapped instance.</param>
        /// <param name="sync">Sync instance.</param>
        public ConcurrentList(IList<T> list, object sync)
        {
            _list = list;
            Sync = sync;
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
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public T this[int index]
        {
            get
            {
                lock (Sync)
                {
                    return _list[index];
                }
            }
            set
            {
                lock (Sync)
                {
                    _list[index] = value;
                }
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="ConcurrentList{T}"/>.
        /// </summary>
        public int Count
        {
            get
            {
                lock (Sync)
                {
                    return _list.Count;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ConcurrentList{T}"/> is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return _list.IsReadOnly;
            }
        }

        /// <summary>
        /// The sync object.
        /// </summary>
        public object Sync { get; }

        #endregion Operators

        #region Methods
        /// <summary>
        /// Adds an item to the <see cref="ConcurrentList{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ConcurrentList{T}"/>.</param>
        public void Add(T item)
        {
            lock (Sync)
            {
                _list.Add(item);
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="ConcurrentList{T}"/>.
        /// </summary>
        public void Clear()
        {
            lock (Sync)
            {
                _list.Clear();
            }
        }

        /// <summary>
        /// Copies the elements of the <see cref="ConcurrentList{T}"/> to an <see cref="Array"/>,
        /// starting at a particular index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied
        /// from <see cref="ConcurrentList{T}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (Sync)
            {
                _list.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Inserts an item to the <see cref="ConcurrentList{T}"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="ConcurrentList{T}"/>.</param>
        public void Insert(int index, T item)
        {
            lock (Sync)
            {
                _list.Insert(index, item);
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ConcurrentList{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ConcurrentList{T}"/>.</param>
        /// <returns><see langword="true"/> if item was successfully removed from the 
        /// <see cref="ConcurrentList{T}"/>; otherwise, <see langword="false"/>.</returns>
        public bool Remove(T item)
        {
            lock (Sync)
            {
                return _list.Remove(item);
            }
        }

        /// <summary>
        /// Removes the <see cref="ConcurrentList{T}"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            lock (Sync)
            {
                _list.RemoveAt(index);
            }
        }
        #endregion Methods

        #region Functions
        /// <summary>
        /// Determines whether the <see cref="ConcurrentList{T}"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="ConcurrentList{T}"/>.</param>
        /// <returns><see langword="true"/> if item is found in the <see cref="ConcurrentList{T}"/>; otherwise, 
        /// <see langword="false"/>.</returns>
        public bool Contains(T item)
        {
            lock (Sync)
            {
                return _list.Contains(item);
            }
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="ConcurrentList{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="ConcurrentList{T}"/>.</param>
        /// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            lock (Sync)
            {
                return _list.IndexOf(item);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new SafeEnumerator<T>(_list.GetEnumerator(), Sync);
        }

        /// <summary>
        /// Create a instance of <see cref="ConcurrentOperationList{T}"/> for this <see cref="ConcurrentList{T}"/>.
        /// </summary>
        /// <returns>Instance of <see cref="ConcurrentOperationList{T}"/></returns>
        public ConcurrentOperationList<T> GetOperationList()
        {
            return new ConcurrentOperationList<T>(_list, Sync);
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
