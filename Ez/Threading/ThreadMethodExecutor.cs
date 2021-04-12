// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Collections;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
namespace Ez.Threading
{
    public class ThreadMethodExecutor : IDisposable
    {
        private readonly Thread _thread;
        private readonly BlockingCollection<ThreadMethodEntry> _entries;
        private bool _disposed;
        public ThreadMethodExecutor(bool autostart = true)
        {
            _thread = new Thread(Main)
            {
                IsBackground = true
            };
            _entries = new BlockingCollection<ThreadMethodEntry>(new ConcurrentQueue<ThreadMethodEntry>());
            _disposed = false;

            if (autostart)
                StartThread();
        }

        ~ThreadMethodExecutor()
        {
            Dispose(false);
        }

        public event EventHandler Awake;
        public event EventHandler Start;
        public event EventHandler BeforeInvoking;
        public event EventHandler AfterInvoking;
        public event EventHandler<DisposeEventArgs> OnDispose;

        private void Main()
        {
            Awake?.Invoke(this, EventArgs.Empty);
            Start?.Invoke(this, EventArgs.Empty);

            ThreadMethodEntry methodEntry;
            while (!_disposed)
            {
                methodEntry = _entries.Take();

                if (!methodEntry.IsCompleted)
                {
                    BeforeInvoking?.Invoke(this, EventArgs.Empty);
                    methodEntry.Invoke();
                    AfterInvoking?.Invoke(this, EventArgs.Empty);
                }

            }
        }

        public void StartThread() => _thread.Start();

        public void Invoke(MethodInvoker methodInvoker) =>
            Invoke(methodInvoker, null, true);

        public void Invoke(EventHandler eventHandler, object sender, EventArgs args) =>
            Invoke(eventHandler, new object[] { sender, args }, true);

        public void Invoke(WaitCallback waitCallback, object state) =>
            Invoke(waitCallback, new object[] { state }, true);

        public object Invoke(Delegate method, params object[] args) =>
            Invoke(method, args, true);

        public T Invoke<T>(Func<T> method)
        {
            object obj = Invoke(method, null, true);

            if (obj is T t)
                return t;

            throw new EzThreadException($"The returned value is not of {typeof(T)} type, the returned value is of {obj.GetType()} type.");
        }

        public T Invoke<T>(Delegate method, params object[] args)
        {
            object obj = Invoke(method, args, true);

            if (obj is T t)
                return t;

            throw new EzThreadException($"The returned value is not of {typeof(T)} type, the returned value is of {obj.GetType()} type.");
        }

        private object Invoke(Delegate method, object[] args, bool synchronous)
        {
            ThreadMethodEntry methodEntry = ThreadMethodEntryPool.Get();
            methodEntry.Initialize(method, args, synchronous);

            _entries.Add(methodEntry);

            if (synchronous && (Thread.CurrentThread.ManagedThreadId == _thread.ManagedThreadId))
                methodEntry.Invoke();

            if (synchronous)
            {
                methodEntry.Wait();

                Exception exception = methodEntry.Exception;
                object returnValue = methodEntry.ReturnValue;

                ThreadMethodEntryPool.Return(methodEntry);
                if (exception != null)
                    throw exception;

                return returnValue;
            }
            else
                return methodEntry;
        }

        public ThreadMethodEntry BeginInvoke(MethodInvoker methodInvoker) =>
            (ThreadMethodEntry)Invoke(methodInvoker, null, false);
        public ThreadMethodEntry BeginInvoke(Delegate method, params object[] args) =>
            (ThreadMethodEntry)Invoke(method, args, false);

        public object EndInvoke(ThreadMethodEntry entry)
        {
            int currentThreadID = Thread.CurrentThread.ManagedThreadId;

            if (entry.CallerThreadID == currentThreadID)
                throw new EzThreadException("The EndInvoke method must be used on the same thread as BeginInvoke.");

            if (entry is null)
                throw new ArgumentNullException(nameof(entry));

            if (!entry.IsCompleted)
            {
                if (_thread.ManagedThreadId == Thread.CurrentThread.ManagedThreadId)
                    entry.Invoke();
                else
                    entry.Wait();
            }

            Debug.Assert(entry.IsCompleted, "Oops, this should have been done.");

            if (entry.Exception != null)
                throw entry.Exception;
            return entry.ReturnValue;
        }
        public T EndInvoke<T>(ThreadMethodEntry entry)
        {
            object obj = EndInvoke(entry);

            if (obj is T t)
                return t;

            throw new EzThreadException($"The returned value is not of {typeof(T)} type, the returned value is of {obj.GetType()} type.");
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                OnDispose?.Invoke(this, new DisposeEventArgs(disposing));

                if (_entries.Count != 0)
                    _thread.Join();
                else
                {
                    MethodInvoker method = Abort;
                    try
                    {
                        Invoke(method);
                    } catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }
        }

        private void Abort()
        {
            Thread.CurrentThread.Abort();
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
