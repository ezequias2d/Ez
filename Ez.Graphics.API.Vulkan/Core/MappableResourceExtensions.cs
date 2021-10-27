// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Resources;
using Ez.Memory;
using System;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal static class MappableResourceExtensions
    {
        public static bool TrySubData<T>(this IMappableResource resource, ReadOnlySpan<T> source, long offset) where T : unmanaged
        {
            if (resource.MemoryUsage.HasFlag(MemoryUsage.CpuOnly) ||
                resource.MemoryUsage.HasFlag(MemoryUsage.CpuToGpu) ||
                resource.MemoryUsage.HasFlag(MemoryUsage.GpuToCpu))
            {
                var (Ptr, _) = resource.Map();
                MemUtil.Copy(new IntPtr(Ptr.ToInt64() + offset), source);
                resource.Unmap();
                return true;
            }
            return false;
        }
    }
}
