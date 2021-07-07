// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes how to combine a source color with the destination color.
    /// </summary>
    public enum BlendOperation
    {
        /// <summary>
        /// Color = source * sourceFactor + destination * destinationFactor
        /// </summary>
        Add,
        /// <summary>
        /// Color = source * sourceFactor - destination * destinationFactor
        /// </summary>
        Subtract,
        /// <summary>
        /// Color = destination * destinationFactor - source * sourceFactor
        /// </summary>
        ReverseSubtract,
        /// <summary>
        /// Color = min(source, destination)
        /// </summary>
        Min,
        /// <summary>
        /// Color = max(source, destination)
        /// </summary>
        Max
    }
}
