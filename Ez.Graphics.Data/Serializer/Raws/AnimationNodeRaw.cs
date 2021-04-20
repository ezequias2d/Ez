// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.Data.Animations;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Ez.Graphics.Data.Serializer.Raws
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct AnimationNodeRaw
    {
        public AnimationBehaviour PostState;
        public AnimationBehaviour PreState;
        
        public uint PositionKeysCount;
        public uint RotationKeysCount;
        public uint ScalingKeysCount;
    }
}
