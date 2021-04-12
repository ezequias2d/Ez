using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections
{
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
        public MultiValueDicionary() : this(new Dictionary<TKey, ICollection<TValue>>(), () => new HashSet<TValue>())
        {

        }

        public MultiValueDicionary(IDictionary<TKey, ICollection<TValue>> dictionary, Func<ICollection<TValue>> newCollectionFactory)
        {
            _dicionary = dictionary;
            NewCollectionFactory = newCollectionFactory;
        }
        #endregion Constructors

        #region Operators
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
        public IEnumerable<TKey> Keys => _dicionary.Keys;

        public IEnumerable<IReadOnlyCollection<TValue>> Values
        {
            get
            {
                foreach (var value in _dicionary.Values)
                    yield return value.AsReadOnly();
            }
        }

        public int Count => _dicionary.Count;

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

        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if(!_dicionary.TryGetValue(key, out ICollection<TValue> collection))
            {
                collection = NewCollectionFactory();
                _dicionary.Add(key, collection);
            }

            collection.Add(value);
            _version++;
        }

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

            foreach (var value in values)
                collection.Add(value);

            _version++;
        }

        public bool Remove(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if(_dicionary.TryGetValue(key, out ICollection<TValue> collection) && collection.Remove(value))
            {
                if (collection.Count == 0)
                    _dicionary.Remove(key);

                _version++;
                return true;
            }
            return false;
        }

        public bool RemoveKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if(_dicionary.Remove(key))
            {
                _version++;
                return true;
            }
            return false;
        }

        public bool Contains(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return _dicionary.TryGetValue(key, out ICollection<TValue> collection) && collection.Contains(value);
        }

        public bool ContainsValue(TValue value)
        {
            foreach (var collection in _dicionary.Values)
                if (collection.Contains(value))
                    return true;
            return false;
        }

        public void Clear()
        {
            _dicionary.Clear();
        }

        #endregion

        #region Implementation of IReadOnlyDicionary 
        public bool ContainsKey(TKey key) => _dicionary.ContainsKey(key);

        public IEnumerator<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> GetEnumerator() => new Enumerator(this);

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