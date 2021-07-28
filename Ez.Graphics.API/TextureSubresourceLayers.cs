// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specifying an image subresource layers.
    /// </summary>
    public struct TextureSubresourceLayers
    {
        /// <summary>
        /// Gets or sets the mipmap level to copy.
        /// </summary>
        public uint MipmapLevel;

        /// <summary>
        /// Gets or sets the starting layer to copy.
        /// </summary>
        public uint BaseArrayLayer;

        /// <summary>
        /// Gets or sets the number of layers to copy.
        /// </summary>
        public uint LayerCount;
    }
}
