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
        /// 
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

        ~SafeEnumerator()
        {
            Dispose(false);
        }

        #endregion Constructors/Destructors

        #region Operators
        public T Current 
        { 
            get 
            {
                if (disposed)
                    throw new ObjectDisposedException("SafeEnumerator");
                return inner.Current; 
            } 
        }

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

        public void Dispose()
        {
            Dispose(true);
        }

        public bool MoveNext()
        {
            if (disposed)
                throw new ObjectDisposedException("SafeEnumerator");
            return inner.MoveNext();
        }
        public void Reset()
        {
            if (disposed)
                throw new ObjectDisposedException("SafeEnumerator");
            inner.Reset();
        }

        #endregion Methods
    }
}
