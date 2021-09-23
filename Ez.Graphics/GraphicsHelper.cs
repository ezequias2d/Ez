// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Ez.Numerics;
using System;
using System.Drawing;

namespace Ez.Graphics
{
    /// <summary>
    /// Graphic data helper.
    /// </summary>
    public static class GraphicsHelper
    {
        /// <summary>
        /// Gets a value indicating whether the <see cref="PixelFormat"/> is depth.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool IsDepthFormat(in PixelFormat format) =>
            format.HasFlag(PixelFormat.Depth);

        /// <summary>
        /// Gets whether that indicates whether the pixel format is stencil. 
        /// </summary>
        /// <param name="format">The <see cref="PixelFormat"/> to determine if it is a stencil format.</param>
        /// <returns><see langword="true"/> if the <paramref name="format"/> is a stencil format; otherwise, <see langword="false"/>.</returns>
        public static bool IsStencilFormat(in PixelFormat format) =>
            format.HasFlag(PixelFormat.Stencil);

        /// <summary>
        /// Gets a value that indicates whether the <see cref="PixelFormat"/> is of the compressed type.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <returns><see langword="true"/>, if <paramref name="format"/> is compressed; otherwise, <see langword="false"/>.</returns>
        public static bool IsCompressedFormat(in PixelFormat format) =>
            format.HasFlag(PixelFormat.Compressed);

        /// <summary>
        /// Gets the size of a pixel format.
        /// </summary>
        /// <param name="format">The <see cref="PixelFormat"/> to measure the size.</param>
        /// <returns>The size of <paramref name="format"/>.</returns>
        public static uint GetUncompressedFormatSize(in PixelFormat format)
        {
            switch (format)
            {
                case PixelFormat.R8UNorm:
                case PixelFormat.R8SNorm:
                case PixelFormat.R8UInt:
                case PixelFormat.R8SInt:

                case PixelFormat.S8UInt:
                    return 1;

                case PixelFormat.R4G4B4A4UNorm:
                case PixelFormat.B4G4R4A4UNorm:
                case PixelFormat.R5G6B5UNorm:
                case PixelFormat.B5G6R5UNorm:
                case PixelFormat.R5G5B5A1UNorm:
                case PixelFormat.B5G5R5A1UNorm:
                case PixelFormat.A1R5G5B5UNorm:

                case PixelFormat.R8G8UNorm:
                case PixelFormat.R8G8SNorm:
                case PixelFormat.R8G8UInt:
                case PixelFormat.R8G8SInt:

                case PixelFormat.D16UNorm:

                case PixelFormat.R16UNorm:
                case PixelFormat.R16SNorm:
                case PixelFormat.R16UInt:
                case PixelFormat.R16SInt:
                case PixelFormat.R16SFloat:
                    return 2;

                case PixelFormat.B10G11R11UFloat:
                case PixelFormat.E5B9G9R9UFloat:
                case PixelFormat.A2R10G10B10UNorm:
                case PixelFormat.A2R10G10B10UInt:
                case PixelFormat.A2B10G10R10UNorm:
                case PixelFormat.A2B10G10R10UInt:

                case PixelFormat.D24UNormS8UInt:
                case PixelFormat.D32SFloat:

                case PixelFormat.R8G8B8A8UNorm:
                case PixelFormat.R8G8B8A8SNorm:
                case PixelFormat.R8G8B8A8UInt:
                case PixelFormat.R8G8B8A8SInt:
                case PixelFormat.R8G8B8A8Srgb:
                case PixelFormat.B8G8R8A8UNorm:
                case PixelFormat.B8G8R8A8Srgb:

                case PixelFormat.R16G16UNorm:
                case PixelFormat.R16G16SNorm:
                case PixelFormat.R16G16UInt:
                case PixelFormat.R16G16SInt:
                case PixelFormat.R16G16SFloat:

                case PixelFormat.R32UInt:
                case PixelFormat.R32SInt:
                case PixelFormat.R32SFloat:
                    return 4;

                case PixelFormat.D32SFloatS8UInt:
                    return 5;

                case PixelFormat.R16G16B16A16UNorm:
                case PixelFormat.R16G16B16A16SNorm:
                case PixelFormat.R16G16B16A16UInt:
                case PixelFormat.R16G16B16A16SInt:
                case PixelFormat.R16G16B16A16SFloat:

                case PixelFormat.R32G32UInt:
                case PixelFormat.R32G32SInt:
                case PixelFormat.R32G32SFloat:
                    return 8;

                case PixelFormat.R32G32B32UInt:
                case PixelFormat.R32G32B32SInt:
                case PixelFormat.R32G32B32SFloat:
                    return 12;

                case PixelFormat.R32G32B32A32UInt:
                case PixelFormat.R32G32B32A32SInt:
                case PixelFormat.R32G32B32A32SFloat:
                    return 16;

                default:
                    throw new GraphicsException("Invalid PixelFormat: " + format);
            }
        }

