using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ez.Memory
{
    /// <summary>
    /// Provides a pinned memory interface.
    /// </summary>
    /// <typeparam name="T">The type of elements in pinned memory.</typeparam>
    public interface IPinnedMemory<T> : IReadOnlyPinnedMemory<T> where T : unmanaged
    {
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The element of the specified index.</returns>
        new T this[int index] { get; set; }
    }
}
