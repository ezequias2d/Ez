// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Vulkan.Core;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using System;
using VkPhysicalDevice = Silk.NET.Vulkan.PhysicalDevice;
namespace Ez.Graphics.API.Vulkan
{
    public class PhysicalDevice : DeviceResource
    {
        internal PhysicalDevice(in Device device, in Silk.NET.Vulkan.PhysicalDevice pd) : base(device)
        {
            VkPhysicalDevice = pd;

            device.Vk.GetPhysicalDeviceFeatures(pd, out var features);
            VkFeatures = features;

            Features = new PhysicalDeviceFeatures(
                features.GeometryShader,
                features.TessellationShader,
                features.MultiViewport,
                features.DrawIndirectFirstInstance,
                features.FillModeNonSolid,
                features.SamplerAnisotropy,
                features.DepthClamp,
                features.IndependentBlend,
                device.Extensions.SupportInstanceExtension(ExtDebugUtils.ExtensionName),
                features.ShaderFloat64);

            ObjectType = ObjectType.PhysicalDevice;
            ObjectHandle = (ulong)VkPhysicalDevice.Handle;
        }

        internal VkPhysicalDevice VkPhysicalDevice { get; }
        internal Silk.NET.Vulkan.PhysicalDeviceFeatures VkFeatures { get; }
        public PhysicalDeviceFeatures Features { get; }

        internal ReadOnlySpan<QueueFamilyProperties> GetQueueFamilyProperties()
        {
            Span<uint> count = stackalloc uint[1];
            Device.Vk.GetPhysicalDeviceQueueFamilyProperties(VkPhysicalDevice, count, Span<QueueFamilyProperties>.Empty);

            Span<QueueFamilyProperties> values = new QueueFamilyProperties[count[0]];
            Device.Vk.GetPhysicalDeviceQueueFamilyProperties(VkPhysicalDevice, count, values);
            return values.Slice(0, (int)count[0]);
        }

        internal unsafe ReadOnlySpan<ExtensionProperties> GetExtensions()
        {
            var count = 0u;
            Device.Vk.EnumerateDeviceExtensionProperties(VkPhysicalDevice, (byte*)null, ref count, null);

            var extensions = new ExtensionProperties[count];
            Device.Vk.EnumerateDeviceExtensionProperties(VkPhysicalDevice, (byte*)null, &count, extensions);
            return extensions;
        }

        internal unsafe ReadOnlySpan<LayerProperties> GetLayers()
        {
            var count = 0u;
            Device.Vk.EnumerateDeviceLayerProperties(VkPhysicalDevice, ref count, null);

            var layers = new LayerProperties[count];
            Device.Vk.EnumerateDeviceLayerProperties(VkPhysicalDevice, &count, layers);

            return layers;
        }

        protected override void UnmanagedDispose()
        {
        }

        protected override void ManagedDispose()
        {
        }

        public static implicit operator VkPhysicalDevice(PhysicalDevice vkd) =>
            vkd.VkPhysicalDevice;
    }
}
