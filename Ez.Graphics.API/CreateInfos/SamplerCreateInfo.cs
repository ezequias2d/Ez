// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

using Ez.Graphics.API.Resources;
using Ez.Numerics;

namespace Ez.Graphics.API.CreateInfos
{
    /// <summary>
    /// Describes a <see cref="ISampler"/> object.
    /// <seealso cref="IResourceFactory"/>.
    /// </summary>
    public struct SamplerCreateInfo : IEquatable<SamplerCreateInfo>
    {
        /// <summary>
        /// The <see cref="EdgeSample"/> specifies the addressing mode for outside [0..1] range for U coordinate.
        /// </summary>
        public EdgeSample EdgeSampleU { get; set; }

        /// <summary>
        /// The <see cref="EdgeSample"/> specifies the addressing mode for outside [0..1] range for V coordinate.
        /// </summary>
        public EdgeSample EdgeSampleV { get; set; }

        /// <summary>
        /// The <see cref="EdgeSample"/> specifies the addressing mode for outside [0..1] range for W coordinate.
        /// </summary>
        public EdgeSample EdgeSampleW { get; set; }

        /// <summary>
        /// The <see cref="SamplerFilter"/> specifies the magnification and minification filters to apply to lookups.
        /// </summary>
        public SamplerFilter Filter { get; set; }

        /// <summary>
        /// The optional <see cref="CompareOperation"/> value specifies the comparison function to apply to fetched data 
        /// before filtering.
        /// If null, comparison sampling is not used.
        /// </summary>
        public CompareOperation? CompareOperation { get; set; }

        /// <summary>
        /// The anisotropy value clamp used by the sampler when <see cref="SamplerFilter.Anisotropic"/> is used.
        /// If <see cref="SamplerFilter.Anisotropic"/> is not used, maximum anisotropy is ignored.
        /// </summary>
        public uint MaximumAnisotropy { get; set; }

        /// <summary>
        /// The value is used to clamp the minimum of the computed LOD(level of detail) value.
        /// </summary>
        public uint MinimumLod { get; set; }

        /// <summary>
        /// The value is used to clamp the maximum of the computed LOD(level of detail) value.
        /// </summary>
        public uint MaximumLod { get; set; }

        /// <summary>
        /// The value is the bias to be added to mipmap LOD(level of detail) calculation.
        /// </summary>
        public int LodBias { get; set; }

        /// <summary>
        /// The value specifies the predefined border color to use.
        /// </summary>
        public ColorSingle BorderColor { get; set; }

        /// <summary>
        /// Creates a new <see cref="SamplerCreateInfo"/> struct.
        /// </summary>
        /// <param name="edgeSampleU">The <see cref="EdgeSample"/> specifies the addressing mode for outside [0..1] range for U coordinate.</param>
        /// <param name="edgeSampleV">The <see cref="EdgeSample"/> specifies the addressing mode for outside [0..1] range for V coordinate.</param>
        /// <param name="edgeSampleW">The <see cref="EdgeSample"/> specifies the addressing mode for outside [0..1] range for W coordinate.</param>
        /// <param name="filter">The <see cref="SamplerFilter"/> specifies the magnification and minification filters to apply to lookups.</param>
        /// <param name="comparisonOperation">The optional <see cref="CompareOperation"/> value specifies the comparison function to apply to fetched 
        /// data before filtering. If null, comparison sampling is not used.</param>
        /// <param name="maximumAnisotropy">The anisotropy value clamp used by the sampler when <see cref="SamplerFilter.Anisotropic"/> is used.
        /// If <see cref="SamplerFilter.Anisotropic"/> is not used, maximum anisotropy is ignored.</param>
        /// <param name="minimumLod">The value is used to clamp the minimum of the computed LOD(level of detail) value.</param>
        /// <param name="maximumLod">The value is used to clamp the maximum of the computed LOD(level of detail) value.</param>
        /// <param name="lodBias">The value is the bias to be added to mipmap LOD(level of detail) calculation.</param>
        /// <param name="borderColor">The value specifies the predefined border color to use.</param>
        public SamplerCreateInfo(
            in EdgeSample edgeSampleU,
            in EdgeSample edgeSampleV,
            in EdgeSample edgeSampleW,
            in SamplerFilter filter,
            in CompareOperation? comparisonOperation,
            in uint maximumAnisotropy,
            in uint minimumLod,
            in uint maximumLod,
            in int lodBias,
            in ColorSingle borderColor)
        {
            EdgeSampleU = edgeSampleU;
            EdgeSampleV = edgeSampleV;
            EdgeSampleW = edgeSampleW;
            Filter = filter;
            CompareOperation = comparisonOperation;
            MaximumAnisotropy = maximumAnisotropy;
            MinimumLod = minimumLod;
            MaximumLod = maximumLod;
            LodBias = lodBias;
            BorderColor = borderColor;
        }

