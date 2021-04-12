// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Memory;
using System;
using System.IO;
using System.Text;

namespace Ez.IO
{
    public static class StreamExtensions
    {

        /// <summary>
        /// Write a string in the stream that ends with 0(byte). 
        /// 
        /// The string is written with utf8 encoding.
        /// <seealso cref="ReadString(Stream)"/>
        /// </summary>
        /// <param name="stream">Stream to write.</param>
        /// <param name="value">String to be written.</param>
        public static void WriteString(this Stream stream, string value)
        {
            byte[] data = Encoding.UTF8.GetBytes(value);
            stream.Write(data, 0, data.Length);
            stream.WriteByte(0);
        }

        /// <summary>
        /// Reads a string from the stream that ends with a 0(byte).
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>The string readed.</returns>
        public static string ReadString(this Stream stream)
        {
            byte[] data;
            using (var bufferStream = new MemoryStream(65536))
            {
                {
                    int aux;
                    while ((aux = stream.ReadByte()) != -1 && aux != 0)
                        bufferStream.WriteByte((byte)aux);
                }
                data = bufferStream.ToArray();
            }

            return Encoding.UTF8.GetString(data);
        }

        public static void WriteSpan<T>(this Stream stream, ReadOnlySpan<T> array) where T : unmanaged
        {
            if (array == null || array.Length == 0)
                return;

            unsafe
            {
                fixed (void* ptr = array)
                {
                    byte[] buffer = new byte[array.Length * sizeof(T)];
                    fixed (void* dst = buffer)
                        MemUtil.Copy(dst, ptr, (ulong)buffer.Length);
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public static ReadOnlySpan<T> ReadSpan<T>(this Stream stream, uint count) where T : unmanaged
        {
            if (count == 0)
                return Array.Empty<T>();

            T[] array = new T[count];
            unsafe
            {
                byte[] buffer = new byte[count * sizeof(T)];
                stream.Read(buffer, 0, buffer.Length);

                fixed (void* src = buffer, dst = array)
                    MemUtil.Copy(dst, src, (ulong)buffer.Length);
            }
            return array;
        }

        public static void WriteStructure<T>(this Stream stream, T value) where T : unmanaged
        {
            unsafe
            {
                byte[] buffer = new byte[sizeof(T)];
                fixed (void* ptr = buffer)
                    *(T*)ptr = value;
                stream.Write(buffer, 0, buffer.Length);
            }
        }

        public static T ReadStructure<T>(this Stream stream) where T : unmanaged
        {
            unsafe
            {
                byte[] buffer = new byte[sizeof(T)];
                stream.Read(buffer, 0, buffer.Length);
                fixed (void* ptr = buffer)
                    return *(T*)ptr;
            }
        }

        public static void CopyTo(this Stream stream, Stream destination, ulong bytes, uint bufferSize = 131072)
        {
            byte[] buffer = new byte[bufferSize];
            int readed;
            do
            {
                readed = stream.Read(buffer, 0, (int)Math.Min((ulong)buffer.Length, bytes));
                destination.Write(buffer, 0, readed);
            } while (readed > 0);
            //} while (bytes > 0 && readed > 0);

            if (readed == -1)
                throw new EzException(
                    $"The CopyTo cannot copy all the bytes, the stream ends before it can copy the requested amount. ({bytes} bytes were missing.)", 
                    new ArgumentOutOfRangeException(nameof(bytes)));
        }
    }
}
