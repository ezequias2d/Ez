// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Memory;
using System;
using System.Text;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal unsafe class Utf8FixedString : IDisposable
    {
        private MemoryBlock _mb;
        public Utf8FixedString(string str)
        {
            var byteCount = Encoding.UTF8.GetByteCount(str);
            _mb = MemoryBlockPool.Get(byteCount + 1, true);
            Encoding.UTF8.GetBytes(str, new Span<byte>((void*)_mb.Ptr, byteCount));
            *(byte*)(_mb.Ptr + byteCount) = 0;
        }

        Utf8FixedString()
        {
            _mb = null;
        }

        public void Dispose()
        {
            MemoryBlockPool.Return(_mb);
            _mb = null;
        }

        public static implicit operator Utf8FixedString(string str) =>
            new Utf8FixedString(str);

        public static implicit operator byte*(Utf8FixedString str) =>
            (byte*)str._mb.Ptr;
    }
}
