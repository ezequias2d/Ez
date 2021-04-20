// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.Data.Materials;
using Ez.Numerics;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// Represents a reference for a texture file.
    /// </summary>
    public sealed class TextureReference : TextureData
    {
        private readonly int _hashcode;

        /// <summary>
        /// Initializes a new instance of <see cref="TextureReference"/> class.
        /// </summary>
        /// <param name="reference">The reference path.</param>
        public TextureReference(string reference)
        {
            Reference = reference;
            _hashcode = HashHelper<TextureReference>.Combine(Reference);
        }

        /// <summary>
        /// Gets the reference path.
        /// </summary>
        public string Reference { get; }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => _hashcode;
    }
}
