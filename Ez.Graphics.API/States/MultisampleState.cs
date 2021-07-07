// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0.
using System;

using Ez.Numerics;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes a set of output attachments and their formats.
    /// </summary>
    public struct MultisampleState : IEquatable<MultisampleState>
    {
        /// <summary>
        /// The number of samples in each target attachment.
        /// </summary>
        public SampleCount SampleCount { get; set; }

        /// <summary>
        /// Enable alpha to coverage.
        /// </summary>
        public bool AlphaToCoverageEnabled { get; set; }

        /// <inheritdoc/>
        public bool Equals(MultisampleState other) => SampleCount == other.SampleCount && AlphaToCoverageEnabled == other.AlphaToCoverageEnabled;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is MultisampleState ms && Equals(ms);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelper<MultisampleState>.Combine(SampleCount, AlphaToCoverageEnabled);

        /// <summary>
        /// Compare two <see cref="MultisampleState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(MultisampleState left, MultisampleState right) => left.Equals(right);

        /// <summary>
        /// Compare two <see cref="MultisampleState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(MultisampleState left, MultisampleState right) =>
            !(left == right);

        /// <summary>
        /// The default multisample state in a render pass.
        /// </summary>
        public static readonly MultisampleState Default = new()
        {
            AlphaToCoverageEnabled = false,
            SampleCount = SampleCount.Count1,
        };
    }
}
