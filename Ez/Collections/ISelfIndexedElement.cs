using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections
{
    public interface ISelfIndexedElement<T>
    {
        T Key { get; }

        /// <summary>
        /// Action(T oldKey)
        /// </summary>
        event Action<T> KeyPropertyChange;
    }
}
