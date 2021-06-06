// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
namespace Ez.Threading
{
    /// <summary>
    /// A thread that consumes delegates and invokes them.
    /// </summary>
    public class ThreadMethodExecutor : IDisposable
    {
        private readonly Thread _thread;
        private readonly BlockingCollection<ThreadMethodEntry> _entries;
        private bool _disposed;

        /// <summary>
        /// Initializes a new <see cref="ThreadMethodExecutor"/> class.
        /// </summary>
        /// <param name="autostart">Auto starts the thread.</param>
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

        /// <summary>
        /// Destroys a <see cref="ThreadMethodExecutor"/> class instance.
        /// </summary>
        ~ThreadMethodExecutor()
        {
            Dispose(false);
        }

        /// <summary>
        /// Occurs when the thread is started, before the <see cref="Start"/> event.
        /// </summary>
        public event EventHandler Awake;

        /// <summary>
        /// Occurs when the thread is started, after the <see cref="Awake"/> event.
        /// </summary>
        public event EventHandler Start;

        /// <summary>
        /// Occurs just before a consumed delegate is invoked.
        /// </summary>
        public event EventHandler BeforeInvoking;

        /// <summary>
        /// Occurs just after a consumed delegate is invoked.
        /// </summary>
        public event EventHandler AfterInvoking;

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

        /// <summary>
        /// Starts the execution of this <see cref="ThreadMethodExecutor"/>.
        /// </summary>
        public void StartThread() => _thread.Start();

        /// <summary>
        /// Synchronously executes the <paramref name="action"/> on this <see cref="ThreadMethodExecutor"/>.
        /// </summary>
        /// <param name="action">A <see cref="Action"/> delegate that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        public void Invoke(Action action) =>
            Invoke(action, null, true);

        /// <summary>
        /// Synchronously executes the <paramref name="eventHandler"/> on this <see cref="ThreadMethodExecutor"/>.
        /// </summary>
        /// <param name="eventHandler">A <see cref="EventHandler"/> delegate that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="sender">The sender parameter of <see cref="EventHandler"/> to pass to the given method.</param>
        /// <param name="args">The e parameter of the <see cref="EventHandler"/> to pass to the given method.</param>
        public void Invoke(EventHandler eventHandler, object sender, EventArgs args) =>
            Invoke(eventHandler, new object[] { sender, args }, true);

        /// <summary>
        /// Synchronously executes the <paramref name="waitCallback"/> on this <see cref="ThreadMethodExecutor"/>.
        /// </summary>
        /// <param name="waitCallback">A <see cref="WaitCallback"/> delegate that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="state">The state parameter of <see cref="WaitCallback"/> to pass to the given method.</param>
        public void Invoke(WaitCallback waitCallback, object state) =>
            Invoke(waitCallback, new object[] { state }, true);

        /// <summary>
        /// Synchronously executes the <paramref name="method"/> on this <see cref="ThreadMethodExecutor"/>.
        /// </summary>
        /// <param name="method">A <see cref="Delegate"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="args">An array of type <see cref="Object"/> that represents the arguments to pass to the given method.</param>
        /// <returns>An <see cref="object"/> that represents the return value from the delegate being invoked, or <see langword="null"/> 
        /// if the delegate has no return value</returns>
        public object Invoke(Delegate method, params object[] args) =>
            Invoke(method, args, true);

        /// <summary>
        /// Synchronously executes the <paramref name="func"/> on this <see cref="ThreadMethodExecutor"/>.
        /// </summary>
        /// <typeparam name="T">The type of return value.</typeparam>
        /// <param name="func">A <see cref="Func{TResult}"/> delegate that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <returns>The return value from the delegate being invoked.</returns>
        public T Invoke<T>(Func<T> func)
        {
            object obj = Invoke(func, null, true);

            if (obj is T t)
                return t;

            throw new EzThreadException($"The returned value is not of {typeof(T)} type, the returned value is of {obj.GetType()} type.");
        }

        /// <summary>
        /// Synchronously executes the <paramref name="method"/> on this <see cref="ThreadMethodExecutor"/>.
        /// </summary>
        /// <typeparam name="T">The type of return value.</typeparam>
        /// <param name="method">A <see cref="Delegate"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="args">An array of type <see cref="Object"/> that represents the arguments to pass to the given method.</param>
        /// <returns>The return value from the delegate being invoked, or <see langword="null"/> if the delegate has no return value.</returns>
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


            if (synchronous && (Thread.CurrentThread.ManagedThreadId == _thread.ManagedThreadId))
                methodEntry.Invoke();
            else
                _entries.Add(methodEntry);

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

        /// <summary>
        /// Asynchronously executes the <see cref="Action"/> delegate on the thread that created this object.
        /// </summary>
        /// <param name="action">A <see cref="Action"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <returns>An <see cref="IAsyncResult"/> interface that represents the asynchronous operation started by calling this method.</returns>
        public IAsyncResult BeginInvoke(Action action) =>
            (ThreadMethodEntry)Invoke(action, null, false);

        /// <summary>
        /// Asynchronously executes the <see cref="Delegate"/> on the thread that created this object.
        /// </summary>
        /// <param name="method">A <see cref="Delegate"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="args">An array of type <see cref="Object"/> that represents the arguments to pass to the given method.</param>
        /// <returns>An <see cref="IAsyncResult"/> interface that represents the asynchronous operation started by calling this method.</returns>
        public IAsyncResult BeginInvoke(Delegate method, params object[] args) =>
            (ThreadMethodEntry)Invoke(method, args, false);

        /// <summary>
        /// Waits until the process started by calling <see cref="BeginInvoke(Action)"/> or  <see cref="BeginInvoke(Delegate, object[])"/> completes, 
        /// and then returns the value generated by the process.
        /// </summary>
        /// <param name="result">An <see cref="IAsyncResult"/> interface that represents the asynchronous operation started by calling 
        /// <see cref="BeginInvoke(Action)"/> or <see cref="BeginInvoke(Delegate, object[])"/>.</param>
        /// <returns>An <see cref="object"/> that represents the return value generated by the asynchronous operation.</returns>
        public object EndInvoke(IAsyncResult result)
        {
            if (!(result is ThreadMethodEntry entry))
                throw new ArgumentException($"The {nameof(result)} object was not created by a preceding call of the {nameof(BeginInvoke)} method from this object.");

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

        /// <summary>
        /// Waits until the process started by calling <see cref="BeginInvoke(Action)"/> or  <see cref="BeginInvoke(Delegate, object[])"/> completes, 
        /// and then returns the value generated by the process.
        /// </summary>
        /// <typeparam name="T">The type of return value.</typeparam>
        /// <param name="result">n <see cref="IAsyncResult"/> interface that represents the asynchronous operation started by calling 
        /// <see cref="BeginInvoke(Action)"/> or <see cref="BeginInvoke(Delegate, object[])"/>.</param>
        /// <returns>An T value that represents the return value generated by the asynchronous operation.</returns>
        public T EndInvoke<T>(IAsyncResult result)
        {
            object obj = EndInvoke(result);

            if (obj is T t)
                return t;

            throw new EzThreadException($"The returned value is not of {typeof(T)} type, the returned value is of {obj.GetType()} type.");
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                
                if (_entries.Count != 0)
                    _thread.Join();
                else
                {
                    Action method = Abort;
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

        /// <summary>
        /// Release all resources used by this instance.
        /// </summary>
        public void Dispose() =>
            Dispose(true);

        private void Abort() =>
            Thread.CurrentThread.Abort();
    }
}
