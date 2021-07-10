// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Threading.Tasks;
using Ez.Graphics.API.CreateInfos;
using Ez.Numerics;

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// A device resource used to store image data.
    /// See <see cref="TextureCreateInfo"/>.
    /// </summary>
    public interface ITexture : IResource, IMappableResource, IEquatable<ITexture>
    {

        /// <summary>
        /// The format of a texture texel.
        /// </summary>
        PixelFormat Format { get; }

        /// <summary>
        /// The dimensions of the texture in texels.
        /// </summary>
        Size3 Size { get; }

        /// <summary>
        /// The total number of mipmap levels in this instance.
        /// </summary>
        uint MipmapLevels { get; }

        /// <summary>
        /// Layers of array.
        /// </summary>
        uint ArrayLayers { get; }

        /// <summary>
        /// The usage flags given when this instance was created. This property controls how this instance is permitted to be
        /// used, and it is an error to attempt to use the Texture outside of those contexts.
        /// </summary>
        TextureUsage Usage { get; }

        /// <summary>
        /// The <see cref="TextureType"/> of this instance.
        /// </summary>
        TextureType Type { get; }

        /// <summary>
        /// The number of samples in this instance. If this returns any value other than <see cref="SampleCount.Count1"/>,
        /// then this instance is a multipsample texture.
        /// </summary>
        SampleCount SampleCount { get; }

        /// <summary>
        /// The tiling arrangement of the texel blocks in memory.
        /// </summary>
        TextureTiling Tiling { get; }

        /// <summary>
        /// Updates a portion of <see cref="ITexture"/> with new data contained in an array
        /// </summary>
        /// <param name="source">An array containing the data to upload. This must contain tightly-packed pixel data for the
        /// region specified.</param>
        /// <param name="x">The minimum X value of the updated region.</param>
        /// <param name="y">The minimum Y value of the updated region.</param>
        /// <param name="z">The minimum Z value of the updated region.</param>
        /// <param name="width">The width of the updated region, in texels.</param>
        /// <param name="height">The height of the updated region, in texels.</param>
        /// <param name="depth">The depth of the updated region, in texels.</param>
        /// <param name="mipLevel">The mipmap level to update. Must be less than the total number of mipmaps contained in the
        /// <see cref="ITexture"/>.</param>
        /// <param name="arrayLayer">The array layer to update. Must be less than the total array layer count contained in the
        /// <see cref="ITexture"/>.</param>
        void SubData<T>(ReadOnlyMemory<T> source, int x, int y, int z, uint width, uint height, uint depth, int mipLevel, int arrayLayer) where T : unmanaged;

        /// <summary>
        /// Updates a portion of <see cref="ITexture"/> with new data contained in an array
        /// </summary>
        /// <param name="source">An array containing the data to upload. This must contain tightly-packed pixel data for the
        /// region specified.</param>
        /// <param name="x">The minimum X value of the updated region.</param>
        /// <param name="y">The minimum Y value of the updated region.</param>
        /// <param name="z">The minimum Z value of the updated region.</param>
        /// <param name="width">The width of the updated region, in texels.</param>
        /// <param name="height">The height of the updated region, in texels.</param>
        /// <param name="depth">The depth of the updated region, in texels.</param>
        /// <param name="mipLevel">The mipmap level to update. Must be less than the total number of mipmaps contained in the
        /// <see cref="ITexture"/>.</param>
        /// <param name="arrayLayer">The array layer to update. Must be less than the total array layer count contained in the
        /// <see cref="ITexture"/>.</param>
        Task SubDataAsync<T>(ReadOnlyMemory<T> source, int x, int y, int z, uint width, uint height, uint depth, int mipLevel, int arrayLayer) where T : unmanaged;
    }
}
