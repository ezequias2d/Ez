// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Ez.Collections
{
    /// <summary>
    /// A wrapper for the list that synchronizes using a "Sync" object until it is IsDisposed.
    /// </summary>
    /// <typeparam name="T">Type of elements using in <see cref="ISynchronizedList{T}"/> interface.</typeparam>
    public class ConcurrentOperationList<T> : Disposable, ISynchronizedList<T>
    {
        private readonly IList<T> _list;

        #region Constructors/Destructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentOperationList{T}"/> class that wraps a <see cref="IList{T}"/> instance and define a <see cref="Sync"/> object.
        /// </summary>
        /// <param name="list">Wrapped instance.</param>
        /// <param name="sync">Sync instance.</param>
        public ConcurrentOperationList(IList<T> list, ReaderWriterLockSlim @lock)
        {
            _list = list;
            Lock = @lock;

            Lock.EnterWriteLock();
        }

        #endregion Constructors/Destructors

        #region Operators

        /// <inheritdoc/>
        public T this[int index]
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(ToString());

                return _list[index];
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(ToString());

                _list[index] = value;
            }
        }

        /// <inheritdoc/>
        public int Count
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(ToString());

                return _list.Count;
            }
        }

        /// <inheritdoc/>
        public bool IsReadOnly
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(ToString());
                
                return _list.IsReadOnly;
            }
        }

        /// <inheritdoc/>
        public ReaderWriterLockSlim Lock { get; }

        #endregion Operators

        #region Methods
        /// <inheritdoc/>
        public void Add(T item)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ToString());

            _list.Add(item);            
        }

        /// <inheritdoc/>
        public void Clear()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ToString());

            _list.Clear();
        }

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ToString());
            
            _list.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public void Insert(int index, T item)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ToString());

            _list.Insert(index, item);
        }

        /// <inheritdoc/>
        public bool Remove(T item)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ToString());

            return _list.Remove(item);            
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ToString());

            _list.RemoveAt(index);
        }
        #endregion Methods

        #region Functions

        /// <inheritdoc/>
        public bool Contains(T item)
        {
            if(IsDisposed)
                throw new ObjectDisposedException(ToString());

            return _list.Contains(item);
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            if(IsDisposed)
                throw new ObjectDisposedException(ToString());
            return _list.GetEnumerator();
        }

        /// <inheritdoc/>
        public int IndexOf(T item)
        {
            if(IsDisposed)
                throw new ObjectDisposedException(ToString());
            
            return _list.IndexOf(item);
        }

        #endregion Functions

        #region Private Interface
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc/>
        protected override void UnmanagedDispose()
        {
            Lock.ExitWriteLock();
        }

        /// <inheritdoc/>
        protected override void ManagedDispose()
        {
            // not used
        }

        #endregion Private Interface
    }
}