        /// <summary>
        /// Gets block size, width and height of a compressed <see cref="PixelFormat"/>.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        public static (uint BlockSize, Size BlockDimension) GetCompressedFormatInfo(in PixelFormat format)
        {
            uint blockSize;
            int blockWidth;
            int blockHeight;

            switch (format)
            {
                case PixelFormat.BC1RgbaSrgb:
                case PixelFormat.BC1RgbaUNorm:
                case PixelFormat.BC1RgbSrgb:
                case PixelFormat.BC1RgbUNorm:
                    blockSize = 8;
                    blockWidth = blockHeight = 4;
                    break;
                case PixelFormat.BC2Srgb:
                case PixelFormat.BC2UNorm:
                case PixelFormat.BC3Srgb:
                case PixelFormat.BC3UNorm:
                case PixelFormat.BC4SNorm:
                case PixelFormat.BC4UNorm:
                case PixelFormat.BC5SNorm:
                case PixelFormat.BC5UNorm:
                case PixelFormat.BC6HSFloat:
                case PixelFormat.BC6HUFloat:
                case PixelFormat.BC7Srgb:
                case PixelFormat.BC7UNorm:
                    blockSize = 16;
                    blockWidth = blockHeight = 4;
                    break;
                case PixelFormat.EacR11SNorm:
                case PixelFormat.EacR11UNorm:
                case PixelFormat.Etc2R8G8B8Srgb:
                case PixelFormat.Etc2R8G8B8UNorm:
                case PixelFormat.Etc2R8G8B8A1Srgb:
                case PixelFormat.Etc2R8G8B8A1UNorm:
                    blockSize = 8;
                    blockWidth = blockHeight = 4;
                    break;
                case PixelFormat.EacR11G11SNorm:
                case PixelFormat.EacR11G11UNorm:
                case PixelFormat.Etc2R8G8B8A8Srgb:
                case PixelFormat.Etc2R8G8B8A8UNorm:
                case PixelFormat.Astc4x4Srgb:
                case PixelFormat.Astc4x4UNorm:
                    blockSize = 16;
                    blockWidth = blockHeight = 4;
                    break;
                case PixelFormat.Astc5x4Srgb:
                case PixelFormat.Astc5x4UNorm:
                    blockSize = 16;
                    blockWidth = 5;
                    blockHeight = 4;
                    break;
                case PixelFormat.Astc5x5Srgb:
                case PixelFormat.Astc5x5UNorm:
                    blockSize = 16;
                    blockWidth = blockHeight = 5;
                    break;
                case PixelFormat.Astc6x5Srgb:
                case PixelFormat.Astc6x5UNorm:
                    blockSize = 16;
                    blockWidth = 6;
                    blockHeight = 5;
                    break;
                case PixelFormat.Astc6x6Srgb:
                case PixelFormat.Astc6x6UNorm:
                    blockSize = 16;
                    blockWidth = blockHeight = 6;
                    break;
                case PixelFormat.Astc8x5Srgb:
                case PixelFormat.Astc8x5UNorm:
                    blockSize = 16;
                    blockWidth = 8;
                    blockHeight = 5;
                    break;
                case PixelFormat.Astc8x6Srgb:
                case PixelFormat.Astc8x6UNorm:
                    blockSize = 16;
                    blockWidth = 8;
                    blockHeight = 6;
                    break;
                case PixelFormat.Astc8x8Srgb:
                case PixelFormat.Astc8x8UNorm:
                    blockSize = 16;
                    blockWidth = blockHeight = 8;
                    break;
                case PixelFormat.Astc10x5Srgb:
                case PixelFormat.Astc10x5UNorm:
                    blockSize = 16;
                    blockWidth = 10;
                    blockHeight = 5;
                    break;
                case PixelFormat.Astc10x6Srgb:
                case PixelFormat.Astc10x6UNorm:
                    blockSize = 16;
                    blockWidth = 10;
                    blockHeight = 6;
                    break;
                case PixelFormat.Astc10x8Srgb:
                case PixelFormat.Astc10x8UNorm:
                    blockSize = 16;
                    blockWidth = 10;
                    blockHeight = 8;
                    break;
                case PixelFormat.Astc10x10Srgb:
                case PixelFormat.Astc10x10UNorm:
                    blockSize = 16;
                    blockWidth = blockHeight = 10;
                    break;
                case PixelFormat.Astc12x10Srgb:
                case PixelFormat.Astc12x10UNorm:
                    blockSize = 16;
                    blockWidth = 12;
                    blockHeight = 10;
                    break;
                case PixelFormat.Astc12x12Srgb:
                case PixelFormat.Astc12x12UNorm:
                    blockSize = 16;
                    blockWidth = blockHeight = 12;
                    break;
                default:
                    blockSize = 0;
                    blockWidth = blockHeight = 0;
                    break;
            }

            return (blockSize, new Size(blockWidth, blockHeight));
        }

