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
    /// Describe the properties of the rasterizer in a <see cref="IPipeline"/>.
    /// </summary>
    public struct RasterizationState : IEquatable<RasterizationState>
    {
        /// <summary>
        /// Describes whether the depth clamp is enable.
        /// </summary>
        public bool DepthClampEnabled { get; set; }

        /// <summary>
        /// Describes whether the rasterizer discard is enable.
        /// </summary>
        public bool RasterizerDiscardEnabled { get; set; }

        /// <summary>
        /// Describes whether the scissor test is enabled.
        /// </summary>
        public bool ScissorTestEnabled { get; set; }

        /// <summary>
        /// Describes how to fill the polygon.
        /// </summary>
        public PolygonMode PolygonMode { get; set; }

        /// <summary>
        /// Describes face that will be discarded.
        /// </summary>
        public CullMode CullMode { get; set; }

        /// <summary>
        /// Describes whether the direction of the vertices of a frontal face is clockwise or anticlockwise.
        /// </summary>
        public FrontFace FrontFace { get; set; }

        /// <summary>
        /// Describes whether the depth bias is enabled.
        /// </summary>
        public bool DepthBiasEnabled { get; set; }
       
        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<RasterizationState>.Combine(CullMode, PolygonMode, FrontFace, DepthClampEnabled, ScissorTestEnabled);

        /// <inheritdoc/>
        public bool Equals(RasterizationState other) =>
            CullMode == other.CullMode &&
            PolygonMode == other.PolygonMode &&
            FrontFace == other.FrontFace &&
            DepthClampEnabled == other.DepthClampEnabled &&
            ScissorTestEnabled == other.ScissorTestEnabled;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is RasterizationState rs && Equals(rs);

        /// <summary>
        /// The default rasterization state in a render pass.
        /// </summary>
        public static readonly RasterizationState Default = new RasterizationState
        {
            CullMode = CullMode.None,
            PolygonMode = PolygonMode.Fill,
            FrontFace = FrontFace.Clockwise,
            DepthClampEnabled = false,
            ScissorTestEnabled = false,
            DepthBiasEnabled = false,
            RasterizerDiscardEnabled = false,
        };

        /// <summary>
        /// Compare two <see cref="RasterizationState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(RasterizationState left, RasterizationState right) => left.Equals(right);


        /// <summary>
        /// Compare two <see cref="RasterizationState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(RasterizationState left, RasterizationState right) =>
            !(left == right);
    }
}
