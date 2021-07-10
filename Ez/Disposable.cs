using System;

namespace Ez
{
    /// <summary>
    /// Represents a implementation of <see cref="IDisposable"/> interface.
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        /// <summary>
        /// Disposes the object.
        /// </summary>
        ~Disposable()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets a value indicating whether the object has been disposed of.
        /// </summary>
        public bool IsDisposed { get; protected set; }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            if (disposing)
                ManagedDispose();

            UnmanagedDispose();
        }

        /// <summary>
        /// Disposes unmanaged resources.
        /// </summary>
        protected abstract void UnmanagedDispose();

        /// <summary>
        /// Disposes managed resources.
        /// </summary>
        protected abstract void ManagedDispose();
    }
}