        /// <summary>
        /// Gets compressed block size of a compressed <see cref="PixelFormat"/>.
        /// </summary>
        /// <param name="format">The compressed pixel format to get the block size in bytes.</param>
        /// <returns>The size of a block of the compressed <paramref name="format"/>.</returns>
        public static uint GetCompressedBlockSize(in PixelFormat format) => format switch
        {
            PixelFormat.BC1RgbaSrgb or
                PixelFormat.BC1RgbaUNorm or
                PixelFormat.BC1RgbSrgb or
                PixelFormat.BC1RgbUNorm or
                PixelFormat.EacR11SNorm or
                PixelFormat.EacR11UNorm or
                PixelFormat.Etc2R8G8B8Srgb or
                PixelFormat.Etc2R8G8B8UNorm or
                PixelFormat.Etc2R8G8B8A1Srgb or
                PixelFormat.Etc2R8G8B8A1UNorm => 8,

            PixelFormat.BC2Srgb or
                PixelFormat.BC2UNorm or
                PixelFormat.BC3Srgb or
                PixelFormat.BC3UNorm or
                PixelFormat.BC4SNorm or
                PixelFormat.BC4UNorm or
                PixelFormat.BC5SNorm or
                PixelFormat.BC5UNorm or
                PixelFormat.BC6HSFloat or
                PixelFormat.BC6HUFloat or
                PixelFormat.BC7Srgb or
                PixelFormat.BC7UNorm or
                PixelFormat.EacR11G11SNorm or
                PixelFormat.EacR11G11UNorm or
                PixelFormat.Etc2R8G8B8A8Srgb or
                PixelFormat.Etc2R8G8B8A8UNorm or
                PixelFormat.Astc4x4Srgb or
                PixelFormat.Astc4x4UNorm or
                PixelFormat.Astc5x4Srgb or
                PixelFormat.Astc5x4UNorm or
                PixelFormat.Astc5x5Srgb or
                PixelFormat.Astc5x5UNorm or
                PixelFormat.Astc6x5Srgb or
                PixelFormat.Astc6x5UNorm or
                PixelFormat.Astc6x6Srgb or
                PixelFormat.Astc6x6UNorm or
                PixelFormat.Astc8x5Srgb or
                PixelFormat.Astc8x5UNorm or
                PixelFormat.Astc8x6Srgb or
                PixelFormat.Astc8x6UNorm or
                PixelFormat.Astc8x8Srgb or
                PixelFormat.Astc8x8UNorm or
                PixelFormat.Astc10x5Srgb or
                PixelFormat.Astc10x5UNorm or
                PixelFormat.Astc10x6Srgb or
                PixelFormat.Astc10x6UNorm or
                PixelFormat.Astc10x8Srgb or
                PixelFormat.Astc10x8UNorm or
                PixelFormat.Astc10x10Srgb or
                PixelFormat.Astc10x10UNorm or
                PixelFormat.Astc12x10Srgb or
                PixelFormat.Astc12x10UNorm or
                PixelFormat.Astc12x12Srgb or
                PixelFormat.Astc12x12UNorm => 16,
            _ => 0,
        };

