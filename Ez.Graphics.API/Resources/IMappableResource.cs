// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// Represents a resource that can be mapped.
    /// </summary>
    public interface IMappableResource
    {
        /// <summary>
        /// Indicates the memory usage of the resource.
        /// </summary>
        MemoryUsage MemoryUsage { get; }

        /// <summary>
        /// Maps a resource to CPU accessible data region.
        /// <para>
        /// Only avaliable in a resource with <see cref="MemoryUsage.GpuToCpu"/>,
        /// <see cref="MemoryUsage.CpuToGpu"/> or <see cref="MemoryUsage.CpuOnly"/> flag.
        /// </para>
        /// </summary>
        /// <param name="subresource">The subresource to map. (For <see cref="ITexture"/> are 
        /// indexed first by mip slice, then by array layer.)</param>
        /// <returns>A <see cref="IntPtr"/> of mapped memory and its length.</returns>
        [Obsolete("Prefer managed mapping.")]
        (IntPtr Ptr, long Length) Map(uint subresource);

        /// <summary>
        /// Invalidates a data region previously mapped with <see cref="Map(uint)"/>.
        /// </summary>
        /// <param name="subresource">The subresource to unmap. (For <see cref="ITexture"/> are 
        /// indexed first by mip slice, then by array layer.)</param>
        [Obsolete("Prefer managed mapping.")]
        void Unmap(uint subresource);
    }
}
