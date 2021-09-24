// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Ez.Graphics.API.Resources;
using Ez.Numerics;
using System;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes a clear attachment to <see cref="ICommandBuffer.ClearAttachments"/>.
    /// </summary>
    public struct ClearAttachment : IEquatable<ClearAttachment>
    {
        /// <summary>
        /// Selects the color attachment to be clear.
        /// </summary>
        public uint ColorAttachment;

        /// <summary>
        /// Gets or sets the color or depth/stencil value to clear the attachment.
        /// </summary>
        public ClearValue ClearValue;

        /// <inheritdoc/>
        public bool Equals(ClearAttachment other) =>
            ColorAttachment == other.ColorAttachment &&
            ClearValue == other.ClearValue;

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is ClearAttachment ca && Equals(ca);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelper<ClearAttachment>.Combine(ColorAttachment, ClearValue);

        /// <summary>
        /// Compare two <see cref="ClearAttachment"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(ClearAttachment left, ClearAttachment right) => left.Equals(right);

        /// <summary>
        /// Compare two <see cref="ClearAttachment"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(ClearAttachment left, ClearAttachment right) => !(left == right);
    }
}
