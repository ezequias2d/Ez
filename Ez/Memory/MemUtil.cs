// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Ez.Collections;
using Ez.Collections.Pools;

namespace Ez.Memory
{
    public static class MemUtil
    {
        static readonly int PlatformWordSize = IntPtr.Size;
        static readonly int PlatformWordSizeBits = PlatformWordSize * 8;

        public static uint SizeOf<T>() where T : unmanaged
        {
            unsafe
            {
                return (uint)sizeof(T);
            }
        }

        public static IntPtr Add(IntPtr ptr, ulong offset)
        {
            unsafe
            {
                return new IntPtr((byte*)ptr.ToPointer() + offset);
            }
        }

        public static bool AnyEquals<T>(T value, params T[] list) where T : unmanaged
        {
            unsafe
            {
                fixed (T* pList = list)
                {
                    T* current = pList;
                    T* max = pList + list.Length;

                    while(current < max)
                    {
                        if (Equals(current, &value, (ulong)sizeof(T)))
                            return true;
                        current++;
                    }
                }
            }

            return false;
        }

        public static bool Equals<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b) where T: unmanaged
        {
            unsafe
            {
                int count = a.Length;

                if (count != b.Length)
                    return false;

                fixed (void* ptrA = a, ptrB = b)
                {
                    return Equals(ptrA, ptrB, (ulong)count * (ulong)sizeof(T));
                }
            }
        }

        public static unsafe bool Equals(void* a, void* b, ulong byteCount)
        {
            ulong i1 = 0;
            ulong i2 = 0;
            ulong i4 = 0;
            ulong i8 = 0;

            ulong c8 = (byteCount >> 3);
            ulong c4 = (byteCount - (c8 << 3)) >> 2;
            ulong c2 = (byteCount - (c4 << 2) - (c8 << 3)) >> 1;
            ulong c1 = (byteCount - (c2 << 1) - (c4 << 2) - (c8 << 3));

            byte* aPos = (byte*)a;
            byte* bPos = (byte*)b;

            while(i8 < c8)
            {
                if (*(ulong*)aPos != *(ulong*)bPos)
                    return false;
                aPos += 8;
                i8++;
            }

            while (i4 < c4)
            {
                if (*(uint*)aPos != *(uint*)bPos)
                    return false;
                aPos += 4;
                i4++;
            }

            while (i2 < c2)
            {
                if (*(ushort*)aPos != *(ushort*)bPos)
                    return false;
                aPos += 2;
                i2++;
            }

            while (i1 < c1)
            {
                if (*aPos != *bPos)
                    return false;
                aPos++;
                i1++;
            }

            return true;
        }


        public static void Set<T>(Span<T> span, byte value) where T : unmanaged
        {
            unsafe
            {
                fixed (T* spanPtr = span)
                {
                    Set(spanPtr, value, (ulong)span.Length * (ulong)sizeof(T));
                }
            }
        }

        public static unsafe void Set(void* memoryPtr, byte value, ulong byteCount)
        {
            if(byteCount <= uint.MaxValue)
                Unsafe.InitBlockUnaligned(memoryPtr, value, (uint)byteCount);
            else
            {
                uint bc;
                byte* ptr = (byte*)memoryPtr;
                while(byteCount > 0)
                {
                    bc = (uint)Math.Min(uint.MaxValue, byteCount);
                    Unsafe.InitBlockUnaligned(ptr, value, bc);
                    byteCount -= bc;
                    ptr += bc;
                }
            }
        }

        public static unsafe void SetValue<T>(T* ptr, in T value, ulong count) where T : unmanaged
        {
            while (count > 0)
            {
                *ptr = value;
                count--;
                ptr++;
            }
        }

        public static ulong SizeOf<T>(ReadOnlySpan<T> span) where T : unmanaged
        {
            unsafe
            {
                return (ulong)span.Length * (ulong)sizeof(T);
            }
        }

        public static ulong Copy<T>(Span<T> destination, ReadOnlySpan<T> source) where T : unmanaged => Copy<T, T>(destination, source);

        public static ulong Copy<TDestination, TSource>(Span<TDestination> destination, ReadOnlySpan<TSource> source) 
            where TDestination : unmanaged 
            where TSource : unmanaged
        {
            unsafe
            {
                ulong srcSize = SizeOf(source);
                if (srcSize < SizeOf<TDestination>(destination))
                    throw new ArgumentOutOfRangeException($"The destination is too small to copy all data from the source. \nSource size: {srcSize} bytes.\nDestination size: {SizeOf<TDestination>(destination)} bytes.");

                fixed (void* dst = destination, src = source)
                    Copy(dst, src, srcSize);

                return srcSize;
            }
        }

        public static unsafe ulong Copy<T>(IntPtr dst, ReadOnlySpan<T> src) where T : unmanaged => Copy(dst.ToPointer(), src);

        public static unsafe ulong Copy<T>(void* dst, ReadOnlySpan<T> src) where T : unmanaged
        {
            fixed (void* srcPtr = src)
            {
                ulong size = SizeOf(src);
                Copy(dst, srcPtr, size);
                return size;
            }
        }

        public static ulong Copy<T>(IntPtr dst, in T src) where T : unmanaged
        {
            unsafe
            {
                *(T*)dst.ToPointer() = src;
                return SizeOf<T>();
            }
        }

        public static unsafe void Copy(void* destination, void* source, ulong byteCount)
        {
            if(byteCount <= uint.MaxValue)
                Unsafe.CopyBlockUnaligned(destination, source, (uint)byteCount);            
            else
            {
                uint bc;
                byte* dst = (byte*)destination;
                byte* src = (byte*)source;
                while (byteCount > 0)
                {
                    bc = (uint)Math.Min(uint.MaxValue, byteCount);
                    Unsafe.CopyBlockUnaligned(dst, src, bc);
                    byteCount -= bc;
                    dst += bc;
                    src += bc;
                }
            }
        }

        public static unsafe void* Alloc(ulong size) => Marshal.AllocHGlobal((IntPtr)size).ToPointer();

        public static unsafe void Free(void* ptr) => Marshal.FreeHGlobal((IntPtr)ptr);

        //public static unsafe void Write<T>(void* destination, T value) => Unsafe.Write(destination, value);
        //public static unsafe T Read<T>(void* source) => Unsafe.Read<T>(source);
    }
}
