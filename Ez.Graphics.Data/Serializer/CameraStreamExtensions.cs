// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.IO;
using Ez.Graphics.Data.Cameras;
using Ez.Graphics.Data.Serializer.Raws;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ez.Graphics.Data.Serializer
{
    /// <summary>
    /// A static class that provides serializer and deserializer for <see cref="Camera"/>.
    /// </summary>
    public static class CameraStreamExtensions
    {
        /// <summary>
        /// Serialize <see cref="Camera"/> into a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write it.</param>
        /// <param name="camera">The camera to write.</param>
        public static void WriteCamera(this Stream stream, in Camera camera)
        {
            stream.WriteString(camera.Name);
            stream.WriteStructure(new CameraRaw
            {
                Up = camera.Up,
                Position = camera.Position,
                Rotation = camera.Rotation,
                FieldOfView = camera.FieldOfView,
                OrthographicSize = camera.OrthographicSize,
                NearDistance = camera.NearDistance,
                FarDistance = camera.FarDistance,
                AspectRatio = camera.AspectRatio,
                CameraMode = camera.CameraMode
            });
        }

        /// <summary>
        /// Derializes an <see cref="Camera"/> from a stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A new instance of <see cref="Camera"/> with data read from the <paramref name="stream"/>.</returns>
        public static Camera ReadCamera(this Stream stream)
        {
            string name = stream.ReadString();
            CameraRaw raw = stream.ReadStructure<CameraRaw>();
            
            return new Camera(
                name, 
                raw.Up,
                raw.Position,
                raw.Rotation,
                raw.FieldOfView,
                raw.OrthographicSize,
                raw.NearDistance,
                raw.FarDistance, 
                raw.AspectRatio,
                raw.CameraMode);
        }
    }
}
