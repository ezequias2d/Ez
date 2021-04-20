// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Ez.Graphics.Data.Serializer.Raws
{
    internal struct BoneRaw
    {
        public Matrix4x4 OffsetMatrix;
        public uint WeightsCount;
    }
}
