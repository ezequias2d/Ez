// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
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
            Sync = sync;
            Monitor.Enter(Sync);
        }

        /// <summary>
        /// Destroys a instance of the <see cref="ConcurrentOperationList{T}"/> class.
        /// </summary>
        ~ConcurrentOperationList() => Dispose();

        #endregion Constructors/Destructors

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

        /// <summary>
        /// Gets the number of elements contained in the <see cref="ConcurrentOperationList{T}"/>.
        /// </summary>
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

        /// <summary>
        /// Gets a value indicating whether the <see cref="ConcurrentOperationList{T}"/> is read-only.
        /// </summary>
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

        /// <summary>
        /// The sync object.
        /// </summary>
        public object Sync { get; }

        #endregion Operators

        #region Methods
        /// <summary>
        /// Adds an item to the <see cref="ConcurrentOperationList{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ConcurrentOperationList{T}"/>.</param>
        public void Add(T item)
        {
            if (!disposed)
                _list.Add(item);
            else
                throw new ObjectDisposedException(ToString());
        }

        /// <summary>
        /// Removes all items from the <see cref="ConcurrentOperationList{T}"/>.
        /// </summary>
        public void Clear()
        {
            if (!disposed)
                _list.Clear();
            else
                throw new ObjectDisposedException(ToString());
        }

        /// <summary>
        /// Copies the elements of the <see cref="ConcurrentOperationList{T}"/> to an <see cref="Array"/>,
        /// starting at a particular index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied
        /// from <see cref="ConcurrentOperationList{T}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (!disposed)
                _list.CopyTo(array, arrayIndex);
            else
                throw new ObjectDisposedException(ToString());
        }

        /// <summary>
        /// Inserts an item to the <see cref="ConcurrentOperationList{T}"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="ConcurrentOperationList{T}"/>.</param>
        public void Insert(int index, T item)
        {
            if (!disposed)
                _list.Insert(index, item);
            else
                throw new ObjectDisposedException(ToString());
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ConcurrentOperationList{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ConcurrentOperationList{T}"/>.</param>
        /// <returns><see langword="true"/> if item was successfully removed from the 
        /// <see cref="ConcurrentOperationList{T}"/>; otherwise, <see langword="false"/>.</returns>
        public bool Remove(T item)
        {
            if (!disposed)
                return _list.Remove(item);
            else
                throw new ObjectDisposedException(ToString());
        }

        /// <summary>
        /// Removes the <see cref="ConcurrentOperationList{T}"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            if (!disposed)
                _list.RemoveAt(index);
            else
                throw new ObjectDisposedException(ToString());
        }

        /// <summary>
        /// Releases the sync object.
        /// </summary>
        public void Dispose()
        {
            if (!disposed)
                Monitor.Exit(Sync);
            disposed = true;
        }
        #endregion Methods

        #region Functions

        /// <summary>
        /// Determines whether the <see cref="ConcurrentOperationList{T}"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="ConcurrentOperationList{T}"/>.</param>
        /// <returns><see langword="true"/> if item is found in the <see cref="ConcurrentOperationList{T}"/>; otherwise, 
        /// <see langword="false"/>.</returns>
        public bool Contains(T item)
        {
            if(!disposed)
                return _list.Contains(item);
            else
                throw new ObjectDisposedException(ToString());
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            if(!disposed)
                return _list.GetEnumerator();
            else
                throw new ObjectDisposedException(ToString());
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="ConcurrentOperationList{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="ConcurrentOperationList{T}"/>.</param>
        /// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            if(!disposed)
                return _list.IndexOf(item);
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
