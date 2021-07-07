// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;
using Ez.Numerics;

namespace Ez.Graphics.API.CreateInfos
{
    /// <summary>
    /// Describes a <see cref="ITextureView"/> object.
    /// </summary>
    public struct TextureViewCreateInfo : IEquatable<TextureViewCreateInfo>
    {
        /// <summary>
        /// The <see cref="ITexture"/> on which the view will be created.
        /// </summary>
        public ITexture Texture { get; set; }

        /// <summary>
        /// Gets or sets the subresource range of the texture view.
        /// </summary>
        public TextureSubresourceRange SubresourceRange { get; set; }

        /// <summary>
        /// An optional value describing the format and type used to interpret texel blocks in the <see cref="Texture"/>.
        /// If this value is null, then the <see cref="ITextureView"/> will use the same <see cref="PixelFormat"/> as the
        /// <see cref="Texture"/>, otherwise this format must be compatible with the <see cref="Texture"/>. For uncompressed
        /// formats, the overall size and number of components in this format must be the same as the underlying format. For
        /// compressed formats, other formats if not the <see cref="Texture"/> format is incompatible.
        /// </summary>
        public PixelFormat? Format;

        /// <summary>
        /// Creates a new instance of<see cref= "TextureViewCreateInfo" /> struct.
        /// </summary>
        /// <param name="target">The <see cref="ITexture"/> on which the view will be created. This <see cref="ITexture"/> must have been created
        /// with the <see cref="TextureUsage.Sampled"/> flag.</param>
        public TextureViewCreateInfo(ITexture target)
        {
            Texture = target;
            SubresourceRange = new TextureSubresourceRange(target);
            Format = target.Format;
        }

        /// <summary>
        /// Constructs a new TextureViewDescription.
        /// </summary>
        /// <param name="texture">The <see cref="ITexture"/> on which the view will be created. This <see cref="ITexture"/> must have been created
        /// with the <see cref="TextureUsage.Sampled"/> flag.</param>
        /// <param name="format">An optional value describing the format and type used to interpret texel blocks in the <see cref="Texture"/>.
        /// If this value is null, then the <see cref="ITextureView"/> will use the same <see cref="PixelFormat"/> as the
        /// <see cref="Texture"/>, otherwise this format must be compatible with the <see cref="Texture"/>. For uncompressed
        /// formats, the overall size and number of components in this format must be the same as the underlying format. For
        /// compressed formats, other formats if not the <see cref="Texture"/> format is incompatible.</param>
        public TextureViewCreateInfo(ITexture texture, PixelFormat? format)
        {
            Texture = texture;
            SubresourceRange = new TextureSubresourceRange(texture);
            Format = format;
        }

        /// <summary>
        /// Constructs a new TextureViewDescription.
        /// </summary>
        /// <param name="texture">The <see cref="ITexture"/> on which the view will be created. This <see cref="ITexture"/> must have been created
        /// with the <see cref="TextureUsage.Sampled"/> flag.</param>
        /// <param name="range">The subresource range of the texture view.</param>
        public TextureViewCreateInfo(ITexture texture, TextureSubresourceRange range)
        {
            Texture = texture;
            SubresourceRange = range;
            Format = texture.Format;
        }

        /// <summary>
        /// Constructs a new TextureViewDescription.
        /// </summary>
        /// <param name="texture">The <see cref="ITexture"/> on which the view will be created. This <see cref="ITexture"/> must have been created
        /// with the <see cref="TextureUsage.Sampled"/> flag.</param>
        /// <param name="format">An optional value describing the format and type used to interpret texel blocks in the <see cref="Texture"/>.
        /// If this value is null, then the <see cref="ITextureView"/> will use the same <see cref="PixelFormat"/> as the
        /// <see cref="Texture"/>, otherwise this format must be compatible with the <see cref="Texture"/>. For uncompressed
        /// formats, the overall size and number of components in this format must be the same as the underlying format. For
        /// compressed formats, other formats if not the <see cref="Texture"/> format is incompatible.</param>
        /// <param name="range">The subresource range of the texture view.</param>
        public TextureViewCreateInfo(ITexture texture, PixelFormat format, TextureSubresourceRange range)
        {
            Texture = texture;
            SubresourceRange = range;
            Format = format;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is TextureViewCreateInfo tvci && Equals(tvci);

        /// <inheritdoc/>
        public bool Equals(TextureViewCreateInfo other)
        {
            return Texture.Equals(other.Texture)
                && SubresourceRange == other.SubresourceRange
                && Format == other.Format;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            if(Format != null)
                return HashHelper<TextureViewCreateInfo>.Combine(
                    Texture,
                    SubresourceRange,
                    Format.Value.GetHashCode());
            else
                return HashHelper<TextureViewCreateInfo>.Combine(
                    Texture,
                    SubresourceRange);
        }

        /// <summary>
        /// Compare two <see cref="TextureViewCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(TextureViewCreateInfo left, TextureViewCreateInfo right) =>            left.Equals(right);


        /// <summary>
        /// Compare two <see cref="TextureViewCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(TextureViewCreateInfo left, TextureViewCreateInfo right) =>
            !(left == right);
    }
}
