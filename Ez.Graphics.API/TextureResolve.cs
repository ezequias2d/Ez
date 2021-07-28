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
    /// Specifies a texture resolve operation.
    /// <para>
    ///     <see cref="ICommandBuffer.ResolveTexture(ITexture, ITexture, ReadOnlySpan{TextureResolve})"/>.
    /// </para>
    /// </summary>
    public struct TextureResolve
    {
        /// <summary>
        /// Gets or sets the source texture subresource for the texture data.
        /// </summary>
        public TextureSubresourceLayers SourceSubresource;

        /// <summary>
        /// Gets or sets the initial x, y, and z offsets in texels of the sub-regions of the 
        /// source texture data.
        /// </summary>
        public Point3 SourceOffset;

        /// <summary>
        /// Gets or sets the the source texture subresource for the texture data.
        /// </summary>
        public TextureSubresourceLayers DestinationSubresource;

        /// <summary>
        /// Gets or sets the initial x, y, and z offsets in texels of the sub-regions of the 
        /// destination
        /// texture data.
        /// </summary>
        public Point3 DestinationOffset;

        /// <summary>
        /// Gets or sets the size in texel of the source texture to resolve in width, height
        /// and depth.
        /// </summary>
        public Size3 Extent;
    }
}
