// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Vulkan.Core;
using Ez.Graphics.Context.SwapchainSources;
using Ez.Graphics.Contexts;
using Microsoft.Extensions.Logging;
using Silk.NET.Core;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using Silk.NET.Vulkan.Extensions.KHR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using VkInstance = Silk.NET.Vulkan.Instance;
namespace Ez.Graphics.API.Vulkan
{
    internal unsafe class Instance : DeviceResource
    {
        private readonly static string ValidationLayer = "VK_LAYER_KHRONOS_validation";
        private DebugUtilsMessengerEXT _debugMessenger;

        public Instance(Device device, VulkanRequiredExtensions context, bool enabledValidationLayers) : base(device)
        {
            if (enabledValidationLayers && !Device.Extensions.SupportInstanceLayer(ValidationLayer))
                throw new VkException("Validation layers requested, but not availabled!");

            EnabledExtensions = context.RequiredInstanceExtensions.ToArray();

            Utf8FixedString[] extensions = new Utf8FixedString[EnabledExtensions.Length];
            byte** ptrExtensions = stackalloc byte*[EnabledExtensions.Length];

            for (var i = 0; i < extensions.Length; i++)
            {
                extensions[i] = EnabledExtensions[i];
                ptrExtensions[i] = extensions[i];
            }

            Utf8FixedString applicationName = "Ez.Graphics.API";
            Utf8FixedString engineName = "Ez.Graphics.API";

            var appInfo = new ApplicationInfo
            {
                SType = StructureType.ApplicationInfo,
                PApplicationName = applicationName,
                ApplicationVersion = new Version32(1, 0, 0),
                PEngineName = engineName,
                EngineVersion = new Version32(0, 1, 1),
                ApiVersion = Vk.Version11,
            };
            var ici = new InstanceCreateInfo
            {
                SType = StructureType.InstanceCreateInfo,
                PApplicationInfo = &appInfo,
                EnabledExtensionCount = (uint)EnabledExtensions.Length,
                PpEnabledExtensionNames = ptrExtensions
            };

            Utf8FixedString validationLayer = ValidationLayer;
            byte** validationLayers = stackalloc byte*[1];
            validationLayers[0] = validationLayer;

            if (enabledValidationLayers)
            {
                ici.EnabledLayerCount = 1;
                ici.PpEnabledLayerNames = validationLayers;
                EnabledLayers = new string[] { ValidationLayer };
            }
            else
            {
                ici.EnabledLayerCount = 0;
                ici.PpEnabledLayerNames = null;
                EnabledLayers = new string[] { };
            }

            if (Device.Vk.CreateInstance(ici, null, out var handle) != Result.Success)
                throw new VkException("Vulkan Error: Failed to create the instance.");

            VkInstance = handle;

            if (enabledValidationLayers && Device.Vk.TryGetInstanceExtension<ExtDebugUtils>(VkInstance, out var debugUtils))
            {
                DebugUtils = debugUtils;
                SetupDebugMessager();
            }

            ObjectHandle = (ulong)VkInstance.Handle;
            ObjectType = ObjectType.Instance;
        }

        public VkInstance VkInstance { get; }

        public ExtDebugUtils DebugUtils { get; }

        private void SetupDebugMessager()
        {
            var ci = new DebugUtilsMessengerCreateInfoEXT
            {
                SType = StructureType.DebugUtilsMessengerCreateInfoExt,
                MessageSeverity = DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityVerboseBitExt | DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityWarningBitExt | DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityErrorBitExt | DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityInfoBitExt,
                MessageType = DebugUtilsMessageTypeFlagsEXT.DebugUtilsMessageTypeGeneralBitExt | DebugUtilsMessageTypeFlagsEXT.DebugUtilsMessageTypeValidationBitExt | DebugUtilsMessageTypeFlagsEXT.DebugUtilsMessageTypePerformanceBitExt,
                PfnUserCallback = (PfnDebugUtilsMessengerCallbackEXT)DebugCallback,
                PUserData = null,
            };

            if (DebugUtils.CreateDebugUtilsMessenger(VkInstance, ci, null, out _debugMessenger) != Result.Success)
                throw new VkException("Failed to set up debug messenger!");
        }

        public string[] EnabledExtensions { get; }
        public string[] EnabledLayers { get; }

        private unsafe uint DebugCallback(DebugUtilsMessageSeverityFlagsEXT messageSeverity, DebugUtilsMessageTypeFlagsEXT messageTypes, DebugUtilsMessengerCallbackDataEXT* pCallbackData, void* pUserData)
        {
            if (Device.Logger == null)
                return Vk.True;

            var message = Marshal.PtrToStringAnsi((IntPtr)pCallbackData->PMessage);
            switch (messageSeverity)
            {
                case DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityErrorBitExt:
                    Device.Logger.LogError(message);
                    break;
                case DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityInfoBitExt:
                    Device.Logger.LogInformation(message);
                    break;
                case DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityVerboseBitExt:
                    Device.Logger.LogDebug(message);
                    break;
                case DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityWarningBitExt:
                    Device.Logger.LogWarning(message);
                    break;
            }


            return Vk.False;
        }

        public IReadOnlyCollection<PhysicalDevice> GetPhysicalDevices()
        {
            var ds = Device.Vk.GetPhysicalDevices(VkInstance);

            List<PhysicalDevice> vpds = new List<PhysicalDevice>();
            foreach (var d in ds)
                vpds.Add(new PhysicalDevice(Device, d));

            return vpds;
        }

        public bool TryGetExtension<T>(out T ext) where T : NativeExtension<Vk> =>
            Device.Vk.TryGetInstanceExtension(VkInstance, out ext);

        public SurfaceKHR CreateWin32Surface(Win32SwapchainSource source)
        {
            var ci = new Win32SurfaceCreateInfoKHR
            {
                SType = StructureType.Win32SurfaceCreateInfoKhr,
                Hwnd = source.Hwnd,
                Hinstance = source.Hinstance,
            };

            if (Device.Instance.TryGetExtension<KhrWin32Surface>(out var ext))
            {
                if (ext.CreateWin32Surface(VkInstance, ci, null, out var surface) != Result.Success)
                    throw new VkException();
                return surface;
            }
            else
                throw new VkException();
        }

        public void DestroySurface(SurfaceKHR surface) =>
            Device.KhrSurface.DestroySurface(VkInstance, surface, null);

        protected override void UnmanagedDispose()
        {
            DebugUtils?.DestroyDebugUtilsMessenger(VkInstance, _debugMessenger, null);
            Device.Vk.DestroyInstance(VkInstance, null);
        }

        protected override void ManagedDispose()
        {
            DebugUtils?.Dispose();
        }

        public static implicit operator VkInstance(Instance vki) =>
            vki.VkInstance;

    }
}
