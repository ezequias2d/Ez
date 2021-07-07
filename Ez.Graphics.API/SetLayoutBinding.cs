// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Enums;
using Ez.Numerics;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Structure specifying a set layout binding.
    /// </summary>
    public struct SetLayoutBinding : IEquatable<SetLayoutBinding>
    {
        /// <summary>
        /// The binding number of this entry and corresponds to a resource of the
        /// same binding number in shader stages.
        /// </summary>
        public uint Binding { get; set; }

        /// <summary>
        /// Specifying which type of the resource for this binding.
        /// </summary>
        public SetType SetType { get; set; }

        /// <summary>
        /// Specifying which pipeline shader stages can access a resource for this 
        /// binding.
        /// </summary>
        public ShaderStages ShaderStages { get; set; }

        /// <inheritdoc/>
        public bool Equals(SetLayoutBinding other) =>
            Binding == other.Binding &&
            SetType == other.SetType &&
            ShaderStages == other.ShaderStages;

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is SetLayoutBinding slb && Equals(slb);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<SetLayoutBinding>.Combine(Binding, SetType, ShaderStages);

        /// <inheritdoc/>
        public override string ToString() =>
            $"<{nameof(SetLayoutBinding)}, {nameof(Binding)} {Binding}, {nameof(SetType)} {SetType}, {nameof(ShaderStages)} {ShaderStages}>";


        /// <summary>
        /// Compare two <see cref="SetLayoutBinding"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(SetLayoutBinding left, SetLayoutBinding right) =>
            left.Equals(right);


        /// <summary>
        /// Compare two <see cref="SetLayoutBinding"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(SetLayoutBinding left, SetLayoutBinding right) =>
            !(left == right);
    }
}
