// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ez.Memory
{
    /// <summary>
    /// A static class with useful methods for memory manipulation operations.
    /// </summary>
    public static class MemUtil
    {
        static readonly int PlatformWordSize = IntPtr.Size;
        static readonly int PlatformWordSizeBits = PlatformWordSize * 8;

        /// <summary>
        /// Gets the size in bytes of an unmanaged type.
        /// </summary>
        /// <typeparam name="T">The unmanaged type to measure.</typeparam>
        /// <returns>Size in bytes of <typeparamref name="T"/>.</returns>
        public static uint SizeOf<T>() where T : unmanaged
        {
            unsafe
            {
                return (uint)sizeof(T);
            }
        }

        /// <summary>
        /// Gets the size of a <see cref="ReadOnlySpan{T}"/> in bytes.
        /// </summary>
        /// <typeparam name="T">The unmanaged type to measure.</typeparam>
        /// <param name="span">The span to measure</param>
        /// <returns>Size in bytes of <paramref name="span"/>.</returns>
        public static ulong SizeOf<T>(ReadOnlySpan<T> span) where T : unmanaged
        {
            unsafe
            {
                return (ulong)span.Length * (ulong)sizeof(T);
            }
        }

        /// <summary>
        /// Adds an offset to the value of a pointer.
        /// </summary>
        /// <param name="ptr">The pointer to add the offset to.</param>
        /// <param name="offset">The offset to add.</param>
        /// <returns>A new pointer that reflects the addition of <paramref name="offset"/> 
        /// to <paramref name="ptr"/>.</returns>
        public static IntPtr Add(IntPtr ptr, ulong offset)
        {
            unsafe
            {
                return new IntPtr((byte*)ptr.ToPointer() + offset);
            }
        }

        /// <summary>
        /// Returns a value indicating whether an instance is anywhere in the array.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of element to check.</typeparam>
        /// <param name="value">The value to compare.</param>
        /// <param name="list">The list of values to compare with <paramref name="value"/>.</param>
        /// <returns><see langword="true"/> if the <paramref name="value"/> parameter 
        /// is contained in the <paramref name="list"/>; otherwise, <see langword="false"/></returns>
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

        /// <summary>
        /// Returns a value indicating whether a <see cref="ReadOnlySpan{T}"/> is equal 
        /// to another <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a">The first <see cref="ReadOnlySpan{T}"/> to compare.</param>
        /// <param name="b">The second <see cref="ReadOnlySpan{T}"/> to compare.</param>
        /// <returns><see langword="true"/> if the span <paramref name="a"/> parameter 
        /// equals to span <paramref name="b"/> parameter; otherwise, <see langword="false"/></returns>
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

        /// <summary>
        /// Returns a value indicating whether the content of one pointer is equal
        /// to that of another pointer by a specified number of bytes.
        /// </summary>
        /// <param name="a">The first pointer to compare.</param>
        /// <param name="b">The second pointer to compare.</param>
        /// <param name="byteCount">The number of bytes to compare.</param>
        /// <returns><see langword="true"/> if the contents of the pointer <paramref name="a"/> 
        /// are equal to contents of the pointer <paramref name="b"/> by <paramref name="byteCount"/> bytes.</returns>
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

        /// <summary>
        /// Sets all bytes of a <see cref="Span{T}"/> to a specified value.
        /// </summary>
        /// <typeparam name="T">The type of items in the <paramref name="span"/>.</typeparam>
        /// <param name="span">The span to be set.</param>
        /// <param name="value">The byte value to set.</param>
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

        public static unsafe void Set<TBuffer, TValue>(ReadOnlySpan<TBuffer> span, TValue value) where TBuffer : unmanaged where TValue : unmanaged
        {
            fixed (void* ptr = span)
            {
                Set((IntPtr)ptr, value, ((ulong)span.Length * (ulong)sizeof(TBuffer)) / (ulong)sizeof(TValue));
            }
        }

        public static unsafe TValue Get<TBuffer, TValue>(ReadOnlySpan<TBuffer> span) where TBuffer : unmanaged where TValue :unmanaged
        {
            fixed (void* ptr = span)
                return *(TValue*)ptr;
        }

        public static unsafe T Get<T>(IntPtr ptr) where T : unmanaged => *(T*)ptr.ToPointer();

        /// <summary>
        /// Sets all first <paramref name="byteCount"/> bytes to the <paramref name="value"/> byte. 
        /// </summary>
        /// <param name="memoryPtr">The pointer to the first byte.</param>
        /// <param name="value">The byte value to set.</param>
        /// <param name="byteCount">The number of bytes to set.</param>
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

        /// <summary>
        /// Sets all the first <paramref name="count"/> Ts to the <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ptr">The pointer to the first T to set.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="count">The number of Ts to set.</param>
        public static unsafe void Set<T>(IntPtr ptr, in T value, ulong count) where T : unmanaged
        {
            var pptr = (T*)ptr;
            while (count > 0)
            {
                *pptr = value;
                count--;
                pptr++;
            }
        }

        public static unsafe void Set<T>(IntPtr ptr, in T value) where T : unmanaged =>
            *((T*)ptr.ToPointer()) = value;

        /// <summary>
        /// Copies all data from one <see cref="ReadOnlySpan{T}"/> to a <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items in the <paramref name="destination"/> and <paramref name="source"/>.</typeparam>
        /// <param name="destination">The <see cref="Span{T}"/> that receives the data.</param>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> that contains the data to copy.</param>
        /// <returns>Number of bytes copied.</returns>
        public static ulong Copy<T>(Span<T> destination, ReadOnlySpan<T> source) where T : unmanaged => Copy<T, T>(destination, source);

        public static ulong Copy<T>(Span<T> destination, IntPtr source) where T : unmanaged =>
            Copy(destination, GetSpan<T>(source, destination.Length));

        /// <summary>
        /// Copies all data from one <see cref="ReadOnlySpan{T}"/> to a <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="TDestination">The type of items in the <paramref name="destination"/>.</typeparam>
        /// <typeparam name="TSource">The type of items in the <paramref name="source"/>.</typeparam>
        /// <param name="destination">The <see cref="Span{T}"/> that receives the data.</param>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> that contains the data to copy.</param>
        /// <returns>Number of bytes copied.</returns>
        public static ulong Copy<TDestination, TSource>(Span<TDestination> destination, ReadOnlySpan<TSource> source) 
            where TDestination : unmanaged 
            where TSource : unmanaged
        {
            unsafe
            {
                ulong srcSize = SizeOf(source);
                if (srcSize > SizeOf<TDestination>(destination))
                    throw new ArgumentOutOfRangeException($"The destination is too small to copy all data from the source. \nSource size: {srcSize} bytes.\nDestination size: {SizeOf<TDestination>(destination)} bytes.");

                fixed (void* dst = destination, src = source)
                    Copy(dst, src, srcSize);

                return srcSize;
            }
        }

        /// <summary>
        /// Copies all data from a <see cref="ReadOnlySpan{T}"/> to a destination address.
        /// </summary>
        /// <typeparam name="T">The type of items in the <paramref name="src"/>.</typeparam>
        /// <param name="dst">The destination address to copy to.</param>
        /// <param name="src">The <see cref="ReadOnlySpan{T}"/> that contains the data to copy.</param>
        /// <returns>Number of bytes copied.</returns>
        public static unsafe ulong Copy<T>(IntPtr dst, ReadOnlySpan<T> src) where T : unmanaged => Copy(dst.ToPointer(), src);

        /// <summary>
        /// Copies all data from a <see cref="ReadOnlySpan{T}"/> to a destination address.
        /// </summary>
        /// <typeparam name="T">The type of items in the <paramref name="src"/>.</typeparam>
        /// <param name="dst">The destination address to copy to.</param>
        /// <param name="src">The <see cref="ReadOnlySpan{T}"/> that contains the data to copy.</param>
        /// <returns>Number of bytes copied.</returns>
        public static unsafe ulong Copy<T>(void* dst, ReadOnlySpan<T> src) where T : unmanaged
        {
            fixed (void* srcPtr = src)
            {
                ulong size = SizeOf(src);
                Copy(dst, srcPtr, size);
                return size;
            }
        }

        /// <summary>
        /// Copies all data from a T value to a destination address.
        /// </summary>
        /// <typeparam name="T">The type of data to copy.</typeparam>
        /// <param name="dst">The destination address to copy to.</param>
        /// <param name="src">The value to copy.</param>
        /// <returns>Number of bytes copied.</returns>
        public static ulong Copy<T>(IntPtr dst, in T src) where T : unmanaged
        {
            unsafe
            {
                *(T*)dst.ToPointer() = src;
                return SizeOf<T>();
            }
        }

        /// <summary>
        /// Copies bytes from the source address to the destination address.
        /// </summary>
        /// <param name="destination">The destination address to copy to.</param>
        /// <param name="source">The source address to copy from.</param>
        /// <param name="byteCount">The number of bytes to copy.</param>
        public static unsafe void Copy(IntPtr destination, IntPtr source, ulong byteCount) => Copy((void*)destination, (void*)source, byteCount);

        /// <summary>
        /// Copies bytes from the source address to the destination address.
        /// </summary>
        /// <param name="destination">The destination address to copy to.</param>
        /// <param name="source">The source address to copy from.</param>
        /// <param name="byteCount">The number of bytes to copy.</param>
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

        /// <summary>
        /// Gets the theoretical limit for an allocation.
        /// <seealso cref="Alloc(ulong)"/>.
        /// </summary>
        public static readonly ulong MaxAllocSize =
            // Maximum allocable memory based on the largest positive number that an IntPtr can represent.
            (1ul << ((int)SizeOf<IntPtr>() * 8 - 1)) - 1ul;

        /// <summary>
        /// Allocates memory from unmanaged memory of process.
        /// </summary>
        /// <param name="size">The required number of bytes in memory.</param>
        /// <returns>A pointer to the newly allocated memory. This memory must be released using the <see cref="Free(void*)"/> method.</returns>
        public static unsafe void* Alloc(ulong size) => Marshal.AllocHGlobal((IntPtr)size).ToPointer();

        /// <summary>
        /// Frees memory previously allocated from the unmanaged memory of the process.
        /// </summary>
        /// <param name="ptr">The handle returned by the original matching call to <see cref="Alloc(ulong)"/>.</param>
        public static unsafe void Free(void* ptr) => Marshal.FreeHGlobal((IntPtr)ptr);

        public unsafe static Span<T> GetSpan<T>(IntPtr ptr, int length) where T : unmanaged =>
            new Span<T>((T*)ptr, length);

        public unsafe static Memory<T> GetMemory<T>(IntPtr ptr, int length) where T : unmanaged =>
            new UnmanagedMemoryManager<T>(ptr, length).Memory;

        public static string GetUtf8String(IntPtr ptr) => Marshal.PtrToStringUTF8(ptr);
    }
}