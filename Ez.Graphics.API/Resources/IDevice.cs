                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     // Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;

using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Provides a device interface to implement this <see cref="Ez.Graphics.API"/>.
    /// </summary>
    public interface IDevice : IDisposable
    {
        #region samplers
        /// <summary>
        /// Gets a simple point-filtered <see cref="ISampler">sampler</see> resource owned by
        /// this <see cref="IDevice">device</see>.
        /// This resource is created with <see cref="SamplerCreateInfo.Nearest"/>.
        /// </summary>
        ISampler NearestSampler { get; }

        /// <summary>
        /// Gets a simple linear-filtered <see cref="ISampler">sampler</see> resource owned by
        /// this <see cref="IDevice">device</see>.
        /// This resource is created with <see cref="SamplerCreateInfo.Linear"/>.
        /// </summary>
        ISampler LinearSampler { get; }

        /// <summary>
        /// Gets a simple 4x anisotropic-filtered <see cref="ISampler">sampler</see> resource owned 
        /// by this <see cref="IDevice">device</see>.
        /// This resource is created with <see cref="SamplerCreateInfo.AnisotropicLinear4"/>.
        /// </summary>
        ISampler AnisotropicLinear4Sampler { get; }
        #endregion

        /// <summary>
        /// Gets a value that indicating whether this <see cref="IDevice"/> has been disposed.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// The minimum required alignment, in bytes, for the uniform buffers offset.
        /// The offset must be a multiple of this value.
        /// </summary>
        uint MinUniformBufferOffsetAlignment { get; }

        /// <summary>
        /// the minimum required alignment, in bytes, for the storage buffers offset.
        /// Offset must be a multiple of this value.
        /// </summary>
        uint MinStorageBufferOffsetAlignment { get; }

        /// <summary>
        /// Gets the <see cref="Factory">factory</see> of this instance.
        /// </summary>
        IFactory Factory { get; }

        /// <summary>
        /// Gets the default <see cref="ISwapchain"/> for this device if it has.
        /// </summary>
        ISwapchain DefaultSwapchain { get; }

        /// <summary>
        /// Gets a object that describing the fine-grained features that can be supported by
        /// an implementation.
        /// </summary>
        PhysicalDeviceFeatures Features { get; }

        /// <summary>
        /// Tries to get the properties of a specific combination of <see cref="PixelFormat">format</see>, 
        /// <see cref="TextureType">type</see> and <see cref="TextureUsage">usage</see>.
        /// </summary>
        /// <param name="format">The <see cref="PixelFormat"/> to query.</param>
        /// <param name="type">The <see cref="TextureType"/> to query.</param>
        /// <param name="usage">The <see cref="TextureUsage"/> to query.</param>
        /// <param name="properties">The limits of a Texture created using the given combination of attributes.</param>
        /// <returns><see langword="true"/> if the given combination is supported, otherwise <see langword="false"/>. 
        /// If the combination is supported, then <paramref name="properties"/> contains the supported properties of the
        /// combination.</returns>
        bool TryGetPixelFormatProperties(PixelFormat format, TextureType type, TextureUsage usage, TextureTiling tiling, out PixelFormatProperties properties);

        /// <summary>
        /// Wait for one or more fences to become signaled.
        /// </summary>
        /// <param name="fences">An array of <see cref="IFence">fences</see>.</param>
        /// <param name="waitAll">The condition that must be satisfied to successfully unblock the 
        /// wait. If <paramref name="waitAll"/> is <see langword="true"/>, then the condition is that 
        /// all fences in <paramref name="fences"/> are signaled. Otherwise, the condition is that at 
        /// least one fence in <paramref name="fences"/> is signaled.</param>
        /// <param name="timeout">The waiting timeout period in units of nanoseconds.</param>
        /// <returns><see langword="true"/> if the condition was satisfied. <see langword="false"/> if 
        /// the timeout was reached.</returns>
        bool WaitForFences(IEnumerable<IFence> fences, bool waitAll, ulong timeout);

        /// <summary>
        /// Wait for one or more fences to become signaled.
        /// </summary>
        /// <param name="fences">An array of <see cref="IFence">fences</see>.</param>
        /// <param name="waitAll">The condition that must be satisfied to successfully unblock the 
        /// wait. If <paramref name="waitAll"/> is <see langword="true"/>, then the condition is that 
        /// all fences in <paramref name="fences"/> are signaled. Otherwise, the condition is that at 
        /// least one fence in <paramref name="fences"/> is signaled.</param>
        /// <param name="timeout">The waiting timeout period.</param>
        /// <returns><see langword="true"/> if the condition was satisfied. <see langword="false"/> if 
        /// the timeout was reached.</returns>
        bool WaitForFences(IEnumerable<IFence> fences, bool waitAll, TimeSpan timeout);

        /// <summary>
        /// Submits a command buffer to execute.
        /// </summary>
        /// <param name="commandBuffer">A command buffer to submit.</param>
        void Submit(ICommandBuffer commandBuffer);

        /// <summary>
        /// Submits a command buffer to execute with a fence.
        /// </summary>
        /// <param name="commandBuffer">A command buffer to submit.</param>
        /// <param name="fence">The fence to submit with the command buffer.</param>
        void Submit(ICommandBuffer commandBuffer, IFence fence);

        /// <summary>
        /// Waits all submitted <see cref="ICommandBuffer"/> objects have fully completed.
        /// </summary>
        void Wait();
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 