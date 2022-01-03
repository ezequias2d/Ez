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
    public sealed class SafeEnumerator<T> : Disposable, IEnumerator<T>, IEnumerator, ISynchronizable, IDisposable
    {
        private readonly IEnumerator<T> _inner;

        #region Constructors/Destructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeEnumerator{T}"/> class that 
        /// contains an <see cref="IEnumerator{T}"/> and a sync object.
        /// </summary>
        /// <param name="inner">Wrapped instance.</param>
        /// <param name="lock">ReaderWriterLockSlim instance.</param>
        public SafeEnumerator(IEnumerator<T> inner, ReaderWriterLockSlim @lock)
        {
            this._inner = inner;
            Lock = @lock;

            Lock.EnterReadLock();
        }

        #endregion Constructors/Destructors

        #region Operators

        /// <inheritdoc/>
        public T Current 
        { 
            get 
            {
                if (IsDisposed)
                    throw GetObjectDisposedException();
                return _inner.Current; 
            } 
        }


        object? IEnumerator.Current => Current;

        /// <inheritdoc/>
        public ReaderWriterLockSlim Lock { get; }

        #endregion Operators

        #region Methods

        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (IsDisposed)
                throw GetObjectDisposedException();
            return _inner.MoveNext();
        }

        /// <inheritdoc/>
        public void Reset()
        {
            if (IsDisposed)
                throw GetObjectDisposedException();
            _inner.Reset();
        }

        /// <inheritdoc/>
        protected override void UnmanagedDispose()
        {
            Lock.ExitReadLock();
        }

        /// <inheritdoc/>
        protected override void ManagedDispose()
        {
            // not used
        }

        private static ObjectDisposedException GetObjectDisposedException() => new("SafeEnumerator");
        #endregion Methods
    }
}
