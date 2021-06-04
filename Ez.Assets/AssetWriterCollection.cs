using Ez.Magics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Ez.Assets
{
    /// <summary>
    /// A collection of <see cref="IAssetWriter"/>.
    /// </summary>
    public class AssetWriterCollection : IAssetWriter, ICollection<IAssetWriter>
    {
        private readonly MagicedCollection<IAssetWriter> _writers;

        /// <summary>
        /// Creates a new instance of <see cref="AssetWriterCollection"/> class.
        /// </summary>
        /// <param name="displayName">The value of <see cref="IMagicSupport.DisplayName"/>
        /// in <see cref="MagicSupport"/>.</param>
        public AssetWriterCollection(string displayName)
        {
            _writers = new MagicedCollection<IAssetWriter>(displayName);
        }

        /// <summary>
        /// Gets <see cref="IMagicSupport"/> of the <see cref="AssetWriterCollection"/>.
        /// </summary>
        public IMagicSupport MagicSupport => _writers;

        /// <summary>
        /// Gets the number of elements contained in the <see cref="AssetWriterCollection"/>.
        /// </summary>
        public int Count => _writers.Count;

        /// <summary>
        /// Gets a value indicating whether the <see cref="AssetWriterCollection"/> is read-only.
        /// </summary>
        public bool IsReadOnly => _writers.IsReadOnly;

        /// <summary>
        /// Adds an item to the <see cref="AssetWriterCollection"/>.
        /// </summary>
        /// <param name="item"></param>
        public void Add(IAssetWriter item) => _writers.Add(item);

        /// <summary>
        /// Removes all items from the <see cref="AssetWriterCollection"/>.
        /// </summary>
        public void Clear() => _writers.Clear();

        /// <summary>
        /// Determines whether the <see cref="AssetWriterCollection"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="AssetWriterCollection"/>.</param>       
        /// <returns><see langword="true"/> if item is found in the <see cref="AssetWriterCollection"/>;
        /// otherwise, <see langword="false"/></returns>
        public bool Contains(IAssetWriter item) => _writers.Contains(item);

        /// <summary>
        /// Copies the elements of the <see cref="AssetWriterCollection"/> to an <see cref="Array"/>,
        /// starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of 
        /// the elements copied from <see cref="AssetWriterCollection"/>. The <see cref="Array"/> must
        /// have zero-based indexing.</param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(IAssetWriter[] array, int arrayIndex) =>
            _writers.CopyTo(array, arrayIndex);

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="AssetWriterCollection"/>.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="AssetWriterCollection"/>.</returns>
        public IEnumerator<IAssetWriter> GetEnumerator() =>
            _writers.GetEnumerator();

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="AssetWriterCollection"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(IAssetWriter item) =>
            _writers.Remove(item);

        /// <summary>
        /// Based on the <see cref="IMagiced.MagicSupport"/> property it takes the first 
        /// <see cref="IAssetWriter"/> of the <see cref="AssetWriterCollection"/> that supports
        /// type <typeparamref name="T"/> and tries to write an <paramref name="value"/> to the stream.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="value"/>.</typeparam>
        /// <param name="value">The instance to be written.</param>
        /// <param name="stream">The destination stream.</param>
        /// <returns><see langword="true"/> if the value was written; otherwise,
        /// <see langword="false"/>.</returns>
        public bool TryWrite<T>(in T value, Stream stream)
        {
            if (!_writers.TryGetMagiced<T>(out var writer))
                return false;

            return writer.TryWrite<T>(value, stream);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
