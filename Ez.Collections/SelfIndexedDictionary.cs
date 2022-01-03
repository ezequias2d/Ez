// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Ez.Collections
{
    /// <summary>
    /// A dictionary with elements that self-index through a <typeparamref name="TKey"/> Key property.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    public class SelfIndexedDictionary<TKey, TValue> : ISelfIndexedDictionary<TKey, TValue> where TValue : ISelfIndexedElement<TKey> where TKey : notnull
    {
        private readonly IDictionary<TKey, TValue> _inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfIndexedDictionary{TKey, TValue}"/> class that is empty.
        /// </summary>
        public SelfIndexedDictionary() : this(new ConcurrentDictionary<TKey, TValue>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfIndexedDictionary{TKey, TValue}"/> class that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new dictionary.</param>
        public SelfIndexedDictionary(IEnumerable<TValue> collection) : this(new ConcurrentDictionary<TKey, TValue>())
        {
            foreach (TValue value in collection)
                Add(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfIndexedDictionary{TKey, TValue}"/> class that wraps the <paramref name="inner"/> dictionary.
        /// </summary>
        /// <param name="inner">The dictionary to wrap.</param>
        public SelfIndexedDictionary(IDictionary<TKey, TValue> inner)
        {
            _inner = inner;
        }

        /// <summary>
        /// Gets the value associated with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the <paramref name="key"/>.</returns>
        public TValue this[TKey key] => _inner[key];

        /// <summary>
        /// Gets a collection containing the keys in the <see cref="SelfIndexedDictionary{TKey, TValue}"/>.
        /// </summary>
        public IEnumerable<TKey> Keys => _inner.Keys;

        /// <summary>
        /// Gets a collection containing the values in the <see cref="SelfIndexedDictionary{TKey, TValue}"/>.
        /// </summary>
        public IEnumerable<TValue> Values => _inner.Values;

        /// <summary>
        /// Gets the number of key/value pairs contained in the <see cref="SelfIndexedDictionary{TKey, TValue}"/>.
        /// </summary>
        public int Count => _inner.Count;

        /// <summary>
        /// Gets a value that indicates whether the <see cref="SelfIndexedDictionary{TKey, TValue}"/> is read-only.
        /// </summary>
        public bool IsReadOnly => _inner.IsReadOnly;

        private void KeyChange(TKey oldest)
        {
            if(_inner.TryGetValue(oldest, out var element))
                if (_inner.Remove(oldest))
                    _inner.Add(element.Key, element);
        }

        /// <summary>
        /// Adds an item to the <see cref="SelfIndexedDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="SelfIndexedDictionary{TKey, TValue}"/>.</param>
        public void Add(TValue item)
        {
            _inner.Add(item.Key, item);
            item.KeyPropertyChange += KeyChange;
        }

        /// <summary>
        /// Removes all items from the <see cref="SelfIndexedDictionary{TKey, TValue}"/>.
        /// </summary>
        public void Clear()
        {
            foreach(TValue item in _inner.Values)
                item.KeyPropertyChange -= KeyChange;
            _inner.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="SelfIndexedDictionary{TKey, TValue}"/> contains a specific value.
        /// </summary>
        /// <param name="item"></param>
        /// <returns><see langword="true"/> if item is found in the <see cref="SelfIndexedDictionary{TKey, TValue}"/>; otherwise, <see langword="false"/>.</returns>
        public bool Contains(TValue item)
        {
            return _inner.Contains(new KeyValuePair<TKey, TValue>(item.Key, item));
        }

        /// <summary>
        /// Determines whether the <see cref="SelfIndexedDictionary{TKey, TValue}"/> contains an element with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="SelfIndexedDictionary{TKey, TValue}"/></param>
        /// <returns><see langword="true"/> if the <see cref="SelfIndexedDictionary{TKey, TValue}"/> contains an element with the <paramref name="key"/>; otherwise, <see langword="false"/>.</returns>
        public bool ContainsKey(TKey key)
        {
            return _inner.ContainsKey(key);
        }

        /// <summary>
        /// Copies the elements of the <see cref="SelfIndexedDictionary{TKey, TValue}"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from 
        /// <see cref="SelfIndexedDictionary{TKey, TValue}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
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

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TValue> GetEnumerator()
        {
            return _inner.Values.GetEnumerator();
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="SelfIndexedDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns><see langword="true"/> if the element is successfully removed; otherwise, <see langword="false"/>. 
        /// This method also returns <see langword="false"/> if key was not found in the original
        /// <see cref="SelfIndexedDictionary{TKey, TValue}"/>.</returns>
        public bool Remove(TKey key)
        {
            if(_inner.TryGetValue(key, out TValue? value))
            {
                value.KeyPropertyChange -= KeyChange;
                return _inner.Remove(key);
            }
            return false;
        }

        /// <summary>
        /// Removes the element with <see cref="ISelfIndexedElement{T}.Key"/> from the <see cref="SelfIndexedDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The item that contains the key to remove.</param>
        /// <returns><see langword="true"/> if the element is successfully removed; otherwise, <see langword="false"/>. 
        /// This method also returns <see langword="false"/> if key was not found in the original
        /// <see cref="SelfIndexedDictionary{TKey, TValue}"/>.</returns>
        public bool Remove(TValue item)
        {
            if (_inner.ContainsKey(item.Key))
            {
                item.KeyPropertyChange -= KeyChange;
                return _inner.Remove(item.Key);
            }
            return false;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, 
        /// if the key is found; otherwise, the default value for the type of the <paramref name="value"/> 
        /// parameter. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the object that implements <see cref="SelfIndexedDictionary{TKey, TValue}"/> 
        /// contains an element with the specified key; otherwise, <see langword="false"/>.</returns>
        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return _inner.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return _inner.GetEnumerator();
        }
    }
}
