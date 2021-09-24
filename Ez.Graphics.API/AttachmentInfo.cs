// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Ez.Numerics;
using System;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Structure specifying attachment informations.
    /// </summary>
    public struct AttachmentInfo : IEquatable<AttachmentInfo>
    {
        /// <summary>
        /// Gets or sets a value that specifying how the contents of components of the attachment are treated
        /// at the beginning of the subpass where it is first used.
        /// </summary>
        public AttachmentLoadOperation LoadOperation { get; set; }

        /// <summary>
        /// Gets or sets a value that specifying how the contents of components of the attachment are treated 
        /// at the end of the subpass where it is last used.
        /// </summary>
        public AttachmentStoreOperation StoreOperation { get; set; }

        /// <summary>
        /// The clear values for attachment, if <see cref="LoadOperation"/> value is
        /// <see cref="AttachmentLoadOperation.Clear"/>.
        /// </summary>
        public ClearValue ClearValue { get; set; }

        /// <inheritdoc/>
        public bool Equals(AttachmentInfo other) =>
            LoadOperation == other.LoadOperation &&
            StoreOperation == other.StoreOperation &&
            ClearValue.Equals(other.ClearValue);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelper<AttachmentInfo>.Combine(LoadOperation, StoreOperation, ClearValue);

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is AttachmentInfo ai && Equals(ai);

        /// <summary>
        /// Compare two <see cref="AttachmentInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(AttachmentInfo left, AttachmentInfo right) => left.Equals(right);

        /// <summary>
        /// Compare two <see cref="AttachmentInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(AttachmentInfo left, AttachmentInfo right) => !(left == right);
    }
}
