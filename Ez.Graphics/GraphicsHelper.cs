// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

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
    }
}
