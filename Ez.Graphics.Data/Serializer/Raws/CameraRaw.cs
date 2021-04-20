// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.Data.Cameras;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Ez.Graphics.Data.Serializer.Raws
{
    internal struct CameraRaw
    {
        public Vector3 Up;
        public Vector3 Position;
        public Quaternion Rotation;
        public float FieldOfView;
        public float OrthographicSize;
        public float NearDistance;
        public float FarDistance;
        public float AspectRatio;
        public CameraMode CameraMode;
    }
}
