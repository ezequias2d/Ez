// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.Data.Serializer.Raws;
using Ez.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ez.Graphics.Data.Serializer
{
    /// <summary>
    /// A static class that provides serializer and deserializer for <see cref="Transform"/>.
    /// </summary>
    public static class TransformStreamExtensions
    {
        /// <summary>
        /// Serialize <see cref="Transform"/> into a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write it.</param>
        /// <param name="transform">The transform to write.</param>
        public static void WriteTransform(this Stream stream, in Transform transform)
        {
            stream.WriteStructure(new TransformRaw
            {
                Position = transform.Position,
                Scale = transform.Scale,
                EulerAngles = transform.EulerAngles,
                FatherIndex = transform.FatherIndex
            });
        }

        /// <summary>
        /// Derializes an <see cref="Transform"/> from a stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="scene">The scene of the transform.</param>
        /// <returns>A new instance of <see cref="Transform"/> with data read from the <paramref name="stream"/>.</returns>
        public static Transform ReadTransform(this Stream stream, in Scene scene)
        {
            var raw = stream.ReadStructure<TransformRaw>();
            return new Transform(scene, raw.Position, raw.Scale, raw.EulerAngles, raw.FatherIndex);
        }
    }
}
