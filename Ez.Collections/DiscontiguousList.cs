// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ez.Collections
{
    /// <summary>
    /// A discontinuous list of values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DiscontiguousList<T> : IList<T>, IReadOnlyList<T>
    {
        /// <summary>
        /// Encapsulates a method that remaps an old index to a new one .
        /// </summary>
        /// <param name="olderIndex"></param>
        /// <param name="newerIndex"></param>
        public delegate void DefragAction(int olderIndex, int newerIndex);

        private readonly List<T> _list;
        private readonly HashSet<int> _removeds;
        private readonly Dictionary<int, bool> _invalids;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscontiguousList{T}"/> class.
        /// </summary>
        public DiscontiguousList()
        {
            _list = new List<T>();
            _removeds = new HashSet<int>();
            _invalids = new Dictionary<int, bool>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscontiguousList{T}"/> class that 
        /// contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        public DiscontiguousList(IEnumerable<T> collection)
        {
            _list = new List<T>(collection);
            _removeds = new HashSet<int>();
            _invalids = new Dictionary<int, bool>();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="DiscontiguousList{T}"/> is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets the number of elements contained in the <see cref="DiscontiguousList{T}"/>.
        /// </summary>
        public int Count => _list.Count;

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index] 
        { 
            get => _list[index];
            set 
            {
                _list[index] = value;

                if(!EqualityComparer<T>.Default.Equals(value, default))
                {
                    if(!_invalids.TryGetValue(index, out var b) || b)
                        _invalids[index] = false;
                    _removeds.Remove(index);
                }
                else
                {
                    if (!_invalids.TryGetValue(index, out var b) || !b)
                        _invalids[index] = true;
                    _removeds.Add(index);
                }
            }
        }


        /// <summary>
        /// Adds an item to the <see cref="DiscontiguousList{T}"/>.
        /// </summary>
        /// <param name="item">The objejct to add to the <see cref="DiscontiguousList{T}"/>.</param>
        /// <returns>The index of <paramref name="item"/> item in the list.</returns>
        public int Add(in T item)
        {
            int index;

            try
            {
                index = _removeds.First();
                _list[index] = item;
            }
            catch(InvalidOperationException)
            {
                index = _list.Count;
                _list.Add(item);
            }

            if (_invalids.ContainsKey(index) && _invalids[index])
                _invalids[index] = false;

            return index;
        }

        /// <summary>
        /// Removes all items form the <see cref="DiscontiguousList{T}"/>.
        /// </summary>
        public void Clear()
        {
            _removeds.Clear();
            _list.Clear();
            _invalids.Clear();
        }

        /// <summary>
        /// Determines wheter the <see cref="DiscontiguousList{T}"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="DiscontiguousList{T}"/>.</param>
        /// <returns></returns>
        public bool Contains(T item) => _list.Contains(item);

        /// <summary>
        /// Copies the elements of the <see cref="DiscontiguousList{T}"/> to an <see cref="Array"/>,
        /// starting at a particular index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the 
        /// elements copied from <see cref="DiscontiguousList{T}"/>.</param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0; i < _list.Count; i++)
            {
                if (!(_invalids.ContainsKey(i) && _invalids[i]))
                    yield return _list[i];
            }
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="DiscontiguousList{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="DiscontiguousList{T}"/>.</param>
        /// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            int index = 0;
            foreach(var element in this)
            {
                if (element is not null && element.Equals(item))
                    return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the first occurence of a specific object from the <see cref="DiscontiguousList{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="DiscontiguousList{T}"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="item"/> was successfully removed 
        /// from the <see cref="DiscontiguousList{T}"/>; otherwise, <see langword="false"/>.</returns>
        public bool Remove(T item)
        {
            int index = _list.IndexOf(item);
            if(index != -1)
            {
                _removeds.Add(index);
                _invalids[index] = true;
#pragma warning disable CS8601
                _list[index] = default;
#pragma warning restore CS8601
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the <see cref="DiscontiguousList{T}"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-bsed index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            if (!(_invalids.ContainsKey(index) &&  _invalids[index]) && index >= 0 && index < _list.Count)
            {
                _invalids[index] = true;
                _removeds.Add(index);
            }
        }

        /// <summary>
        /// Degragment the <see cref="DiscontiguousList{T}"/>.
        /// </summary>
        /// <param name="action">Function used to remap indexes externally.</param>
        public void Defrag(DefragAction action)
        {
            List<T> aux = new List<T>(_list.Count);
            for (int i = 0; i < _list.Count; i++)
            {
                if (!(_invalids.ContainsKey(i) && _invalids[i]))
                {
                    aux.Add(_list[i]);
                    action?.Invoke(i, aux.Count - 1);
                }
            }
            Clear();
            aux.AddRange(aux);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<T>.Add(T item) => Add(item);
    }
}
