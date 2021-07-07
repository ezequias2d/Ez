// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// Represents a pipeline resource.
    /// </summary>
    public interface IPipeline : IResource
    {
        /// <summary>
        /// Indicates that the pipeline is a graphic pipeline.
        /// </summary>
        bool IsGraphicPipeline { get; }
    }
}
