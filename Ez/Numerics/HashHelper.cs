using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ez.Numerics
{
    public static class HashHelper<T>
    {
        static HashHelper()
        {
            BaseHash = new Random().Next();
        }

        public static int BaseHash
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Combine(in int h1, in int h2) => ((h1 << 5) + h1) ^ h2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetHash<U>(in U u) => HashHelper<U>.Combine(BaseHash, u.GetHashCode());


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2>(in T1 t1, in T2 t2) =>
            Combine(Combine(BaseHash, GetHash(t1)), GetHash(t2));


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3>(in T1 t1, in T2 t2, in T3 t3) =>
            Combine(Combine(t1, t2), GetHash(t3));


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4>(in T1 t1, in T2 t2, in T3 t3, in T4 t4) =>
            Combine(Combine(t1, t2, t3), GetHash(t4));


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5>(in T1 t1, in T2 t2, in T3 t3, in T4 t4, in T5 t5) =>
            Combine(Combine(t1, t2, t3, t4), GetHash(t5));


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6>(in T1 t1, in T2 t2, in T3 t3, in T4 t4, in T5 t5, in T6 t6) =>
            Combine(Combine(t1, t2, t3, t4, t5), GetHash(t6));


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7>(in T1 t1, in T2 t2, in T3 t3, in T4 t4, in T5 t5, in T6 t6, in T7 t7) =>
            Combine(Combine(t1, t2, t3, t4, t5, t6), GetHash(t7));


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(in T1 t1, in T2 t2, in T3 t3, in T4 t4, in T5 t5, in T6 t6, in T7 t7, in T8 t8) =>
            Combine(Combine(t1, t2, t3, t4, t5, t6, t7), GetHash(t8));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9>(in T1 t1, in T2 t2, in T3 t3, in T4 t4, in T5 t5, in T6 t6, in T7 t7, in T8 t8, in T9 t9) =>
            Combine(Combine(t1, t2, t3, t4, t5, t6, t7, t8), GetHash(t9));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in T1 t1, in T2 t2, in T3 t3, in T4 t4, in T5 t5, in T6 t6, in T7 t7, in T8 t8, in T9 t9, in T10 t10) =>
            Combine(Combine(t1, t2, t3, t4, t5, t6, t7, t8, t9), GetHash(t10));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(in T1 t1, in T2 t2, in T3 t3, in T4 t4, in T5 t5, in T6 t6, in T7 t7, in T8 t8, in T9 t9, in T10 t10, in T11 t11) =>
            Combine(Combine(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10), GetHash(t11));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(in T1 t1, in T2 t2, in T3 t3, in T4 t4, in T5 t5, in T6 t6, in T7 t7, in T8 t8, in T9 t9, in T10 t10, in T11 t11, in T12 t12) =>
            Combine(Combine(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11), GetHash(t12));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(in T1 t1, in T2 t2, in T3 t3, in T4 t4, in T5 t5, in T6 t6, in T7 t7, in T8 t8, in T9 t9, in T10 t10, in T11 t11, in T12 t12, in T13 t13) =>
            Combine(Combine(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12), GetHash(t13));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine(params T[] ts)
        {
            if (ts is null)
                return 0;

            return Combine((ReadOnlySpan<T>)ts);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<U>(params U[] us)
        {
            int hash = BaseHash;
            if (us != null)
                for (int i = 0; i < us.Length; i++)
                    hash = Combine(hash, us[i].GetHashCode());
            return hash;
        }

        public static int Combine<T>(ReadOnlySpan<T> ts)
        {
            int hash = BaseHash;
            if (ts != null && ts.Length > 0)
            {
                hash = ts[0].GetHashCode();
                for (int i = 1; i < ts.Length; i++)
                    hash = Combine(hash, ts[i].GetHashCode());
            }
            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<U>(IEnumerable<U> ts)
        {
            int hash = BaseHash;
            foreach(var val in ts)
                hash = Combine(hash, val.GetHashCode());
            return hash;
        }
    }
}
