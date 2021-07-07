// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// Represents a sampler resource that is used to sampler 
    /// a texture.
    /// </summary>
    public interface ISampler : IResource
    {
        /// <summary>
        /// Gets the <see cref="EdgeSample"/> that specifies the addressing mode for outside [0..1] range for U coordinate.
        /// </summary>
        EdgeSample EdgeSampleU { get; }

        /// <summary>
        /// Gets the <see cref="EdgeSample"/> that specifies the addressing mode for outside [0..1] range for V coordinate.
        /// </summary>
        EdgeSample EdgeSampleV { get; }

        /// <summary>
        /// Gets the <see cref="EdgeSample"/> that specifies the addressing mode for outside [0..1] range for W coordinate.
        /// </summary>
        EdgeSample EdgeSampleW { get; }

        /// <summary>
        /// Gets the <see cref="SamplerFilter"/> that specifies the magnification and minification filters to apply to lookups.
        /// </summary>
        SamplerFilter Filter { get; }

        /// <summary>
        /// Gets the optional <see cref="CompareOperation"/> value specifies the comparison function to apply to fetched data 
        /// before filtering.
        /// If null, comparison sampling is not used.
        /// </summary>
        CompareOperation? CompareOperation { get; }

        /// <summary>
        /// Gets the anisotropy value that specifies the clamp used by the sampler when <see cref="SamplerFilter.Anisotropic"/>
        /// is used. If <see cref="SamplerFilter.Anisotropic"/> is not used, maximum anisotropy is ignored.
        /// </summary>
        uint MaximumAnisotropy { get; }

        /// <summary>
        /// Gets the value that specifies the minimum value of clamp the computed LOD(level of detail) value.
        /// </summary>
        uint MinimumLod { get; }

        /// <summary>
        /// Gets the value that specifies the maximum value of clamp the computed LOD(level of detail) value.
        /// </summary>
        uint MaximumLod { get; }

        /// <summary>
        /// Gets the value that specifies the bias to add to mipmap LOD(level of detail) calculation.
        /// </summary>
        int LodBias { get; }

        /// <summary>
        /// Gets the value that specifies the predefined border color used.
        /// </summary>
        ColorSingle BorderColor { get; }
    }
}
