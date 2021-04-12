// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ez.Collections;

namespace Ez.Threading
{
    public class ThreadMethodEntry : IAsyncResult, IResettable, IDisposable
    {
        private readonly object _invokeSyncObject;
        private readonly ManualResetEventSlim _resetEvent;
        private Delegate _method;
        private object[] _args;

        internal ThreadMethodEntry()
        {
            _invokeSyncObject = new object();
            _resetEvent = new ManualResetEventSlim();
        }

        ~ThreadMethodEntry()
        {
            Dispose(false);
        }

        public bool Synchronous { get; private set; }

        public bool CompletedSynchronously => IsCompleted && Synchronous;

        public bool IsCompleted { get; private set; }

        public object ReturnValue { get; private set; }

        public Exception Exception { get; private set; }

        public int CallerThreadID { get; private set; }

        public void Reset()
        {
            _resetEvent.Set();
            _args = null;
            Synchronous = false;

            ReturnValue = null;
            IsCompleted = false;
            Exception = null;
            CallerThreadID = Thread.CurrentThread.ManagedThreadId;
        }

        public void Set()
        {
            _resetEvent.Reset();
        }

        public void Wait()
        {
            if(!IsCompleted)
                _resetEvent.Wait();
        }

        public void Wait(CancellationToken cancellationToken)
        {
            if (!IsCompleted)
                _resetEvent.Wait(cancellationToken);
        }

        public void Wait(int millisecondsTimeout)
        {
            if (!IsCompleted)
                _resetEvent.Wait(millisecondsTimeout);
        }

        public void Wait(TimeSpan timeout)
        {
            if (!IsCompleted)
                _resetEvent.Wait(timeout);
        }

        public void Wait(int millisecondsTimeout, CancellationToken cancellationToken)
        {
            if (!IsCompleted)
                _resetEvent.Wait(millisecondsTimeout, cancellationToken);
        }

        public void Wait(TimeSpan timeout, CancellationToken cancellationToken)
        {
            if (!IsCompleted)
                _resetEvent.Wait(timeout, cancellationToken);
        }


        internal void Invoke()
        {
            try
            {
                if (_method is MethodInvoker method)
                    method();
                else if (_method is EventHandler eventHandler)
                {
                    if (_args is null || _args.Length == 0)
                        eventHandler(null, EventArgs.Empty);
                    else if (_args.Length == 1)
                        eventHandler(_args[0], EventArgs.Empty);
                    else
                        eventHandler(_args[0], (EventArgs)_args[1]);
                }
                else if(_method is WaitCallback waitCallback)
                {
                    Debug.Assert(_args.Length == 1, "Number of argument is wrong for WaitCallback delegate.");
                    waitCallback(_args[0]);
                }
                else
                    ReturnValue = _method.DynamicInvoke(_args);
            }
            catch (Exception e)
            {
                Exception = e;
            }
            Complete();
        }

        internal void Initialize(Delegate method, object[] args, bool synchronous)
        {
            _method = method;
            _args = args;
            Synchronous = synchronous;
        }

        private void Complete()
        {
            lock (_invokeSyncObject)
            {
                IsCompleted = true;
                _resetEvent.Set();
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
                ThreadMethodEntryPool.Return(this);
            else
                _resetEvent.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        WaitHandle IAsyncResult.AsyncWaitHandle => _resetEvent.WaitHandle;
        object IAsyncResult.AsyncState => null;
    }
}
