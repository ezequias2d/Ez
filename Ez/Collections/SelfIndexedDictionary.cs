// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections
{
    public class SelfIndexedDictionary<TKey, TValue> : ISelfIndexedDictionary<TKey, TValue> where TValue : ISelfIndexedElement<TKey>
    {
        private readonly IDictionary<TKey, TValue> _inner;

        public SelfIndexedDictionary() : this(new ConcurrentDictionary<TKey, TValue>())
        {

        }

        public SelfIndexedDictionary(IEnumerable<TValue> collection) : this(new ConcurrentDictionary<TKey, TValue>())
        {
            foreach (TValue value in collection)
                Add(value);
        }

        public SelfIndexedDictionary(IDictionary<TKey, TValue> inner)
        {
            _inner = inner;
        }

        public TValue this[TKey key] { get => _inner[key]; set => _inner[key] = value; }

        public ICollection<TKey> Keys => _inner.Keys;

        public ICollection<TValue> Values => _inner.Values;

        public int Count => _inner.Count;

        public bool IsReadOnly => _inner.IsReadOnly;

        private void KeyChange(TKey oldest)
        {
            if(_inner.TryGetValue(oldest, out var element))
                if (_inner.Remove(oldest))
                    _inner.Add(element.Key, element);
        }

        public void Add(TValue item)
        {
            _inner.Add(item.Key, item);
            item.KeyPropertyChange += KeyChange;
        }

        public void Clear()
        {
            foreach(TValue item in _inner.Values)
                item.KeyPropertyChange -= KeyChange;
            _inner.Clear();
        }

        public bool Contains(TValue item)
        {
            return _inner.Contains(new KeyValuePair<TKey, TValue>(item.Key, item));
        }

        public bool ContainsKey(TKey key)
        {
            return _inner.ContainsKey(key);
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new IndexOutOfRangeException();

            uint i = (uint)arrayIndex;
            foreach(TValue value in _inner.Values)
            {
                array[i++] = value;
            }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _inner.Values.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            if(_inner.TryGetValue(key, out TValue value))
            {
                value.KeyPropertyChange -= KeyChange;
                return _inner.Remove(key);
            }
            return false;
        }

        public bool Remove(TValue item)
        {
            if (_inner.ContainsKey(item.Key))
            {
                item.KeyPropertyChange -= KeyChange;
                return _inner.Remove(item.Key);
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _inner.TryGetValue(key, out value);
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            if(key.GetHashCode() == value.Key.GetHashCode() && key.Equals(value.Key))
                Add(value);
            else
                throw new ArgumentException("The key is not equal to key in value.", nameof(key));
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<TKey, TValue>)this).Add(item.Key, item.Value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return Contains(item.Value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _inner.CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key.GetHashCode() == item.Value.Key.GetHashCode() && item.Key.Equals(item.Value.Key))
                return Remove(item.Value);
            else
                throw new ArgumentException("The key is not equal to key in value.", nameof(item));
        }
    }
}
