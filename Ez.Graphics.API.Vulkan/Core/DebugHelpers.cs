// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Silk.NET.Vulkan;
using System;
using System.Diagnostics;
using System.Threading;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal static class DebugHelpers
    {
        [DebuggerNonUserCode]
        public static void CheckDispose(this DeviceResource resource)
        {
            if (resource.IsDisposed)
                throw new ObjectDisposedException(resource.Name);
        }

        [DebuggerNonUserCode]
        public static void CheckResult(this Result result)
        {
            if (result != Result.Success)
                throw new VkResultErrorException(result);
        }

        [DebuggerNonUserCode]
        public static void CheckResult(this Result result, string message)
        {
            if (result != Result.Success)
                throw new VkResultErrorException(result, message);
        }

        public static void SetDefaultName(this DeviceResource resource)
        {
            resource.Name = $"{resource.GetType().Name}{resource.GetHashCode()}";
        }

        [DebuggerNonUserCode]
        public static void CheckThreadId(this CommandBuffer cb)
        {
            if (cb.CommandPool.PoolThread.ManagedThreadId != Thread.CurrentThread.ManagedThreadId)
                throw new VkException();
        }
    }
}
