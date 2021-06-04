// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics
{
    /// <summary>
    /// A pixel format enum.
    /// </summary>
    public enum PixelFormat : byte
    {
        /// <summary>
        /// Specifies that the format is not specified.
        /// </summary>
        Undefined,
        #region packed
        /// <summary>
        /// Specifies a four-component, 16-bit packed unsigned normalized format 
        /// that has a 4-bit R component in bits 12..15, a 4-bit G component in 
        /// bits 8..11, a 4-bit B component in bits 4..7, and a 4-bit A component
        /// in bits 0..3.
        /// </summary>
        R4G4B4A4UNorm,
        /// <summary>
        /// Specifies a four-component, 16-bit packed unsigned normalized format 
        /// that has a 4-bit B component in bits 12..15, a 4-bit G component in 
        /// bits 8..11, a 4-bit R component in bits 4..7, and a 4-bit A component 
        /// in bits 0..3.
        /// </summary>
        B4G4R4A4UNorm,
        /// <summary>
        /// Specifies a three-component, 16-bit packed unsigned normalized format 
        /// that has a 5-bit R component in bits 11..15, a 6-bit G component in 
        /// bits 5..10, and a 5-bit B component in bits 0..4.
        /// </summary>
        R5G6B5UNorm,
        /// <summary>
        /// Specifies a three-component, 16-bit packed unsigned normalized format 
        /// that has a 5-bit B component in bits 11..15, a 6-bit G component in 
        /// bits 5..10, and a 5-bit R component in bits 0..4.
        /// </summary>
        B5G6R5UNorm,
        /// <summary>
        /// Specifies a four-component, 16-bit packed unsigned normalized format 
        /// that has a 5-bit R component in bits 11..15, a 5-bit G component in 
        /// bits 6..10, a 5-bit B component in bits 1..5, and a 1-bit A component
        /// in bit 0.
        /// </summary>
        R5G5B5A1UNorm,
        /// <summary>
        /// Specifies a four-component, 16-bit packed unsigned normalized format 
        /// that has a 5-bit B component in bits 11..15, a 5-bit G component in 
        /// bits 6..10, a 5-bit R component in bits 1..5, and a 1-bit A component
        /// in bit 0.
        /// </summary>
        B5G5R5A1UNorm,
        /// <summary>
        /// Specifies a four-component, 16-bit packed unsigned normalized format 
        /// that has a 1-bit A component in bit 15, a 5-bit R component in bits 
        /// 10..14, a 5-bit G component in bits 5..9, and a 5-bit B component in 
        /// bits 0..4.
        /// </summary>
        A1R5G5B5UNorm,
        /// <summary>
        /// Specifies a three-component, 32-bit packed unsigned floating-point format that has a 10-bit B 
        /// component in bits 22..31, an 11-bit G component in bits 11..21, an 11-bit R component in bits 
        /// 0..10.
        /// </summary>
        R11G11B10UFloat,
        /// <summary>
        /// Specifies a three-component, 32-bit packed unsigned floating-point format that has a 5-bit 
        /// shared exponent in bits 27..31, a 9-bit B component mantissa in bits 18..26, a 9-bit G component 
        /// mantissa in bits 9..17, and a 9-bit R component mantissa in bits 0..8.
        /// </summary>
        R9G9B9E5UFloat,
        #endregion
        #region R8
        /// <summary>
        /// Specifies a one-component, 8-bit unsigned normalized format that has 
        /// a single 8-bit R component.
        /// </summary>
        R8UNorm,
        /// <summary>
        /// Specifies a one-component, 8-bit signed normalized format that has a 
        /// single 8-bit R component.
        /// </summary>
        R8SNorm,
        /// <summary>
        /// Specifies a one-component, 8-bit unsigned integer format that has a 
        /// single 8-bit R component.
        /// </summary>
        R8UInt,
        /// <summary>
        /// Specifies a one-component, 8-bit signed integer format that has a 
        /// single 8-bit R component.
        /// </summary>
        R8SInt,
        #endregion
        #region R8G8
        /// <summary>
        /// Specifies a two-component, 16-bit unsigned normalized format that has
        /// an 8-bit R component in byte 0, and an 8-bit G component in byte 1.
        /// </summary>
        R8G8UNorm,
        /// <summary>
        /// Specifies a two-component, 16-bit signed normalized format that has an
        /// 8-bit R component in byte 0, and an 8-bit G component in byte 1.
        /// </summary>
        R8G8SNorm,
        /// <summary>
        /// Specifies a two-component, 16-bit unsigned integer format that has an 
        /// 8-bit R component in byte 0, and an 8-bit G component in byte 1.
        /// </summary>
        R8G8UInt,
        /// <summary>
        /// Specifies a two-component, 16-bit signed integer format that has an 
        /// 8-bit R component in byte 0, and an 8-bit G component in byte 1.
        /// </summary>
        R8G8SInt,
        #endregion
        #region R8G8B8A8
        /// <summary>
        /// Specifies a four-component, 32-bit unsigned normalized format that has an 
        /// 8-bit R component in byte 0, an 8-bit G component in byte 1, an 8-bit B 
        /// component in byte 2, and an 8-bit A component in byte 3.
        /// </summary>
        R8G8B8A8UNorm,
        /// <summary>
        /// Specifies a four-component, 32-bit signed normalized format that has an 
        /// 8-bit R component in byte 0, an 8-bit G component in byte 1, an 8-bit B 
        /// component in byte 2, and an 8-bit A component in byte 3.
        /// </summary>
        R8G8B8A8SNorm,
        /// <summary>
        /// Specifies a four-component, 32-bit unsigned integer format that has an 
        /// 8-bit R component in byte 0, an 8-bit G component in byte 1, an 8-bit B 
        /// component in byte 2, and an 8-bit A component in byte 3.
        /// </summary>
        R8G8B8A8UInt,
        /// <summary>
        /// Specifies a four-component, 32-bit signed integer format that has an 8-bit
        /// R component in byte 0, an 8-bit G component in byte 1, an 8-bit B component 
        /// in byte 2, and an 8-bit A component in byte 3.
        /// </summary>
        R8G8B8A8SInt,
        /// <summary>
        /// Specifies a four-component, 32-bit unsigned normalized format that has an 
        /// 8-bit R component stored with sRGB nonlinear encoding in byte 0, an 8-bit 
        /// G component stored with sRGB nonlinear encoding in byte 1, an 8-bit B 
        /// component stored with sRGB nonlinear encoding in byte 2, and an 8-bit A 
        /// component in byte 3.
        /// </summary>
        R8G8B8A8Srgb,
        #endregion
        #region B8G8R8A8
        /// <summary>
        /// Specifies a four-component, 32-bit unsigned normalized format that has an 
        /// 8-bit B component in byte 0, an 8-bit G component in byte 1, an 8-bit R 
        /// component in byte 2, and an 8-bit A component in byte 3.
        /// </summary>
        B8G8R8A8UNorm,
        /// <summary>
        /// Specifies a four-component, 32-bit unsigned normalized format that has an 
        /// 8-bit B component stored with sRGB nonlinear encoding in byte 0, an 8-bit 
        /// G component stored with sRGB nonlinear encoding in byte 1, an 8-bit R 
        /// component stored with sRGB nonlinear encoding in byte 2, and an 8-bit A 
        /// component in byte 3.
        /// </summary>
        B8G8R8A8Srgb,
        #endregion

        #region B10G10R10A2
        /// <summary>
        /// Specifies a four-component, 32-bit packed unsigned normalized format that
        /// has a 2-bit A component in bits 30..31, a 10-bit R component in bits 20..29, 
        /// a 10-bit G component in bits 10..19, and a 10-bit B component in bits 0..9.
        /// </summary>
        B10G10R10A2UNorm,
        /// <summary>
        /// Specifies a four-component, 32-bit packed unsigned integer format that has
        /// a 2-bit A component in bits 30..31, a 10-bit R component in bits 20..29, a 
        /// 10-bit G component in bits 10..19, and a 10-bit B component in bits 0..9.
        /// </summary>
        B10G10R10A2UInt,
        #endregion
        #region R10G10B10A2
        /// <summary>
        /// Specifies a four-component, 32-bit packed unsigned normalized format that 
        /// has a 2-bit A component in bits 30..31, a 10-bit B component in bits 20..29, 
        /// a 10-bit G component in bits 10..19, and a 10-bit R component in bits 0..9.
        /// </summary>
        R10G10B10A2UNorm,
        /// <summary>
        /// Specifies a four-component, 32-bit packed unsigned integer format that has
        /// a 2-bit A component in bits 30..31, a 10-bit B component in bits 20..29, a
        /// 10-bit G component in bits 10..19, and a 10-bit R component in bits 0..9.
        /// </summary>
        R10G10B10A2UInt,
        #endregion

        #region R16
        /// <summary>
        /// Specifies a one-component, 16-bit unsigned normalized format that has a 
        /// single 16-bit R component.
        /// </summary>
        R16UNorm,
        /// <summary>
        /// Specifies a one-component, 16-bit signed normalized format that has a 
        /// single 16-bit R component.
        /// </summary>
        R16SNorm,
        /// <summary>
        /// Specifies a one-component, 16-bit unsigned integer format that has a
        /// single 16-bit R component.
        /// </summary>
        R16UInt,
        /// <summary>
        /// Specifies a one-component, 16-bit signed integer format that has a single
        /// 16-bit R component.
        /// </summary>
        R16SInt,
        /// <summary>
        /// Specifies a one-component, 16-bit signed floating-point format that has a
        /// single 16-bit R component.
        /// </summary>
        R16SFloat,
        #endregion
        #region R16G16
        /// <summary>
        /// Specifies a two-component, 32-bit unsigned normalized format that has a 
        /// 16-bit R component in bytes 0..1, and a 16-bit G component in bytes 2..3.
        /// </summary>
        R16G16UNorm,
        /// <summary>
        /// Specifies a two-component, 32-bit signed normalized format that has a 16-bit
        /// R component in bytes 0..1, and a 16-bit G component in bytes 2..3.
        /// </summary>
        R16G16SNorm,
        /// <summary>
        /// Specifies a two-component, 32-bit unsigned integer format that has a 16-bit
        /// R component in bytes 0..1, and a 16-bit G component in bytes 2..3.
        /// </summary>
        R16G16UInt,
        /// <summary>
        /// Specifies a two-component, 32-bit signed integer format that has a 16-bit R 
        /// component in bytes 0..1, and a 16-bit G component in bytes 2..3.
        /// </summary>
        R16G16SInt,
        /// <summary>
        /// Specifies a two-component, 32-bit signed floating-point format that has a 
        /// 16-bit R component in bytes 0..1, and a 16-bit G component in bytes 2..3.
        /// </summary>
        R16G16SFloat,
        #endregion
        #region R16G16B16A16
        /// <summary>
        /// Specifies a four-component, 64-bit unsigned normalized format that has a 16-bit
        /// R component in bytes 0..1, a 16-bit G component in bytes 2..3, a 16-bit B component
        /// in bytes 4..5, and a 16-bit A component in bytes 6..7.
        /// </summary>
        R16G16B16A16UNorm,
        /// <summary>
        /// Specifies a four-component, 64-bit signed normalized format that has a 16-bit R 
        /// component in bytes 0..1, a 16-bit G component in bytes 2..3, a 16-bit B component
        /// in bytes 4..5, and a 16-bit A component in bytes 6..7.
        /// </summary>
        R16G16B16A16SNorm,
        /// <summary>
        /// Specifies a four-component, 64-bit unsigned integer format that has a 16-bit R 
        /// component in bytes 0..1, a 16-bit G component in bytes 2..3, a 16-bit B component 
        /// in bytes 4..5, and a 16-bit A component in bytes 6..7.
        /// </summary>
        R16G16B16A16UInt,
        /// <summary>
        /// Specifies a four-component, 64-bit signed integer format that has a 16-bit R 
        /// component in bytes 0..1, a 16-bit G component in bytes 2..3, a 16-bit B component 
        /// in bytes 4..5, and a 16-bit A component in bytes 6..7.
        /// </summary>
        R16G16B16A16SInt,
        /// <summary>
        /// Specifies a four-component, 64-bit signed floating-point format that has a 16-bit R 
        /// component in bytes 0..1, a 16-bit G component in bytes 2..3, a 16-bit B component 
        /// in bytes 4..5, and a 16-bit A component in bytes 6..7.
        /// </summary>
        R16G16B16A16SFloat,
        #endregion

        #region R32
        /// <summary>
        /// Specifies a one-component, 32-bit unsigned integer format that has a single 32-bit 
        /// R component.
        /// </summary>
        R32UInt,
        /// <summary>
        /// Specifies a one-component, 32-bit signed integer format that has a single 32-bit R
        /// component.
        /// </summary>
        R32SInt,
        /// <summary>
        /// Specifies a one-component, 32-bit signed floating-point format that has a single
        /// 32-bit R component.
        /// </summary>
        R32SFloat,
        #endregion
        #region R32G32
        /// <summary>
        /// Specifies a two-component, 64-bit unsigned integer format that has a 32-bit R 
        /// component in bytes 0..3, and a 32-bit G component in bytes 4..7.
        /// </summary>
        R32G32UInt,
        /// <summary>
        /// Specifies a two-component, 64-bit signed integer format that has a 32-bit R 
        /// component in bytes 0..3, and a 32-bit G component in bytes 4..7.
        /// </summary>
        R32G32SInt,
        /// <summary>
        /// Specifies a two-component, 64-bit signed floating-point format that has a 32-bit 
        /// R component in bytes 0..3, and a 32-bit G component in bytes 4..7.
        /// </summary>
        R32G32SFloat,
        #endregion
        #region R32G32B32
        /// <summary>
        /// Specifies a three-component, 96-bit unsigned integer format that has a 32-bit R 
        /// component in bytes 0..3, a 32-bit G component in bytes 4..7, and a 32-bit B 
        /// component in bytes 8..11.
        /// </summary>
        R32G32B32UInt,
        /// <summary>
        /// Specifies a three-component, 96-bit signed integer format that has a 32-bit R 
        /// component in bytes 0..3, a 32-bit G component in bytes 4..7, and a 32-bit B 
        /// component in bytes 8..11.
        /// </summary>
        R32G32B32SInt,
        /// <summary>
        /// Specifies a three-component, 96-bit signed floating-point format that has a 32-bit 
        /// R component in bytes 0..3, a 32-bit G component in bytes 4..7, and a 32-bit B
        /// component in bytes 8..11.
        /// </summary>
        R32G32B32SFloat,
        #endregion
        #region R32G32B32A32
        /// <summary>
        /// Specifies a four-component, 128-bit unsigned integer format that has a 32-bit R 
        /// component in bytes 0..3, a 32-bit G component in bytes 4..7, a 32-bit B component
        /// in bytes 8..11, and a 32-bit A component in bytes 12..15.
        /// </summary>
        R32G32B32A32UInt,
        /// <summary>
        /// Specifies a four-component, 128-bit signed integer format that has a 32-bit R component 
        /// in bytes 0..3, a 32-bit G component in bytes 4..7, a 32-bit B component in bytes 8..11,
        /// and a 32-bit A component in bytes 12..15.
        /// </summary>
        R32G32B32A32SInt,
        /// <summary>
        /// Specifies a four-component, 128-bit signed floating-point format that has a 32-bit R 
        /// component in bytes 0..3, a 32-bit G component in bytes 4..7, a 32-bit B component in bytes 
        /// 8..11, and a 32-bit A component in bytes 12..15.
        /// </summary>
        R32G32B32A32SFloat,
        #endregion
        #region Depth and Stencil
        /// <summary>
        /// Specifies a one-component, 16-bit unsigned normalized format that has a single 16-bit depth 
        /// component.
        /// </summary>
        D16UNorm,
        /// <summary>
        /// Specifies a two-component, 32-bit packed format that has 8 unsigned integer bits in the stencil 
        /// component, and 24 unsigned normalized bits in the depth component.
        /// </summary>
        D24UNormS8UInt,
        /// <summary>
        /// Specifies a one-component, 32-bit signed floating-point format that has 32-bits in the depth 
        /// component.
        /// </summary>
        D32SFloat,
        /// <summary>
        /// Specifies a two-component format that has 32 signed float bits in the depth component and 8
        /// unsigned integer bits in the stencil component. There are optionally: 24-bits that are unused.
        /// </summary>
        D32SFloatS8UInt,
        /// <summary>
        /// Specifies a one-component, 8-bit unsigned integer format that has 8-bits in the stencil component.
        /// </summary>
        S8UInt,
        #endregion

        #region BC
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data with sRGB nonlinear encoding, 
        /// and provides 1 bit of alpha.
        /// </summary>
        BC1RgbaSrgb,
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data, and provides 1 bit of alpha.
        /// </summary>
        BC1RgbaUNorm,
        /// <summary>
        /// Specifies a three-component, block-compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data with sRGB nonlinear encoding.
        /// This format has no alpha and is considered opaque.
        /// </summary>
        BC1RgbSrgb,
        /// <summary>
        /// Specifies a three-component, block-compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data. This format has no alpha and 
        /// is considered opaque.
        /// </summary>
        BC1RgbUNorm,
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with the first 64 bits encoding
        /// alpha values followed by 64 bits encoding RGB values with sRGB nonlinear encoding.
        /// </summary>
        BC2Srgb,
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with the first 64 bits encoding
        /// alpha values followed by 64 bits encoding RGB values.
        /// </summary>
        BC2UNorm,
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with the first 64 bits encoding 
        /// alpha values followed by 64 bits encoding RGB values with sRGB nonlinear encoding.
        /// </summary>
        BC3Srgb,
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with the first 64 bits encoding 
        /// alpha values followed by 64 bits encoding RGB values.
        /// </summary>
        BC3UNorm,
        /// <summary>
        /// Specifies a one-component, block-compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of signed normalized red texel data.
        /// </summary>
        BC4SNorm,
        /// <summary>
        /// Specifies a one-component, block-compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized red texel data.
        /// </summary>
        BC4UNorm,
        /// <summary>
        /// Specifies a two-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of signed normalized RG texel data with the first 64 bits encoding 
        /// red values followed by 64 bits encoding green values.
        /// </summary>
        BC5SNorm,
        /// <summary>
        /// Specifies a two-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RG texel data with the first 64 bits encoding
        /// red values followed by 64 bits encoding green values.
        /// </summary>
        BC5UNorm,
        /// <summary>
        /// Specifies a three-component, block-compressed format where each 128-bit compressed texel block
        /// encodes a 4×4 rectangle of signed floating-point RGB texel data.
        /// </summary>
        BC6HSFloat,
        /// <summary>
        /// Specifies a three-component, block-compressed format where each 128-bit compressed texel block
        /// encodes a 4×4 rectangle of unsigned floating-point RGB texel data.
        /// </summary>
        BC6HUFloat,
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        BC7Srgb,
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        BC7UNorm,

        #endregion
        #region ETC and EAC
        /// <summary>
        /// Specifies a one-component, ETC2 compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of signed normalized red texel data.
        /// </summary>
        EacR11SNorm,
        /// <summary>
        /// Specifies a one-component, ETC2 compressed format where each 64-bit compressed texel block
        /// encodes a 4×4 rectangle of unsigned normalized red texel data.
        /// </summary>
        EacR11UNorm,
        /// <summary>
        /// Specifies a two-component, ETC2 compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of signed normalized RG texel data with the first 64 bits encoding 
        /// red values followed by 64 bits encoding green values.
        /// </summary>
        EacR11G11SNorm,
        /// <summary>
        /// Specifies a two-component, ETC2 compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RG texel data with the first 64 bits encoding 
        /// red values followed by 64 bits encoding green values.
        /// </summary>
        EacR11G11UNorm,
        /// <summary>
        /// Specifies a three-component, ETC2 compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data with sRGB nonlinear encoding. 
        /// This format has no alpha and is considered opaque.
        /// </summary>
        Etc2R8G8B8Srgb,
        /// <summary>
        /// Specifies a three-component, ETC2 compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data with sRGB nonlinear encoding. 
        /// This format has no alpha and is considered opaque.
        /// </summary>
        Etc2R8G8B8UNorm,
        /// <summary>
        /// Specifies a four-component, ETC2 compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data with sRGB nonlinear encoding, 
        /// and provides 1 bit of alpha.
        /// </summary>
        Etc2R8G8B8A1Srgb,
        /// <summary>
        /// Specifies a four-component, ETC2 compressed format where each 64-bit compressed texel block
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data, and provides 1 bit of alpha.
        /// </summary>
        Etc2R8G8B8A1UNorm,
        /// <summary>
        /// Specifies a four-component, ETC2 compressed format where each 128-bit compressed texel block
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with the first 64 bits encoding
        /// alpha values followed by 64 bits encoding RGB values with sRGB nonlinear encoding applied.
        /// </summary>
        Etc2R8G8B8A8Srgb,
        /// <summary>
        /// Specifies a four-component, ETC2 compressed format where each 128-bit compressed texel block
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with the first 64 bits encoding
        /// alpha values followed by 64 bits encoding RGB values.
        /// </summary>
        Etc2R8G8B8A8UNorm,
        #endregion
        #region ASTC
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes a 10×10 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc10x10Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 10×10 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc10x10UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 10×5 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc10x5Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 10×5 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc10x5UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes a 10×6 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc10x6Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 10×6 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc10x6UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 10×8 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc10x8Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes a 10×8 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc10x8UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 12×10 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc12x10Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 12×10 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc12x10UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 12×12 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc12x12Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 12×12 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc12x12UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc4x4Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc4x4UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes a 5×4 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc5x4Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 5×4 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc5x4UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 5×5 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc5x5Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 5×5 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc5x5UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 6×5 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc6x5Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 6×5 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc6x5UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 6×6 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc6x6Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes a 6×6 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc6x6UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes an 8×5 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc8x5Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes an 8×5 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc8x5UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes an 8×6 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc8x6Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes an 8×6 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc8x6UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes an 8×8 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc8x8Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes an 8×8 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc8x8UNorm,
        #endregion
    }
}
