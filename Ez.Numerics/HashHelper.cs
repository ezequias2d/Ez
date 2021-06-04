// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ez.Numerics
{
    /// <summary>
    /// A static helper class to handle things related to hash code.
    /// </summary>
    /// <typeparam name="T">The destination type to calculate a <see cref="HashHelper{T}.BaseHash"/>.</typeparam>
    public static class HashHelper<T>
    {
        static HashHelper()
        {
            BaseHash = new Random().Next();
        }

        /// <summary>
        /// A hash code to be the basis of a hash combine function.
        /// </summary>
        public static int BaseHash
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Combine(in int h1, in int h2) => HashCode.Combine(h1, h2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetHash<U>(in U u) => HashHelper<U>.Combine(BaseHash, u.GetHashCode());

        /// <summary>
        /// Combines one values into a hash code.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <returns>The hash code that represents the one value.</returns>

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1>(in T1 value1) =>
            Combine(BaseHash, GetHash(value1));

        /// <summary>
        /// Combines two values into a hash code.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
        /// <typeparam name="T2">The type of the second value to combine into the hash code.</typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <returns>The hash code that represents the two values.</returns>

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2>(in T1 value1, in T2 value2) =>
            Combine(Combine(BaseHash, GetHash(value1)), GetHash(value2));

        /// <summary>
        /// Combines three values into a hash code.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
        /// <typeparam name="T2">The type of the second value to combine into the hash code.</typeparam>
        /// <typeparam name="T3">The type of the third value to combine into the hash code.</typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <returns>The hash code that represents the three values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3>(in T1 value1, in T2 value2, in T3 value3) =>
            Combine(Combine(value1, value2), GetHash(value3));

        /// <summary>
        /// Combines four values into a hash code.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
        /// <typeparam name="T2">The type of the second value to combine into the hash code.</typeparam>
        /// <typeparam name="T3">The type of the third value to combine into the hash code.</typeparam>
        /// <typeparam name="T4">The type of the fourth value to combine into the hash code.</typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <returns>The hash code that represents the four values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4>(in T1 value1, in T2 value2, in T3 value3, in T4 value4) =>
            Combine(Combine(value1, value2, value3), GetHash(value4));

        /// <summary>
        /// Combines five values into a hash code.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
        /// <typeparam name="T2">The type of the second value to combine into the hash code.</typeparam>
        /// <typeparam name="T3">The type of the third value to combine into the hash code.</typeparam>
        /// <typeparam name="T4">The type of the fourth value to combine into the hash code.</typeparam>
        /// <typeparam name="T5">The type of the fifth value to combine into the hash code.</typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <param name="value5">The fifth value to combine into the hash code.</param>
        /// <returns>The hash code that represents the five values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5>(in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5) =>
            Combine(Combine(value1, value2, value3, value4), GetHash(value5));

        /// <summary>
        /// Combines six values into a hash code.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
        /// <typeparam name="T2">The type of the second value to combine into the hash code.</typeparam>
        /// <typeparam name="T3">The type of the third value to combine into the hash code.</typeparam>
        /// <typeparam name="T4">The type of the fourth value to combine into the hash code.</typeparam>
        /// <typeparam name="T5">The type of the fifth value to combine into the hash code.</typeparam>
        /// <typeparam name="T6">The type of the sixth value to combine into the hash code.</typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <param name="value5">The fifth value to combine into the hash code.</param>
        /// <param name="value6">The sixth value to combine into the hash code.</param>
        /// <returns>The hash code that represents the six values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6>(in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5, in T6 value6) =>
            Combine(Combine(value1, value2, value3, value4, value5), GetHash(value6));

        /// <summary>
        /// Combines seven values into a hash code.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
        /// <typeparam name="T2">The type of the second value to combine into the hash code.</typeparam>
        /// <typeparam name="T3">The type of the third value to combine into the hash code.</typeparam>
        /// <typeparam name="T4">The type of the fourth value to combine into the hash code.</typeparam>
        /// <typeparam name="T5">The type of the fifth value to combine into the hash code.</typeparam>
        /// <typeparam name="T6">The type of the sixth value to combine into the hash code.</typeparam>
        /// <typeparam name="T7">The type of the seventh value to combine into the hash code.</typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <param name="value5">The fifth value to combine into the hash code.</param>
        /// <param name="value6">The sixth value to combine into the hash code.</param>
        /// <param name="value7">The seventh value to combine into the hash code.</param>
        /// <returns>The hash code that represents the seven values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7>(in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5, in T6 value6, in T7 value7) =>
            Combine(Combine(value1, value2, value3, value4, value5, value6), GetHash(value7));

        /// <summary>
        /// Combines eight values into a hash code.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
        /// <typeparam name="T2">The type of the second value to combine into the hash code.</typeparam>
        /// <typeparam name="T3">The type of the third value to combine into the hash code.</typeparam>
        /// <typeparam name="T4">The type of the fourth value to combine into the hash code.</typeparam>
        /// <typeparam name="T5">The type of the fifth value to combine into the hash code.</typeparam>
        /// <typeparam name="T6">The type of the sixth value to combine into the hash code.</typeparam>
        /// <typeparam name="T7">The type of the seventh value to combine into the hash code.</typeparam>
        /// <typeparam name="T8">The type of the eighth value to combine into the hash code.</typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <param name="value5">The fifth value to combine into the hash code.</param>
        /// <param name="value6">The sixth value to combine into the hash code.</param>
        /// <param name="value7">The seventh value to combine into the hash code.</param>
        /// <param name="value8">The eighth value to combine into the hash code.</param>
        /// <returns>The hash code that represents the eight values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5, in T6 value6, in T7 value7, in T8 value8) =>
            Combine(Combine(value1, value2, value3, value4, value5, value6, value7), GetHash(value8));

        /// <summary>
        /// Combines nine values into a hash code.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
        /// <typeparam name="T2">The type of the second value to combine into the hash code.</typeparam>
        /// <typeparam name="T3">The type of the third value to combine into the hash code.</typeparam>
        /// <typeparam name="T4">The type of the fourth value to combine into the hash code.</typeparam>
        /// <typeparam name="T5">The type of the fifth value to combine into the hash code.</typeparam>
        /// <typeparam name="T6">The type of the sixth value to combine into the hash code.</typeparam>
        /// <typeparam name="T7">The type of the seventh value to combine into the hash code.</typeparam>
        /// <typeparam name="T8">The type of the eighth value to combine into the hash code.</typeparam>
        /// <typeparam name="T9">The type of the ninth value to combine into the hash code.</typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <param name="value5">The fifth value to combine into the hash code.</param>
        /// <param name="value6">The sixth value to combine into the hash code.</param>
        /// <param name="value7">The seventh value to combine into the hash code.</param>
        /// <param name="value8">The eighth value to combine into the hash code.</param>
        /// <param name="value9">The ninth value to combine into the hash code.</param>
        /// <returns>The hash code that represents the nine values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9>(in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5, in T6 value6, in T7 value7, in T8 value8, in T9 value9) =>
            Combine(Combine(value1, value2, value3, value4, value5, value6, value7, value8), GetHash(value9));

        /// <summary>
        /// Combines ten values into a hash code.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
        /// <typeparam name="T2">The type of the second value to combine into the hash code.</typeparam>
        /// <typeparam name="T3">The type of the third value to combine into the hash code.</typeparam>
        /// <typeparam name="T4">The type of the fourth value to combine into the hash code.</typeparam>
        /// <typeparam name="T5">The type of the fifth value to combine into the hash code.</typeparam>
        /// <typeparam name="T6">The type of the sixth value to combine into the hash code.</typeparam>
        /// <typeparam name="T7">The type of the seventh value to combine into the hash code.</typeparam>
        /// <typeparam name="T8">The type of the eighth value to combine into the hash code.</typeparam>
        /// <typeparam name="T9">The type of the ninth value to combine into the hash code.</typeparam>
        /// <typeparam name="T10">The type of the tenth value to combine into the hash code.</typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <param name="value5">The fifth value to combine into the hash code.</param>
        /// <param name="value6">The sixth value to combine into the hash code.</param>
        /// <param name="value7">The seventh value to combine into the hash code.</param>
        /// <param name="value8">The eighth value to combine into the hash code.</param>
        /// <param name="value9">The ninth value to combine into the hash code.</param>
        /// <param name="value10">The tenth value to combine into the hash code.</param>
        /// <returns>The hash code that represents the ten values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5, in T6 value6, in T7 value7, in T8 value8, in T9 value9, in T10 value10) =>
            Combine(Combine(value1, value2, value3, value4, value5, value6, value7, value8, value9), GetHash(value10));

        /// <summary>
        /// Combines eleven values into a hash code.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
        /// <typeparam name="T2">The type of the second value to combine into the hash code.</typeparam>
        /// <typeparam name="T3">The type of the third value to combine into the hash code.</typeparam>
        /// <typeparam name="T4">The type of the fourth value to combine into the hash code.</typeparam>
        /// <typeparam name="T5">The type of the fifth value to combine into the hash code.</typeparam>
        /// <typeparam name="T6">The type of the sixth value to combine into the hash code.</typeparam>
        /// <typeparam name="T7">The type of the seventh value to combine into the hash code.</typeparam>
        /// <typeparam name="T8">The type of the eighth value to combine into the hash code.</typeparam>
        /// <typeparam name="T9">The type of the ninth value to combine into the hash code.</typeparam>
        /// <typeparam name="T10">The type of the tenth value to combine into the hash code.</typeparam>
        /// <typeparam name="T11">The type of the eleventh value to combine into the hash code.</typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <param name="value5">The fifth value to combine into the hash code.</param>
        /// <param name="value6">The sixth value to combine into the hash code.</param>
        /// <param name="value7">The seventh value to combine into the hash code.</param>
        /// <param name="value8">The eighth value to combine into the hash code.</param>
        /// <param name="value9">The ninth value to combine into the hash code.</param>
        /// <param name="value10">The tenth value to combine into the hash code.</param>
        /// <param name="value11">The eleventh value to combine into the hash code.</param>
        /// <returns>The hash code that represents the eleven values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5, in T6 value6, in T7 value7, in T8 value8, in T9 value9, in T10 value10, in T11 value11) =>
            Combine(Combine(value1, value2, value3, value4, value5, value6, value7, value8, value9, value10), GetHash(value11));

        /// <summary>
        /// Combines twelve values into a hash code.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
        /// <typeparam name="T2">The type of the second value to combine into the hash code.</typeparam>
        /// <typeparam name="T3">The type of the third value to combine into the hash code.</typeparam>
        /// <typeparam name="T4">The type of the fourth value to combine into the hash code.</typeparam>
        /// <typeparam name="T5">The type of the fifth value to combine into the hash code.</typeparam>
        /// <typeparam name="T6">The type of the sixth value to combine into the hash code.</typeparam>
        /// <typeparam name="T7">The type of the seventh value to combine into the hash code.</typeparam>
        /// <typeparam name="T8">The type of the eighth value to combine into the hash code.</typeparam>
        /// <typeparam name="T9">The type of the ninth value to combine into the hash code.</typeparam>
        /// <typeparam name="T10">The type of the tenth value to combine into the hash code.</typeparam>
        /// <typeparam name="T11">The type of the eleventh value to combine into the hash code.</typeparam>
        /// <typeparam name="T12">The type of the twelfth value to combine into the hash code.</typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <param name="value5">The fifth value to combine into the hash code.</param>
        /// <param name="value6">The sixth value to combine into the hash code.</param>
        /// <param name="value7">The seventh value to combine into the hash code.</param>
        /// <param name="value8">The eighth value to combine into the hash code.</param>
        /// <param name="value9">The ninth value to combine into the hash code.</param>
        /// <param name="value10">The tenth value to combine into the hash code.</param>
        /// <param name="value11">The eleventh value to combine into the hash code.</param>
        /// <param name="value12">The twelfth value to combine into the hash code.</param>
        /// <returns>The hash code that represents the twelve values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5, in T6 value6, in T7 value7, in T8 value8, in T9 value9, in T10 value10, in T11 value11, in T12 value12) =>
            Combine(Combine(value1, value2, value3, value4, value5, value6, value7, value8, value9, value10, value11), GetHash(value12));

        /// <summary>
        /// Combines thirteen values into a hash code.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
        /// <typeparam name="T2">The type of the second value to combine into the hash code.</typeparam>
        /// <typeparam name="T3">The type of the third value to combine into the hash code.</typeparam>
        /// <typeparam name="T4">The type of the fourth value to combine into the hash code.</typeparam>
        /// <typeparam name="T5">The type of the fifth value to combine into the hash code.</typeparam>
        /// <typeparam name="T6">The type of the sixth value to combine into the hash code.</typeparam>
        /// <typeparam name="T7">The type of the seventh value to combine into the hash code.</typeparam>
        /// <typeparam name="T8">The type of the eighth value to combine into the hash code.</typeparam>
        /// <typeparam name="T9">The type of the ninth value to combine into the hash code.</typeparam>
        /// <typeparam name="T10">The type of the tenth value to combine into the hash code.</typeparam>
        /// <typeparam name="T11">The type of the eleventh value to combine into the hash code.</typeparam>
        /// <typeparam name="T12">The type of the twelfth value to combine into the hash code.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth value to combine into the hash code.</typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <param name="value5">The fifth value to combine into the hash code.</param>
        /// <param name="value6">The sixth value to combine into the hash code.</param>
        /// <param name="value7">The seventh value to combine into the hash code.</param>
        /// <param name="value8">The eighth value to combine into the hash code.</param>
        /// <param name="value9">The ninth value to combine into the hash code.</param>
        /// <param name="value10">The tenth value to combine into the hash code.</param>
        /// <param name="value11">The eleventh value to combine into the hash code.</param>
        /// <param name="value12">The twelfth value to combine into the hash code.</param>
        /// <param name="value13">The thirteenth value to combine into the hash code.</param>
        /// <returns>The hash code that represents the thirteen values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5, in T6 value6, in T7 value7, in T8 value8, in T9 value9, in T10 value10, in T11 value11, in T12 value12, in T13 value13) =>
            Combine(Combine(value1, value2, value3, value4, value5, value6, value7, value8, value9, value10, value11, value12), GetHash(value13));

        /// <summary>
        /// Combines values into a hash code.
        /// </summary>
        /// <typeparam name="U">The type of <paramref name="values"/> to combine into a hash code.</typeparam>
        /// <param name="values">The <paramref name="values"/> to combine into the hash code.</param>
        /// <returns>The hash code that represents the <paramref name="values"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<U>(IEnumerable<U> values)
        {
            int hash = BaseHash;
            if(values != null)
                foreach(var val in values)
                    hash = Combine(hash, val.GetHashCode());
            return hash;
        }

        /// <summary>
        /// Combines values into a hash code.
        /// </summary>
        /// <param name="values">The <paramref name="values"/> to combine into the hash code.</param>
        /// <returns>The hash code that represents the <paramref name="values"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine(IEnumerable<T> values)
        {
            int hash = BaseHash;
            if (values != null)
            {
                var enumerator = values.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    hash = enumerator.Current.GetHashCode();
                    while (enumerator.MoveNext())
                        hash = Combine(hash, enumerator.Current.GetHashCode());
                }
            }
            return hash;
        }

        /// <summary>
        /// Combines values into a hash code.
        /// </summary>
        /// <param name="values">The <paramref name="values"/> to combine into the hash code.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine(ReadOnlySpan<T> values)
        {
            int hash = BaseHash;
            if (values.Length > 0)
            {
                var enumerator = values.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    hash = enumerator.Current.GetHashCode();
                    while (enumerator.MoveNext())
                        hash = Combine(hash, enumerator.Current.GetHashCode());
                }
            }
            return hash;
        }

        /// <summary>
        /// Combines values into a hash code.
        /// </summary>
        /// <typeparam name="U">The type of <paramref name="values"/> to combine into a hash code.</typeparam>
        /// <param name="values">The <paramref name="values"/> to combine into the hash code.</param>
        /// <returns>The hash code that represents the <paramref name="values"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<U>(ReadOnlySpan<U> values)
        {
            int hash = BaseHash;
            if (values.Length > 0)
                foreach(var u in values)
                    hash = Combine(hash, u.GetHashCode());
            return hash;
        }
    }
}