        /// <summary>
        /// Settings a nearest filter sampler, with repeat edge sampler.
        /// </summary>
        public static readonly SamplerCreateInfo Nearest = new()
        {
            EdgeSampleU = EdgeSample.Repeat,
            EdgeSampleV = EdgeSample.Repeat,
            EdgeSampleW = EdgeSample.Repeat,
            Filter = SamplerFilter.MinNearest | SamplerFilter.MagNearest | SamplerFilter.MipmapNearest,
            LodBias = 0,
            MinimumLod = 0,
            MaximumLod = uint.MaxValue,
            MaximumAnisotropy = 0,
        };

        /// <summary>
        /// Settings a linear filter sampler, with repeat edge sampler.
        /// </summary>
        public static readonly SamplerCreateInfo Linear = new()
        {
            EdgeSampleU = EdgeSample.Repeat,
            EdgeSampleV = EdgeSample.Repeat,
            EdgeSampleW = EdgeSample.Repeat,
            Filter = SamplerFilter.MinLinear | SamplerFilter.MagLinear | SamplerFilter.MipmapLinear,
            LodBias = 0,
            MinimumLod = 0,
            MaximumLod = uint.MaxValue,
            MaximumAnisotropy = 0,
        };

        /// <summary>
        /// Settings a 4X anisotropic linear filter sampler, with repeat edge sampler.
        /// </summary>
        public static readonly SamplerCreateInfo AnisotropicLinear4 = new()
        {
            EdgeSampleU = EdgeSample.Repeat,
            EdgeSampleV = EdgeSample.Repeat,
            EdgeSampleW = EdgeSample.Repeat,
            Filter = SamplerFilter.MinLinear | SamplerFilter.MagLinear | SamplerFilter.MipmapLinear | SamplerFilter.Anisotropic,
            LodBias = 0,
            MinimumLod = 0,
            MaximumLod = uint.MaxValue,
            MaximumAnisotropy = 4,
        };

        /// <summary>
        /// Returns a value that indicates whether this instance and another 
        /// <see cref="SamplerCreateInfo"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="SamplerCreateInfo"/>.</param>
        /// <returns><see langword="true"/> if the two are equal;
        /// otherwise, <see langword="false"/>.</returns>
        public bool Equals(SamplerCreateInfo other) =>
             EdgeSampleU == other.EdgeSampleU
                && EdgeSampleV == other.EdgeSampleV
                && EdgeSampleW == other.EdgeSampleW
                && Filter == other.Filter
                && CompareOperation.GetValueOrDefault() == other.CompareOperation.GetValueOrDefault()
                && MaximumAnisotropy == other.MaximumAnisotropy
                && MinimumLod == other.MinimumLod
                && MaximumLod == other.MaximumLod
                && LodBias == other.LodBias
                && BorderColor == other.BorderColor;


        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() =>
            HashHelper<SamplerCreateInfo>.Combine(
                EdgeSampleU,
                EdgeSampleV,
                EdgeSampleW,
                Filter,
                CompareOperation,
                MaximumAnisotropy,
                MinimumLod,
                MaximumLod,
                LodBias,
                BorderColor);

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is SamplerCreateInfo sci && Equals(sci);

        /// <inheritdoc/>
        public override string ToString() =>
            $"(EdgeSampleU: {EdgeSampleU}, EdgeSampleV: {EdgeSampleV}, " +
            $"EdgeSampleW: {EdgeSampleW}, Filter: {Filter} " +
            $"ComparisonFunction: {CompareOperation}, MaximumAnisotropy: {MaximumAnisotropy}, " +
            $"MaximumAnisotropy: {MinimumLod}, MaximumAnisotropy: {MaximumLod}, " +
            $"MaximumAnisotropy: {LodBias}, MaximumAnisotropy: {BorderColor})";

        /// <summary>
        /// Compare two <see cref="SamplerCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(SamplerCreateInfo left, SamplerCreateInfo right) =>
            left.Equals(right);


        /// <summary>
        /// Compare two <see cref="SamplerCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(SamplerCreateInfo left, SamplerCreateInfo right) =>
            !(left == right);
    }
}
