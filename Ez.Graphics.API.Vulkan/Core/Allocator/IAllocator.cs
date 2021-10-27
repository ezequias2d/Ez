// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;
using System.Threading.Tasks;
using VkBuffer = Silk.NET.Vulkan.Buffer;
using VkImage = Silk.NET.Vulkan.Image;
namespace Ez.Graphics.API.Vulkan.Core.Allocator
{
    internal interface IAllocator : IResource
    {
        IAllocation CreateTexture(VkImage texture, TextureCreateInfo textureCreateInfo, bool isDedicatedAllocation);
        IAllocation CreateBuffer(VkBuffer buffer, BufferCreateInfo bufferCreateInfo, bool isDedicatedAllocation);

        Task<IAllocation> CreateTextureAsync(VkImage texture, TextureCreateInfo textureCreateInfo, bool isDedicatedAllocation);

        Task<IAllocation> CreateBufferAsync(VkBuffer buffer, BufferCreateInfo bufferCreateInfo, bool isDedicatedAllocation);
    }
}
