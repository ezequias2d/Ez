// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace Ez.Graphics
{
    /// <summary>
    /// A pixel format enum.
    /// </summary>
    [Flags]
    public enum PixelFormat : int
    {
        /// <summary>
        /// Specifies that the format is not specified.
        /// </summary>
        Undefined,

        #region components flags
        /// <summary>
        /// Red component.
        /// </summary>
        R = 1,
        /// <summary>
        /// Red and Green components.
        /// </summary>
        RG = 1 << 1,
        /// <summary>
        /// Red, Green and Blue components.
        /// </summary>
        RGB = 1 << 2,
        /// <summary>
        /// Red, Green, Blue and Alpha components.
        /// </summary>
        RGBA = 1 << 3,
        /// <summary>
        /// Blue, Green, Red and Alpha components.
        /// </summary>
        BGRA = 1 << 4,
        /// <summary>
        /// Depth component.
        /// </summary>
        Depth = 1 << 5,
        /// <summary>
        /// Stencil component.
        /// </summary>
        Stencil = 1 << 6,
        #endregion

        #region size format
        /// <summary>
        /// 8 bits format.
        /// </summary>
        F8 = 1 << 7,
        /// <summary>
        /// 8 bits and 1 bit formats.
        /// </summary>
        F8F1 = 1 << 8,
        /// <summary>
        /// 10 bits and 2 bits formats.
        /// </summary>
        F10F10F10F2 = 1 << 9,
        /// <summary>
        /// 11 bits format.
        /// </summary>
        F11 = 1 << 10,
        /// <summary>
        /// 16 bits format.
        /// </summary>
        F16 = 1 << 11,
        /// <summary>
        /// 32 bits format.
        /// </summary>
        F32 = 1 << 12,
        /// <summary>
        /// 24 bits and 8 bits formats.
        /// </summary>
        F24F8 = 1 << 13,
        /// <summary>
        /// 32 bits and 8 bits formats.
        /// </summary>
        F32F8 = 1 << 14,
        #endregion

        #region pixel value.
        /// <summary>
        /// Pixel value format mask.
        /// </summary>
        PixelValueMask = Unsigned | SNorm | SInt | Srgb | SFloat,

        /// <summary>
        /// Unsigned.
        /// </summary>
        Unsigned = 1 << 15,
        /// <summary>
        /// Signed normalized.
        /// </summary>
        SNorm = 1 << 16,
        /// <summary>
        /// Unsigned normalized.
        /// </summary>
        UNorm = SNorm | Unsigned,
        /// <summary>
        /// Signed integer.
        /// </summary>
        SInt = 1 << 17,
        /// <summary>
        /// Unsigned integer.
        /// </summary>
        UInt = SInt | Unsigned,
        /// <summary>
        /// Srgb.
        /// </summary>
        Srgb = 1 << 18,
        /// <summary>
        /// Signed float.
        /// </summary>
        SFloat = 1 << 19,
        /// <summary>
        /// Unsigned float.
        /// </summary>
        UFloat = SFloat | Unsigned,
        #endregion

        #region compression flags
        /// <summary>
        /// Compressed.
        /// </summary>
        Compressed = 1 << 20,

        #region block compression type
        /// <summary>
        /// Block compression 1.
        /// </summary>
        BC1 = 1 << 21,
        /// <summary>
        /// Block compression 2.
        /// </summary>
        BC2 = BC1 * 2,
        /// <summary>
        /// Block compression 3.
        /// </summary>
        BC3 = BC1 * 3,
        /// <summary>
        /// Block compression 4.
        /// </summary>
        BC4 = BC1 * 4,
        /// <summary>
        /// Block compression 5.
        /// </summary>
        BC5 = BC1 * 5,
        /// <summary>
        /// Block compression 6.
        /// </summary>
        BC6H = BC1 * 6,
        /// <summary>
        /// Block compression 7.
        /// </summary>
        BC7 = BC1 * 7,
        #endregion
        
        /// <summary>
        /// Ericsson texture compression.
        /// </summary>
        EacEtc2 = 1 << 24,

        #region Astc flags
        /// <summary>
        /// Adaptive scalable texture compression.
        /// </summary>
        Astc = 1 << 25,
        /// <summary>
        /// 10 pixels in dimension.
        /// </summary>
        X10 = 1 << 26,
        /// <summary>
        /// 5 pixels in dimension.
        /// </summary>
        X5 = 1 << 27,
        /// <summary>
        /// 6 pixels in dimension.
        /// </summary>
        X6 = 1 << 28,
        /// <summary>
        /// 8 pixels in dimension.
        /// </summary>
        X8 = 1 << 29,
        /// <summary>
        /// 12 pixels in dimension.
        /// </summary>
        X12 = 1 << 30,
        /// <summary>
        /// 4 pixels in dimension.
        /// </summary>
        X4 = 1 << 31,
        #endregion
        #endregion

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
        R8UNorm = R | F8 | UNorm,
        /// <summary>
        /// Specifies a one-component, 8-bit signed normalized format that has a 
        /// single 8-bit R component.
        /// </summary>
        R8SNorm = R | F8 | SNorm,
        /// <summary>
        /// Specifies a one-component, 8-bit unsigned integer format that has a 
        /// single 8-bit R component.
        /// </summary>
        R8UInt = R | F8 | UInt,
        /// <summary>
        /// Specifies a one-component, 8-bit signed integer format that has a 
        /// single 8-bit R component.
        /// </summary>
        R8SInt = R | F8 | SInt,
        #endregion
        #region R8G8
        /// <summary>
        /// Specifies a two-component, 16-bit unsigned normalized format that has
        /// an 8-bit R component in byte 0, and an 8-bit G component in byte 1.
        /// </summary>
        R8G8UNorm = RG | F8 | UNorm,
        /// <summary>
        /// Specifies a two-component, 16-bit signed normalized format that has an
        /// 8-bit R component in byte 0, and an 8-bit G component in byte 1.
        /// </summary>
        R8G8SNorm = RG | F8 | SNorm,
        /// <summary>
        /// Specifies a two-component, 16-bit unsigned integer format that has an 
        /// 8-bit R component in byte 0, and an 8-bit G component in byte 1.
        /// </summary>
        R8G8UInt = RG | F8 | UInt,
        /// <summary>
        /// Specifies a two-component, 16-bit signed integer format that has an 
        /// 8-bit R component in byte 0, and an 8-bit G component in byte 1.
        /// </summary>
        R8G8SInt = RG | F8 | SInt,
        #endregion
        #region R8G8B8A8
        /// <summary>
        /// Specifies a four-component, 32-bit unsigned normalized format that has an 
        /// 8-bit R component in byte 0, an 8-bit G component in byte 1, an 8-bit B 
        /// component in byte 2, and an 8-bit A component in byte 3.
        /// </summary>
        R8G8B8A8UNorm = RGBA | F8 | UNorm,
        /// <summary>
        /// Specifies a four-component, 32-bit signed normalized format that has an 
        /// 8-bit R component in byte 0, an 8-bit G component in byte 1, an 8-bit B 
        /// component in byte 2, and an 8-bit A component in byte 3.
        /// </summary>
        R8G8B8A8SNorm = RGBA | F8 | SNorm,
        /// <summary>
        /// Specifies a four-component, 32-bit unsigned integer format that has an 
        /// 8-bit R component in byte 0, an 8-bit G component in byte 1, an 8-bit B 
        /// component in byte 2, and an 8-bit A component in byte 3.
        /// </summary>
        R8G8B8A8UInt = RGBA | F8 | UInt,
        /// <summary>
        /// Specifies a four-component, 32-bit signed integer format that has an 8-bit
        /// R component in byte 0, an 8-bit G component in byte 1, an 8-bit B component 
        /// in byte 2, and an 8-bit A component in byte 3.
        /// </summary>
        R8G8B8A8SInt = RGBA | F8 | SInt,
        /// <summary>
        /// Specifies a four-component, 32-bit unsigned normalized format that has an 
        /// 8-bit R component stored with sRGB nonlinear encoding in byte 0, an 8-bit 
        /// G component stored with sRGB nonlinear encoding in byte 1, an 8-bit B 
        /// component stored with sRGB nonlinear encoding in byte 2, and an 8-bit A 
        /// component in byte 3.
        /// </summary>
        R8G8B8A8Srgb = RGBA | F8 | Srgb,
        #endregion
        #region B8G8R8A8
        /// <summary>
        /// Specifies a four-component, 32-bit unsigned normalized format that has an 
        /// 8-bit B component in byte 0, an 8-bit G component in byte 1, an 8-bit R 
        /// component in byte 2, and an 8-bit A component in byte 3.
        /// </summary>
        B8G8R8A8UNorm = BGRA | F8 | UNorm,
        /// <summary>
        /// Specifies a four-component, 32-bit unsigned normalized format that has an 
        /// 8-bit B component stored with sRGB nonlinear encoding in byte 0, an 8-bit 
        /// G component stored with sRGB nonlinear encoding in byte 1, an 8-bit R 
        /// component stored with sRGB nonlinear encoding in byte 2, and an 8-bit A 
        /// component in byte 3.
        /// </summary>
        B8G8R8A8Srgb = BGRA | F8 | SNorm,
        #endregion

        #region B10G10R10A2
        /// <summary>
        /// Specifies a four-component, 32-bit packed unsigned normalized format that
        /// has a 2-bit A component in bits 30..31, a 10-bit R component in bits 20..29, 
        /// a 10-bit G component in bits 10..19, and a 10-bit B component in bits 0..9.
        /// </summary>
        B10G10R10A2UNorm = BGRA | F10F10F10F2 | UNorm,
        /// <summary>
        /// Specifies a four-component, 32-bit packed unsigned integer format that has
        /// a 2-bit A component in bits 30..31, a 10-bit R component in bits 20..29, a 
        /// 10-bit G component in bits 10..19, and a 10-bit B component in bits 0..9.
        /// </summary>
        B10G10R10A2UInt = BGRA | F10F10F10F2 | UInt,
        #endregion
        #region R10G10B10A2
        /// <summary>
        /// Specifies a four-component, 32-bit packed unsigned normalized format that 
        /// has a 2-bit A component in bits 30..31, a 10-bit B component in bits 20..29, 
        /// a 10-bit G component in bits 10..19, and a 10-bit R component in bits 0..9.
        /// </summary>
        R10G10B10A2UNorm = RGBA | F10F10F10F2 | UNorm,
        /// <summary>
        /// Specifies a four-component, 32-bit packed unsigned integer format that has
        /// a 2-bit A component in bits 30..31, a 10-bit B component in bits 20..29, a
        /// 10-bit G component in bits 10..19, and a 10-bit R component in bits 0..9.
        /// </summary>
        R10G10B10A2UInt = RGBA | F10F10F10F2 | UInt,
        #endregion

        #region R16
        /// <summary>
        /// Specifies a one-component, 16-bit unsigned normalized format that has a 
        /// single 16-bit R component.
        /// </summary>
        R16UNorm = R | F16 | UNorm,
        /// <summary>
        /// Specifies a one-component, 16-bit signed normalized format that has a 
        /// single 16-bit R component.
        /// </summary>
        R16SNorm = R | F16 | SNorm,
        /// <summary>
        /// Specifies a one-component, 16-bit unsigned integer format that has a
        /// single 16-bit R component.
        /// </summary>
        R16UInt = R | F16 | UInt,
        /// <summary>
        /// Specifies a one-component, 16-bit signed integer format that has a single
        /// 16-bit R component.
        /// </summary>
        R16SInt = R | F16 | SInt,
        /// <summary>
        /// Specifies a one-component, 16-bit signed floating-point format that has a
        /// single 16-bit R component.
        /// </summary>
        R16SFloat = R | F16 | SFloat,
        #endregion
        #region R16G16
        /// <summary>
        /// Specifies a two-component, 32-bit unsigned normalized format that has a 
        /// 16-bit R component in bytes 0..1, and a 16-bit G component in bytes 2..3.
        /// </summary>
        R16G16UNorm = RG | F16 | UNorm,
        /// <summary>
        /// Specifies a two-component, 32-bit signed normalized format that has a 16-bit
        /// R component in bytes 0..1, and a 16-bit G component in bytes 2..3.
        /// </summary>
        R16G16SNorm = RG | F16 | SNorm,
        /// <summary>
        /// Specifies a two-component, 32-bit unsigned integer format that has a 16-bit
        /// R component in bytes 0..1, and a 16-bit G component in bytes 2..3.
        /// </summary>
        R16G16UInt = RG | F16 | UInt,
        /// <summary>
        /// Specifies a two-component, 32-bit signed integer format that has a 16-bit R 
        /// component in bytes 0..1, and a 16-bit G component in bytes 2..3.
        /// </summary>
        R16G16SInt = RG | F16 | SInt,
        /// <summary>
        /// Specifies a two-component, 32-bit signed floating-point format that has a 
        /// 16-bit R component in bytes 0..1, and a 16-bit G component in bytes 2..3.
        /// </summary>
        R16G16SFloat = RG | F16 | SFloat,
        #endregion
        #region R16G16B16A16
        /// <summary>
        /// Specifies a four-component, 64-bit unsigned normalized format that has a 16-bit
        /// R component in bytes 0..1, a 16-bit G component in bytes 2..3, a 16-bit B component
        /// in bytes 4..5, and a 16-bit A component in bytes 6..7.
        /// </summary>
        R16G16B16A16UNorm = RGBA | F16 | UNorm,
        /// <summary>
        /// Specifies a four-component, 64-bit signed normalized format that has a 16-bit R 
        /// component in bytes 0..1, a 16-bit G component in bytes 2..3, a 16-bit B component
        /// in bytes 4..5, and a 16-bit A component in bytes 6..7.
        /// </summary>
        R16G16B16A16SNorm = RGBA | F16 | SNorm,
        /// <summary>
        /// Specifies a four-component, 64-bit unsigned integer format that has a 16-bit R 
        /// component in bytes 0..1, a 16-bit G component in bytes 2..3, a 16-bit B component 
        /// in bytes 4..5, and a 16-bit A component in bytes 6..7.
        /// </summary>
        R16G16B16A16UInt = RGBA | F16 | UInt,
        /// <summary>
        /// Specifies a four-component, 64-bit signed integer format that has a 16-bit R 
        /// component in bytes 0..1, a 16-bit G component in bytes 2..3, a 16-bit B component 
        /// in bytes 4..5, and a 16-bit A component in bytes 6..7.
        /// </summary>
        R16G16B16A16SInt = RGBA | F16 | SInt,
        /// <summary>
        /// Specifies a four-component, 64-bit signed floating-point format that has a 16-bit R 
        /// component in bytes 0..1, a 16-bit G component in bytes 2..3, a 16-bit B component 
        /// in bytes 4..5, and a 16-bit A component in bytes 6..7.
        /// </summary>
        R16G16B16A16SFloat = RGBA | F16 | SFloat,
        #endregion

        #region R32
        /// <summary>
        /// Specifies a one-component, 32-bit unsigned integer format that has a single 32-bit 
        /// R component.
        /// </summary>
        R32UInt = R | F32 | UInt,
        /// <summary>
        /// Specifies a one-component, 32-bit signed integer format that has a single 32-bit R
        /// component.
        /// </summary>
        R32SInt = R | F32 | SInt,
        /// <summary>
        /// Specifies a one-component, 32-bit signed floating-point format that has a single
        /// 32-bit R component.
        /// </summary>
        R32SFloat = R | F32 | SFloat,
        #endregion
        #region R32G32
        /// <summary>
        /// Specifies a two-component, 64-bit unsigned integer format that has a 32-bit R 
        /// component in bytes 0..3, and a 32-bit G component in bytes 4..7.
        /// </summary>
        R32G32UInt = RG | F32 | UInt,
        /// <summary>
        /// Specifies a two-component, 64-bit signed integer format that has a 32-bit R 
        /// component in bytes 0..3, and a 32-bit G component in bytes 4..7.
        /// </summary>
        R32G32SInt = RG | F32 | SInt,
        /// <summary>
        /// Specifies a two-component, 64-bit signed floating-point format that has a 32-bit 
        /// R component in bytes 0..3, and a 32-bit G component in bytes 4..7.
        /// </summary>
        R32G32SFloat = RG | F32 | SFloat,
        #endregion
        #region R32G32B32
        /// <summary>
        /// Specifies a three-component, 96-bit unsigned integer format that has a 32-bit R 
        /// component in bytes 0..3, a 32-bit G component in bytes 4..7, and a 32-bit B 
        /// component in bytes 8..11.
        /// </summary>
        R32G32B32UInt = RGB | F32 | UInt,
        /// <summary>
        /// Specifies a three-component, 96-bit signed integer format that has a 32-bit R 
        /// component in bytes 0..3, a 32-bit G component in bytes 4..7, and a 32-bit B 
        /// component in bytes 8..11.
        /// </summary>
        R32G32B32SInt = RGB | F32 | SInt,
        /// <summary>
        /// Specifies a three-component, 96-bit signed floating-point format that has a 32-bit 
        /// R component in bytes 0..3, a 32-bit G component in bytes 4..7, and a 32-bit B
        /// component in bytes 8..11.
        /// </summary>
        R32G32B32SFloat = RGB | F32 | SFloat,
        #endregion
        #region R32G32B32A32
        /// <summary>
        /// Specifies a four-component, 128-bit unsigned integer format that has a 32-bit R 
        /// component in bytes 0..3, a 32-bit G component in bytes 4..7, a 32-bit B component
        /// in bytes 8..11, and a 32-bit A component in bytes 12..15.
        /// </summary>
        R32G32B32A32UInt = RGBA | F32 | UInt,
        /// <summary>
        /// Specifies a four-component, 128-bit signed integer format that has a 32-bit R component 
        /// in bytes 0..3, a 32-bit G component in bytes 4..7, a 32-bit B component in bytes 8..11,
        /// and a 32-bit A component in bytes 12..15.
        /// </summary>
        R32G32B32A32SInt = RGBA | F32 | SInt,
        /// <summary>
        /// Specifies a four-component, 128-bit signed floating-point format that has a 32-bit R 
        /// component in bytes 0..3, a 32-bit G component in bytes 4..7, a 32-bit B component in bytes 
        /// 8..11, and a 32-bit A component in bytes 12..15.
        /// </summary>
        R32G32B32A32SFloat = RGBA | F32 | SFloat,
        #endregion
        #region Depth and Stencil
        /// <summary>
        /// Specifies a one-component, 16-bit unsigned normalized format that has a single 16-bit depth 
        /// component.
        /// </summary>
        D16UNorm = Depth | F16 | UNorm,
        /// <summary>
        /// Specifies a two-component, 32-bit packed format that has 8 unsigned integer bits in the stencil 
        /// component, and 24 unsigned normalized bits in the depth component.
        /// </summary>
        D24UNormS8UInt = Depth | Stencil | F24F8 | UNorm | UInt,
        /// <summary>
        /// Specifies a one-component, 32-bit signed floating-point format that has 32-bits in the depth 
        /// component.
        /// </summary>
        D32SFloat = Depth | F32 | SFloat,
        /// <summary>
        /// Specifies a two-component format that has 32 signed float bits in the depth component and 8
        /// unsigned integer bits in the stencil component. There are optionally: 24-bits that are unused.
        /// </summary>
        D32SFloatS8UInt = Depth | F32F8 | SFloat | UInt,
        /// <summary>
        /// Specifies a one-component, 8-bit unsigned integer format that has 8-bits in the stencil component.
        /// </summary>
        S8UInt = Stencil | F8 | UInt,
        #endregion

        #region BC
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data with sRGB nonlinear encoding, 
        /// and provides 1 bit of alpha.
        /// </summary>
        BC1RgbaSrgb = BC1 | Compressed | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data, and provides 1 bit of alpha.
        /// </summary>
        BC1RgbaUNorm = BC1 | Compressed | RGBA | UNorm,
        /// <summary>
        /// Specifies a three-component, block-compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data with sRGB nonlinear encoding.
        /// This format has no alpha and is considered opaque.
        /// </summary>
        BC1RgbSrgb = BC1 | Compressed | RGB | Srgb,
        /// <summary>
        /// Specifies a three-component, block-compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data. This format has no alpha and 
        /// is considered opaque.
        /// </summary>
        BC1RgbUNorm = BC1 | Compressed | RGB | UNorm,
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with the first 64 bits encoding
        /// alpha values followed by 64 bits encoding RGB values with sRGB nonlinear encoding.
        /// </summary>
        BC2Srgb = BC2 | Compressed | RGBA | Srgb, 
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with the first 64 bits encoding
        /// alpha values followed by 64 bits encoding RGB values.
        /// </summary>
        BC2UNorm = BC2 | Compressed | RGBA | UNorm,
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with the first 64 bits encoding 
        /// alpha values followed by 64 bits encoding RGB values with sRGB nonlinear encoding.
        /// </summary>
        BC3Srgb = BC3 | Compressed | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with the first 64 bits encoding 
        /// alpha values followed by 64 bits encoding RGB values.
        /// </summary>
        BC3UNorm = BC3 | Compressed | RGBA | UNorm,
        /// <summary>
        /// Specifies a one-component, block-compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of signed normalized red texel data.
        /// </summary>
        BC4SNorm = BC4 | Compressed | R | SNorm,
        /// <summary>
        /// Specifies a one-component, block-compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized red texel data.
        /// </summary>
        BC4UNorm = BC4 | Compressed | R | UNorm,
        /// <summary>
        /// Specifies a two-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of signed normalized RG texel data with the first 64 bits encoding 
        /// red values followed by 64 bits encoding green values.
        /// </summary>
        BC5SNorm = BC5 | Compressed | RG | SNorm,
        /// <summary>
        /// Specifies a two-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RG texel data with the first 64 bits encoding
        /// red values followed by 64 bits encoding green values.
        /// </summary>
        BC5UNorm = BC5 | Compressed | RG | UNorm,
        /// <summary>
        /// Specifies a three-component, block-compressed format where each 128-bit compressed texel block
        /// encodes a 4×4 rectangle of signed floating-point RGB texel data.
        /// </summary>
        BC6HSFloat = BC6H | Compressed | RGB | SFloat,
        /// <summary>
        /// Specifies a three-component, block-compressed format where each 128-bit compressed texel block
        /// encodes a 4×4 rectangle of unsigned floating-point RGB texel data.
        /// </summary>
        BC6HUFloat = BC6H | Compressed | RGB | UFloat,
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        BC7Srgb = BC7 | Compressed | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, block-compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        BC7UNorm = BC7 | Compressed | RGBA | UNorm,

        #endregion
        #region ETC and EAC
        /// <summary>
        /// Specifies a one-component, ETC2 compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of signed normalized red texel data.
        /// </summary>
        EacR11SNorm = EacEtc2 | Compressed | R | F11 | SNorm,
        /// <summary>
        /// Specifies a one-component, ETC2 compressed format where each 64-bit compressed texel block
        /// encodes a 4×4 rectangle of unsigned normalized red texel data.
        /// </summary>
        EacR11UNorm = EacEtc2 | Compressed | R | F11 | UNorm,
        /// <summary>
        /// Specifies a two-component, ETC2 compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of signed normalized RG texel data with the first 64 bits encoding 
        /// red values followed by 64 bits encoding green values.
        /// </summary>
        EacR11G11SNorm = EacEtc2 | Compressed | RG | F11 | SNorm,
        /// <summary>
        /// Specifies a two-component, ETC2 compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RG texel data with the first 64 bits encoding 
        /// red values followed by 64 bits encoding green values.
        /// </summary>
        EacR11G11UNorm = EacEtc2 | Compressed | RG | F11 | UNorm,
        /// <summary>
        /// Specifies a three-component, ETC2 compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data with sRGB nonlinear encoding. 
        /// This format has no alpha and is considered opaque.
        /// </summary>
        Etc2R8G8B8Srgb = EacEtc2 | Compressed | RGB | F8 | Srgb,
        /// <summary>
        /// Specifies a three-component, ETC2 compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data with sRGB nonlinear encoding. 
        /// This format has no alpha and is considered opaque.
        /// </summary>
        Etc2R8G8B8UNorm = EacEtc2 | Compressed | RGB | F8 | UNorm,
        /// <summary>
        /// Specifies a four-component, ETC2 compressed format where each 64-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data with sRGB nonlinear encoding, 
        /// and provides 1 bit of alpha.
        /// </summary>
        Etc2R8G8B8A1Srgb = EacEtc2 | Compressed | RGBA | F8F1 | Srgb,
        /// <summary>
        /// Specifies a four-component, ETC2 compressed format where each 64-bit compressed texel block
        /// encodes a 4×4 rectangle of unsigned normalized RGB texel data, and provides 1 bit of alpha.
        /// </summary>
        Etc2R8G8B8A1UNorm = EacEtc2 | Compressed | RGBA | F8F1 | UNorm,
        /// <summary>
        /// Specifies a four-component, ETC2 compressed format where each 128-bit compressed texel block
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with the first 64 bits encoding
        /// alpha values followed by 64 bits encoding RGB values with sRGB nonlinear encoding applied.
        /// </summary>
        Etc2R8G8B8A8Srgb = EacEtc2 | Compressed | RGBA | F8 | Srgb,
        /// <summary>
        /// Specifies a four-component, ETC2 compressed format where each 128-bit compressed texel block
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with the first 64 bits encoding
        /// alpha values followed by 64 bits encoding RGB values.
        /// </summary>
        Etc2R8G8B8A8UNorm = EacEtc2 | Compressed | RGBA | F8 | UNorm,
        #endregion
        #region ASTC
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes a 10×10 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc10x10Srgb = Astc | Compressed | X10 | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 10×10 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc10x10UNorm = Astc | Compressed | X10 | RGBA | UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 10×5 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc10x5Srgb = Astc | Compressed | X10 | X5 | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 10×5 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc10x5UNorm =  Astc | Compressed | X10 | X5 | RGBA | UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes a 10×6 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc10x6Srgb = Astc | Compressed | X10 | X6 | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 10×6 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc10x6UNorm = Astc | Compressed | X10 | X6 | RGBA | UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 10×8 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc10x8Srgb = Astc | Compressed | X10 | X8 | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes a 10×8 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc10x8UNorm = Astc | Compressed | X10 | X8 | RGBA | UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 12×10 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc12x10Srgb = Astc | Compressed | X12 | X10 | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 12×10 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc12x10UNorm = Astc | Compressed | X12 | X10 | RGBA |UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 12×12 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc12x12Srgb = Astc | Compressed | X12 | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 12×12 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc12x12UNorm = Astc | Compressed | X12 | RGBA | UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc4x4Srgb = Astc | Compressed | X4  | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 4×4 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc4x4UNorm = Astc | Compressed | X4 | RGBA | UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes a 5×4 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc5x4Srgb = Astc | Compressed | X5 | X4 | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 5×4 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc5x4UNorm = Astc | Compressed | X5 | X4 | RGBA | UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 5×5 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc5x5Srgb = Astc | Compressed | X5 | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 5×5 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc5x5UNorm = Astc | Compressed | X5 | RGBA | UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 6×5 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc6x5Srgb = Astc | Compressed | X6 | X5 | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 6×5 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc6x5UNorm = Astc | Compressed | X6 | X5 | RGBA | UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes a 6×6 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc6x6Srgb = Astc | Compressed | X6 | RGBA | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes a 6×6 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc6x6UNorm = Astc | Compressed | X6 | RGBA | UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes an 8×5 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding 
        /// applied to the RGB components.
        /// </summary>
        Astc8x5Srgb = Astc | Compressed | X8 | X5 | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes an 8×5 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc8x5UNorm = Astc | Compressed | X8 | X5 | UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes an 8×6 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc8x6Srgb = Astc | Compressed | X8 | X6 | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes an 8×6 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc8x6UNorm = Astc | Compressed | X8 | X6 | UNorm,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block 
        /// encodes an 8×8 rectangle of unsigned normalized RGBA texel data with sRGB nonlinear encoding
        /// applied to the RGB components.
        /// </summary>
        Astc8x8Srgb = Astc | Compressed | X8 | Srgb,
        /// <summary>
        /// Specifies a four-component, ASTC compressed format where each 128-bit compressed texel block
        /// encodes an 8×8 rectangle of unsigned normalized RGBA texel data.
        /// </summary>
        Astc8x8UNorm = Astc | Compressed | X8 | UNorm,
        #endregion
    }
}
