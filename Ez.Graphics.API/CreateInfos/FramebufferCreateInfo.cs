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
        /// The attachments of the <see cref="IFramebuffer"/>.
        /// </summary>
        public FramebufferAttachment[] Attachments { get; set; }

        /// <summary>
        /// Creates a new <see cref="FramebufferAttachment"/> struct by textures.
        /// </summary>
        /// <param name="attachments">The array of texture attachments.</param>
        public FramebufferCreateInfo(params ITexture[] attachments)
        {
            Attachments = new FramebufferAttachment[attachments.Length];
            for (int i = 0; i < attachments.Length; i++)
                Attachments[i] = new FramebufferAttachment(attachments[i], 0);
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<FramebufferCreateInfo>.Combine((ReadOnlySpan<FramebufferAttachment>)Attachments);


        /// <inheritdoc/>
        public bool Equals(FramebufferCreateInfo other) =>
            (Attachments == other.Attachments || 
                (Attachments != null && other.Attachments != null && Attachments.SequenceEqual(other.Attachments)));

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is BufferCreateInfo bci && Equals(bci);

        /// <inheritdoc/>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append('(');

            builder.Append("DepthOrStencilTarget: ");
            builder.Append(Attachments);

            builder.Append("Attachments: ");

            for(var i = 0; i < Attachments.Length; i++)
            {
                builder.Append(Attachments[i]);
                if (i != Attachments.Length - 1)
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
