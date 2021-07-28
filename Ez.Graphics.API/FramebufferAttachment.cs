// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Represents a single output of a <see cref="IFramebuffer"/>. May be a color or depth attachment.
    /// </summary>
    public readonly struct FramebufferAttachment
    {
        /// <summary>
        /// The target <see cref="ITexture"/> which will be rendered to.
        /// </summary>
        public readonly ITexture Target;

        /// <summary>
        /// The target array layer.
        /// </summary>
        public readonly uint ArrayLayer;

        /// <summary>
        /// The target mip level.
        /// </summary>
        public readonly uint  MipLevel;

        /// <summary>
        /// Creates a new instance of<see cref= "FramebufferAttachment" /> struct.
        /// </summary>
        /// <param name="target">The target <see cref="ITexture"/> which will be rendered to.</param>
        /// <param name="arrayLayer">The target array layer.</param>
        public FramebufferAttachment(ITexture target, uint arrayLayer)
        {
            Target = target;
            ArrayLayer = arrayLayer;
            MipLevel = 0;
        }

        /// <summary>
        /// Creates a new instance of<see cref= "FramebufferAttachment" /> struct.
        /// </summary>
        /// <param name="target">The target <see cref="ITexture"/> which will be rendered to.</param>
        /// <param name="arrayLayer">The target array layer.</param>
        /// <param name="mipLevel">The target mip level.</param>
        public FramebufferAttachment(ITexture target, uint arrayLayer, uint mipLevel)
        {
            Target = target;
            ArrayLayer = arrayLayer;
            MipLevel = mipLevel;
        }
    }
}
