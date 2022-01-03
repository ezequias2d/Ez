using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ez.Threading
{
    /// <summary>
    /// A class that implements TaskScheduler that uses a single thread.
    /// </summary>
    public sealed class SingleTaskScheduler : TaskScheduler, IDisposable
    {
        [ThreadStatic]
        private bool _currentThreadIsProcessingItems;

        private readonly Thread _thread;
        private readonly BlockingCollection<Task> _tasks;
        
        private bool _disposed;
        private event EventHandler? _awake;
        private event EventHandler? _start;
        private event EventHandler? _beforeInvoking;
        private event EventHandler? _afterInvoking;

        /// <summary>
        /// Creates a new instance of <see cref="SingleTaskScheduler"/>.
        /// </summary>
        public SingleTaskScheduler()
        {
            _tasks = new(new ConcurrentQueue<Task>());
            _currentThreadIsProcessingItems = false;
            _thread = new Thread(Main)
            {
                IsBackground = true,
            };
            
        }

        /// <summary>
        /// Occurs when the thread is started, before the <see cref="Start"/> event.
        /// </summary>
        public event EventHandler Awake
        {
            add
            {
                _awake += value;
            }
            remove
            {
                _awake -= value;
            }
        }

        /// <summary>
        /// Occurs when the thread is started, after the <see cref="Awake"/> event.
        /// </summary>
        public event EventHandler Start
        {
            add
            {
                _start += value;
            }
            remove
            {
                _start -= value;
            }
        }

        /// <summary>
        /// Occurs just before a consumed delegate is invoked.
        /// </summary>
        public event EventHandler BeforeInvoking
        {
            add
            {
                _beforeInvoking += value;
            }
            remove
            {
                _beforeInvoking -= value;
            }
        }

        /// <summary>
        /// Occurs just after a consumed delegate is invoked.
        /// </summary>
        public event EventHandler AfterInvoking
        {
            add
            {
                _afterInvoking += value;
            }
            remove
            {
                _afterInvoking -= value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the scheduling priority of the thread.
        /// <para>
        ///     The default value is <see cref="ThreadPriority.Normal"/>.
        /// </para>
        /// </summary>
        public ThreadPriority Priority { get => _thread.Priority; set => _thread.Priority = value; }

        /// <summary>
        /// Gets or sets a value indicating whether or not a thread is a background thread.
        /// <para>
        ///     The default value is <see langword="true"/>.
        /// </para>
        /// </summary>
        public bool IsBackground { get => _thread.IsBackground; set => _thread.IsBackground = value; }


        /// <inheritdoc/>
        public override int MaximumConcurrencyLevel => 1;

        /// <summary>
        /// Starts the execution of this <see cref="SingleTaskScheduler"/>.
        /// </summary>
        public void StartThread()
        {
            _thread.Start();
        }

        /// <inheritdoc/>
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return _tasks.ToArray();
        }

        /// <inheritdoc/>
        protected override void QueueTask(Task task)
        {
            _tasks.Add(task);
        }

        /// <inheritdoc/>
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (!_currentThreadIsProcessingItems)
                return false;

            return base.TryExecuteTask(task);
        }

        private void Main()
        {
            _currentThreadIsProcessingItems = true;
            _awake?.Invoke(this, EventArgs.Empty);
            _start?.Invoke(this, EventArgs.Empty);

            while (!_disposed && !_tasks.IsCompleted)
            {
                Task task;
                try
                {
                    task = _tasks.Take();
                }
                catch
                {
                    continue;
                }

                if (!task.IsCompleted)
                {
                    _beforeInvoking?.Invoke(this, EventArgs.Empty);
                    base.TryExecuteTask(task);
                    _afterInvoking?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _tasks.CompleteAdding();
            _disposed = true;
            _thread.Join();
        }

        /// <summary>
        /// Release all resources used by this instance.
        /// </summary>
        public void Dispose() =>
            Dispose(true);
    }
}
