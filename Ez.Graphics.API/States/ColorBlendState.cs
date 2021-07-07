// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;
using Ez.Numerics;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describe the properties of the blend in a <see cref="IPipeline"/>.
    /// </summary>
    public struct ColorBlendState : IEquatable<ColorBlendState>
    {
        /// <summary>
        /// Defines whether the logic operation is enabled.
        /// </summary>
        public bool LogicOperationEnabled { get; set; }

        /// <summary>
        /// Selects which logical operation to apply.
        /// </summary>
        public LogicOperation LogicOperation { get; set; }

        /// <summary>
        /// Describe property of blend for earch color target.
        /// </summary>
        public ColorBlendAttachmentState[] AttachmentStates { get; set; }

        /// <summary>
        /// Constructor of a <see cref="ColorBlendState"/>
        /// </summary>
        /// <param name="logicOpEnabled">The logic operation enable.</param>
        /// <param name="logicOp">The logic operation.</param>
        /// <param name="attachmentStates">The blend attachment states.</param>
        public ColorBlendState(bool logicOpEnabled,
            LogicOperation logicOp,
            params ColorBlendAttachmentState[] attachmentStates) =>
            (LogicOperationEnabled, LogicOperation, AttachmentStates) =
            (logicOpEnabled, logicOp, attachmentStates);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<ColorBlendState>.Combine(
                LogicOperationEnabled, 
                LogicOperation, 
                HashHelper<ColorBlendAttachmentState>.Combine(AttachmentStates));

        /// <inheritdoc/>
        public bool Equals(ColorBlendState other) =>
            LogicOperationEnabled == other.LogicOperationEnabled &&
            LogicOperation == other.LogicOperation &&
            MemoryExtensions.SequenceEqual<ColorBlendAttachmentState>(
                AttachmentStates, other.AttachmentStates);

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is ColorBlendState cbs && Equals(cbs);

        /// <summary>
        /// Describes a blend state with a unique color target is blended with <see cref="ColorBlendAttachmentState.Override"/>
        /// </summary>
        public static readonly ColorBlendState Override = new()
        {
            LogicOperationEnabled = false,
            AttachmentStates = new ColorBlendAttachmentState[] { ColorBlendAttachmentState.Override }
        };

        /// <summary>
        /// Describes a blend state with a unique color target is blended with <see cref="ColorBlendAttachmentState.Alpha"/>
        /// </summary>
        public static readonly ColorBlendState Alpha = new()
        {
            LogicOperationEnabled = false,
            AttachmentStates = new ColorBlendAttachmentState[] { ColorBlendAttachmentState.Alpha }
        };

        /// <summary>
        /// Describes a blend state with a unique color target is blended with <see cref="ColorBlendAttachmentState.Additive"/>
        /// </summary>
        public static readonly ColorBlendState Additive = new()
        {
            LogicOperationEnabled = false,
            AttachmentStates = new ColorBlendAttachmentState[] { ColorBlendAttachmentState.Additive }
        };

        /// <summary>
        /// Describes a blend state with a unique color target is blended with <see cref="ColorBlendAttachmentState.Disabled"/>
        /// </summary>
        public static readonly ColorBlendState Disabled = new()
        {
            LogicOperationEnabled = false,
            AttachmentStates = new ColorBlendAttachmentState[] { ColorBlendAttachmentState.Disabled }
        };

        /// <summary>
        /// Describes an empty blend state in which no color targets are used.
        /// </summary>
        public static readonly ColorBlendState Empty = new()
        {
            LogicOperationEnabled = false,
            AttachmentStates = Array.Empty<ColorBlendAttachmentState>()
        };

        /// <summary>
        /// Describes the default value of color blend state in a render pass.
        /// </summary>
        public static readonly ColorBlendState Default = new()
        {
            LogicOperationEnabled = false,
            LogicOperation = LogicOperation.Set,
            AttachmentStates = Array.Empty<ColorBlendAttachmentState>(),
        };

        /// <summary>
        /// Compare two <see cref="ColorBlendState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(ColorBlendState left, ColorBlendState right) => left.Equals(right);


        /// <summary>
        /// Compare two <see cref="ColorBlendState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(ColorBlendState left, ColorBlendState right) =>
            !(left == right);
    }
}
