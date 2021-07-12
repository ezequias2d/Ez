// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Linq;
using System.Text;

using Ez.Graphics.API.Resources;
using Ez.Numerics;

namespace Ez.Graphics.API.CreateInfos
{
    /// <summary>
    /// Describes a <see cref="IShader"/> object.
    /// </summary>
    public struct ShaderCreateInfo : IEquatable<ShaderCreateInfo>
    {
        /// <summary>
        /// The shader name for debugging.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The stages of the <see cref="IShader"/>.
        /// </summary>
        public ShaderStages Stages { get; set; }

        /// <summary>
        /// The format of shader <see cref="Source"/>.
        /// </summary>
        public ShaderFormat Format { get; set; }

        /// <summary>
        /// The shader entry-point.
        /// </summary>
        public string EntryPoint { get; set; }

        /// <summary>
        /// The source of the shader.
        /// </summary>
        public ReadOnlyMemory<byte> Source { get; set; }

        /// <summary>
        /// Creates a new <see cref="ShaderCreateInfo"/> struct.
        /// </summary>
        /// <param name="name">The shader name for debugging.</param>
        /// <param name="entryPoint">The shader entry-point.</param>
        /// <param name="stages">The stages of the shader.</param>
        /// <param name="format">The format of shader <paramref name="source"/>.</param>
        /// <param name="source">The source of the shader.</param>
        public ShaderCreateInfo(string name, string entryPoint, ShaderStages stages, ShaderFormat format, ReadOnlyMemory<byte> source) =>
            (Name, EntryPoint, Stages, Format, Source) = (name, entryPoint, stages, format, source.ToArray());

        /// <summary>
        /// Creates a new <see cref="ShaderCreateInfo"/> struct from a GLSL string.
        /// </summary>
        /// <param name="name">The shader name for debugging.</param>
        /// <param name="entryPoint">The shader entry-point.</param>
        /// <param name="stages">The stages of the shader.</param>
        /// <param name="source">The GLSL source of the shader.</param>
        public ShaderCreateInfo(string name, string entryPoint, ShaderStages stages, string source) 
            : this(name, entryPoint, stages, ShaderFormat.Glsl, Encoding.ASCII.GetBytes(source)) { }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<ShaderCreateInfo>.Combine(Stages, Format, HashHelper<byte>.Combine(Source));

        /// <inheritdoc/>
        public bool Equals(ShaderCreateInfo other) => 
            Stages == other.Stages && 
            Format == other.Format && 
            (Source.Equals(other.Source) || Source.Span.SequenceEqual(other.Source.Span));

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is ShaderCreateInfo sci && Equals(sci);

        /// <inheritdoc/>
        public override string ToString() => $"(Name: {Name}, Stages: {Stages}, Format: {Format}, Source(Hash): {HashHelper<byte>.Combine(Source)})";

        /// <summary>
        /// Compare two <see cref="ShaderCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(ShaderCreateInfo left, ShaderCreateInfo right) =>
            left.Equals(right);

        /// <summary>
        /// Compare two <see cref="ShaderCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(ShaderCreateInfo left, ShaderCreateInfo right) =>
            !(left == right);
    }
}
