// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specifies the usage of a <see cref="ITexture"/>.
    /// </summary>
    [Flags]
    public enum TextureUsage : byte
    {
        /// <summary>
        /// Specifies that the texture can be used to create a <see cref="ITextureView"/> and can be
        /// sampled by a shader.
        /// </summary>
        Sampled = 1 << 0,
        /// <summary>
        /// Specifies that the texture can be used to create a <see cref="ITextureView"/> and can be
        /// accessed from a shader.
        /// </summary>
        Storage = 1 << 1,
        /// <summary>
        /// Specifies that the texture can be used as the color target of a <see cref="IFramebuffer"/>.
        /// </summary>
        ColorAttachment = 1 << 2,
        /// <summary>
        /// Specifies that the texture can be used as the depth target of a <see cref="IFramebuffer"/>.
        /// </summary>
        DepthStencilAttachment = 1 << 3,
        /// <summary>
        /// Specifies that the texture is a cubemap.
        /// </summary>
        Cubemap = 1 << 4,
        /// <summary>
        /// Specifies that the texture can be used as the source of a transfer command.
        /// </summary>
        TransferDestination = 1 << 5,
        /// <summary>
        /// Specifies that the texture can be used as the destination of a transfer command.
        /// </summary>
        TransferSource = 1 << 6,
    }
}
