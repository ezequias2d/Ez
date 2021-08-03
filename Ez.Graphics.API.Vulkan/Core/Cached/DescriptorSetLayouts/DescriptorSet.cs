// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Silk.NET.Vulkan;
using VkDescriptorSet = Silk.NET.Vulkan.DescriptorSet;
namespace Ez.Graphics.API.Vulkan.Core
{
    internal class DescriptorSet : DeviceResource
    {
        private readonly VkDescriptorSet _handle;

        public DescriptorSet(Device device, VkDescriptorSet handle, DescriptorPoolNode pool, DescriptorSetLayout layout) : base(device)
        {
            Pool = pool;
            _handle = handle;
            Layout = layout;

            ObjectHandle = _handle.Handle;
            ObjectType = ObjectType.DescriptorSet;
        }

        public DescriptorPoolNode Pool { get; }
        public DescriptorSetLayout Layout { get; }


        protected override void ManagedDispose()
        {

        }

        protected override void UnmanagedDispose()
        {
            Pool.FreeDescriptorSet(this);
        }

        public static implicit operator VkDescriptorSet(DescriptorSet set) =>
            set._handle;
    }
}
