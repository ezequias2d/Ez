// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using Silk.NET.Vulkan;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal static class EzHelper
    {
        public static Size3 ToEz(this in Extent3D value) => new(value.Width, value.Height, value.Depth);

        public static SampleCount ToEz(this SampleCountFlags value) =>
            value switch
            {
                SampleCountFlags.SampleCount1Bit => SampleCount.Count1,
                SampleCountFlags.SampleCount2Bit => SampleCount.Count2,
                SampleCountFlags.SampleCount4Bit => SampleCount.Count4,
                SampleCountFlags.SampleCount8Bit => SampleCount.Count8,
                SampleCountFlags.SampleCount16Bit => SampleCount.Count16,
                SampleCountFlags.SampleCount32Bit => SampleCount.Count32,
                SampleCountFlags.SampleCount64Bit => SampleCount.Count32,
                _ => throw new VkException()
            };
    }
}
