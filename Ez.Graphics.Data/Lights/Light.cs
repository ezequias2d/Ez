// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// A structure to store the information of a light.
    /// </summary>
    public readonly struct Light : IEquatable<Light>
    {
        private readonly int _hashcode;

        /// <summary>
        /// Initializes a new instance of direction light.
        /// </summary>
        /// <param name="name">The name of the light.</param>
        /// <param name="direction">Direction of the light.</param>
        /// <param name="ambient">The ambient color of the light.</param>
        /// <param name="diffuse">The diffuse color of the light.</param>
        /// <param name="specular">The specular color of the light.</param>
        public Light(string name, Vector3 direction, ColorSingle ambient, ColorSingle diffuse, ColorSingle specular) :
            this(name, LightSource.Directional, default, direction, default, ambient, diffuse, specular, default, default, default, default, default)
        {
        }

        /// <summary>
        /// Initializes a new instance of point light.
        /// </summary>
        /// <param name="name">The name of the light.</param>
        /// <param name="position">The position of the light.</param>
        /// <param name="ambient">The ambient color of the light.</param>
        /// <param name="diffuse">The diffuse color of the light.</param>
        /// <param name="specular">The specular color of the light.</param>
        /// <param name="constant">The constant light attenuation factor.</param>
        /// <param name="linear">The linear light attenuation factor.</param>
        /// <param name="quadratic">The quadratic light attenuation factor.</param>
        public Light(string name, Vector3 position, ColorSingle ambient, ColorSingle diffuse, ColorSingle specular, float constant, float linear, float quadratic) :
            this(name, LightSource.Point, position, default, default, ambient, diffuse, specular, constant, linear, quadratic, default, default)
        {
        }

        /// <summary>
        /// Initializes a new instance of spot light.
        /// </summary>
        /// <param name="name">The name of the light.</param>
        /// <param name="position">The position of the light.</param>
        /// <param name="direction">Direction of the light.</param>
        /// <param name="ambient">The ambient color of the light.</param>
        /// <param name="diffuse">The diffuse color of the light.</param>
        /// <param name="specular">The specular color of the light.</param>
        /// <param name="constant">The constant light attenuation factor.</param>
        /// <param name="linear">The linear light attenuation factor.</param>
        /// <param name="quadratic">The quadratic light attenuation factor.</param>
        /// <param name="innerConde">The inner angle of a spot light's cone.</param>
        /// <param name="outerCone">The outer angle of a spot light</param>
        public Light(string name, Vector3 position, Vector3 direction, ColorSingle ambient, ColorSingle diffuse, ColorSingle specular, float constant, float linear, float quadratic, float innerConde, float outerCone) :
            this(name, LightSource.Spot, position, direction, default, ambient, diffuse, specular, constant, linear, quadratic, innerConde, outerCone)
        {
        }

        /// <summary>
        /// Initializes a new instance of area light.
        /// </summary>
        /// <param name="name">The name of the light.</param>
        /// <param name="areaSize">The area size of the light.</param>
        /// <param name="ambient">The ambient color of the light.</param>
        /// <param name="diffuse">The diffuse color of the light.</param>
        /// <param name="specular">The specular color of the light.</param>
        /// <param name="position">The position of the light.</param>
        /// <param name="constant">The constant light attenuation factor.</param>
        /// <param name="linear">The linear light attenuation factor.</param>
        /// <param name="quadratic">The quadratic light attenuation factor.</param>
        public Light(string name, Vector2 areaSize, ColorSingle ambient, ColorSingle diffuse, ColorSingle specular, Vector3 position, float constant, float linear, float quadratic):
            this(name, LightSource.Area, position, default, areaSize, ambient, diffuse, specular, constant, linear, quadratic, default, default)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Light"/> struct.
        /// </summary>
        /// <param name="name">The name of the light.</param>
        /// <param name="lightSource">The light source.</param>
        /// <param name="position">The position of the light.</param>
        /// <param name="direction">Direction of the light.</param>
        /// <param name="areaSize">The area size of the light.</param>
        /// <param name="ambient">The ambient color of the light.</param>
        /// <param name="diffuse">The diffuse color of the light.</param>
        /// <param name="specular">The specular color of the light.</param>
        /// <param name="constant">The constant light attenuation factor.</param>
        /// <param name="linear">The linear light attenuation factor.</param>
        /// <param name="quadratic">The quadratic light attenuation factor.</param>
        /// <param name="inner">The inner angle of a spot light's cone.</param>
        /// <param name="outer">The outer angle of a spot light</param>
        public Light(string name, LightSource lightSource, Vector3 position, Vector3 direction, Vector2 areaSize, ColorSingle ambient, ColorSingle diffuse, ColorSingle specular, float constant, float linear, float quadratic, float inner, float outer)
        {
            (Name,
                LightSource,
                Position,
                Direction,
                Ambient,
                Diffuse,
                Specular,
                AttenuationConstant,
                AttenuationLinear,
                AttenuationQuadratic,
                AngleInnerCone,
                AngleOuterCone,
                AreaSize) =
                (name,
                    lightSource,
                    position, 
                    direction, 
                    diffuse, 
                    ambient, 
                    specular, 
                    constant, 
                    linear, 
                    quadratic, 
                    inner, 
                    outer, 
                    areaSize);
            _hashcode = HashHelper<Light>.Combine(Name, LightSource, Position, Direction, AreaSize, Ambient, Diffuse, 
                Specular, AttenuationConstant, AttenuationLinear, AttenuationQuadratic, AngleInnerCone, AngleOuterCone);
        }

        /// <summary>
        /// The name of the light.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The type of the light source.
        /// </summary>
        public LightSource LightSource { get; }

        /// <summary>
        /// Position of the light source in space.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// Direction of the light source in space.
        /// </summary>
        public Vector3 Direction { get; }
       
        /// <summary>
        /// Area size of a <see cref="LightSource.Area"/> Light.
        /// </summary>
        public Vector2 AreaSize { get; }

        /// <summary>
        /// Ambient color of the light source.
        /// </summary>
        public ColorSingle Ambient { get; }

        /// <summary>
        /// Diffuse color of the light source.
        /// </summary>
        public ColorSingle Diffuse { get; }

        /// <summary>
        /// Specular color of the light source.
        /// </summary>
        public ColorSingle Specular { get; }

        /// <summary>
        /// Constant light attenuation factor.
        /// </summary>
        public float AttenuationConstant { get; }

        /// <summary>
        /// Linear light attenuation factor.
        /// </summary>
        public float AttenuationLinear { get; }

        /// <summary>
        /// Quadratic light attenuation factor.
        /// </summary>
        public float AttenuationQuadratic { get; }

        /// <summary>
        /// Inner angle of a spot light's light cone.
        /// </summary>
        public float AngleInnerCone { get; }

        /// <summary>
        /// Outer angle of a spot light's light cone.
        /// </summary>
        public float AngleOuterCone { get; }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => _hashcode;

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="Light"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="Light"/>.</param>
        /// <returns><see langword="true"/> if the two animations are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Light other) =>
            _hashcode == other._hashcode &&
            Name == other.Name &&
            LightSource == other.LightSource &&
            Position == other.Position &&
            Direction == other.Direction &&
            AreaSize == other.AreaSize &&
            Ambient == other.Ambient &&
            Diffuse == other.Diffuse &&
            Specular == other.Specular &&
            AttenuationConstant == other.AttenuationConstant &&
            AttenuationLinear == other.AttenuationLinear &&
            AttenuationQuadratic == other.AttenuationQuadratic &&
            AngleInnerCone == other.AngleInnerCone &&
            AngleOuterCone == other.AngleOuterCone;

        /// <summary>
        /// Returns a value that indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="obj"/> are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) =>
            obj is Light light && Equals(light);
    }
}
