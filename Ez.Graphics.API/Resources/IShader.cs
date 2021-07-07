// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// A resource thats contains a single shader program.
    /// </summary>
    public interface IShader : IResource, IEquatable<IShader>
    {
        /// <summary>
        /// The shader entry-point.
        /// </summary>
        string EntryPoint { get; }

        /// <summary>
        /// Gets the shader stage supported.
        /// </summary>
        ShaderStages Stage { get; }
    }
}
