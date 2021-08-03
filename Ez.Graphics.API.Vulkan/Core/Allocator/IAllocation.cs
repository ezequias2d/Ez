// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Silk.NET.Vulkan;
using System;

namespace Ez.Graphics.API.Vulkan.Core.Allocator
{
    internal interface IAllocation : IDisposable
    {
        DeviceMemory Handle { get; }
        uint MemoryType { get; }
        ulong Offset { get; }
        ulong Size { get; }
        bool IsMapped { get; }

        IntPtr MapMemory();
        void UnmapMemory();
    }
}
