using Ez.Magics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Ez.Assets
{
    /// <summary>
    /// A collection of <see cref="IAssetReader"/>.
    /// </summary>
    public class AssetReaderCollection : IAssetReader, ICollection<IAssetReader>
    {
        private readonly MagicedCollection<IAssetReader> _readers;

        /// <summary>
        /// Creates a new instance of <see cref="AssetReaderCollection"/> class.
        /// </summary>
        /// <param name="displayName">The value of <see cref="IMagicSupport.DisplayName"/>
        /// in <see cref="MagicSupport"/>.</param>
        public AssetReaderCollection(string displayName)
        {
            _readers = new MagicedCollection<IAssetReader>(displayName);
        }

        /// <summary>
        /// Gets <see cref="IMagicSupport"/> of the <see cref="AssetReaderCollection"/>
        /// </summary>
        public IMagicSupport MagicSupport => _readers;

        /// <summary>
        /// Gets the number of elements contained in the <see cref="AssetReaderCollection"/>.
        /// </summary>
        public int Count => _readers.Count;

        /// <summary>
        /// Gets a value indicating whether the <see cref="AssetReaderCollection"/> is read-only.
        /// </summary>
        public bool IsReadOnly => _readers.IsReadOnly;

        /// <summary>
        /// Adds an item to the <see cref="AssetReaderCollection"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="AssetReaderCollection"/>.</param>
        public void Add(IAssetReader item) => _readers.Add(item);

        /// <summary>
        /// Removes all items from the <see cref="AssetReaderCollection"/>.
        /// </summary>
        public void Clear() => _readers.Clear();

        /// <summary>
        /// Determines whether the <see cref="AssetReaderCollection"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="AssetReaderCollection"/>.</param>
        /// <returns><see langword="true"/> if item is found in the <see cref="AssetReaderCollection"/>; 
        /// otherwise, <see langword="false"/>.</returns>
        public bool Contains(IAssetReader item) => _readers.Contains(item);

        /// <summary>
        /// Copies the elements of the <see cref="AssetReaderCollection"/> to an <see cref="Array"/>, 
        /// starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of 
        /// the elements copied from <see cref="AssetReaderCollection"/>. The <see cref="Array"/> must
        /// have zero-based indexing.</param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(IAssetReader[] array, int arrayIndex) =>
            _readers.CopyTo(array, arrayIndex);

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="AssetReaderCollection"/>.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="AssetReaderCollection"/>.</returns>
        public IEnumerator<IAssetReader> GetEnumerator() =>
            _readers.GetEnumerator();

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="AssetReaderCollection"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(IAssetReader item) =>
            _readers.Remove(item);

        /// <summary>
        /// Based on the <see cref="IMagiced.MagicSupport"/> property it takes the first 
        /// <see cref="IAssetReader"/> of the <see cref="AssetReaderCollection"/> that supports
        /// type <typeparamref name="T"/> and tries to read an <paramref name="value"/> of the stream.
        /// </summary>
        /// <typeparam name="T">The type of instance to be readed.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="value">When this method returns, contains the readed instance, if it is 
        /// available, otherwise <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the value was read; otherwise,
        /// <see langword="false"/>.</returns>
        public bool TryRead<T>(Stream stream, out T value)
        {
            if (!_readers.TryGetMagiced<T>(out var reader))
            {
                value = default;
                return false;
            }

            return reader.TryRead(stream, out value);
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
