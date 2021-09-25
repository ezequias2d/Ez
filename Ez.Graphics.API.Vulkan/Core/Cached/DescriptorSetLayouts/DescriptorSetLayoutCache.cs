// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Vulkan.Core.Cached;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class DescriptorSetLayoutCache : Cache<DescriptorSetLayoutCreateInfo, DescriptorSetLayout>
    {
        public DescriptorSetLayoutCache(Device device)
        {
            Device = device;
        }

        private Device Device { get; }

        public override DescriptorSetLayout CreateCached(in DescriptorSetLayoutCreateInfo createInfo) =>
            new(Device, createInfo);
    }
}
