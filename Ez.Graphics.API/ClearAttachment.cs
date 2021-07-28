// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes a clear attachment to <see cref="ICommandBuffer.ClearAttachments"/>.
    /// </summary>
    public struct ClearAttachment
    {
        /// <summary>
        /// Selects the color attachment to be clear.
        /// </summary>
        public uint ColorAttachment;

        /// <summary>
        /// Gets or sets the color or depth/stencil value to clear the attachment.
        /// </summary>
        public ClearValue ClearValue;
    }
}
