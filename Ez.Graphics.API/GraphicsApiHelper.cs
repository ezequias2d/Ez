// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;
using Ez.Numerics;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Graphics.API helpers.
    /// </summary>
    public static class GraphicsApiHelper
    {
        /// <summary>
        /// Gets the size of <see cref="VertexElementType"/> in bytes.
        /// </summary>
        /// <param name="value">The vertex element type.</param>
        /// <returns>The size of <paramref name="value"/>.</returns>
        public static uint GetSizeInBytes(VertexElementType value)
        {
            switch (value)
            {
                case VertexElementType.Byte2Norm:
                case VertexElementType.Byte2:
                case VertexElementType.SByte2Norm:
                case VertexElementType.SByte2:
                case VertexElementType.Half1:
                    return 2;
                case VertexElementType.Single1:
                case VertexElementType.UInt1:
                case VertexElementType.Int1:
                case VertexElementType.Byte4Norm:
                case VertexElementType.Byte4:
                case VertexElementType.SByte4Norm:
                case VertexElementType.SByte4:
                case VertexElementType.UShort2Norm:
                case VertexElementType.UShort2:
                case VertexElementType.Short2Norm:
                case VertexElementType.Short2:
                case VertexElementType.Half2:
                    return 4;
                case VertexElementType.Single2:
                case VertexElementType.UInt2:
                case VertexElementType.Int2:
                case VertexElementType.UShort4Norm:
                case VertexElementType.UShort4:
                case VertexElementType.Short4Norm:
                case VertexElementType.Short4:
                case VertexElementType.Half4:
                    return 8;
                case VertexElementType.Single3:
                case VertexElementType.UInt3:
                case VertexElementType.Int3:
                    return 12;
                case VertexElementType.Single4:
                case VertexElementType.UInt4:
                case VertexElementType.Int4:
                    return 16;
                default:
                    throw new GraphicsApiException($"The VertexElementFormat is not supported by this OpenGL backend. Value: {value}");
            }
        }

        /// <summary>
        /// Gets a <see cref="TextureSubresourceRange"/> that covers the entire <paramref name="texture"/>.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <returns>A <see cref="TextureSubresourceRange"/> that covers the entire <paramref name="texture"/>.</returns>
        public static TextureSubresourceRange GetFullRange(this ITexture texture) => new()
        {
            ArrayLayerCount = texture.ArrayLayers,
            BaseArrayLayer = 0,
            BaseMipmapLevel = 0,
            MipmapLevelCount = texture.MipmapLevels,
        };
    }
}
