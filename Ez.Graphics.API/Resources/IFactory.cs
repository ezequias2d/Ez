// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Ez.Graphics.API.CreateInfos;

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// Provides a interface to create <see cref="IResource">resources</see> of a
    /// <see cref="IDevice"/>.
    /// </summary>
    public interface IFactory : IDisposable
    {
        /// <summary>
        /// The <see cref="IDevice">device</see> that owns this <see cref="IFactory">factory</see>.
        /// </summary>
        IDevice Device { get; }

        /// <summary>
        /// Get <see cref="IEnumerable{IDeviceResource}"/> with all resources created and not released by this <see cref="IFactory"/>.
        /// </summary>
        IEnumerable<IResource> Resources { get; }

        /// <summary>
        /// Try get a resource of type T(<see cref="IResource"/>) with the specific name.
        /// </summary>
        /// <typeparam name="T">The type of resource sought.</typeparam>
        /// <param name="name">Searched resource name.</param>
        /// <param name="resource">The resource found, if not found, resource will be default value.</param>
        /// <returns>True if found, otherwise false.</returns>
        bool TryGetResource<T>(string name, out T resource) where T : IResource;


        /// <summary>
        /// Creates a new <see cref="IPipeline"/> instance from <paramref name="shaders"/>.
        /// </summary>
        /// <param name="shaders">The shaders of the pipeline.</param>
        /// <returns>A new graphics <see cref="IPipeline"/>.</returns>
        IPipeline CreateGraphicsPipeline(params IShader[] shaders);

        /// <summary>
        /// Creates a new <see cref="IPipeline"/> instance from a <paramref name="createInfo"/>.
        /// </summary>
        /// <param name="createInfo">A structure containing parameters to be used to create the
        /// <see cref="IPipeline"/>.</param>
        /// <returns>A new <see cref="IPipeline"/>.</returns>
        IPipeline CreatePipeline(in PipelineCreateInfo createInfo);

        /// <summary>
        /// Create a new <see cref="ISampler"/> instance from a <paramref name="samplerCreateInfo"/>.
        /// </summary>
        /// <param name="samplerCreateInfo">A structure containing parameters to be 
        /// used to create the <see cref="ISampler"/>.</param>
        /// <returns>A new <see cref="ISampler"/> described by a <see cref="SamplerCreateInfo"/>.</returns>
        ISampler CreateSampler(in SamplerCreateInfo samplerCreateInfo);

        /// <summary>
        /// Creates a new <see cref="IBuffer"/> instance from a <paramref name="bufferCreateInfo"/>.
        /// </summary>
        /// <typeparam name="T">The type of data in buffer.</typeparam>
        /// <param name="bufferCreateInfo">A structure containing parameters to be 
        /// used to create the <see cref="IBuffer"/>.</param>
        /// <returns>A new <see cref="IBuffer"/> described by a <see cref="BufferCreateInfo"/>.</returns>
        Task<IBuffer> CreateBuffer(BufferCreateInfo bufferCreateInfo);

        /// <summary>
        /// Creates a new <see cref="ITexture"/> instance from a <see cref="TextureCreateInfo"/>.
        /// </summary>
        /// <param name="textureCreateInfo">A structure containing parameters to be 
        /// used to create the <see cref="ITexture"/>.</param>
        /// <returns>A <see cref="ITexture"/> described by a <see cref="TextureCreateInfo"/>.</returns>
        ITexture CreateTexture(in TextureCreateInfo textureCreateInfo);

        /// <summary>
        /// Creates a new <see cref="ITextureView"/> instance from a <paramref name="textureViewCreateInfo"/>.
        /// </summary>
        /// <param name="textureViewCreateInfo">A structure containing parameters to be 
        /// used to create the <see cref="ITextureView"/>.</param>
        /// <returns>A <see cref="ITextureView"/> described by a <see cref="TextureCreateInfo"/>.</returns>
        ITextureView CreateTextureView(in TextureViewCreateInfo textureViewCreateInfo);

        /// <summary>
        /// Create a new <see cref="IFramebuffer"/> instance from a <see cref="FramebufferCreateInfo"/>.
        /// </summary>
        /// <param name="descripton">A structure containing parameters to be 
        /// used to create the <see cref="IFramebuffer"/>.</param>
        /// <returns>A <see cref="IFramebuffer"/> described by a <see cref="FramebufferCreateInfo"/>.</returns>
        IFramebuffer CreateFramebuffer(in FramebufferCreateInfo descripton);

        /// <summary>
        /// Creates a new <see cref="ISwapchain"/> instance from the <see cref="SwapchainCreateInfo"/>.
        /// </summary>
        /// <param name="description">A structure containing parameters to be 
        /// used to create the <see cref="ISw"/>.</param>
        /// <returns>A <see cref="ISwapchain"/> described by a <see cref="SwapchainCreateInfo"/>.</returns>
        ISwapchain CreateSwapchain(in SwapchainCreateInfo description);

        /// <summary>
        /// Create a new <see cref="ICommandBuffer"/> object.
        /// </summary>
        /// <returns>A <see cref="ICommandBuffer"/> object.</returns>
        ICommandBuffer CreateCommandBuffer();

        /// <summary>
        /// Create a new <see cref="IShader"/> object from the <see cref="ShaderCreateInfo"/>.
        /// </summary>
        /// <param name="description">The descripton for a <see cref="IShader"/>.</param>
        /// <returns>A <see cref="IShader"/> described by a <see cref="ShaderCreateInfo"/></returns>
        IShader CreateShader(in ShaderCreateInfo description);

        /// <summary>
        /// Creates a new <see cref="IFence"/>.
        /// </summary>
        /// <returns>A new <see cref="IFence"/>.</returns>
        IFence CreateFence();        
    }
}
