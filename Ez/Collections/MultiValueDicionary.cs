// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections
{
    /// <summary>
    /// Represents a colleciton of keys and values that each key can have more than one value.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    public class MultiValueDicionary<TKey, TValue> : IReadOnlyDictionary<TKey, IReadOnlyCollection<TValue>>
    {
        #region Variables

        private readonly IDictionary<TKey, ICollection<TValue>> _dicionary;

        /// <summary>
        /// The function to construct a new <see cref="ICollection{TValue}"/>
        /// </summary>
        private readonly Func<ICollection<TValue>> NewCollectionFactory;

        private long _version;

        #endregion Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="MultiValueDicionary{TKey, TValue}"/> class that is empty.
        /// </summary>
        public MultiValueDicionary() : this(new Dictionary<TKey, ICollection<TValue>>(), () => new HashSet<TValue>())
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="MultiValueDicionary{TKey, TValue}"/> class that wraps a
        /// <see cref="IDictionary{TKey, TValue}"/> dictionary and with a custom function to create a sub-collection.
        /// </summary>
        /// <param name="dictionary">The dictionary to wraps.</param>
        /// <param name="newCollectionFactory">A delegate to create a new sub-collection used in
        /// <see cref="MultiValueDicionary{TKey, TValue}"/></param>
        public MultiValueDicionary(IDictionary<TKey, ICollection<TValue>> dictionary, Func<ICollection<TValue>> newCollectionFactory)
        {
            _dicionary = dictionary;
            NewCollectionFactory = newCollectionFactory;
            
            Count = 0;
            foreach (var c in _dicionary.Values)
                Count += c.Count;
        }
        #endregion Constructors

        #region Operators
        /// <summary>
        /// Gets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the values to get.</param>
        /// <returns>The value associated with the specified key. If the specified key is not found, 
        /// a get operation throws a <see cref="KeyNotFoundException"/>.</returns>
        public IReadOnlyCollection<TValue> this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                if (_dicionary.TryGetValue(key, out ICollection<TValue> collection))
                    return collection.AsReadOnly();

                throw new KeyNotFoundException();
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<TKey> Keys => _dicionary.Keys;

        /// <summary>
        /// Gets an enumerable collection that contains the values in the read-only multi-value dictionary.
        /// </summary>
        public IEnumerable<IReadOnlyCollection<TValue>> Values
        {
            get
            {
                foreach (var value in _dicionary.Values)
                    yield return value.AsReadOnly();
            }
        }

        /// <summary>
        ///  Gets the number of elements in the collection.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Get an enumerable collection that contains all the values in the read-only multi-value dictionary.
        /// </summary>
        public IEnumerable<TValue> AllElements
        {
            get
            {
                foreach (var value in _dicionary.Values)
                    foreach (var element in value)
                        yield return element;
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if(!_dicionary.TryGetValue(key, out var collection))
            {
                collection = NewCollectionFactory();
                _dicionary.Add(key, collection);
            }

            collection.Add(value);
            Count++;
            _version++;
        }

        /// <summary>
        /// Adds the specified key and multiple values to the dictionary.
        /// </summary>
        /// <param name="key">The key of elements to add.</param>
        /// <param name="values">The values of elements to add.</param>
        public void AddRange(TKey key, IEnumerable<TValue> values)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (!_dicionary.TryGetValue(key, out ICollection<TValue> collection))
            {
                collection = NewCollectionFactory();
                _dicionary.Add(key, collection);
            }

            int count = 0;
            foreach (var value in values)
            {
                collection.Add(value);
                count++;
            }
            Count += count;

            _version++;
        }

        /// <summary>
        /// Removes the value with the specified key from the <see cref="MultiValueDicionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <param name="value">The value of the element to remove.</param>
        /// <returns><see langword="true"/> if the element is successfully found and removed; otherwise, 
        /// <see langword="false"/>. This method returns <see langword="false"/> if <paramref name="key"/> 
        /// is not found in the <see cref="MultiValueDicionary{TKey, TValue}"/>.</returns>
        public bool Remove(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if(_dicionary.TryGetValue(key, out var collection) && collection.Remove(value))
            {
                _version++;
                Count--;

                if (collection.Count == 0)
                    _dicionary.Remove(key);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes all values with the specified <paramref name="key"/> from the 
        /// <see cref="MultiValueDicionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the elements to remove.</param>
        /// <returns><see langword="true"/> if the element is successfully found and removed; otherwise, 
        /// <see langword="false"/>. This method returns <see langword="false"/> if <paramref name="key"/> 
        /// is not found in the <see cref="MultiValueDicionary{TKey, TValue}"/>.</returns>
        public bool RemoveKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if(_dicionary.TryGetValue(key, out var value) && _dicionary.Remove(key))
            {
                _version++;
                Count -= value.Count;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines wheter the <see cref="MultiValueDicionary{TKey, TValue}"/> contains 
        /// specific linked <paramref name="key"/> and <paramref name="value"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return _dicionary.TryGetValue(key, out ICollection<TValue> collection) && collection.Contains(value);
        }

        /// <summary>
        /// Removes all items from the <see cref="MultiValueDicionary{TKey, TValue}"/>.
        /// </summary>
        public void Clear()
        {
            _dicionary.Clear();
            Count = 0;
        }

        #endregion

        #region Implementation of IReadOnlyDicionary 
        /// <summary>
        /// Determines whether the <see cref="MultiValueDicionary{TKey, TValue}"/> contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="MultiValueDicionary{TKey, TValue}"/>.</param>
        /// <returns><see langword="true"/> if the <see cref="MultiValueDicionary{TKey, TValue}"/>
        /// contains an element with the specified key; otherwise, <see langword="false"/>.</returns>
        public bool ContainsKey(TKey key) => _dicionary.ContainsKey(key);

        /// <summary>
        /// Determines wheter the <see cref="MultiValueDicionary{TKey, TValue}"/> contains a specific value.
        /// </summary>
        /// <param name="value">The value to locate in the <see cref="MultiValueDicionary{TKey, TValue}"/>.
        /// The value can be null for reference types.</param>
        /// <returns><see langword="true"/> if the <see cref="MultiValueDicionary{TKey, TValue}"/>
        /// contains an element with the specified value; otherwise, <see langword="false"/>.</returns>
        public bool ContainsValue(TValue value)
        {
            foreach (var collection in _dicionary.Values)
                if (collection.Contains(value))
                    return true;
            return false;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="MultiValueDicionary{TKey, TValue}"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> GetEnumerator() => new Enumerator(this);

        /// <summary>
        /// Gets values associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the values to get.</param>
        /// <param name="value">When this method returns, contains the value associated withthe specified key,
        /// if the key is foumd; otherwise, null.</param>
        /// <returns><see langword="true"/> if the <see cref="MultiValueDicionary{TKey, TValue}"/> contains an
        /// element with the specified key; otherwise, <see langword="false"/>.</returns>
        public bool TryGetValue(TKey key, out IReadOnlyCollection<TValue> value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if(_dicionary.TryGetValue(key, out ICollection<TValue> collection))
            {
                value = collection.AsReadOnly();
                return true;
            }
            value = null;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        private class Enumerator : IEnumerator<KeyValuePair<TKey, IReadOnlyCollection<TValue>>>
        {
            private readonly IEnumerator<KeyValuePair<TKey, ICollection<TValue>>> _enumerator;
            private readonly long _version;
            private readonly MultiValueDicionary<TKey, TValue> _dicionary;

            public Enumerator(MultiValueDicionary<TKey, TValue> dicionary)
            {
                _dicionary = dicionary;
                _enumerator = dicionary._dicionary.GetEnumerator();
                _version = dicionary._version;
            }

            public KeyValuePair<TKey, IReadOnlyCollection<TValue>> Current
            {
                get
                {
                    if(_version != _dicionary._version)
                        throw new InvalidOperationException("InvalidOperation_EnumFailedVersion");

                    KeyValuePair<TKey, ICollection<TValue>> current = _enumerator.Current;
                    return new KeyValuePair<TKey, IReadOnlyCollection<TValue>>(current.Key, current.Value.AsReadOnly());
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() => _enumerator.Dispose();

            public bool MoveNext() => _enumerator.MoveNext();

            public void Reset()
            {
                if (_version != _dicionary._version)
                    throw new InvalidOperationException("InvalidOperation_EnumFailedVersion");
                _enumerator.Reset();
            }
        }
        #endregion
    }
}