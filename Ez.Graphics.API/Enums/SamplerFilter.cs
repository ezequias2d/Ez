// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.CreateInfos;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes how texture values are sampled.
    /// </summary>
    [Flags]
    public enum SamplerFilter : byte
    {
        /// <summary>
        /// Nearest sampling is used for minification.
        /// This flag cannot be combined with <see cref="MinLinear"/>.
        /// </summary>
        MinNearest = 1,
        /// <summary>
        /// Linear sampling is used for minification.
        /// This flag cannot be combined with <see cref="MinNearest"/>.
        /// </summary>
        MinLinear = 1 << 1,
        /// <summary>
        /// Nearest sampling is used for magnification.
        /// This flag cannot be combined with <see cref="MagLinear"/>.
        /// </summary>
        MagNearest = 1 << 2,
        /// <summary>
        /// Linear sampling is used for magnification.
        /// This flag cannot be combined with <see cref="MagNearest"/>.
        /// </summary>
        MagLinear = 1 << 3,
        /// <summary>
        /// Nearest sampling is used for mip-level sampling.
        /// This flag cannot be combined with <see cref="MipmapLinear"/>.
        /// </summary>
        MipmapNearest = 1 << 4,
        /// <summary>
        /// Linear sampling is used for mip-level sampling.
        /// This flag cannot be combined with <see cref="MipmapNearest"/>.
        /// </summary>
        MipmapLinear = 1 << 5,
        /// <summary>
        /// Anisotropic filtering isn't used.
        /// This flag cannot be combined with <see cref="Anisotropic"/>.
        /// </summary>
        NoAnisotropic = 1 << 6,
        /// <summary>
        /// Anisotropic filtering is used. The maximum anisotropy is controlled by
        /// <see cref="SamplerCreateInfo.MaximumAnisotropy"/>
        /// This flag cannot be combined with <see cref="NoAnisotropic"/>.
        /// </summary>
        Anisotropic = 1 << 7
    }
}
