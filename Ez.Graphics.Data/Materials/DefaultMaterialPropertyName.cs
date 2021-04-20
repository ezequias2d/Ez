// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Graphics.Data.Materials
{
    /// <summary>
    /// The default material property names.
    /// </summary>
    public static class DefaultMaterialPropertyName
    {
        /// <summary>
        /// Defines the opacity of the material in a range between 0..1.
        /// </summary>
        public const string Opacity = "Opacity";

        /// <summary>
        /// Defines the shininess of a phong-shaded material. 
        /// This is actually the exponent of the phong specular equation.
        /// </summary>
        public const string Shininess = "Shininess";

        /// <summary>
        /// Defines the reflectivity of material(float).
        /// </summary>
        public const string Reflectivity = "Reflectivitity";

        /// <summary>
        /// Scales the specular color of the material.
        /// </summary>
        public const string ShininessStrength = "ShininessStrength";

        /// <summary>
        /// Diffuse color of the material.
        /// This is typically scaled by the amount of incoming diffuse light
        /// (e.g. using gouraud shading)
        /// </summary>
        public const string ColorDiffuse = "ColorDiffuse";

        /// <summary>
        /// Ambient color of the material. 
        /// This is typically scaled by the amount of ambient light
        /// </summary>
        public const string ColorAmbient = "ColorAmbient";

        /// <summary>
        /// Specular color of the material. 
        /// This is typically scaled by the amount of incoming specular light
        /// (e.g. using phong shading)
        /// </summary>
        public const string ColorSpecular = "ColorSpecular";

        /// <summary>
        /// Emissive color of the material.
        /// This is the amount of light emitted by the object.
        /// In real time applications it will usually not affect surrounding objects, 
        /// but raytracing applications may wish to treat emissive objects as light sources.
        /// </summary>
        public const string ColorEmissive = "ColorEmissive";

        /// <summary>
        /// Reflective color of the material.
        /// </summary>
        public const string ColorReflective = "ColorReflective";

        /// <summary>
        /// A texture of the material.
        /// </summary>
        public const string Texture = "Texture";
    }
}
