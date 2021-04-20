// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System.Runtime.InteropServices;

namespace Ez.Graphics.Data.Serializer.Raws
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct AnimationRaw
    {
        public uint ChannelsCount;
        public double Duration;
        public double TicksPerSecond;
    }
}
