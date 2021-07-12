// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System.Collections.Generic;

using Ez.Numerics;

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// Represents a framebuffer resource.
    /// </summary>
    public interface IFramebuffer : IResource
    {
        /// <summary>
        /// Gets the collection of attachments associated with this instance.
        /// </summary>
        IReadOnlyList<FramebufferAttachment> Attachments { get; }

        /// <summary>
        /// Gets the size of the <see cref="IFramebuffer"/>.
        /// </summary>
        Size3 Size { get; }
    }
}
