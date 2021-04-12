using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ez.Collections
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Converts the <paramref name="collection"/> to <see cref="IReadOnlyCollection{T}"/>, if implemented, 
        /// or create a <see cref="IReadOnlyCollection{T}"/> wrapper for the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">Type of elements in <paramref name="collection"/>.</typeparam>
        /// <param name="collection">The <see cref="ICollection{T}"/> to turn into <see cref="IReadOnlyCollection{T}"/>.</param>
        /// <returns>The <see cref="IReadOnlyCollection{T}"/> representation of <paramref name="collection"/>.</returns>
        public static IReadOnlyCollection<T> AsReadOnly<T>(this ICollection<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            return collection as IReadOnlyCollection<T> ?? new ReadOnlyCollectionWrapper<T>(collection);
        }

        /// <summary>
        /// Creates a shallow copy of <paramref name="ts"/>.
        /// </summary>
        /// <typeparam name="T">Type of each element in array.</typeparam>
        /// <param name="ts">The array to create a shallow copy.</param>
        /// <returns>A shallow copy of <paramref name="ts"/>.</returns>
        public static T[] ShallowCopy<T>(this T[] ts) => 
            (T[])ts.Clone();

        /// <summary>
        /// Creates a shallow copy of <paramref name="us"/> in other format.
        /// </summary>
        /// <typeparam name="T">The type of each element in the shallow array copy.</typeparam>
        /// <typeparam name="U">The type of each element in the <paramref name="us"/> array.</typeparam>
        /// <param name="us">The array to make a copy.</param>
        /// <returns>A shallow copy of <paramref name="us"/> with <see cref="T"/> type.</returns>
        public static T[] ShallowCopy<T, U>(this U[] us) where U : T =>
            Array.ConvertAll(us, (U u) => (T)u);

        /// <summary>
        /// Creates a shallow copy of <paramref name="us"/> in other format.
        /// </summary>
        /// <typeparam name="U">The type of each element in the <paramref name="us"/> array.</typeparam>
        /// <typeparam name="T">The type of each element in the shallow array copy.</typeparam>
        /// <param name="us">The array to make a copy.</param>
        /// <returns>A shallow copy of <paramref name="us"/> with <see cref="T"/> type.</returns>
        public static T[] ShallowCopy<U, T>(this U[] us) where T : U =>
            Array.ConvertAll(us, (U u) => (T)u);

        /// <summary>
        /// Resize <paramref name="array"/> to <paramref name="size"/>, or creates a new array, if <paramref name="array"/> is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="size"></param>
        public static void MinimumArray<T>(ref T[] array, int size)
        {
            if (array == null)
                array = new T[size];
            else
                Array.Resize(ref array, size);
        }

        private class ReadOnlyCollectionWrapper<T> : IReadOnlyCollection<T>
        {
            private readonly ICollection<T> _collection;

            public ReadOnlyCollectionWrapper(ICollection<T> collection)
            {
                _collection = collection;
            }

            public int Count => _collection.Count;

            public IEnumerator<T> GetEnumerator() => _collection.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();
        }
    }
}
