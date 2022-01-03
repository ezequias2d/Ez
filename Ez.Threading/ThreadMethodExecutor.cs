// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ez.Threading
{
    /// <summary>
    /// A thread that consumes delegates and invokes them.
    /// </summary>
    public class ThreadMethodExecutor : IDisposable
    {
        private readonly SingleTaskScheduler _taskScheduler;
        private readonly TaskFactory _taskFactory;

        private bool _disposed;

        /// <summary>
        /// Initializes a new <see cref="ThreadMethodExecutor"/> class.
        /// </summary>
        /// <param name="autostart">Auto starts the thread.</param>
        public ThreadMethodExecutor(bool autostart = true)
        {
            _taskScheduler = new();
            _taskFactory = new TaskFactory(_taskScheduler);

            if (autostart)
                _taskScheduler.StartThread();
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
        public event EventHandler Awake
        {
            add
            {
                _taskScheduler.Awake += value;
            }
            remove 
            {
                _taskScheduler.Awake -= value;
            } 
        }

        /// <summary>
        /// Occurs when the thread is started, after the <see cref="Awake"/> event.
        /// </summary>
        public event EventHandler Start
        {
            add
            {
                _taskScheduler.Start += value;
            }
            remove
            {
                _taskScheduler.Start -= value;
            }
        }

        /// <summary>
        /// Occurs just before a consumed delegate is invoked.
        /// </summary>
        public event EventHandler BeforeInvoking
        {
            add
            {
                _taskScheduler.BeforeInvoking += value;
            }
            remove
            {
                _taskScheduler.BeforeInvoking -= value;
            }
        }

        /// <summary>
        /// Occurs just after a consumed delegate is invoked.
        /// </summary>
        public event EventHandler AfterInvoking
        {
            add
            {
                _taskScheduler.AfterInvoking += value;
            }
            remove
            {
                _taskScheduler.AfterInvoking -= value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the scheduling priority of the thread.
        /// <para>
        ///     The default value is <see cref="ThreadPriority.Normal"/>.
        /// </para>
        /// </summary>
        public ThreadPriority Priority
        {
            get => _taskScheduler.Priority;
            set => _taskScheduler.Priority = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not a thread is a background thread.
        /// <para>
        ///     The default value is <see langword="true"/>.
        /// </para>
        /// </summary>
        public bool IsBackground
        {
            get => _taskScheduler.IsBackground;
            set => _taskScheduler.IsBackground = value;
        }

        /// <summary>
        /// Starts the execution of this <see cref="ThreadMethodExecutor"/>.
        /// </summary>
        public void StartThread() => _taskScheduler.StartThread();

        /// <summary>
        /// Synchronously executes the <paramref name="action"/> on this <see cref="ThreadMethodExecutor"/>.
        /// </summary>
        /// <param name="action">A <see cref="Action"/> delegate that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        public void Invoke(Action action)
        {
            var task = InvokeAsync(action);
            var awaiter = task.GetAwaiter();
            awaiter.GetResult();
        }

        /// <summary>
        /// Synchronously executes the <paramref name="action"/> on this <see cref="ThreadMethodExecutor"/>.
        /// </summary>
        /// <param name="action">A <see cref="WaitCallback"/> delegate that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="state">The state parameter of <see cref="WaitCallback"/> to pass to the given method.</param>
        public void Invoke(Action<object?> action, object? state)
        {
            InvokeAsync(action, state).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Synchronously executes the <paramref name="function"/> on this <see cref="ThreadMethodExecutor"/>.
        /// </summary>
        /// <typeparam name="T">The type of return value.</typeparam>
        /// <param name="function">A <see cref="Func{TResult}"/> delegate that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <returns>The return value from the delegate being invoked.</returns>
        public T Invoke<T>(Func<T> function)
        {
            var task = InvokeAsync(function);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Action"/> delegate on the thread that created this object.
        /// </summary>
        /// <param name="action">A <see cref="Action"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <returns>An <see cref="Task"/> that represents the asynchronous operation started by calling this method.</returns>
        public Task InvokeAsync(Action action)
        {
            return _taskFactory.StartNew(action);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Action"/> delegate on the thread that created this object.
        /// </summary>
        /// <param name="action">A <see cref="Action"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
        /// <returns>An <see cref="Task"/> that represents the asynchronous operation started by calling this method.</returns>
        public async Task InvokeAsync(Action action, CancellationToken cancellationToken)
        {
            await _taskFactory.StartNew(action, cancellationToken);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Action"/> delegate on the thread that created this object.
        /// </summary>
        /// <param name="action">A <see cref="Action"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
        /// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
        /// <returns>An <see cref="Task"/> that represents the asynchronous operation started by calling this method.</returns>
        public async Task InvokeAsync(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
        {
            await _taskFactory.StartNew(action, cancellationToken, creationOptions, _taskScheduler);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Action"/> delegate on the thread that created this object.
        /// </summary>
        /// <param name="action">A <see cref="Action"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
        /// <returns>An <see cref="Task"/> that represents the asynchronous operation started by calling this method.</returns>
        public async Task InvokeAsync(Action action, TaskCreationOptions creationOptions)
        {
            await _taskFactory.StartNew(action, creationOptions);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Action"/> delegate on the thread that created this object.
        /// </summary>
        /// <param name="action">A <see cref="Action"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="state">The state parameter of <see cref="Action{T1}"/> to pass to the given method.</param>
        /// <returns>An <see cref="Task"/> that represents the asynchronous operation started by calling this method.</returns>
        public async Task InvokeAsync(Action<object?> action, object? state)
        {
            await _taskFactory.StartNew(action, state);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Action"/> delegate on the thread that created this object.
        /// </summary>
        /// <param name="action">A <see cref="Action"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="state">The state parameter of <see cref="Action{T1}"/> to pass to the given method.</param>
        /// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
        /// <returns>An <see cref="Task"/> that represents the asynchronous operation started by calling this method.</returns>
        public async Task InvokeAsync(Action<object?> action, object? state, CancellationToken cancellationToken)
        {
            await _taskFactory.StartNew(action, state, cancellationToken);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Action"/> delegate on the thread that created this object.
        /// </summary>
        /// <param name="action">A <see cref="Action"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="state">The state parameter of <see cref="Action{T1}"/> to pass to the given method.</param>
        /// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
        /// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
        /// <returns>An <see cref="Task"/> that represents the asynchronous operation started by calling this method.</returns>
        public async Task InvokeAsync(Action<object?> action, object? state, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
        {
            await _taskFactory.StartNew(action, state, cancellationToken, creationOptions, _taskScheduler);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Action"/> delegate on the thread that created this object.
        /// </summary>
        /// <param name="action">A <see cref="Action"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="state">The state parameter of <see cref="Action{T1}"/> to pass to the given method.</param>
        /// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
        /// <returns>An <see cref="Task"/> that represents the asynchronous operation started by calling this method.</returns>
        public async Task InvokeAsync(Action<object?> action, object? state, TaskCreationOptions creationOptions)
        {
            await _taskFactory.StartNew(action, state, creationOptions);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Func{T, TResult}"/> delegate on the thread that created this object.
        /// </summary>
        /// <param name="function">A <see cref="Func{T, TResult}"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <returns>An <see cref="Task"/> that represents the asynchronous operation started by calling this method.</returns>
        public async Task<TResult> InvokeAsync<TResult>(Func<TResult> function)
        {
            return await _taskFactory.StartNew(function);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Func{T, TResult}"/> delegate on the thread that created this object.
        /// </summary>
        /// <param name="function">A <see cref="Func{T, TResult}"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
        /// <returns>An <see cref="Task"/> that represents the asynchronous operation started by calling this method.</returns>
        public async Task<TResult> InvokeAsync<TResult>(Func<TResult> function, CancellationToken cancellationToken)
        {
            return await _taskFactory.StartNew(function, cancellationToken);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Func{T, TResult}"/> delegate on the thread that created this object.
        /// </summary>
        /// <param name="function">A <see cref="Func{T, TResult}"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
        /// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
        /// <returns>An <see cref="Task"/> that represents the asynchronous operation started by calling this method.</returns>
        public async Task<TResult> InvokeAsync<TResult>(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
        {
            return await _taskFactory.StartNew(function, cancellationToken, creationOptions, _taskScheduler);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Func{T, TResult}"/> delegate on the thread that created this object.
        /// </summary>
        /// <param name="function">A <see cref="Func{T, TResult}"/> that contains a method to call in this <see cref="ThreadMethodExecutor"/>.</param>
        /// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
        /// <returns>An <see cref="Task"/> that represents the asynchronous operation started by calling this method.</returns>
        public async Task<TResult> InvokeAsync<TResult>(Func<TResult> function, TaskCreationOptions creationOptions)
        {
            return await _taskFactory.StartNew(function, creationOptions);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                _taskScheduler.Dispose();
            }
        }

        /// <summary>
        /// Release all resources used by this instance.
        /// </summary>
        public void Dispose() =>
            Dispose(true);
    }
}
