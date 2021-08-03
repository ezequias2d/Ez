// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;
using Ez.Graphics.Context;
using Ez.Graphics.Context.SwapchainSources;
using Ez.Memory;
using Microsoft.Extensions.Logging;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using System;
using System.Collections.Generic;
using System.Drawing;
using Framebuffer = Ez.Graphics.API.Vulkan.Core.Cached.Framebuffers.Framebuffer;
using VkFence = Silk.NET.Vulkan.Fence;
using VkSemaphore = Silk.NET.Vulkan.Semaphore;
namespace Ez.Graphics.API.Vulkan.Core
{
    internal class Swapchain : DeviceResource, ISwapchain
    {
        private SwapchainCreateInfo _createInfo;

        public Swapchain(Device device, in SwapchainCreateInfo scCreateInfo) : base(device)
        {
            _createInfo = scCreateInfo;
            Surface = CreateSurface(_createInfo.Source);
            CreateSwapchain();
        }

        private Queue PresentQueue { get; set; }
        private SurfaceKHR Surface { get; set; }
        private SwapchainKHR VkSwapchain { get; set; }
        private ReadOnlyMemory<TextureView> Views { get; set; }

        public IReadOnlyList<IFramebuffer> Framebuffers { get; private set; }
        public int CurrentIndex { get; private set; }
        public bool VSync { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Size Size
        {
            get => _createInfo.Size;
            set
            {
                _createInfo.Size = value;
                RecreateSwapchain();
            }
        }

        public override bool Equals(IResource other)
        {
            throw new NotImplementedException();
        }

        protected override void ManagedDispose()
        {
            foreach (var view in Views.Span)
                view.Dispose();
        }

        protected unsafe override void UnmanagedDispose()
        {
            Device.KhrSwapchain.DestroySwapchain(Device.Handle, VkSwapchain, null);
            Device.Instance.DestroySurface(Surface);
        }

        private unsafe ReadOnlyMemory<TextureView> CreateViews(ReadOnlySpan<Texture> textures)
        {
            var array = new TextureView[textures.Length];
            for (var i = 0; i < textures.Length; i++)
            {
                array[i] = new TextureView(
                    Device,
                    new TextureViewCreateInfo
                    {
                        Texture = textures[i],
                        SubresourceRange = new TextureSubresourceRange
                        {
                            BaseMipmapLevel = 0,
                            MipmapLevelCount = 1,
                            BaseArrayLayer = 0,
                            ArrayLayerCount = 1
                        },
                    });
            }
            return array;
        }

        private unsafe ReadOnlySpan<Texture> GetTextures(Format format, Extent2D extent)
        {
            var count = 0u;
            Device.KhrSwapchain.GetSwapchainImages(Device.Handle, VkSwapchain, ref count, null);

            Image* images = stackalloc Image[(int)count];
            Device.KhrSwapchain.GetSwapchainImages(Device.Handle, VkSwapchain, &count, images);

            var textures = new Texture[count];
            for (var i = 0; i < count; i++)
                textures[i] = new Texture(Device, images[i], format, extent, false);
            return textures;
        }

        private SurfaceKHR CreateSurface(in ISwapchainSource source)
        {
            switch (source)
            {
                case Win32SwapchainSource w32:
                    return Device.Instance.CreateWin32Surface(w32);
                default:
                    throw new VkException();
            }
        }

        private unsafe (SwapchainKHR, Format, Extent2D) CreateSwapchain(in SwapchainCreateInfo ci)
        {
            Device.KhrSurface.GetPhysicalDeviceSurfaceCapabilities(
                Device.PhysicalDevice,
                Surface,
                out var capabilities);

            var formats = LoadSupportedFormats();
            var presentModes = LoadSupportedPresentModes();

            var surfaceFormat = ChooseSwapSurfaceFormat(ci.IsSrgbFormat, formats);
            var presentMode = ChooseSwapPresentMode(presentModes);
            var extent = ChooseSwapExtent(ci.Size, capabilities);

            uint imageCount = Math.Clamp(capabilities.MinImageCount + 1, capabilities.MinImageCount, capabilities.MaxImageCount);

            var ciKhr = new SwapchainCreateInfoKHR
            {
                SType = StructureType.SwapchainCreateInfoKhr,
                Surface = Surface,
                MinImageCount = imageCount,
                ImageFormat = surfaceFormat.Format,
                ImageColorSpace = surfaceFormat.ColorSpace,
                ImageExtent = extent,
                ImageArrayLayers = 1,
                ImageUsage = ImageUsageFlags.ImageUsageColorAttachmentBit | ImageUsageFlags.ImageUsageTransferDstBit,
                PreTransform = capabilities.CurrentTransform,
                CompositeAlpha = CompositeAlphaFlagsKHR.CompositeAlphaOpaqueBitKhr,
                PresentMode = presentMode,
                Clipped = true,
                OldSwapchain = new SwapchainKHR(null),
            };

            uint* families = stackalloc uint[Device.Families.Count];

            var i = 0u;
            foreach (var family in Device.Families)
                if (family.SupportsPresentation(this) || ((family.QueueFlags & QueueFlags.QueueGraphicsBit) != default))
                    families[i++] = family.Index;

            if (i > 1)
            {
                ciKhr.ImageSharingMode = SharingMode.Concurrent;
                ciKhr.QueueFamilyIndexCount = i;
                ciKhr.PQueueFamilyIndices = families;
            }
            else
            {
                ciKhr.ImageSharingMode = SharingMode.Exclusive;
                ciKhr.QueueFamilyIndexCount = 0;
                ciKhr.PQueueFamilyIndices = null;
            }

            if (Device.KhrSwapchain.CreateSwapchain(Device.Handle, ciKhr, null, out var swapchain) != Result.Success)
                throw new VkException("Can not possible to create the swapchain.");

            return (swapchain, surfaceFormat.Format, extent);
        }

        private SurfaceFormatKHR ChooseSwapSurfaceFormat(bool isSrgb, ReadOnlySpan<SurfaceFormatKHR> surfaceFormats)
        {
            foreach (var surfaceFormat in surfaceFormats)
            {
                if ((isSrgb && (surfaceFormat.Format == Format.R8G8B8A8Srgb || surfaceFormat.Format == Format.B8G8R8A8Srgb)) ||
                    (!isSrgb && (surfaceFormat.Format == Format.R8G8B8A8Unorm || surfaceFormat.Format == Format.B8G8R8A8Unorm)))
                {
                    return surfaceFormat;
                }
            }

            Device.Logger.LogWarning("Could not find a suitable surface format.");
            return surfaceFormats[0];
        }

        private Extent2D ChooseSwapExtent(in Size size, in SurfaceCapabilitiesKHR capabilities)
        {
            if (capabilities.CurrentExtent.Width != uint.MaxValue)
                return capabilities.CurrentExtent;

            return new()
            {
                Width = Math.Clamp(
                    (uint)size.Width,
                    capabilities.MinImageExtent.Width,
                    capabilities.MaxImageExtent.Width),

                Height = Math.Clamp(
                    (uint)size.Height,
                    capabilities.MinImageExtent.Height,
                    capabilities.MaxImageExtent.Height)
            };
        }

        private static PresentModeKHR ChooseSwapPresentMode(ReadOnlySpan<PresentModeKHR> presentModes)
        {
            // force fifo mode in mobile devices.
            if (OperatingSystem.IsAndroid() || OperatingSystem.IsIOS())
                return PresentModeKHR.PresentModeFifoKhr;

            foreach (var presentMode in presentModes)
                if (presentMode == PresentModeKHR.PresentModeMailboxKhr)
                    return presentMode;

            return PresentModeKHR.PresentModeFifoKhr;
        }

        private unsafe ReadOnlySpan<SurfaceFormatKHR> LoadSupportedFormats()
        {
            var formatCount = 0u;
            Device.KhrSurface.GetPhysicalDeviceSurfaceFormats(
                physicalDevice: Device.PhysicalDevice,
                surface: Surface,
                pSurfaceFormatCount: ref formatCount,
                pSurfaceFormats: null);

            if (formatCount != 0)
            {
                var array = ArrayPool<byte>.GetT(sizeof(SurfaceFormatKHR) * formatCount, true);
                Span<SurfaceFormatKHR> formats = MemUtil.Cast<byte, SurfaceFormatKHR>(array).Slice(0, (int)formatCount);

                Device.KhrSurface.GetPhysicalDeviceSurfaceFormats(
                    physicalDevice: Device.PhysicalDevice,
                    surface: Surface,
                    pSurfaceFormatCount: &formatCount,
                    pSurfaceFormats: formats);

                return formats;
            }
            else
                throw new VkException();
        }

        private unsafe Span<PresentModeKHR> LoadSupportedPresentModes()
        {
            var formatCount = 0u;
            Device.KhrSurface.GetPhysicalDeviceSurfacePresentModes(
                physicalDevice: Device.PhysicalDevice,
                surface: Surface,
                pPresentModeCount: ref formatCount,
                pPresentModes: null);

            if (formatCount != 0)
            {
                var array = ArrayPool<byte>.GetT(sizeof(PresentModeKHR) * formatCount, true);
                Span<PresentModeKHR> presentModes = MemUtil.Cast<byte, PresentModeKHR>(array).Slice(0, (int)formatCount);

                Device.KhrSurface.GetPhysicalDeviceSurfacePresentModes(
                    physicalDevice: Device.PhysicalDevice,
                    surface: Surface,
                    pPresentModeCount: &formatCount,
                    pPresentModes: presentModes);

                return presentModes;
            }
            else
                throw new VkException();
        }

        public void Present(ReadOnlySpan<ISemaphore> waitSemaphores, ISemaphore signalSemaphore, IFence fence)
        {
            Span<VkSemaphore> tempWaitSemaphores = stackalloc VkSemaphore[waitSemaphores.Length];
            for (var i = 0; i < waitSemaphores.Length; i++)
                tempWaitSemaphores[i] = (Semaphore)waitSemaphores[i];

            Present(tempWaitSemaphores,
                signalSemaphore == null ? default(VkSemaphore) : (Semaphore)signalSemaphore,
                fence == null ? default(VkFence) : (Fence)fence);
        }

        public void Present(ISemaphore waitSemaphore, ISemaphore signalSemaphore, IFence fence)
        {
            Span<VkSemaphore> tempWaitSemaphores = stackalloc VkSemaphore[1];

            if (waitSemaphore != null)
                tempWaitSemaphores[0] = (Semaphore)waitSemaphore;
            else
                tempWaitSemaphores = Span<VkSemaphore>.Empty;

            Present(tempWaitSemaphores, (Semaphore)signalSemaphore, (Fence)fence);
        }

        private unsafe void Present(ReadOnlySpan<VkSemaphore> waitSemaphores, VkSemaphore signalSemaphore, VkFence fence)
        {
            var swapchain = VkSwapchain;
            var imageIndex = (uint)CurrentIndex;

            fixed (VkSemaphore* pWaitSemaphores = waitSemaphores)
            {
                // prepare VkPresentInfoKHR for submission
                var presentInfo = new PresentInfoKHR
                {
                    SType = StructureType.PresentInfoKhr,
                    SwapchainCount = 1,
                    PSwapchains = &swapchain,
                    PImageIndices = &imageIndex,
                    WaitSemaphoreCount = (uint)waitSemaphores.Length,
                    PWaitSemaphores = pWaitSemaphores,
                };

                Device.KhrSwapchain.QueuePresent(PresentQueue, presentInfo);
            }

            Device.KhrSwapchain.AcquireNextImage(Device, swapchain, ulong.MaxValue, signalSemaphore, fence, ref imageIndex);
            CurrentIndex = (int)imageIndex;
        }

        private void RecreateSwapchain()
        {
            CleanupSwapchain();
            CreateSwapchain();
        }

        private void CreateSwapchain()
        {
            QueueFamily family;
            foreach (var f in Device.Families)
                if (f.SupportsPresentation(this))
                    family = f;

            var (swapchain, format, extent) = CreateSwapchain(_createInfo);
            VkSwapchain = swapchain;

            var textures = GetTextures(format, extent);
            Views = CreateViews(textures);
            var viewsSpan = Views.Span;

            PresentQueue = Device.GetPresentQueue(this);

            var framebuffers = new IFramebuffer[Views.Length];
            for (var i = 0; i < framebuffers.Length; i++)
                framebuffers[i] = new Framebuffer(Device, new()
                {
                    Attachments = new FramebufferAttachment[]
                    {
                        new(viewsSpan[i], 0),
                    }
                },
                true);

            Framebuffers = Array.AsReadOnly(framebuffers);

            var imageIndex = 0u;

            using var fence = new Fence(Device);
            Device.KhrSwapchain.AcquireNextImage(Device, VkSwapchain, ulong.MaxValue, default, fence, ref imageIndex);
            fence.Wait(ulong.MaxValue);

            CurrentIndex = (int)imageIndex;
        }

        private unsafe void CleanupSwapchain()
        {
            foreach (var framebuffer in Framebuffers)
                framebuffer.Dispose();

            foreach (var view in Views.Span)
                view.Dispose();

            Device.KhrSwapchain.DestroySwapchain(Device, VkSwapchain, null);
        }

        public static implicit operator SurfaceKHR(Swapchain sc) =>
            sc.Surface;

        public static implicit operator SwapchainKHR(Swapchain sc) =>
            sc.VkSwapchain;
    }
}
