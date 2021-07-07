// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes the stencil operation state.
    /// </summary>
    public struct StencilOperationState : IEquatable<StencilOperationState>
    {
        /// <summary>
        /// The operation when fail the stencil test.
        /// </summary>
        public StencilOperation FailOperation;

        /// <summary>
        /// The operation when pass the stencil test.
        /// </summary>
        public StencilOperation PassOperation;

        /// <summary>
        /// The operation when pass the stencil test but fail the depth test.
        /// </summary>
        public StencilOperation DepthFailOperation;

        /// <summary>
        /// The <see cref="API.CompareOperation"/> used in stencil test.
        /// </summary>
        public CompareOperation CompareOperation;

        /// <inheritdoc/>
        public bool Equals(StencilOperationState other) =>
                CompareOperation == other.CompareOperation &&
                FailOperation == other.FailOperation &&
                PassOperation == other.PassOperation &&
                DepthFailOperation == other.DepthFailOperation;
        
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is StencilOperationState sos && Equals(sos);
        
        /// <inheritdoc/>
        public override int GetHashCode() => HashHelper<StencilOperationState>.Combine(CompareOperation, FailOperation, PassOperation, DepthFailOperation);

        /// <summary>
        /// Compare two <see cref="StencilOperationState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(StencilOperationState left, StencilOperationState right) => left.Equals(right);


        /// <summary>
        /// Compare two <see cref="ColorBlendState"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(StencilOperationState left, StencilOperationState right) =>
            !(left == right);
    }
}
