using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections
{
    /// <summary>
    /// Interface that specifies that an object of the class can be synced using the object provided by Sync.
    /// </summary>
    public interface ISynchronizable
    {
        /// <summary>
        /// Object used for sync.
        /// </summary>
        object Sync { get; }
    }
}
