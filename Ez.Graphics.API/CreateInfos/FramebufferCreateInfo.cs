// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Linq;
using System.Text;

using Ez.Graphics.API.Resources;
using Ez.Numerics;

namespace Ez.Graphics.API.CreateInfos
{
    /// <summary>
    /// Describes a <see cref="IFramebuffer"/> object.
    /// </summary>
    public struct FramebufferCreateInfo : IEquatable<FramebufferCreateInfo>
    {
        /// <summary>
        /// The depth attachment of the <see cref="IFramebuffer"/>.
        /// </summary>
        public FramebufferAttachment? DepthOrStencilTarget { get; set; }

        /// <summary>
        /// The array of color attachments of the <see cref="IFramebuffer"/>.
        /// </summary>
        public FramebufferAttachment[] ColorTargets { get; set; }

        /// <summary>
        /// Creates a new <see cref="FramebufferAttachment"/> struct by textures.
        /// </summary>
        /// <param name="depthTarget">The depth texture target.</param>
        /// <param name="colorTargets">The array of color texture targets.</param>
        public FramebufferCreateInfo(ITexture depthTarget, params ITexture[] colorTargets)
        {
            if (depthTarget != null)
                DepthOrStencilTarget = new FramebufferAttachment(depthTarget, 0);
            else
                DepthOrStencilTarget = null;

            ColorTargets = new FramebufferAttachment[colorTargets.Length];
            for (int i = 0; i < colorTargets.Length; i++)
                ColorTargets[i] = new FramebufferAttachment(colorTargets[i], 0);
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<FramebufferCreateInfo>.Combine(
                DepthOrStencilTarget,
                HashHelper<FramebufferAttachment>.Combine(ColorTargets));


        /// <inheritdoc/>
        public bool Equals(FramebufferCreateInfo other) =>
            ((!DepthOrStencilTarget.HasValue && !other.DepthOrStencilTarget.HasValue) ||
                (DepthOrStencilTarget.HasValue && other.DepthOrStencilTarget.HasValue && DepthOrStencilTarget.Value.Equals(other.DepthOrStencilTarget.Value))) &&
            (ColorTargets == other.ColorTargets || 
                (ColorTargets != null && other.ColorTargets != null && ColorTargets.SequenceEqual(other.ColorTargets)));

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is BufferCreateInfo bci && Equals(bci);

        /// <inheritdoc/>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append('(');

            builder.Append("DepthOrStencilTarget: ");
            builder.Append(DepthOrStencilTarget);

            builder.Append("ColorTargets: ");

            if (ColorTargets == null)
                builder.Append("null");
            else
                for(var i = 0; i < ColorTargets.Length; i++)
                {
                    builder.Append(ColorTargets[i]);
                    if (i != ColorTargets.Length - 1)
                        builder.Append('|');
                }

            builder.Append(')');

            return builder.ToString();
        }

        /// <summary>
        /// Compare two <see cref="FramebufferCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(FramebufferCreateInfo left, FramebufferCreateInfo right) =>
            left.Equals(right);


        /// <summary>
        /// Compare two <see cref="FramebufferCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(FramebufferCreateInfo left, FramebufferCreateInfo right) =>
            !(left == right);
    }
}
