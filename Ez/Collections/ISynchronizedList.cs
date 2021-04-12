using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections
{
    /// <summary>
    /// A List that implements synchronization using ISynchronizable interface.
    /// </summary>
    /// <typeparam name="T">Type of elements using in <see cref="IList{T}"/> interface.</typeparam>
    public interface ISynchronizedList<T> : IList<T>, ISynchronizable
    {
        ReadOnlySpan<T> GetReadOnlySpan();
    }
}
