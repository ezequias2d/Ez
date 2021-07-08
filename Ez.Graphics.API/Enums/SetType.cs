// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes the type of a set in a <see cref="SetLayoutBinding"/>.
    /// </summary>
    public enum SetType
    {
        /// <summary>
        /// Specifies a uniform buffer set.
        /// </summary>
        UniformBuffer,
        /// <summary>
        /// Specifies a storage buffer set.
        /// </summary>
        StorageBuffer,
        /// <summary>
        /// Specifies a sampled texture set.
        /// </summary>
        SampledTexture,
        /// <summary>
        /// Specifies a storage texture set.
        /// </summary>
        StorageTexture,
        /// <summary>
        /// Specifies a sampler set.
        /// </summary>
        Sampler,
    }
}
