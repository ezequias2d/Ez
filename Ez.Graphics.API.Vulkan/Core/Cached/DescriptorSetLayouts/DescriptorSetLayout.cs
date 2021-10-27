// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Memory;
using Silk.NET.Vulkan;
using System;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal unsafe class DescriptorSetLayout : DeviceResource
    {
        public DescriptorSetLayout(Device device, in DescriptorSetLayoutCreateInfo createInfo) : base(device)
        {
            var aux = createInfo.Bindings.Span;
            var bindings = stackalloc DescriptorSetLayoutBinding[aux.Length];

            for (var i = 0; i < aux.Length; i++)
                bindings[i] = new()
                {
                    Binding = aux[i].Binding,
                    DescriptorType = ToDescriptorType(aux[i].SetType),
                    DescriptorCount = 1,
                    StageFlags = aux[i].ShaderStages.ToVk(),
                };

            var bindingsArray = new DescriptorSetLayoutBinding[aux.Length];
            MemUtil.Copy<DescriptorSetLayoutBinding>(bindingsArray, (IntPtr)bindings);
            Bindings = bindingsArray;

            var ci = new Silk.NET.Vulkan.DescriptorSetLayoutCreateInfo
            {
                SType = StructureType.DescriptorSetLayoutCreateInfo,
                BindingCount = (uint)aux.Length,
                PBindings = bindings,
            };


            if (Device.Vk.CreateDescriptorSetLayout(Device, ci, null, out var setLayout) != Result.Success)
                throw new VkException();

            VkDescriptorSetLayout = setLayout;

            DescriptorPool = new DescriptorPool(Device, this);

            ObjectHandle = VkDescriptorSetLayout.Handle;
            ObjectType = ObjectType.DescriptorSetLayout;
            this.SetDefaultName();
        }

        private Silk.NET.Vulkan.DescriptorSetLayout VkDescriptorSetLayout { get; }
        public ReadOnlyMemory<DescriptorSetLayoutBinding> Bindings { get; }
        public DescriptorPool DescriptorPool { get; }

        public bool TryGetLayoutBinding(uint binding, out DescriptorSetLayoutBinding layoutBinding)
        {
            foreach (var temp in Bindings.Span)
                if (temp.Binding == binding)
                {
                    layoutBinding = temp;
                    return true;
                }
            layoutBinding = default;
            return false;
        }

        private static DescriptorType ToDescriptorType(in SetType setType) =>
            setType switch
            {
                SetType.SampledTexture => DescriptorType.SampledImage,
                SetType.Sampler => DescriptorType.Sampler,
                SetType.StorageBuffer => DescriptorType.StorageBuffer,
                SetType.StorageTexture => DescriptorType.StorageImage,
                SetType.UniformBuffer => DescriptorType.UniformBuffer,
                _ => throw new NotSupportedException()
            };

        protected override void UnmanagedDispose()
        {
            Device.Vk.DestroyDescriptorSetLayout(Device, VkDescriptorSetLayout, null);
        }

        protected override void ManagedDispose()
        {
        }

        public DescriptorSet AllocateDescriptorSet() => DescriptorPool.AllocateDescriptorSet();

        public static implicit operator Silk.NET.Vulkan.DescriptorSetLayout(DescriptorSetLayout layout)
        {
            layout.CheckDispose();
            return layout.VkDescriptorSetLayout;
        }
    }
}
