// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System.Collections.Generic;
using System.Drawing;

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// Represents a framebuffer resource.
    /// </summary>
    public interface IFramebuffer : IResource
    {
        /// <summary>
        /// Gets the depth attachment associated with this instance. May be null if no depth texture is used.
        /// </summary>
        FramebufferAttachment? DepthTarget { get; }

        /// <summary>
        /// Gets the collection of color attachments associated with this instance. May be empty.
        /// </summary>
        IReadOnlyList<FramebufferAttachment> ColorTargets { get; }

        /// <summary>
        /// Gets the size of the <see cref="IFramebuffer"/>.
        /// </summary>
        Size Size { get; }
    }
}
