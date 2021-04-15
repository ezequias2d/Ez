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
    /// A wrapper that makes an enumerator secure using a synchronization object.
    /// </summary>
    /// <typeparam name="T">The type of objects to enumerate.</typeparam>
    public sealed class SafeEnumerator<T> : IEnumerator<T>, IEnumerator, ISynchronizable, IDisposable
    {
        private readonly IEnumerator<T> inner;
        private readonly object _sync;
        private bool disposed;

        #region Constructors/Destructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeEnumerator{T}"/> class that 
        /// contains an <see cref="IEnumerator{T}"/> and a sync object.
        /// </summary>
        /// <param name="inner">Wrapped instance.</param>
        /// <param name="sync">Sync instance.</param>
        public SafeEnumerator(IEnumerator<T> inner, object sync)
        {
            this.inner = inner;
            this._sync = sync;
            this.disposed = false;

            Monitor.Enter(sync);
        }

        /// <summary>
        /// Destroys a instance of <see cref="SafeEnumerator{T}"/> class.
        /// </summary>
        ~SafeEnumerator()
        {
            Dispose(false);
        }

        #endregion Constructors/Destructors

        #region Operators
        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public T Current 
        { 
            get 
            {
                if (disposed)
                    throw new ObjectDisposedException("SafeEnumerator");
                return inner.Current; 
            } 
        }

        /// <summary>
        /// Gets the sync object.
        /// </summary>
        public object Sync => _sync;

        object IEnumerator.Current => Current;

        #endregion Operators

        #region Methods
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                }
                Monitor.Exit(_sync);
                disposed = true;
            }
        }

        /// <summary>
        /// Dispose a instance of <see cref="SafeEnumerator{T}"/> class and release the <see cref="Sync"/> object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns><see langword="true"/> if the enumerator was successfully advanced to 
        /// the next element; <see langword="false"/> if the enumerator has passed the end 
        /// of the collection.</returns>
        public bool MoveNext()
        {
            if (disposed)
                throw new ObjectDisposedException("SafeEnumerator");
            return inner.MoveNext();
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            if (disposed)
                throw new ObjectDisposedException("SafeEnumerator");
            inner.Reset();
        }

        #endregion Methods
    }
}
