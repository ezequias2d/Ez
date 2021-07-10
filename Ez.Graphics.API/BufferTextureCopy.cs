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
    public struct BufferTextureCopy
    {
        /// <summary>
        /// Specifies the starting offset in bytes from the start of the source buffer.
        /// </summary>
        public long BufferOffset { get; set; }

        /// <summary>
        /// Specifies in texels  a subregion of a larger two-dimensional
        /// texture in buffer memory, and control the addressing calculations.
        /// If it is zero, that aspect of the buffer memory is considered to be 
        /// tightly packed according to the <see cref="TextureExtent"/>.
        /// </summary>
        public uint BufferRowLength { get; set; }

        /// <summary>
        /// Specifies in texels  a subregion of a larger three-dimensional
        /// texture in buffer memory, and control the addressing calculations.
        /// If it is zero, that aspect of the buffer memory is considered to be 
        /// tightly packed according to the <see cref="TextureExtent"/>.
        /// </summary>
        public uint BufferTextureHeight { get; set; }

        /// <summary>
        /// Specifies the number of bytes to copy.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Specifies  the subresource of the texture used for the source or 
        /// destination image data.
        /// </summary>
        public TextureSubresourceLayers TextureSubresource { get; set; }

        /// <summary>
        /// Specifies the initial X, Y, and Z offsets in texels of the
        /// sub-region of the source or destination image data.
        /// </summary>
        public Point3 TextureOffset { get; }

        /// <summary>
        /// Specifies the size in texels of the texture to copy in width,
        /// height and depth.
        /// </summary>
        public Size3 TextureExtent { get; set; }
    }
}
