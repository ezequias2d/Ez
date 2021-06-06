using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ez.Memory
{
    /// <summary>
    /// Provides a read-only pinned memory interface.
    /// </summary>
    /// <typeparam name="T">The type of elements in read-only pinned memory.</typeparam>
    public interface IReadOnlyPinnedMemory<T> : IReadOnlyList<T> where T : unmanaged
    {
        /// <summary>
        /// The pointer to the first element of the <see cref="PinnedMemory{T}"/>.
        /// </summary>
        IntPtr Ptr { get; }

        /// <summary>
        /// A <see cref="ReaderWriterLockSlim"/> used to control access(optional, so can be <see langword="null"/>).
        /// </summary>
        ReaderWriterLockSlim ReaderWriterLock { get; }
    }
}
