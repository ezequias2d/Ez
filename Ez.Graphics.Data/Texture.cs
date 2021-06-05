// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Memory;
using Ez.Numerics;
using System;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// Represents a texture.
    /// </summary>
    public sealed class Texture : TextureData, IEquatable<Texture>
    {
        private readonly byte[] _data;
        private readonly int _length;
        private readonly int _hashcode;

        /// <summary>
        /// Initializes a new instance of <see cref="Texture"/> class.
        /// </summary>
        /// <param name="pixelFormat">The pixel format of texture.</param>
        /// <param name="width">The width of texture.</param>
        /// <param name="height">The height of texture.</param>
        /// <param name="depth">The depth of texture.</param>
        /// <param name="mipmapLevels">The number of mipmap levels of texture.</param>
        /// <param name="layers">The number of layers of texture.</param>
        /// <param name="textureType">The texture type of texture.</param>
        /// <param name="data">The pixels data.</param>
        public Texture(
            PixelFormat pixelFormat, 
            uint width, 
            uint height, 
            uint depth, 
            uint mipmapLevels, 
            uint layers, 
            TextureType textureType, 
            ReadOnlySpan<byte> data)
        {
            PixelFormat = pixelFormat;
            Width = width;
            Height = height;
            Depth = depth;
            _data = default;
            _length = 0;
            MipmapLevels = mipmapLevels;
            ArrayLayers = layers;
            TextureType = textureType;
            InternalHelper.Set(ref _data, ref _length, data);

            _hashcode = HashHelper<Texture>.Combine(PixelFormat, Width, Height, Depth, MipmapLevels, ArrayLayers, TextureType, HashHelper<Texture>.Combine(Data));
        }

        /// <summary>
        /// The pixel format.
        /// </summary>
        public PixelFormat PixelFormat { get; }
        /// <summary>
        /// Width of the texture, in pixels.
        /// </summary>
        public uint Width { get; }
        /// <summary>
        /// Height of the texture, in pixels.
        /// </summary>
        public uint Height { get; }

        /// <summary>
        /// Depth of the texture, in pixels.
        /// </summary>
        public uint Depth { get; }

        /// <summary>
        /// Number of mipmaps in texture.
        /// </summary>
        public uint MipmapLevels { get; }

        /// <summary>
        /// Number of layers in texture.
        /// </summary>
        public uint ArrayLayers { get; }

        /// <summary>
        /// Texture type of this texture.
        /// </summary>
        public TextureType TextureType { get; }
        
        /// <summary>
        /// Data of texture.
        /// </summary>
        public ReadOnlySpan<byte> Data { get => new ReadOnlySpan<byte>(_data, 0, _length); }

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="Texture"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="Texture"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="Texture"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Texture other) =>
            ReferenceEquals(this, other) ||
            (PixelFormat == other.PixelFormat &&
                Width == other.Width &&
                Height == other.Height &&
                Data.SequenceEqual(other.Data));

        /// <summary>
        /// Returns a value that indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="obj"/> are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) =>
            obj is Texture texture && Equals(texture);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => _hashcode;
    }
}
