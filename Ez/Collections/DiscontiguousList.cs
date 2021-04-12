using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections
{
    public delegate void DefragAction(int olderIndex, int newerIndex);
    public class DiscontiguousList<T> : IList<T>, IReadOnlyList<T>
    {
        private readonly List<T> _list;
        private readonly Stack<int> _removeds;
        private readonly Dictionary<int, bool> _invalids;

        public DiscontiguousList()
        {
            _list = new List<T>();
            _removeds = new Stack<int>();
            _invalids = new Dictionary<int, bool>();
        }

        public DiscontiguousList(IEnumerable<T> collection)
        {
            _list = new List<T>(collection);
            _removeds = new Stack<int>();
            _invalids = new Dictionary<int, bool>();
        }

        public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public int Add(in T item)
        {
            int index;

            try
            {
                index = _removeds.Pop();
                _list[index] = item;
            }
            catch(InvalidOperationException _)
            {
                index = _list.Count;
                _list.Add(item);
            }

            if (_invalids.ContainsKey(index) && _invalids[index])
                _invalids[index] = false;

            return index;
        }

        public void Clear()
        {
            _removeds.Clear();
            _list.Clear();
            _invalids.Clear();
        }

        public bool Contains(T item) => _list.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0; i < _list.Count; i++)
            {
                if (!(_invalids.ContainsKey(i) && _invalids[i]))
                    yield return _list[i];
            }
        }

        public int IndexOf(T item)
        {
            int index = 0;
            foreach(var element in this)
            {
                if (element.Equals(item))
                    return index;
                index++;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            int index = _list.IndexOf(item);
            if(index != -1)
            {
                _removeds.Push(index);
                _invalids[index] = true;
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (!(_invalids.ContainsKey(index) &&  _invalids[index]) && index >= 0 && index < _list.Count)
            {
                _invalids[index] = true;
                _removeds.Push(index);
            }
        }
        public void Defrag(DefragAction action)
        {
            List<T> aux = new List<T>(_list.Count);
            for (int i = 0; i < _list.Count; i++)
            {
                if (!(_invalids.ContainsKey(i) && _invalids[i]))
                {
                    action?.Invoke(i, aux.Count);
                    aux.Add(_list[i]);
                }
            }
            Clear();
            aux.AddRange(aux);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<T>.Add(T item) => Add(item);
    }
}
