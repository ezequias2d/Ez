// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Linq;
using System.Text;

using Ez.Graphics.API.Resources;
using Ez.Memory;
using Ez.Numerics;

namespace Ez.Graphics.API.CreateInfos
{
    /// <summary>
    /// Describes a <see cref="IPipeline"/> object.
    /// <seealso cref="IResourceFactory"/>.
    /// </summary>
    public struct PipelineCreateInfo : IEquatable<PipelineCreateInfo>
    {
        /// <summary>
        /// Indicates that the pipeline is a graphic pipeline.
        /// </summary>
        public bool IsGraphicPipeline { get; set; }

        /// <summary>
        /// The resource set layout bindings of the pipeline.
        /// </summary>
        public SetLayoutBinding[] Bindings { get; set; }

        /// <summary>
        /// The shaders of the pipeline.
        /// </summary>
        public IShader[] Shaders { get; set; }

        /// <inheritdoc/>
        public bool Equals(PipelineCreateInfo other) =>
            IsGraphicPipeline == other.IsGraphicPipeline &&
            MemUtil.Equals<SetLayoutBinding>(Bindings, other.Bindings) &&
            Shaders.ToHashSet().SetEquals(other.Shaders);

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is SetLayoutBinding slci && Equals(slci);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper< PipelineCreateInfo >.Combine(
                IsGraphicPipeline,
                HashHelper<IShader>.Combine(Shaders),
                HashHelper<SetLayoutBinding>.Combine((ReadOnlySpan<SetLayoutBinding>)Bindings));

        /// <inheritdoc/>
        public override string ToString()
        {
            var separator = ", ";
            var sb = new StringBuilder();
            sb.Append('<');
            sb.Append(nameof(PipelineCreateInfo));
            sb.Append(separator);
            sb.Append(IsGraphicPipeline);
            foreach (var shader in Shaders)
            {
                sb.Append(separator);
                sb.Append(shader);
            }
            foreach (var binding in Bindings)
            {
                sb.Append(separator);
                sb.Append(binding);
            }
            sb.Append('>');
            return sb.ToString();
        }

        /// <summary>
        /// Compare two <see cref="PipelineCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(PipelineCreateInfo left, PipelineCreateInfo right) =>
            left.Equals(right);


        /// <summary>
        /// Compare two <see cref="PipelineCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(PipelineCreateInfo left, PipelineCreateInfo right) =>
            !(left == right);
    }
}
