// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Features supported by a given <see cref="IDevice"/>.
    /// </summary>
    public class PhysicalDeviceFeatures
    {
        /// <summary>
        /// Specifies whether geometry shaders are supported. 
        /// If this feature is not enabled, the <see cref="ShaderStages.Geometry"/> value must not be used.
        /// This also specifies whether shader can declare the Geometry capability.
        /// </summary>
        public bool GeometryShader { get; }

        /// <summary>
        /// Specifies whether tessellation control and evaluation shaders are supported. 
        /// If this feature is not enabled, the <see cref="ShaderStages.TessellationControl"/> and
        /// <see cref="ShaderStages.TessellationEvaluation"/>  values must not be used.
        /// This also specifies whether shader can declare the Tessellation capability.
        /// </summary>
        public bool TessellationShader { get; }

        /// <summary>
        /// Specifies whether more than one viewport is supported. If this feature is not enabled:
        /// <list type="bullet">
        /// <item>The viewportCount parameter of the <see cref="ICommandBuffer.SetViewportCount(uint)"/> command must be set to 1.</item>
        /// <item>The index parameter to the <see cref="ICommandBuffer.SetViewport(int, in Viewport)"/> command must be set to 0.</item>
        /// <item>The index parameter to the <see cref="ICommandBuffer.SetScissor(int, in System.Drawing.Rectangle)"/> command must be set to 0.</item>
        /// </list>
        /// </summary>
        public bool MultiViewport { get; }

        /// <summary>
        /// Specifies whether indirect drawing calls support the firstInstance parameter. 
        /// If this feature is not enabled, the firstInstance member of all <see cref="ICommandBuffer.DrawIndirect(IBuffer, uint, uint, uint)"/> and
        /// <see cref="ICommandBuffer.DrawIndexedIndirect(IBuffer, uint, uint, uint)"/> commands must be 0.
        /// </summary>
        public bool DrawIndirectFirstInstance { get; }

        /// <summary>
        /// Specifies whether <see cref="PolygonMode.Point"/> and <see cref="PolygonMode.Line"/> fill modes are supported.
        /// If this feature is not enabled, the <see cref="PolygonMode.Point"/> and <see cref="PolygonMode.Line"/> values must not be used.
        /// Indicates whether <see cref="PolygonMode.Line"/> is supported.
        /// </summary>
        public bool FillModeNonSolid { get; }

        /// <summary>
        /// Specifies whether anisotropic filtering is supported. If this feature is not enabled, 
        /// the <see cref="SamplerFilter.Anisotropic"/> value must not be used in <see cref="SamplerCreateInfo"/>.
        /// </summary>
        public bool SamplerAnisotropy { get; }

        /// <summary>
        /// Specifies whether depth clamping is supported. 
        /// If this feature is not enabled, the <see cref="RasterizationState.DepthClampEnabled"/> value
        /// must be set to <see langword="false"/>. Otherwise, setting <see cref="RasterizationState.DepthClampEnabled"/> to
        /// <see langword="true"/> will enable depth clamping.
        /// </summary>
        public bool DepthClamp { get; }

        /// <summary>
        /// Specifies whether the <see cref="ColorBlendAttachmentState"/> settings are controlled independently per-attachment. 
        /// If this feature is not enabled, the <see cref="ColorBlendAttachmentState"/> settings for all color attachments must be identical. 
        /// Otherwise, a different <see cref="ColorBlendAttachmentState"/> can be provided for each bound color attachment.
        /// </summary>
        public bool IndependentBlend { get; }

        /// <summary>
        /// Indicates whether <see cref="ICommandBuffer"/> instances created with this device support the
        /// <see cref="ICommandBuffer.DebugMarkerBegin(string, ColorSingle)"/>, <see cref="ICommandBuffer.DebugMarkerEnd"/>, and
        /// <see cref="ICommandBuffer.DebugMessageInsert(string)"/> methods. If not, these methods will have no effect.
        /// </summary>
        public bool DebugUtils { get; }

        /// <summary>
        /// Specifies whether 64-bit floats (doubles) are supported in shader code. If this feature is not enabled, 64-bit 
        /// floating-point types must not be used in shader code. 
        /// This also specifies whether shader modules can declare the Float64 capability. Declaring and using 64-bit floats
        /// is enabled for all storage classes that SPIR-V allows with the Float64 capability.
        /// </summary>
        public bool ShaderFloat64 { get; }

        /// <summary>
        /// Creates a new instance of <see cref="PhysicalDeviceFeatures"/> class.
        /// </summary>
        /// <param name="geometryShader"></param>
        /// <param name="tessellationShaders"></param>
        /// <param name="multiViewport"></param>
        /// <param name="drawIndirectFirstInstance"></param>
        /// <param name="fillModeNoSolid"></param>
        /// <param name="samplerAnisotropy"></param>
        /// <param name="depthClamp"></param>
        /// <param name="independentBlend"></param>
        /// <param name="debugUtils"></param>
        /// <param name="shaderFloat64"></param>
        public PhysicalDeviceFeatures(
            bool geometryShader,
            bool tessellationShaders,
            bool multiViewport,
            bool drawIndirectFirstInstance,
            bool fillModeNoSolid,
            bool samplerAnisotropy,
            bool depthClamp,
            bool independentBlend,
            bool debugUtils,
            bool shaderFloat64)
        {
            GeometryShader = geometryShader;
            TessellationShader = tessellationShaders;
            MultiViewport = multiViewport;
            DrawIndirectFirstInstance = drawIndirectFirstInstance;
            FillModeNonSolid = fillModeNoSolid;
            SamplerAnisotropy = samplerAnisotropy;
            DepthClamp = depthClamp;
            IndependentBlend = independentBlend;
            DebugUtils = debugUtils;
            ShaderFloat64 = shaderFloat64;
        }
    }
}
