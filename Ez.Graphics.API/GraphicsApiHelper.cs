// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;
using Ez.Numerics;

namespace Ez.Graphics.API
{
    public static class GraphicsApiHelper
    {
        public static Size3 GetMipmapDimensions(ITexture texture, int level) => new()
        {
            Width = GetMipmapDimension(texture.Width, level),
            Height = GetMipmapDimension(texture.Height, level),
            Depth = GetMipmapDimension(texture.Depth, level)
        };

        public static uint GetMipmapDimension(uint originalDimension, int level) =>
            Math.Max(originalDimension >> level, 1u);

        public static uint CalculateSubresource(this ITexture texture, uint mipmapLevel, uint arrayLayer) =>
            arrayLayer * texture.MipmapLevels + mipmapLevel;

        public static (uint MipmapLevel, uint ArrayLayer) GetSubresourceLevelAndLayer(ITexture tex, uint subresource)
        {
            var arrayLayer = subresource / tex.MipmapLevels;
            var mipmapLevel = subresource - (arrayLayer * tex.MipmapLevels);
            return (mipmapLevel, arrayLayer);
        }

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
    }
}
