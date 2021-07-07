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
    /// Describe the blend attachment state in a color-target of <see cref="IPipeline"/> and <see cref="IFramebuffer"/>.
    /// </summary>
    public struct ColorBlendAttachmentState : IEquatable<ColorBlendAttachmentState>
    {
        /// <summary>
        /// Defines whether the blend is enabled for the color attachment.
        /// </summary>
        public bool BlendEnabled { get; set; }

        /// <summary>
        /// The factor of source color components in blend function.
        /// </summary>
        public BlendFactor SourceColorBlendFactor { get; set; }

        /// <summary>
        /// The factor of destiantion color components in blend function.
        /// </summary>
        public BlendFactor DestinationColorBlendFactor { get; set; }

        /// <summary>
        /// The function used to combine the source and destination colors components.
        /// </summary>
        public BlendOperation ColorBlendOperation { get; set; }

        /// <summary>
        /// The factor of source alpha component in blend function. 
        /// </summary>
        public BlendFactor SourceAlphaBlendFactor { get; set; }

        /// <summary>
        /// The factor of the destination alpha component in blend function.
        /// </summary>
        public BlendFactor DestinationAlphaBlendFactor { get; set; }

        /// <summary>
        /// The function used to combine the source and destination alpha component.
        /// </summary>
        public BlendOperation AlphaBlendOperation { get; set; }

        /// <summary>
        /// Specifies which of the R, G, B and/or A components are enabled for writing.
        /// </summary>
        public ColorComponents ColorWriteMask { get; set; }

        /// <summary>
        /// Constructs a new <see cref="ColorBlendAttachmentState"/>.
        /// </summary>
        /// <param name="enabled">Defines whether the blend is enabled.</param>
        /// <param name="srcColorFactor">The factor of source color components in blend function.</param>
        /// <param name="dstColorFactor">The factor of destiantion color components in blend function.</param>
        /// <param name="colorFunc">The function used to combine the source and destination colors components.</param>
        /// <param name="srcAlphaFactor">The factor of source alpha component in blend function. </param>
        /// <param name="dstAlphaFactor">The factor of the destination alpha component in blend function.</param>
        /// <param name="alphaFunc">The function used to combine the source and destination alpha component.</param>
        /// <param name="colorWriteMask">Specifies which of the R, G, B and/or A components are enabled for writing.</param>
        public ColorBlendAttachmentState(
            bool enabled, 
            BlendFactor srcColorFactor, 
            BlendFactor dstColorFactor, 
            BlendOperation colorFunc, 
            BlendFactor srcAlphaFactor,
            BlendFactor dstAlphaFactor, 
            BlendOperation alphaFunc, 
            ColorComponents colorWriteMask)
        {
            BlendEnabled = enabled;
            SourceColorBlendFactor = srcColorFactor;
            DestinationColorBlendFactor = dstColorFactor;
            ColorBlendOperation = colorFunc;
            SourceAlphaBlendFactor = srcAlphaFactor;
            DestinationAlphaBlendFactor = dstAlphaFactor;
            AlphaBlendOperation = alphaFunc;
            ColorWriteMask = colorWriteMask;
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<ColorBlendAttachmentState>.Combine(BlendEnabled, SourceColorBlendFactor, DestinationColorBlendFactor, ColorBlendOperation, SourceAlphaBlendFactor, DestinationAlphaBlendFactor, AlphaBlendOperation);


        /// <inheritdoc/>
        public bool Equals(ColorBlendAttachmentState other) =>
            GetHashCode() == other.GetHashCode() &&
            BlendEnabled == other.BlendEnabled &&
            SourceColorBlendFactor == other.SourceColorBlendFactor &&
            DestinationColorBlendFactor == other.DestinationColorBlendFactor &&
            ColorBlendOperation == other.ColorBlendOperation &&
            SourceAlphaBlendFactor == other.SourceAlphaBlendFactor &&
            DestinationAlphaBlendFactor == other.DestinationAlphaBlendFactor &&
            AlphaBlendOperation == other.AlphaBlendOperation;

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is ColorBlendAttachmentState cba && Equals(cba);

        /// <summary>
        /// A blend description that completely replaces the destination with the source.
        /// </summary>
        public static readonly ColorBlendAttachmentState Override = new()
        {
            BlendEnabled = true,

            SourceColorBlendFactor = BlendFactor.One,
            DestinationColorBlendFactor = BlendFactor.Zero,
            ColorBlendOperation = BlendOperation.Add,

            SourceAlphaBlendFactor = BlendFactor.One,
            DestinationAlphaBlendFactor = BlendFactor.Zero,
            AlphaBlendOperation = BlendOperation.Add,

            ColorWriteMask = ColorComponents.RGBA,
        };

        /// <summary>
        /// A blend description that the source and destination are combined in an inverse relationship.
        /// </summary>
        public static readonly ColorBlendAttachmentState Alpha = new()
        {
            BlendEnabled = true,

            SourceColorBlendFactor = BlendFactor.SourceAlpha,
            DestinationColorBlendFactor = BlendFactor.OneMinusSourceAlpha,
            ColorBlendOperation = BlendOperation.Add,

            SourceAlphaBlendFactor = BlendFactor.SourceAlpha,
            DestinationAlphaBlendFactor = BlendFactor.OneMinusSourceAlpha,
            AlphaBlendOperation = BlendOperation.Add,

            ColorWriteMask = ColorComponents.RGBA,
        };

        /// <summary>
        /// A blend description that the source is added to the destination based on its alpha channel.
        /// </summary>
        public static readonly ColorBlendAttachmentState Additive = new()
        {
            BlendEnabled = true,

            SourceColorBlendFactor = BlendFactor.SourceAlpha,
            DestinationColorBlendFactor = BlendFactor.One,
            ColorBlendOperation = BlendOperation.Add,

            SourceAlphaBlendFactor = BlendFactor.SourceAlpha,
            DestinationAlphaBlendFactor = BlendFactor.One,
            AlphaBlendOperation = BlendOperation.Add,

            ColorWriteMask = ColorComponents.RGBA,
        };

        /// <summary>
        /// A blend description that the blending is disabled.
        /// </summary>
        public static readonly ColorBlendAttachmentState Disabled = new()
        {
            BlendEnabled = false,
            SourceColorBlendFactor = BlendFactor.One,
            DestinationColorBlendFactor = BlendFactor.Zero,
            ColorBlendOperation = BlendOperation.Add,

            SourceAlphaBlendFactor = BlendFactor.One,
            DestinationAlphaBlendFactor = BlendFactor.Zero,
            AlphaBlendOperation = BlendOperation.Add,

            ColorWriteMask = ColorComponents.RGBA,
        };

        /// <summary>
        /// The default value in a new render pass.
        /// </summary>
        public static readonly ColorBlendAttachmentState Default = Disabled;

        /// <summary>
        /// Compare two <see cref="ColorBlendAttachmentState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(ColorBlendAttachmentState left, ColorBlendAttachmentState right) => left.Equals(right);


        /// <summary>
        /// Compare two <see cref="ColorBlendAttachmentState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(ColorBlendAttachmentState left, ColorBlendAttachmentState right) =>
            !(left == right);
    }
}
