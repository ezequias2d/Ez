// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;
using Ez.Memory;
using Ez.Numerics;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Structure specifying render pass begin information.
    /// </summary>
    public struct RenderPassBeginInfo : IEquatable<RenderPassBeginInfo>
    {
        /// <summary>
        /// Gets or sets the target framebuffer.
        /// </summary>
        public IFramebuffer Framebuffer;

        /// <summary>
        /// Gets or sets an array of attachments which define load and store operations
        /// and clear values for color attachments and depth stencil attachment.
        /// </summary>
        public ReadOnlyMemory<AttachmentInfo> Attachments;

        /// <summary>
        /// The index of depth stencil attachment in <see cref="Attachments"/>.
        /// </summary>
        public int DepthStencilAttachmentIndex;

        /// <inheritdoc/>
        public bool Equals(RenderPassBeginInfo other) =>
            Framebuffer == other.Framebuffer &&
            MemUtil.Equals(Attachments.Span, other.Attachments.Span) &&
            DepthStencilAttachmentIndex == other.DepthStencilAttachmentIndex;

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is RenderPassBeginInfo rpbi && Equals(rpbi);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<RenderPassBeginInfo>.Combine(
                Framebuffer, 
                HashHelper<AttachmentInfo>.Combine(Attachments.Span), 
                DepthStencilAttachmentIndex);

        /// <summary>
        /// Compare two <see cref="RenderPassBeginInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(RenderPassBeginInfo left, RenderPassBeginInfo right) => left.Equals(right);


        /// <summary>
        /// Compare two <see cref="RenderPassBeginInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(RenderPassBeginInfo left, RenderPassBeginInfo right) =>
            !(left == right);
    }
}
