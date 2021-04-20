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
    /// A static class that provides serializer and deserializer for <see cref="Light"/>.
    /// </summary>
    public static class LightStreamExtensions 
    {
        /// <summary>
        /// Serialize <see cref="Light"/> into a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write it.</param>
        /// <param name="light">The light to write.</param>
        public static void WriteLight(this Stream stream, in Light light)
        {
            stream.WriteString(light.Name);
            stream.WriteStructure(new LightRaw
            {
                LightSource = light.LightSource,
                Position = light.Position,
                Direction = light.Direction,
                AreaSize = light.AreaSize,
                Ambient = light.Ambient,
                Diffuse = light.Diffuse,
                Specular = light.Specular,

                AttenuationConstant = light.AttenuationConstant,
                AttenuationLinear = light.AttenuationLinear,
                AttenuationQuadratic = light.AttenuationQuadratic,

                AngleInnerCone = light.AngleInnerCone,
                AngleOuterCone = light.AngleOuterCone
            });
        }

        /// <summary>
        /// Derializes an <see cref="Light"/> from a stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A new instance of <see cref="Light"/> with data read from the <paramref name="stream"/>.</returns>
        public static Light ReadLight(this Stream stream)
        {
            var name = stream.ReadString();
            var raw = stream.ReadStructure<LightRaw>();

            return new Light(name, 
                raw.LightSource, 
                raw.Position, 
                raw.Direction, 
                raw.AreaSize, 
                raw.Ambient, 
                raw.Diffuse, 
                raw.Specular, 
                raw.AttenuationConstant, 
                raw.AttenuationLinear, 
                raw.AttenuationQuadratic, 
                raw.AngleInnerCone, 
                raw.AngleOuterCone);
        }
    }
}
