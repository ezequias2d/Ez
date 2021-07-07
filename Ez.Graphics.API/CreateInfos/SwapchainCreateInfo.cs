// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Drawing;

using Ez.Graphics.API.Resources;
using Ez.Graphics.Context;
using Ez.Numerics;

namespace Ez.Graphics.API.CreateInfos
{
    /// <summary>
    /// Describes a <see cref="ISwapchain"/> object.
    /// 
    /// <seealso cref="IResourceFactory"/>
    /// </summary>
    public struct SwapchainCreateInfo : IEquatable<SwapchainCreateInfo>
    {
        /// <summary>
        /// The <see cref="ISwapchainSource"/> for render operations.
        /// </summary>
        public ISwapchainSource Source { get; set; }

        /// <summary>
        /// The size of the <see cref="ISwapchain"/> surface.
        /// </summary>
        public Size Size { get; set; }

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

        /// <summary>
        /// Creates a new instance of<see cref= "SwapchainCreateInfo" /> struct.
        /// </summary>
        /// <param name="source">The <see cref="ISwapchainSource"/> for render operations.</param>
        /// <param name="size">The size of the <see cref="ISwapchain"/> surface.</param>
        /// <param name="depthFormat">The optional format of the depth target of the <see cref="ISwapchain"/>'s <see cref="IFramebuffer"/>.
        /// If non-null, this must be a valid depth <see cref="ITexture"/> format.
        /// If null, then no depth target will be created.</param>
        /// <param name="isVSync">Indicates whether presentation of the <see cref="ISwapchain"/> will be synchronized to the window system's
        /// vertical refresh rate.</param>
        /// <param name="isSrgbFormat">Indicates whether the color target of the <see cref="ISwapchain"/> will use an sRGB <see cref="PixelFormat"/>.</param>
        public SwapchainCreateInfo(ISwapchainSource source, Size size, PixelFormat? depthFormat, bool isVSync, bool isSrgbFormat)
        {
            Source = source;
            Size = size;
            DepthFormat = depthFormat;
            IsVSync = isVSync;
            IsSrgbFormat = isSrgbFormat;
        }

        /// <summary>
        /// Creates a new instance of<see cref= "SwapchainCreateInfo" /> struct.
        /// </summary>
        /// <param name="source">The <see cref="ISwapchainSource"/> for render operations.</param>
        /// <param name="size">The size of the <see cref="ISwapchain"/> surface.</param>
        /// <param name="depthFormat">The optional format of the depth target of the <see cref="ISwapchain"/>'s <see cref="IFramebuffer"/>.
        /// If non-null, this must be a valid depth <see cref="ITexture"/> format.
        /// If null, then no depth target will be created.</param>
        /// <param name="isVSync">Indicates whether presentation of the <see cref="ISwapchain"/> will be synchronized to the window system's
        /// vertical refresh rate.</param>
        public SwapchainCreateInfo(ISwapchainSource source, Size size, PixelFormat? depthFormat, bool isVSync) :
            this(source, size, depthFormat, isVSync, false)
        {

        }

        /// <inheritdoc/>
        public bool Equals(SwapchainCreateInfo other) =>
            Source == other.Source &&
            Size == other.Size &&
            DepthFormat == other.DepthFormat &&
            IsVSync == other.IsVSync &&
            IsSrgbFormat == other.IsSrgbFormat;

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashHelper<SwapchainCreateInfo>.Combine(Source, Size, DepthFormat, IsVSync, IsSrgbFormat);

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is SwapchainCreateInfo sci && Equals(sci);

        /// <inheritdoc/>
        public override string ToString() => 
            $"(Source: {Source}, Size: {Size}, " +
            $"DepthFormat: {DepthFormat}, IsVSync: {IsVSync}, " +
            $"IsSrgbColor: {IsSrgbFormat})";


        /// <summary>
        /// Compare two <see cref="SwapchainCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are equals, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(SwapchainCreateInfo left, SwapchainCreateInfo right) =>
            left.Equals(right);


        /// <summary>
        /// Compare two <see cref="SwapchainCreateInfo"/> structures.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if are not equals, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(SwapchainCreateInfo left, SwapchainCreateInfo right) =>
            !(left == right);
    }
}
