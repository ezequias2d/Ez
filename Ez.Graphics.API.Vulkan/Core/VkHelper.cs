// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Vulkan.Core.Textures;
using Ez.Numerics;
using Silk.NET.Vulkan;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using VkBlendFactor = Silk.NET.Vulkan.BlendFactor;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal static class VkHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Extent3D ToVk(this Size3 size) =>
            new(size.Width, size.Height, size.Depth);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Offset3D ToVk(this Point3 size) =>
            new(size.X, size.Y, size.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Format ToVk(this PixelFormat f) =>
            f switch
            {
                #region packed
                PixelFormat.R4G4B4A4UNorm => Format.R4G4B4A4UnormPack16,
                PixelFormat.B4G4R4A4UNorm => Format.B4G4R4A4UnormPack16,
                PixelFormat.R5G6B5UNorm => Format.R5G6B5UnormPack16,
                PixelFormat.B5G6R5UNorm => Format.B5G6R5UnormPack16,
                PixelFormat.B5G5R5A1UNorm => Format.B5G5R5A1UnormPack16,
                PixelFormat.A1R5G5B5UNorm => Format.A1R5G5B5UnormPack16,
                PixelFormat.B10G11R11UFloat => Format.B10G11R11UfloatPack32,
                PixelFormat.E5B9G9R9UFloat => Format.E5B9G9R9UfloatPack32,
                #endregion

                #region R8
                PixelFormat.R8UNorm => Format.R8Unorm,
                PixelFormat.R8SNorm => Format.R8SNorm,
                PixelFormat.R8UInt => Format.R8Uint,
                PixelFormat.R8SInt => Format.R8Sint,
                #endregion
                #region R8G8
                PixelFormat.R8G8UNorm => Format.R8G8Unorm,
                PixelFormat.R8G8SNorm => Format.R8G8SNorm,
                PixelFormat.R8G8UInt => Format.R8G8Uint,
                PixelFormat.R8G8SInt => Format.R8G8Sint,
                #endregion
                #region R8G8B8A8
                PixelFormat.R8G8B8A8UNorm => Format.R8G8B8A8Unorm,
                PixelFormat.R8G8B8A8SNorm => Format.R8G8B8A8SNorm,
                PixelFormat.R8G8B8A8UInt => Format.R8G8B8A8Uint,
                PixelFormat.R8G8B8A8SInt => Format.R8G8B8A8Sint,
                #endregion
                #region B8G8R8A8
                PixelFormat.B8G8R8A8UNorm => Format.B8G8R8A8Unorm,
                PixelFormat.B8G8R8A8Srgb => Format.B8G8R8A8Srgb,
                #endregion

                #region A2R10G10B10
                PixelFormat.A2R10G10B10UNorm => Format.A2R10G10B10UnormPack32,
                PixelFormat.A2R10G10B10UInt => Format.A2R10G10B10UintPack32,
                #endregion
                #region A2B10G10R10
                PixelFormat.A2B10G10R10UNorm => Format.A2B10G10R10UnormPack32,
                PixelFormat.A2B10G10R10UInt => Format.A2B10G10R10UintPack32,
                #endregion

                #region R16
                PixelFormat.R16UNorm => Format.R16Unorm,
                PixelFormat.R16SNorm => Format.R16SNorm,
                PixelFormat.R16UInt => Format.R16Uint,
                PixelFormat.R16SInt => Format.R16Sint,
                PixelFormat.R16SFloat => Format.R16Sfloat,
                #endregion
                #region R16G16
                PixelFormat.R16G16UNorm => Format.R16G16Unorm,
                PixelFormat.R16G16SNorm => Format.R16G16SNorm,
                PixelFormat.R16G16UInt => Format.R16G16Uint,
                PixelFormat.R16G16SInt => Format.R16G16Sint,
                PixelFormat.R16G16SFloat => Format.R16G16Sfloat,
                #endregion
                #region R16G16B16A16
                PixelFormat.R16G16B16A16UNorm => Format.R16G16B16A16Unorm,
                PixelFormat.R16G16B16A16SNorm => Format.R16G16B16A16SNorm,
                PixelFormat.R16G16B16A16UInt => Format.R16G16B16A16Uint,
                PixelFormat.R16G16B16A16SInt => Format.R16G16B16A16Sint,
                PixelFormat.R16G16B16A16SFloat => Format.R16G16B16A16Sfloat,
                #endregion

                #region R32
                PixelFormat.R32UInt => Format.R32Uint,
                PixelFormat.R32SInt => Format.R32Sint,
                PixelFormat.R32SFloat => Format.R32Sfloat,
                #endregion
                #region R32G32
                PixelFormat.R32G32UInt => Format.R32G32Uint,
                PixelFormat.R32G32SInt => Format.R32G32Sint,
                PixelFormat.R32G32SFloat => Format.R32G32Sfloat,
                #endregion
                #region R32G32B32
                PixelFormat.R32G32B32UInt => Format.R32G32B32Uint,
                PixelFormat.R32G32B32SInt => Format.R32G32B32Sint,
                PixelFormat.R32G32B32SFloat => Format.R32G32B32Sfloat,
                #endregion
                #region R32G32B32A32
                PixelFormat.R32G32B32A32UInt => Format.R32G32B32A32Uint,
                PixelFormat.R32G32B32A32SInt => Format.R32G32B32A32Sint,
                PixelFormat.R32G32B32A32SFloat => Format.R32G32B32A32Sfloat,
                #endregion

                #region Depth and Stencil
                PixelFormat.D16UNorm => Format.D16Unorm,
                PixelFormat.D24UNormS8UInt => Format.D24UnormS8Uint,
                PixelFormat.D32SFloat => Format.D32Sfloat,
                PixelFormat.D32SFloatS8UInt => Format.D32SfloatS8Uint,
                PixelFormat.S8UInt => Format.S8Uint,
                #endregion

                #region BC
                PixelFormat.BC1RgbaSrgb => Format.BC1RgbaSrgbBlock,
                PixelFormat.BC1RgbaUNorm => Format.BC1RgbaUnormBlock,
                PixelFormat.BC1RgbSrgb => Format.BC1RgbSrgbBlock,
                PixelFormat.BC1RgbUNorm => Format.BC1RgbUnormBlock,

                PixelFormat.BC2Srgb => Format.BC2SrgbBlock,
                PixelFormat.BC2UNorm => Format.BC2UnormBlock,

                PixelFormat.BC3Srgb => Format.BC3SrgbBlock,
                PixelFormat.BC3UNorm => Format.BC3UnormBlock,

                PixelFormat.BC4SNorm => Format.BC4SNormBlock,
                PixelFormat.BC4UNorm => Format.BC4UnormBlock,

                PixelFormat.BC5SNorm => Format.BC5SNormBlock,
                PixelFormat.BC5UNorm => Format.BC5UnormBlock,

                PixelFormat.BC6HSFloat => Format.BC6HSfloatBlock,
                PixelFormat.BC6HUFloat => Format.BC6HUfloatBlock,

                PixelFormat.BC7Srgb => Format.BC7SrgbBlock,
                PixelFormat.BC7UNorm => Format.BC7UnormBlock,
                #endregion
                #region ETC and EAC
                PixelFormat.EacR11SNorm => Format.EacR11SNormBlock,
                PixelFormat.EacR11UNorm => Format.EacR11UnormBlock,

                PixelFormat.EacR11G11SNorm => Format.EacR11G11SNormBlock,
                PixelFormat.EacR11G11UNorm => Format.EacR11G11UnormBlock,

                PixelFormat.Etc2R8G8B8Srgb => Format.Etc2R8G8B8SrgbBlock,
                PixelFormat.Etc2R8G8B8UNorm => Format.Etc2R8G8B8UnormBlock,

                PixelFormat.Etc2R8G8B8A1Srgb => Format.Etc2R8G8B8A1SrgbBlock,
                PixelFormat.Etc2R8G8B8A1UNorm => Format.Etc2R8G8B8A1UnormBlock,

                PixelFormat.Etc2R8G8B8A8Srgb => Format.Etc2R8G8B8A8SrgbBlock,
                PixelFormat.Etc2R8G8B8A8UNorm => Format.Etc2R8G8B8A8UnormBlock,
                #endregion
                #region ASTC
                PixelFormat.Astc10x10Srgb => Format.Astc10x10SrgbBlock,
                PixelFormat.Astc10x10UNorm => Format.Astc10x10UnormBlock,

                PixelFormat.Astc10x5Srgb => Format.Astc10x5SrgbBlock,
                PixelFormat.Astc10x5UNorm => Format.Astc10x5UnormBlock,

                PixelFormat.Astc10x6Srgb => Format.Astc10x6SrgbBlock,
                PixelFormat.Astc10x6UNorm => Format.Astc10x6UnormBlock,

                PixelFormat.Astc10x8Srgb => Format.Astc10x8SrgbBlock,
                PixelFormat.Astc10x8UNorm => Format.Astc10x8UnormBlock,

                PixelFormat.Astc12x10Srgb => Format.Astc12x10SrgbBlock,
                PixelFormat.Astc12x10UNorm => Format.Astc12x10UnormBlock,

                PixelFormat.Astc12x12Srgb => Format.Astc12x12SrgbBlock,
                PixelFormat.Astc12x12UNorm => Format.Astc12x12UnormBlock,

                PixelFormat.Astc4x4Srgb => Format.Astc4x4SrgbBlock,
                PixelFormat.Astc4x4UNorm => Format.Astc4x4UnormBlock,

                PixelFormat.Astc5x4Srgb => Format.Astc5x4SrgbBlock,
                PixelFormat.Astc5x4UNorm => Format.Astc5x4UnormBlock,

                PixelFormat.Astc5x5Srgb => Format.Astc5x5SrgbBlock,
                PixelFormat.Astc5x5UNorm => Format.Astc5x5UnormBlock,

                PixelFormat.Astc6x5Srgb => Format.Astc6x5SrgbBlock,
                PixelFormat.Astc6x5UNorm => Format.Astc6x5UnormBlock,

                PixelFormat.Astc6x6Srgb => Format.Astc6x6SrgbBlock,
                PixelFormat.Astc6x6UNorm => Format.Astc6x6UnormBlock,

                PixelFormat.Astc8x5Srgb => Format.Astc8x5SrgbBlock,
                PixelFormat.Astc8x5UNorm => Format.Astc8x5UnormBlock,

                PixelFormat.Astc8x6Srgb => Format.Astc8x6SrgbBlock,
                PixelFormat.Astc8x6UNorm => Format.Astc8x6UnormBlock,

                PixelFormat.Astc8x8Srgb => Format.Astc8x8SrgbBlock,
                PixelFormat.Astc8x8UNorm => Format.Astc8x8UnormBlock,
                #endregion
                _ => throw new VkException("Unsuported format.")
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ShaderStageFlags ToVk(this ShaderStages stages) =>
            stages switch
            {
                ShaderStages.Compute => ShaderStageFlags.ShaderStageComputeBit,
                ShaderStages.Fragment => ShaderStageFlags.ShaderStageFragmentBit,
                ShaderStages.Geometry => ShaderStageFlags.ShaderStageGeometryBit,
                ShaderStages.TessellationControl => ShaderStageFlags.ShaderStageTessellationControlBit,
                ShaderStages.TessellationEvaluation => ShaderStageFlags.ShaderStageTessellationEvaluationBit,
                ShaderStages.Vertex => ShaderStageFlags.ShaderStageVertexBit,
                _ => throw new VkException($"Unsuported stage {stages}.")
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Silk.NET.Vulkan.VertexInputRate ToVk(this VertexInputRate vir) =>
           vir switch
           {
               VertexInputRate.Instance => Silk.NET.Vulkan.VertexInputRate.Instance,
               VertexInputRate.Vertex => Silk.NET.Vulkan.VertexInputRate.Vertex,
               _ => throw new VkException()
           };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Format ToVk(this VertexElementType format) =>
            format switch
            {
                VertexElementType.Byte2 => Format.R8G8Uint,
                VertexElementType.Byte2Norm => Format.R8G8Unorm,
                VertexElementType.SByte2 => Format.R8G8Sint,
                VertexElementType.SByte2Norm => Format.R8G8SNorm,

                VertexElementType.Byte4 => Format.R8G8B8A8Uint,
                VertexElementType.Byte4Norm => Format.R8G8B8A8Unorm,
                VertexElementType.SByte4 => Format.R8G8B8A8Sint,
                VertexElementType.SByte4Norm => Format.R8G8B8A8SNorm,

                VertexElementType.UShort1 => Format.R16Uint,
                VertexElementType.UShort1Norm => Format.R16Unorm,
                VertexElementType.Short1 => Format.R16Sint,
                VertexElementType.Short1Norm => Format.R16G16B16A16SNorm,

                VertexElementType.UShort2 => Format.R16G16Uint,
                VertexElementType.UShort2Norm => Format.R16G16Unorm,
                VertexElementType.Short2 => Format.R16G16Sint,
                VertexElementType.Short2Norm => Format.R16G16SNorm,

                VertexElementType.UShort4 => Format.R16G16B16A16Uint,
                VertexElementType.UShort4Norm => Format.R16G16B16A16Unorm,
                VertexElementType.Short4 => Format.R16G16B16A16Sint,
                VertexElementType.Short4Norm => Format.R16G16B16A16SNorm,

                VertexElementType.Int1 => Format.R32Sint,
                VertexElementType.Int2 => Format.R32G32Sint,
                VertexElementType.Int3 => Format.R32G32B32Sint,
                VertexElementType.Int4 => Format.R32G32B32A32Sint,

                VertexElementType.UInt1 => Format.R32Uint,
                VertexElementType.UInt2 => Format.R32G32Uint,
                VertexElementType.UInt3 => Format.R32G32B32Uint,
                VertexElementType.UInt4 => Format.R32G32B32A32Uint,

                VertexElementType.Half1 => Format.R16Sfloat,
                VertexElementType.Half2 => Format.R16G16Sfloat,
                VertexElementType.Half4 => Format.R16G16B16A16Sfloat,

                VertexElementType.Single1 => Format.R32Sfloat,
                VertexElementType.Single2 => Format.R32G32Sfloat,
                VertexElementType.Single3 => Format.R32G32B32Sfloat,
                VertexElementType.Single4 => Format.R32G32B32A32Sfloat,

                _ => throw new VkException($"Unsuported vertex element format '{format}'.")
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Silk.NET.Vulkan.PrimitiveTopology ToVk(this PrimitiveTopology topology) =>
            topology switch
            {
                PrimitiveTopology.LineList => Silk.NET.Vulkan.PrimitiveTopology.LineList,
                PrimitiveTopology.LineStrip => Silk.NET.Vulkan.PrimitiveTopology.LineStrip,
                PrimitiveTopology.PointList => Silk.NET.Vulkan.PrimitiveTopology.PointList,
                PrimitiveTopology.TriangleList => Silk.NET.Vulkan.PrimitiveTopology.TriangleList,
                PrimitiveTopology.TriangleStrip => Silk.NET.Vulkan.PrimitiveTopology.TriangleStrip,

                _ => throw new VkException($"Unsuported primitive topology '{topology}'.")
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Silk.NET.Vulkan.PolygonMode ToVk(this PolygonMode mode) =>
            mode switch
            {
                PolygonMode.Fill => Silk.NET.Vulkan.PolygonMode.Fill,
                PolygonMode.Line => Silk.NET.Vulkan.PolygonMode.Line,
                PolygonMode.Point => Silk.NET.Vulkan.PolygonMode.Point,

                _ => throw new VkException($"Unsuported polygon mode '{mode}'.")
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CullModeFlags ToVk(this CullMode mode) =>
            mode switch
            {
                CullMode.None => CullModeFlags.CullModeNone,
                CullMode.Back => CullModeFlags.CullModeBackBit,
                CullMode.Front => CullModeFlags.CullModeFrontBit,
                CullMode.FrontAndBack => CullModeFlags.CullModeFrontAndBack,

                _ => throw new VkException($"Unsuported cull mode '{mode}'.")
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Silk.NET.Vulkan.FrontFace ToVk(this FrontFace frontFace) =>
            frontFace switch
            {
                FrontFace.Anticlockwise => Silk.NET.Vulkan.FrontFace.CounterClockwise,
                FrontFace.Clockwise => Silk.NET.Vulkan.FrontFace.Clockwise,

                _ => throw new VkException($"Unsuported front face '{frontFace}'.")
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SampleCountFlags ToVk(this SampleCount sampleCount) =>
            sampleCount switch
            {
                SampleCount.Count1 => SampleCountFlags.SampleCount1Bit,
                SampleCount.Count2 => SampleCountFlags.SampleCount2Bit,
                SampleCount.Count4 => SampleCountFlags.SampleCount4Bit,
                SampleCount.Count8 => SampleCountFlags.SampleCount8Bit,
                SampleCount.Count16 => SampleCountFlags.SampleCount16Bit,
                SampleCount.Count32 => SampleCountFlags.SampleCount32Bit,

                _ => throw new VkException($"Unsuported sample count '{sampleCount}'")
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LogicOp ToVk(this LogicOperation op) =>
            op switch
            {
                LogicOperation.And => LogicOp.And,
                LogicOperation.AndInverted => LogicOp.AndInverted,
                LogicOperation.AndReverse => LogicOp.AndReverse,
                LogicOperation.Clear => LogicOp.Clear,
                LogicOperation.Copy => LogicOp.Copy,
                LogicOperation.CopyInverted => LogicOp.CopyInverted,
                LogicOperation.Equivalent => LogicOp.Equivalent,
                LogicOperation.Invert => LogicOp.Invert,
                LogicOperation.Nand => LogicOp.Nand,
                LogicOperation.NoOp => LogicOp.NoOp,
                LogicOperation.Nor => LogicOp.Nor,
                LogicOperation.Or => LogicOp.Or,
                LogicOperation.OrInverted => LogicOp.OrInverted,
                LogicOperation.OrReverse => LogicOp.OrReverse,
                LogicOperation.Set => LogicOp.Set,
                LogicOperation.Xor => LogicOp.Xor,

                _ => throw new VkException($"Unsuported logic operation '{op}'.")
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BlendOp ToVk(this BlendOperation op) =>
            op switch
            {
                BlendOperation.Add => BlendOp.Add,
                BlendOperation.Max => BlendOp.Max,
                BlendOperation.Min => BlendOp.Min,
                BlendOperation.ReverseSubtract => BlendOp.ReverseSubtract,
                BlendOperation.Subtract => BlendOp.Subtract,

                _ => throw new VkException($"Unsuported blend operation '{op}'.")
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ColorComponentFlags ToVk(this ColorComponents components)
        {
            ColorComponentFlags flags = default;

            if ((components & ColorComponents.R) != default)
                flags |= ColorComponentFlags.ColorComponentRBit;

            if ((components & ColorComponents.G) != default)
                flags |= ColorComponentFlags.ColorComponentGBit;

            if ((components & ColorComponents.B) != default)
                flags |= ColorComponentFlags.ColorComponentBBit;

            if ((components & ColorComponents.A) != default)
                flags |= ColorComponentFlags.ColorComponentABit;

            return flags;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VkBlendFactor ToVk(this BlendFactor factor) =>
            factor switch
            {
                BlendFactor.ConstantAlpha => VkBlendFactor.ConstantAlpha,
                BlendFactor.ConstantColor => VkBlendFactor.ConstantColor,
                BlendFactor.DestinationAlpha => VkBlendFactor.DstAlpha,
                BlendFactor.DestinationColor => VkBlendFactor.DstColor,
                BlendFactor.One => VkBlendFactor.One,
                BlendFactor.OneMinusConstantAlpha => VkBlendFactor.OneMinusConstantAlpha,
                BlendFactor.OneMinusConstantColor => VkBlendFactor.OneMinusConstantColor,
                BlendFactor.OneMinusDestinationAlpha => VkBlendFactor.OneMinusDstAlpha,
                BlendFactor.OneMinusDestinationColor => VkBlendFactor.OneMinusDstColor,
                BlendFactor.OneMinusSourceAlpha => VkBlendFactor.OneMinusSrcAlpha,
                BlendFactor.OneMinusSourceColor => VkBlendFactor.OneMinusSrcColor,
                BlendFactor.SourceAlpha => VkBlendFactor.SrcAlpha,
                BlendFactor.SourceColor => VkBlendFactor.SrcColor,
                BlendFactor.Zero => VkBlendFactor.Zero,

                _ => throw new VkException($"Unsuported blend factor '{factor}'.")
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CompareOp ToVk(this CompareOperation operation) =>
            operation switch
            {
                CompareOperation.Always => CompareOp.Always,
                CompareOperation.Equal => CompareOp.Equal,
                CompareOperation.Greater => CompareOp.Greater,
                CompareOperation.GreaterEqual => CompareOp.GreaterOrEqual,
                CompareOperation.Less => CompareOp.Less,
                CompareOperation.LessEqual => CompareOp.LessOrEqual,
                CompareOperation.Never => CompareOp.Never,
                CompareOperation.NotEqual => CompareOp.NotEqual,

                _ => throw new VkException($"Unsuported compare operation '{operation}'.")
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StencilOp ToVk(this StencilOperation operation) =>
            operation switch
            {
                StencilOperation.DecrementAndClamp => StencilOp.DecrementAndClamp,
                StencilOperation.DecrementAndWrap => StencilOp.DecrementAndWrap,
                StencilOperation.IncrementAndClamp => StencilOp.IncrementAndClamp,
                StencilOperation.IncrementAndWrap => StencilOp.IncrementAndWrap,
                StencilOperation.Invert => StencilOp.Invert,
                StencilOperation.Keep => StencilOp.Keep,
                StencilOperation.Replace => StencilOp.Replace,
                StencilOperation.Zero => StencilOp.Zero,

                _ => throw new VkException($"Unsuported stencil operation '{operation}'.")
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StencilOpState ToVk(this StencilOperationState state) =>
            new()
            {
                CompareOp = state.CompareOperation.ToVk(),
                DepthFailOp = state.DepthFailOperation.ToVk(),
                FailOp = state.FailOperation.ToVk(),
                PassOp = state.PassOperation.ToVk(),
                CompareMask = uint.MaxValue,
                WriteMask = uint.MaxValue,
                Reference = uint.MaxValue,
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AttachmentLoadOp ToVk(this AttachmentLoadOperation operation) =>
            operation switch
            {
                AttachmentLoadOperation.Clear => AttachmentLoadOp.Clear,
                AttachmentLoadOperation.DontCare => AttachmentLoadOp.DontCare,
                AttachmentLoadOperation.Load => AttachmentLoadOp.Load,
                _ => throw new System.NotImplementedException(),
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AttachmentStoreOp ToVk(this AttachmentStoreOperation operation) =>
            operation switch
            {
                AttachmentStoreOperation.Store => AttachmentStoreOp.Store,
                AttachmentStoreOperation.DontCare => AttachmentStoreOp.DontCare,
                _ => throw new System.NotImplementedException(),
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageType ToVk(this TextureType type) =>
            type switch
            {
                TextureType.Texture1D => ImageType.ImageType1D,
                TextureType.Texture2D => ImageType.ImageType2D,
                TextureType.Texture3D => ImageType.ImageType3D,
                _ => throw new System.NotImplementedException()
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageTiling ToVk(this TextureTiling tiling) =>
            tiling switch
            {
                TextureTiling.Linear => ImageTiling.Linear,
                TextureTiling.Optimal => ImageTiling.Optimal,
                _ => throw new System.NotImplementedException()
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageUsageFlags ToVk(this TextureUsage usage)
        {
            var flags = default(ImageUsageFlags);

            if ((usage & TextureUsage.ColorAttachment) != 0)
                flags |= ImageUsageFlags.ImageUsageColorAttachmentBit;

            if ((usage & TextureUsage.DepthStencilAttachment) != 0)
                flags |= ImageUsageFlags.ImageUsageDepthStencilAttachmentBit;

            if ((usage & TextureUsage.Sampled) != 0)
                flags |= ImageUsageFlags.ImageUsageSampledBit;

            if ((usage & TextureUsage.Storage) != 0)
                flags |= ImageUsageFlags.ImageUsageStorageBit;

            if ((usage & TextureUsage.TransferDestination) != 0)
                flags |= ImageUsageFlags.ImageUsageTransferDstBit;

            if ((usage & TextureUsage.TransferSource) != 0)
                flags |= ImageUsageFlags.ImageUsageTransferSrcBit;

            return flags;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BufferUsageFlags ToVk(this BufferUsage usage)
        {
            var result = default(BufferUsageFlags);

            if ((usage & BufferUsage.IndexBuffer) != 0)
                result |= BufferUsageFlags.BufferUsageIndexBufferBit;

            if ((usage & BufferUsage.IndirectBuffer) != 0)
                result |= BufferUsageFlags.BufferUsageIndirectBufferBit;

            if ((usage & BufferUsage.StorageBuffer) != 0)
                result |= BufferUsageFlags.BufferUsageStorageBufferBit;

            if ((usage & BufferUsage.TransferDestination) != 0)
                result |= BufferUsageFlags.BufferUsageTransferDstBit;

            if ((usage & BufferUsage.TransferSource) != 0)
                result |= BufferUsageFlags.BufferUsageTransferSrcBit;

            if ((usage & BufferUsage.UniformBuffer) != 0)
                result |= BufferUsageFlags.BufferUsageUniformBufferBit;

            if ((usage & BufferUsage.VertexBuffer) != 0)
                result |= BufferUsageFlags.BufferUsageVertexBufferBit;

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Silk.NET.Vulkan.ClearValue ToVk(this ClearValue clearValue, in PixelFormat format)
        {
            if ((format & (PixelFormat.Depth | PixelFormat.Stencil)) != default)
                return new()
                {
                    DepthStencil = clearValue.DepthStencil.ToVk()
                };
            else
                return new()
                {
                    Color = clearValue.Color.ToVk(format)
                };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Silk.NET.Vulkan.ClearColorValue ToVk(this ClearColorValue clearColorValue, in PixelFormat format)
        {
            if ((format & PixelFormat.PixelValueMask) == PixelFormat.SInt)
                return new()
                {
                    Int32_0 = clearColorValue.IntValue.R,
                    Int32_1 = clearColorValue.IntValue.G,
                    Int32_2 = clearColorValue.IntValue.B,
                    Int32_3 = clearColorValue.IntValue.A,
                };
            else if ((format & PixelFormat.PixelValueMask) == PixelFormat.UInt)
                return new()
                {
                    Uint32_0 = clearColorValue.UIntValue.R,
                    Uint32_1 = clearColorValue.UIntValue.G,
                    Uint32_2 = clearColorValue.UIntValue.B,
                    Uint32_3 = clearColorValue.UIntValue.A,
                };
            else
                return new()
                {
                    Float32_0 = clearColorValue.SingleValue.R,
                    Float32_1 = clearColorValue.SingleValue.G,
                    Float32_2 = clearColorValue.SingleValue.B,
                    Float32_3 = clearColorValue.SingleValue.A,
                };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Silk.NET.Vulkan.ClearDepthStencilValue ToVk(this ClearDepthStencilValue clearDepthStencilValue) =>
            new()
            {
                Depth = clearDepthStencilValue.Depth,
                Stencil = clearDepthStencilValue.Stencil,
            };

        public static Silk.NET.Vulkan.Viewport ToVk(this Viewport viewport) =>
            new()
            {
                X = viewport.X,
                Y = viewport.Y,
                Width = viewport.Width,
                Height = viewport.Height,
                MinDepth = viewport.MinDepth,
                MaxDepth = viewport.MaxDepth,
            };

        public static Rect2D ToVk(this Rectangle rectangle) =>
            new()
            {
                Extent = new((uint)rectangle.Width, (uint)rectangle.Height),
                Offset = new(rectangle.X, rectangle.Y),
            };

        public static Silk.NET.Vulkan.IndexType ToVk(this IndexType indexType) =>
            indexType switch
            {
                IndexType.UInt => Silk.NET.Vulkan.IndexType.Uint32,
                IndexType.UShort => Silk.NET.Vulkan.IndexType.Uint16,
                _ => throw new System.NotImplementedException()
            };

        public static (Filter Mag, Filter Min) ToVk(this SamplerFilter filter)
        {
            (Filter min, Filter mag) = (default, default);

            if (filter.HasFlag(SamplerFilter.MinNearest))
                min = Filter.Nearest;
            else if (filter.HasFlag(SamplerFilter.MinLinear))
                min = Filter.Linear;


            if (filter.HasFlag(SamplerFilter.MagNearest))
                mag = Filter.Nearest;
            else if (filter.HasFlag(SamplerFilter.MagLinear))
                mag = Filter.Linear;

            return (mag, min);
        }

        public static SamplerAddressMode ToVk(this EdgeSample edgeSample) =>
            edgeSample switch
            {
                EdgeSample.Repeat => SamplerAddressMode.Repeat,
                EdgeSample.MirrorRepeat => SamplerAddressMode.MirroredRepeat,
                EdgeSample.ClampToEdge => SamplerAddressMode.ClampToEdge,
                _ => throw new VkException($"Unsupported {nameof(EdgeSample)} '{edgeSample}'.")
            };

        public static ImageSubresourceLayers ToVk(this TextureSubresourceLayers tsl) =>
            new()
            {
                AspectMask = ImageAspectFlags.ImageAspectColorBit,
                BaseArrayLayer = tsl.BaseArrayLayer,
                LayerCount = tsl.LayerCount,
                MipLevel = tsl.MipmapLevel
            };

        public static Silk.NET.Vulkan.ClearAttachment ToVk(this in ClearAttachment clearAttachment, in FramebufferAttachment framebufferAttachment)
        {
            return new()
            {
                AspectMask = framebufferAttachment.GetImageAspectFlags(),
                ColorAttachment = clearAttachment.ColorAttachment,
                ClearValue = clearAttachment.ClearValue.ToVk(framebufferAttachment.Target.Format)
            };
        }

        public static ImageAspectFlags GetImageAspectFlags(this in FramebufferAttachment attachment)
        {
            var format = attachment.Target.Format;

            var result = default(ImageAspectFlags);
            var hasDepth = format.HasFlag(PixelFormat.Depth);
            var hasStencil = format.HasFlag(PixelFormat.Stencil);

            if (hasDepth)
                result |= ImageAspectFlags.ImageAspectDepthBit;
            if (hasStencil)
                result |= ImageAspectFlags.ImageAspectStencilBit;
            if (!(hasDepth || hasStencil))
                result = ImageAspectFlags.ImageAspectColorBit;
            return result;
        }

        public static ClearRect ToVk(this in ClearRectangle rect) => new()
        {
            BaseArrayLayer = rect.BaseArrayLayer,
            LayerCount = rect.ArrayLayerCount,
            Rect = rect.Rectangle.ToVk(),
        };

        public static AccessFlags GetSrcAccessMask(this ImageLayout older, ImageLayout newer) => older switch
        {
            ImageLayout.Undefined or ImageLayout.Preinitialized => AccessFlags.AccessNoneKhr,

            ImageLayout.ShaderReadOnlyOptimal => AccessFlags.AccessShaderReadBit,

            ImageLayout.General => newer switch
            {
                ImageLayout.ShaderReadOnlyOptimal => AccessFlags.AccessTransferReadBit,
                ImageLayout.TransferSrcOptimal => AccessFlags.AccessShaderWriteBit,
                _ => throw new NotImplementedException()
            },

            ImageLayout.TransferSrcOptimal => AccessFlags.AccessTransferReadBit,
            ImageLayout.TransferDstOptimal => AccessFlags.AccessTransferWriteBit,
            ImageLayout.ColorAttachmentOptimal => AccessFlags.AccessColorAttachmentWriteBit,
            ImageLayout.DepthStencilAttachmentOptimal => AccessFlags.AccessDepthStencilAttachmentWriteBit,
            _ => throw new NotImplementedException(),
        };

        public static AccessFlags GetDstAccessMask(this ImageLayout newer) => newer switch
        {
            ImageLayout.TransferDstOptimal => AccessFlags.AccessTransferWriteBit,
            ImageLayout.TransferSrcOptimal => AccessFlags.AccessTransferReadBit,
            ImageLayout.General => AccessFlags.AccessShaderReadBit,
            ImageLayout.ShaderReadOnlyOptimal => AccessFlags.AccessShaderReadBit,
            ImageLayout.PresentSrcKhr => AccessFlags.AccessMemoryReadBit,
            ImageLayout.ColorAttachmentOptimal => AccessFlags.AccessColorAttachmentWriteBit,
            ImageLayout.DepthStencilAttachmentOptimal => AccessFlags.AccessDepthStencilAttachmentWriteBit,
            _ => throw new NotImplementedException(),
        };

        public static PipelineStageFlags GetSrcStageFlags(this ImageLayout older, ImageLayout newer) => older switch
        {
            ImageLayout.Undefined or
                ImageLayout.Preinitialized => PipelineStageFlags.PipelineStageTopOfPipeBit,
            ImageLayout.ShaderReadOnlyOptimal => PipelineStageFlags.PipelineStageFragmentShaderBit,
            ImageLayout.General => newer switch
            {
                ImageLayout.ShaderReadOnlyOptimal => PipelineStageFlags.PipelineStageTransferBit,
                ImageLayout.TransferSrcOptimal => PipelineStageFlags.PipelineStageComputeShaderBit,
                _ => throw new NotImplementedException(),
            },
            ImageLayout.TransferSrcOptimal or
                ImageLayout.TransferDstOptimal => PipelineStageFlags.PipelineStageTransferBit,
            ImageLayout.ColorAttachmentOptimal => PipelineStageFlags.PipelineStageColorAttachmentOutputBit,
            ImageLayout.DepthStencilAttachmentOptimal => PipelineStageFlags.PipelineStageLateFragmentTestsBit,
            _ => throw new NotImplementedException(),
        };

        public static PipelineStageFlags GetDstStageFlags(this ImageLayout newer) => newer switch
        {
            ImageLayout.TransferSrcOptimal or
                ImageLayout.TransferDstOptimal => PipelineStageFlags.PipelineStageTransferBit,

            ImageLayout.General => PipelineStageFlags.PipelineStageComputeShaderBit,
            ImageLayout.ShaderReadOnlyOptimal => PipelineStageFlags.PipelineStageFragmentShaderBit,
            ImageLayout.PresentSrcKhr => PipelineStageFlags.PipelineStageBottomOfPipeBit,
            ImageLayout.ColorAttachmentOptimal => PipelineStageFlags.PipelineStageColorAttachmentOutputBit,
            ImageLayout.DepthStencilAttachmentOptimal => PipelineStageFlags.PipelineStageLateFragmentTestsBit,
            _ => throw new NotImplementedException(),
        };

        public static ImageSubresourceRange ToVk(this TextureSubresourceRange range, Texture texture) => new()
        {
            AspectMask = texture.ImageAspect,
            BaseArrayLayer = range.BaseArrayLayer,
            BaseMipLevel = range.BaseMipmapLevel,
            LayerCount = range.ArrayLayerCount,
            LevelCount = range.MipmapLevelCount,
        };

        public static Texture RevoluteSubresources(this BaseTexture baseTexture, ReadOnlySpan<TextureSubresourceRange> source, Span<ImageSubresourceRange> destination)
        {
            if (baseTexture is null)
                throw new ArgumentNullException(nameof(baseTexture));

            if (source.Length > destination.Length)
                throw new ArgumentOutOfRangeException(nameof(destination));

            Texture texture = null;
            var realBaseArrayLayer = 0u;
            var realBaseMipmapLevel = 0u;
            while (texture == null)
            {
                if (texture is null)
                    throw new InvalidOperationException();
                else if (baseTexture is Texture tTemp)
                    texture = tTemp;
                else if (baseTexture is TextureView vTemp)
                {
                    baseTexture = (BaseTexture)vTemp.Texture;
                    realBaseArrayLayer += vTemp.Range.BaseArrayLayer;
                    realBaseMipmapLevel += vTemp.Range.BaseMipmapLevel;
                }
            }

            for(var i = 0; i < source.Length; i++)
            {
                destination[i] = source[i].ToVk(texture);
                destination[i].BaseArrayLayer += realBaseArrayLayer;
                destination[i].BaseMipLevel += realBaseMipmapLevel;
            }

            return texture;
        }
    }
}
