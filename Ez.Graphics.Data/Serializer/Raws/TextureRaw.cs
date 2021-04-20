// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Graphics.Data.Serializer.Raws
{
    internal struct TextureRaw
    {
        public PixelFormat PixelFormat;
        public uint Width;
        public uint Height;
        public uint Depth;
        public uint MipmapLevels;
        public uint Layers;
        public TextureType TextureType;
        public uint Size;
    }
}
