// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes the format of the <see cref="ShaderCreateInfo.Source"/>.
    /// </summary>
    public enum ShaderFormat
    {
        /// <summary>
        /// SPIR-V bytecode.
        /// </summary>
        SpirV,
        /// <summary>
        /// ASCII-encoded GLSL text.
        /// </summary>
        Glsl,
    }
}
