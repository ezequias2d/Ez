// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// A device resource used to access a <see cref="ITexture"/>.
    /// See <see cref="TextureViewCreateInfo"/>.
    /// </summary>
    public interface ITextureView : ITexture
    {
        /// <summary>
        /// Returns the hidden texture. 
        /// </summary>
        ITexture Texture { get; }

        /// <summary>
        /// Gets the view range.
        /// </summary>
        TextureSubresourceRange Range { get; }
    }
}
