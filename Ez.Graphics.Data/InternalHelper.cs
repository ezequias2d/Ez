using Ez.Memory;
using System;
using System.Buffers;

namespace Ez.Graphics.Data
{
    internal class InternalHelper
    {
        public static void Set<T>(ref T[] array, ref int length, ReadOnlySpan<T> value) where T : unmanaged
        {
            if (array == null || !(value.Length <= array.Length))
            {
                if (array != null && array.Length > 0)
                    ArrayPool<T>.Shared.Return(array);
                array = ArrayPool<T>.Shared.Rent(value.Length);
            }
            length = value.Length;
            MemUtil.Copy(array, value);
        }

        public static void SetManaged<T>(ref T[] array, ref int length, ReadOnlySpan<T> value)
        {
            if (value == default || value.Length == 0)
            {
                array = Array.Empty<T>();
                length = 0;
                return;
            }

            if (!(value.Length <= array.Length))
            {
                ArrayPool<T>.Shared.Return(array);
                array = ArrayPool<T>.Shared.Rent(value.Length);
            }

            length = value.Length;

            for (int i = 0; i < value.Length; i++)
                array[i] = value[i];
        }
    }
}