        /// <summary>
        /// Gets compressed block dimensions of a compressed <see cref="PixelFormat"/>.
        /// </summary>
        /// <param name="format">The compressed pixel format to get the block</param>
        /// <returns>The block dimensions of a compressed <paramref name="format"/>.</returns>
        public static Size GetCompressedBlockDimension(in PixelFormat format) => format switch
        {
            PixelFormat.BC1RgbaSrgb or
                PixelFormat.BC1RgbaUNorm or
                PixelFormat.BC1RgbSrgb or
                PixelFormat.BC1RgbUNorm or
                PixelFormat.BC2Srgb or
                PixelFormat.BC2UNorm or
                PixelFormat.BC3Srgb or
                PixelFormat.BC3UNorm or
                PixelFormat.BC4SNorm or
                PixelFormat.BC4UNorm or
                PixelFormat.BC5SNorm or
                PixelFormat.BC5UNorm or
                PixelFormat.BC6HSFloat or
                PixelFormat.BC6HUFloat or
                PixelFormat.BC7Srgb or
                PixelFormat.BC7UNorm or
                PixelFormat.EacR11SNorm or
                PixelFormat.EacR11UNorm or
                PixelFormat.Etc2R8G8B8Srgb or
                PixelFormat.Etc2R8G8B8UNorm or
                PixelFormat.Etc2R8G8B8A1Srgb or
                PixelFormat.Etc2R8G8B8A1UNorm or
                PixelFormat.EacR11G11SNorm or
                PixelFormat.EacR11G11UNorm or
                PixelFormat.Etc2R8G8B8A8Srgb or
                PixelFormat.Etc2R8G8B8A8UNorm or
                PixelFormat.Astc4x4Srgb or
                PixelFormat.Astc4x4UNorm => new(4, 4),

            PixelFormat.Astc5x4Srgb or PixelFormat.Astc5x4UNorm => new(5, 4),
            PixelFormat.Astc5x5Srgb or PixelFormat.Astc5x5UNorm => new(5, 5),
            PixelFormat.Astc6x5Srgb or PixelFormat.Astc6x5UNorm => new(6, 5),
            PixelFormat.Astc6x6Srgb or PixelFormat.Astc6x6UNorm => new(6, 6),
            PixelFormat.Astc8x5Srgb or PixelFormat.Astc8x5UNorm => new(8, 5),
            PixelFormat.Astc8x6Srgb or PixelFormat.Astc8x6UNorm => new(8, 6),
            PixelFormat.Astc8x8Srgb or PixelFormat.Astc8x8UNorm => new(8, 8),
            PixelFormat.Astc10x5Srgb or PixelFormat.Astc10x5UNorm => new(10, 5),
            PixelFormat.Astc10x6Srgb or PixelFormat.Astc10x6UNorm => new(10, 6),
            PixelFormat.Astc10x8Srgb or PixelFormat.Astc10x8UNorm => new(10, 8),
            PixelFormat.Astc10x10Srgb or PixelFormat.Astc10x10UNorm => new(10, 10),
            PixelFormat.Astc12x10Srgb or PixelFormat.Astc12x10UNorm => new(12, 10),
            PixelFormat.Astc12x12Srgb or PixelFormat.Astc12x12UNorm => new(12, 12),
            _ => new(),
        };

