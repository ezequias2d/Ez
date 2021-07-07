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
    /// A structure to pass in <see cref="IDevice"/> creation.
    /// </summary>
    public struct DeviceCreateInfo : IEquatable<DeviceCreateInfo>
    {
        /// <summary>
        /// Indicates that <see cref="IDevice"/> has the debug features enables.
        /// </summary>
        public bool Debug { get; set; }

        #region Swapchain

        /// <summary>
        /// The optional format of the depth target of the <see cref="ISwapchain"/>'s <see cref="IFramebuffer"/>.
        /// If non-null, this must be a valid depth <see cref="ITexture"/> format.
        /// If null, then no depth target will be created.
        /// </summary>
        public PixelFormat? DepthFormat { get; set; }

        /// <summary>
        /// Indicates whether presentation of the <see cref="ISwapchain"/> will be synchronized to the window system's vertical refresh rate.
        /// </summary>
        public bool IsVSync { get; set; }

        /// <summary>
        /// Indicates whether the color target of the <see cref="ISwapchain"/> will use an sRGB <see cref="PixelFormat"/>.
        /// </summary>
        public bool IsSrgbFormat { get; set; }
        #endregion

        /// <summary>
        /// Creates a new instance of <see cref="DeviceCreateInfo"/> struct.
        /// </summary>
        /// <param name="debug">Indicates that <see cref="IDevice"/> has the debug features enables.</param>
        /// <param name="depthFormat">The optional format of the depth target of the <see cref="ISwapchain"/>'s <see cref="IFramebuffer"/>. If non-null, this must be a valid depth <see cref="ITexture"/> format. If null, then no depth target will be created.</param>
        /// <param name="isVSync">Indicates whether presentation of the Swapchain will be synchronized to the window system's vertical refresh rate.</param>
        /// <param name="isSrgbFormat">Indicates whether the color target of the Swapchain will use an sRGB <see cref="PixelFormat"/>.</param>
        public DeviceCreateInfo(
            bool debug,
            PixelFormat? depthFormat, 
            bool isVSync, 
            bool isSrgbFormat)
        {
            (Debug,
                DepthFormat,
                IsVSync,
                IsSrgbFormat) = 
                    (debug, 
                        depthFormat, 
                        isVSync, 
                        isSrgbFormat);
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<DeviceCreateInfo>.Combine(Debug, 
                DepthFormat, 
                IsVSync, 
                IsSrgbFormat);

        /// <inheritdoc/>
        public bool Equals(DeviceCreateInfo other) =>
            Debug == other.Debug &&
            DepthFormat == other.DepthFormat &&
            IsVSync == other.IsVSync &&
            IsSrgbFormat == other.IsSrgbFormat;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is DeviceCreateInfo dci && Equals(dci);

        /// <inheritdoc/>
        public override string ToString() =>
            $"(Debug: {Debug}, DepthFormat: {DepthFormat}, IsVSync: {IsVSync}, IsSrgbFormat: {IsSrgbFormat}";

        /// <summary>
        /// Compare two <see cref="DeviceCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(DeviceCreateInfo left, DeviceCreateInfo right) =>
            left.Equals(right);


        /// <summary>
        /// Compare two <see cref="DeviceCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(DeviceCreateInfo left, DeviceCreateInfo right) =>
            !(left == right);
    }
}
