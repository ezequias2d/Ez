// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Numerics;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes the depth and stencil test.
    /// </summary>
    public struct DepthStencilState : IEquatable<DepthStencilState>
    {
        /// <summary>
        /// Defines whether the depth test is enabled.
        /// </summary>
        public bool DepthTestEnabled;

        /// <summary>
        /// Defines whether writing to the depth buffer is enabled.
        /// </summary>
        public bool DepthWriteEnabled;

        /// <summary>
        /// The <see cref="CompareOperation"/> used in depth test.
        /// </summary>
        public CompareOperation DepthComparison;

        /// <summary>
        /// Defines whether depth bounds testing is enabled.
        /// </summary>
        public bool DepthBoundsTestEnabled;

        /// <summary>
        /// Defines whether the stencil test is enabled.
        /// </summary>
        public bool StencilTestEnabled;

        /// <summary>
        /// Describes the stencil tests in front faces.
        /// </summary>
        public StencilOperationState StencilFront;
        
        /// <summary>
        /// Describes the stencil tests in back faces.
        /// </summary>
        public StencilOperationState StencilBack;

        /// <summary>
        /// The stencil mask used in reading.
        /// </summary>
        public byte StencilReadMask;

        /// <summary>
        /// The stencil mask used for writing.
        /// </summary>
        public byte StencilWriteMask;

        /// <summary>
        /// The reference value used in <see cref="StencilOperation.Replace"/> operation.
        /// </summary>
        public uint StencilReference;

        /// <summary>
        /// Construct a new <see cref="DepthStencilState"/>.
        /// </summary>
        /// <param name="depthTestEnabled">Defines whether the depth test is enabled.</param>
        /// <param name="depthWriteEnabled">Defines whether writing to the depth buffer is enabled.</param>
        /// <param name="depthComparasion">The <see cref="CompareOperation"/> used in depth test.</param>
        /// <param name="depthBoundsTestEnabled">Defines whether depth bounds testing is enabled.</param>
        /// <param name="stencilTestEnabled">Defines whether the stencil test is enabled.</param>
        /// <param name="stencilFront">Describes the stencil tests in front faces.</param>
        /// <param name="stencilBack">Describes the stencil tests in back faces.</param>
        /// <param name="stencilReadMask">The stencil mask used in reading.</param>
        /// <param name="stencilWriteMask">The stencil mask used for writing.</param>
        /// <param name="stencilReference">The reference value used in <see cref="StencilOperation.Replace"/> operation.</param>
        public DepthStencilState(
            bool depthTestEnabled,
            bool depthWriteEnabled,
            CompareOperation depthComparasion,
            bool depthBoundsTestEnabled,
            bool stencilTestEnabled,
            StencilOperationState stencilFront,
            StencilOperationState stencilBack,
            byte stencilReadMask,
            byte stencilWriteMask,
            uint stencilReference)
        {
            DepthTestEnabled = depthTestEnabled;
            DepthWriteEnabled = depthWriteEnabled;
            DepthComparison = depthComparasion;
            DepthBoundsTestEnabled = depthBoundsTestEnabled;

            StencilTestEnabled = stencilTestEnabled;
            StencilFront = stencilFront;
            StencilBack = stencilBack;
            StencilReadMask = stencilReadMask;
            StencilWriteMask = stencilWriteMask;
            StencilReference = stencilReference;
        }

        /// <summary>
        /// Constructor of a new <see cref="DepthStencilState"/>.
        /// This only describes the depth test, stencil test is disabled.
        /// </summary>
        /// <param name="depthTestEnabled">Defines whether the depth test is enabled.</param>
        /// <param name="depthWriteEnabled">Defines whether writing to the depth buffer is enabled.</param>
        /// <param name="depthComparasion">The <see cref="CompareOperation"/> used in depth test.</param>
        public DepthStencilState(bool depthTestEnabled, bool depthWriteEnabled, CompareOperation depthComparasion)
        {
            DepthTestEnabled = depthTestEnabled;
            DepthWriteEnabled = depthWriteEnabled;
            DepthComparison = depthComparasion;

            DepthBoundsTestEnabled = false;
            StencilTestEnabled = false;
            StencilFront = default;
            StencilBack = default;
            StencilReadMask = 0x00;
            StencilWriteMask = 0x00;
            StencilReference = 0;
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<DepthStencilState>.Combine(DepthTestEnabled, DepthWriteEnabled, DepthComparison, StencilTestEnabled, StencilFront, StencilBack, StencilReadMask, StencilWriteMask, StencilReference);


        /// <inheritdoc/>
        public bool Equals(DepthStencilState other) =>
            GetHashCode() == other.GetHashCode() &&
                DepthTestEnabled == other.DepthTestEnabled &&
                DepthWriteEnabled == other.DepthWriteEnabled &&
                DepthComparison == other.DepthComparison &&
                StencilTestEnabled == other.StencilTestEnabled &&
                StencilFront.Equals(other.StencilFront) &&
                StencilBack.Equals(other.StencilBack) &&
                StencilReadMask == other.StencilReadMask &&
                StencilWriteMask == other.StencilWriteMask &&
                StencilReference == other.StencilReference;

        /// <summary>
        /// The default depth stencil state in a render pass.
        /// </summary>
        public static readonly DepthStencilState Default = new()
        {
            DepthTestEnabled = false,
            DepthWriteEnabled = true,
            DepthComparison = CompareOperation.LessEqual,
            DepthBoundsTestEnabled = false,
            StencilTestEnabled = false,
            StencilFront = new() 
            {
                FailOperation = StencilOperation.Replace,
                PassOperation = StencilOperation.Replace,
                DepthFailOperation = StencilOperation.Replace,
                CompareOperation = CompareOperation.Never,
            },
            StencilBack = new() 
            {
                FailOperation = StencilOperation.Replace,
                PassOperation = StencilOperation.Replace,
                DepthFailOperation = StencilOperation.Replace,
                CompareOperation = CompareOperation.Never,
            },
        };

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is DepthStencilState dss && Equals(dss);

        /// <summary>
        /// Compare two <see cref="DepthStencilState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(DepthStencilState left, DepthStencilState right) => left.Equals(right);


        /// <summary>
        /// Compare two <see cref="DepthStencilState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(DepthStencilState left, DepthStencilState right) =>
            !(left == right);
    }
}