        /// <summary>
        /// Gets <paramref name="dimension"/> in blocks of a compressed <paramref name="format"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public static Size3 DimensionInBlocks(this PixelFormat format, Size3 dimension)
        {
            if (!format.HasFlag(PixelFormat.Compressed))
                return dimension;

            var adjustedExtent = dimension;
            var block = GetCompressedBlockDimension(format);

            adjustedExtent.Width = (uint)((adjustedExtent.Width + block.Width - 1) / block.Width);
            adjustedExtent.Height = (uint)((adjustedExtent.Height + block.Height - 1) / block.Height);

            return dimension;
        }

        /// <summary>
        /// Gets the <paramref name="width"/> dimension in blocks of a compressed <paramref name="format"/>.
        /// </summary>
        /// <param name="format">The compressed pixel format to get the block width dimension.</param>
        /// <param name="width">The original width to get block width dimension.</param>
        /// <returns>The block dimension size of width dimension.</returns>
        public static uint WidthInBlocks(this PixelFormat format, uint width)
        {
            if (!format.HasFlag(PixelFormat.Compressed))
                return width;

            var adjustedExtent = width;
            var block = GetCompressedBlockDimension(format);

            return (uint)((width + block.Width - 1) / block.Width);
        }

        /// <summary>
        /// Gets the <paramref name="height"/> dimension in blocks of a compressed <paramref name="format"/>.
        /// </summary>
        /// <param name="format">The compressed pixel format to get the block width dimension.</param>
        /// <param name="height">The original height to get block width dimension.</param>
        /// <returns>The block dimension size of width dimension.</returns>
        public static uint HeightInBlocks(this PixelFormat format, uint height)
        {
            if (!format.HasFlag(PixelFormat.Compressed))
                return height;

            var adjustedExtent = height;
            var block = GetCompressedBlockDimension(format);

            return (uint)((height + block.Height - 1) / block.Height);
        }

        /// <summary>
        /// Gets the row pitch of a image with <paramref name="format"/> and <paramref name="width"/>.
        /// </summary>
        /// <param name="format">The pixel format to get the row pitch.</param>
        /// <param name="width">The width of image to get the row pitch.</param>
        /// <returns>The row pitch value of a image with <paramref name="format"/> and <paramref name="width"/>.</returns>
        public static uint GetRowPitch(this PixelFormat format, uint width)
        {
            if (format.HasFlag(PixelFormat.Compressed))
            {
                var widthInBlocks = format.WidthInBlocks(width);
                return widthInBlocks * GetCompressedBlockSize(format);
            }
            return GetUncompressedFormatSize(format) * width;
        }

        /// <summary>
        /// Gets the depth pitch of a image with <paramref name="format"/> and <paramref name="size"/>.
        /// </summary>
        /// <param name="format">The pixel format to get the row pitch.</param>
        /// <param name="size">The size of image to get the row pitch.</param>
        /// <returns>The depth pitch value of a image with <paramref name="format"/> and <paramref name="size"/>.</returns>
        public static uint GetDepthPitch(this PixelFormat format, Size2 size)
        {
            if (format.HasFlag(PixelFormat.Compressed))
            {
                var widthInBlocks = format.WidthInBlocks(size.Width);
                var heightInBlocks = format.HeightInBlocks(size.Height);
                return GetCompressedBlockSize(format) * widthInBlocks * heightInBlocks;
            }
            return GetUncompressedFormatSize(format) * size.Width * size.Height;
        }

