using Ez.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ez.Magics
{
    /// <summary>
    /// A collection of <see cref="IMagiced"/> with <see cref="IMagicSupport"/> implementation.
    /// </summary>
    /// <typeparam name="TMagiced">The type of elements in collection</typeparam>
    internal sealed class MagicedCollection<TMagiced> : IMagicSupport, ICollection<TMagiced> where TMagiced : IMagiced
    {
        private readonly HashSet<Type> _supported;
        private readonly HashSet<TMagiced> _references;
        private readonly MultiValueDicionary<Type, TMagiced> _links;

        /// <summary>
        /// Creates a new instance of <see cref="MagicedCollection{TMagiced}"/> class.
        /// </summary>
        /// <param name="displayName">The display name of the new instance.</param>
        public MagicedCollection(string displayName)
        {
            DisplayName = displayName;
            _supported = new HashSet<Type>();
            _references = new HashSet<TMagiced>();
            _links = new MultiValueDicionary<Type, TMagiced>();
        }

        /// <summary>
        /// Try gets a <typeparamref name="TMagiced"/> instance with <typeparamref name="T"/> support.
        /// </summary>
        /// <typeparam name="T">The type expected to be supported in 
        /// <paramref name="magiced"/>.</typeparam>
        /// <param name="magiced">A <typeparamref name="TMagiced"/> that has support for type 
        /// <typeparamref name="T"/>.</param>
        /// <returns></returns>
        public bool TryGetMagiced<T>(out TMagiced magiced)
        {
            if (!_links.TryGetValue(typeof(T), out var collection))
            {
                magiced = default;
                return false;
            }
            magiced = collection.FirstOrDefault();
            return true;
        }

        #region IMagicSupport implementation
        /// <summary>
        /// The display name of this collection.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Gets the types supported by <typeparamref name="TMagiced"/> itens in
        /// this collection.
        /// </summary>
        public IEnumerable<Type> Types => _supported;

        /// <summary>
        /// Checks if <typeparamref name="T"/> is suppoted by any
        /// <typeparamref name="TMagiced"/> item in this collection.
        /// </summary>
        /// <typeparam name="T">The type to be checked.</typeparam>
        /// <returns><see langword="true"/> if suppoted, otherwise 
        /// <see langword="false"/>.</returns>
        public bool Supports<T>() => _supported.Contains(typeof(T));
        #endregion

        #region ICollection<Magiced> implementation

        /// <summary>
        /// Gets the number of elements contained in the
        /// <see cref="MagicedCollection{TMagiced}"/>.
        /// </summary>
        public int Count => _references.Count;

        /// <summary>
        /// Always <see langword="true"/>.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Adds an item to the <see cref="MagicedCollection{TMagiced}"/>.
        /// </summary>
        /// <param name="item">The object to add to the
        /// <see cref="MagicedCollection{TMagiced}"/>.</param>
        public void Add(TMagiced item)
        {
            if (_references.Add(item))
                foreach (var type in item.MagicSupport.Types)
                {
                    _links.Add(type, item);
                    _supported.Add(type);
                }
        }

        /// <summary>
        /// Removes all items from the <see cref="MagicedCollection{TMagiced}"/>.
        /// </summary>
        public void Clear()
        {
            _supported.Clear();
            _references.Clear();
            _links.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="MagicedCollection{TMagiced}"/> 
        /// contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the
        /// <see cref="MagicedCollection{TMagiced}"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="item"/> is 
        /// found in the <see cref="MagicedCollection{TMagiced}"/>; otherwise,
        /// <see langword="false"/>.</returns>
        public bool Contains(TMagiced item) => _references.Contains(item);

        /// <summary>
        /// Removes the first occurrence of a specific object from the
        /// <see cref="MagicedCollection{TMagiced}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the 
        /// <see cref="MagicedCollection{TMagiced}"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="item"/> was successfully 
        /// removed from the <see cref="MagicedCollection{TMagiced}"/>; otherwise, 
        /// <see langword="false"/>. This method also returns <see langword="false"/> 
        /// if item is not found in the original <see cref="MagicedCollection{TMagiced}"/>.</returns>
        public bool Remove(TMagiced item)
        {
            if (!_references.Remove(item))
                return false;

            foreach (var type in item.MagicSupport.Types)
            {
                _links.Remove(type, item);

                if (!_links.ContainsKey(type))
                    _supported.Remove(type);
            }

            return true;
        }

        /// <summary>
        /// Copies the elements of the <see cref="MagicedCollection{TMagiced}"/> 
        /// to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the
        /// destination of the elements copied from <see cref="MagicedCollection{TMagiced}"/>. 
        /// The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at 
        /// which copying begins.</param>
        public void CopyTo(TMagiced[] array, int arrayIndex) =>
            _references.CopyTo(array, arrayIndex);

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="MagicedCollection{TMagiced}"/>.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through 
        /// the <see cref="MagicedCollection{TMagiced}"/>.</returns>
        public IEnumerator<TMagiced> GetEnumerator() =>
            _references.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
        #endregion
    }
}
