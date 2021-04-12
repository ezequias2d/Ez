using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections
{
    public interface ISelfIndexedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<TValue> where TValue : ISelfIndexedElement<TKey>
    {


    }
}