        /// <summary>
        /// Gets the memory offset in bytes of a mipmap level in a image with a <paramref name="format"/>.
        /// </summary>
        /// <param name="format">The pixel format of image.</param>
        /// <param name="size">The size of image.</param>
        /// <param name="samples">The number of samples of image.</param>
        /// <param name="mipLevel">The mipmap level.</param>
        /// <returns>The memory offset to a given mipmap level.</returns>
        public static uint GetMemoryOffset(this PixelFormat format, Size3 size, uint samples, uint mipLevel)
        {
            var offset = 0u;
            for (var i = 0u; i < mipLevel; ++i)
            {
                offset += format.GetMultiSampledLevelSize(size, samples, i);
            }
            return offset;
        }

        /// <summary>
        /// Gets the memory offset in bytes of a mipmap level and a array layer in a image with a <paramref name="format"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="size"></param>
        /// <param name="samples"></param>
        /// <param name="mipLevels"></param>
        /// <param name="mipLevel"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static uint GetMemoryOffset(this PixelFormat format, Size3 size, uint samples, uint mipLevels, uint mipLevel, uint layer)
        {
            return layer * format.GetLayerSize(size, samples, mipLevels) + format.GetMemoryOffset(size, samples, mipLevel);
        }

        /// <summary>
        /// Gets the mipmap level byte size of a image.
        /// </summary>
        /// <param name="format">The image pixel format.</param>
        /// <param name="size">The image size.</param>
        /// <param name="mipLevel">The mipmap level.</param>
        /// <returns>The byte size of a mipmap level.</returns>
        public static uint GetMipLevelSize(this PixelFormat format, Size3 size, uint mipLevel)
        {
            var mipDimensions = size.GetMipmapDimensions(mipLevel);
            return mipDimensions.Depth * format.GetDepthPitch(mipDimensions);
        }

        /// <summary>
        /// Gets the mipmap level byte size of a multi-sampled image.
        /// </summary>
        /// <param name="format">The image pixel format.</param>
        /// <param name="size">The image size.</param>
        /// <param name="samples">The number of image samples.</param>
        /// <param name="mipLevel">The mipmap level.</param>
        /// <returns>The byte size of a mipmap level of a multi-sampled image.</returns>
        public static uint GetMultiSampledLevelSize(this PixelFormat format, Size3 size, uint samples, uint mipLevel) =>
            format.GetMipLevelSize(size, mipLevel) * samples;

        /// <summary>
        /// Gets the size of a layer of a image.
        /// </summary>
        /// <param name="format">The image pixel format.</param>
        /// <param name="size">The image size.</param>
        /// <param name="samples">The number of image samples.</param>
        /// <param name="mipLevels">The image mipmap levels.</param>
        /// <returns>The size of a layer of a image.</returns>
        public static uint GetLayerSize(this PixelFormat format, Size3 size, uint samples, uint mipLevels)
        {
            var layerSize = 0u;

            for (var mipLevel = 0u; mipLevel < mipLevels; ++mipLevel)
            {
                layerSize += format.GetMultiSampledLevelSize(size, samples, mipLevel);
            }

            return layerSize;
        }

        /// <summary>
        /// Gets dimensions of a mipmap level.
        /// </summary>
        /// <param name="size">The original size.</param>
        /// <param name="level">The mipmap level.</param>
        /// <returns>The dimensions of the mipmap <paramref name="level"/>.</returns>
        public static Size3 GetMipmapDimensions(this Size3 size, uint level) => new()
        {
            Width = GetMipmapDimension(size.Width, level),
            Height = GetMipmapDimension(size.Height, level),
            Depth = GetMipmapDimension(size.Depth, level)
        };

        /// <summary>
        /// Gets dimension of a mipmap level.
        /// </summary>
        /// <param name="originalDimension">The original dimension.</param>
        /// <param name="level">The mipmap level.</param>
        /// <returns>The dimension of the mipmap <paramref name="level"/>.</returns>
        public static uint GetMipmapDimension(uint originalDimension, uint level) =>
            Math.Max(originalDimension >> (int)level, 1u);
    }
}
