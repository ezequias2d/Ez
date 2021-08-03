// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Silk.NET.Vulkan;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class ExtensionsHelper
    {
        private readonly IDictionary<string, VulkanExtensionOrLayer> _extensions;
        private readonly IDictionary<string, VulkanExtensionOrLayer> _deviceExtensions;
        private readonly IDictionary<string, VulkanExtensionOrLayer> _layers;
        private readonly IDictionary<string, VulkanExtensionOrLayer> _deviceLayers;
        public ExtensionsHelper(Device device)
        {
            _extensions = new ConcurrentDictionary<string, VulkanExtensionOrLayer>();
            _deviceExtensions = new ConcurrentDictionary<string, VulkanExtensionOrLayer>();
            _layers = new ConcurrentDictionary<string, VulkanExtensionOrLayer>();
            _deviceLayers = new ConcurrentDictionary<string, VulkanExtensionOrLayer>();

            Device = device;

            uint count = 0;
            uint layersCount = 0;
            unsafe
            {
                Device.Vk.EnumerateInstanceExtensionProperties((byte*)null, ref count, null);
                Device.Vk.EnumerateInstanceLayerProperties(ref layersCount, null);
            }
            Span<ExtensionProperties> properties = stackalloc ExtensionProperties[(int)count];
            Span<LayerProperties> layers = stackalloc LayerProperties[(int)layersCount];
            unsafe
            {
                Device.Vk.EnumerateInstanceExtensionProperties((byte*)null, &count, properties);
                Device.Vk.EnumerateInstanceLayerProperties(&layersCount, layers);
            }

            foreach (var p in properties)
            {
                VulkanExtensionOrLayer e = p;
                if (!_extensions.ContainsKey(e.Name))
                    _extensions.Add(e.Name, e);
            }
            foreach (var p in layers)
            {
                VulkanExtensionOrLayer e = p;
                if (!_layers.ContainsKey(e.Name))
                    _layers.Add(e.Name, e);
            }
        }

        public Device Device { get; }

        public IEnumerable<VulkanExtensionOrLayer> InstanceLayers => _layers.Values;
        public IEnumerable<VulkanExtensionOrLayer> InstanceExtensions => _extensions.Values;
        public IEnumerable<VulkanExtensionOrLayer> DeviceLayers => _deviceLayers.Values;
        public IEnumerable<VulkanExtensionOrLayer> DeviceExtensions => _deviceExtensions.Values;

        public bool SupportInstanceExtension(string name) => _extensions.TryGetValue(name, out var value) && !value.IsLayer;

        public bool SupportInstanceLayer(string layer) => _layers.TryGetValue(layer, out var value) && value.IsLayer;

        public bool SupportDeviceExtension(string name) => _deviceExtensions.TryGetValue(name, out var value) && !value.IsLayer;

        public bool SupportDeviceLayer(string layer) => _deviceLayers.TryGetValue(layer, out var value) && value.IsLayer;

        public void LoadDeviceExtensions(PhysicalDevice device)
        {
            foreach (var p in device.GetExtensions())
            {
                VulkanExtensionOrLayer e = p;
                if (!_deviceExtensions.ContainsKey(e.Name))
                    _deviceExtensions.Add(e.Name, e);
            }
            foreach (var p in device.GetLayers())
            {
                VulkanExtensionOrLayer e = p;
                if (!_deviceLayers.ContainsKey(e.Name))
                    _deviceLayers.Add(e.Name, e);
            }
        }
    }
}
