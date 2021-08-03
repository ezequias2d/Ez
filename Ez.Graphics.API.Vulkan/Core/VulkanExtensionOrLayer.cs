// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Silk.NET.Vulkan;
using System;
using System.Runtime.InteropServices;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal struct VulkanExtensionOrLayer
    {
        public VulkanExtensionOrLayer(ExtensionProperties properties)
        {
            unsafe
            {
                Name = Marshal.PtrToStringAnsi((IntPtr)properties.ExtensionName);
                Version = new VkVersion(properties.SpecVersion);
                IsLayer = false;
            }
        }

        public VulkanExtensionOrLayer(LayerProperties properties)
        {
            unsafe
            {
                Name = Marshal.PtrToStringAnsi((IntPtr)properties.LayerName);
                Version = new VkVersion(properties.SpecVersion);
                IsLayer = true;
            }
        }

        public string Name { get; }
        public VkVersion Version { get; }
        public bool IsLayer { get; }

        public static implicit operator VulkanExtensionOrLayer(ExtensionProperties properties) =>
            new VulkanExtensionOrLayer(properties);

        public static implicit operator VulkanExtensionOrLayer(LayerProperties properties) =>
            new VulkanExtensionOrLayer(properties);
    }
}
