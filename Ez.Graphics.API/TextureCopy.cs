// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specifying an texture copy operation.
    /// </summary>
    public struct TextureCopy
    {
        /// <summary>
        /// Specifies  the subresource of the texture used for the source
        /// texture data.
        /// </summary>
        public TextureSubresourceLayers SrcSubresource { get; set; }

        /// <summary>
        /// Specifies the initial X, Y, and Z offsets in texels of the
        /// sub-region of the source texture data.
        /// </summary>
        public Point3 SrcOffset { get; }

        /// <summary>
        /// Specifies the subresource of the texture used for the destination
        /// texture data.
        /// </summary>
        public TextureSubresourceLayers DstSubresource { get; set; }

        /// <summary>
        /// Specifies the initial X, Y, and Z offsets in texels of the
        /// sub-region of the destination texture data.
        /// </summary>
        public Point3 DstOffset { get; set; }

        /// <summary>
        /// Specifies the size in texels of the texture to copy in width,
        /// height and depth.
        /// </summary>
        public Size3 Extent { get; set; }
    }
}
