// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Structure specifying render pass begin information.
    /// </summary>
    public struct RenderPassBeginInfo
    {
        /// <summary>
        /// Gets or sets the target framebuffer.
        /// </summary>
        public IFramebuffer Framebuffer { get; set; }

        /// <summary>
        /// Gets or sets an array of attachments which define load and store operations
        /// and clear values for color attachments.
        /// </summary>
        public ReadOnlyMemory<AttachmentInfo> ColorAttachments { get; set; }

        /// <summary>
        /// Gets or sets attachments which define load and store operations and clear
        /// values for depth or stencil attachment.
        /// </summary>
        public AttachmentInfo? DepthOrStencilAttachment { get; set; }
    }
}
