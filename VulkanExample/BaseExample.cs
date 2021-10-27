using System;
using System.Buffers;
using System.Drawing;
using System.Linq;
using System.Threading;
using Ez.Graphics;
using Ez.Graphics.API;
using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;
using Ez.Graphics.API.Vulkan;
using Ez.Memory;
using Ez.Windowing;
using Ez.Windowing.GLFW;
using Microsoft.Extensions.Logging;

namespace VulkanExample
{
    public abstract class BaseExample
    {
        private readonly (ICommandBuffer Cb, IFence Fence, ISemaphore SignalSemaphore)[] _commandBuffers;

        public BaseExample(ILogger logger)
        {
            Logger = logger;
            Window = CreateWindow(logger);
            Device = CreateDevice(logger, Window);
            Swapchain = Device.DefaultSwapchain;
            Factory = Device.Factory;

            _commandBuffers = new (ICommandBuffer, IFence, ISemaphore)[Swapchain.Framebuffers.Count];
            for (var i = 0; i < _commandBuffers.Length; i++)
                _commandBuffers[i] = (Factory.CreateCommandBuffer(), null, Factory.CreateSemaphore());
        }
        public ILogger Logger { get; }
        public IWindow Window { get; }
        public IDevice Device { get; }
        public ISwapchain Swapchain { get; }
        public IFactory Factory { get; }

        public abstract void Load();
        public abstract void Render(ICommandBuffer commandBuffer, RenderPassBeginInfo beginInfo);

        public void Run()
        {
            var presentFences = new IFence[Swapchain.Framebuffers.Count];

            var recreateSwapchain = false;
            var size = Swapchain.Size;
            Window.FramebufferResize += (w, s) =>
            {
                recreateSwapchain = true;
                size = s;
            };

            var commandBuffers = new ICommandBuffer[1];
            var renderSemaphoreArray = new ISemaphore[1];
            var attachments = new AttachmentInfo[]
            {
                new()
                {
                    LoadOperation = AttachmentLoadOperation.Clear,
                    StoreOperation = AttachmentStoreOperation.Store,
                    ClearValue = new()
                    {
                        Color = new()
                        {
                            SingleValue = ColorSingle.CornflowerBlue,
                        }
                    }
                }
            };

            Load();

            while (!Window.IsDisposed)
            {
                using var processEventsResult = Window.BeginProcessEvents();

                if (recreateSwapchain)
                {
                    if (presentFences[Swapchain.CurrentIndex] != null)
                    {
                        var fence = presentFences[Swapchain.CurrentIndex];
                        fence.Wait(ulong.MaxValue);
                        fence.Reset();
                    }

                    Device.Wait();
                    recreateSwapchain = false;
                    Swapchain.Size = size;

                    for (var i = 0; i < presentFences.Length; i++)
                    {
                        presentFences[i].Dispose();
                        presentFences[i] = null;
                    }
                }
                else
                {
                    ref var cpair = ref _commandBuffers[Swapchain.CurrentIndex];
                    var cb = cpair.Cb;
                    var signal = cpair.SignalSemaphore;

                    if (cpair.Fence != null)
                    {
                        cpair.Fence.Wait(ulong.MaxValue);
                        cpair.Fence.Reset();
                        cb.Reset();
                    }
                    else
                        cpair.Fence = Factory.CreateFence();

                    cb.Set();
                    cb.Begin();

                    var renderPassBeginInfo = new RenderPassBeginInfo
                    {
                        Framebuffer = Swapchain.Framebuffers[Swapchain.CurrentIndex],
                        Attachments = attachments,
                        DepthStencilAttachmentIndex = -1,
                    };

                    cb.BeginRenderPass(renderPassBeginInfo);
                    Render(cb, renderPassBeginInfo);
                    cb.EndRenderPass();

                    cb.End();

                    commandBuffers[0] = cb;
                    renderSemaphoreArray[0] = signal;
                    Device.Submit(new()
                    {
                        CommandBuffers = commandBuffers,
                        //WaitSemaphores = ReadOnlySpan<ISemaphore>.Empty,
                        SignalSemaphores = renderSemaphoreArray,
                    }, cpair.Fence);

                    if (presentFences[Swapchain.CurrentIndex] != null)
                    {
                        var fence = presentFences[Swapchain.CurrentIndex];
                        fence.Wait(ulong.MaxValue);
                        fence.Reset();
                    }

                    var nextIndex = (Swapchain.CurrentIndex + 1) % Swapchain.Framebuffers.Count;
                    if (presentFences[nextIndex] == null)
                        presentFences[nextIndex] = Factory.CreateFence();

                    Swapchain.Present(renderSemaphoreArray, null, presentFences[nextIndex]);
                }

                //Thread.Sleep(1);
                Window.EndProcessEvents(processEventsResult);
            }

            Window.WaitClose();
        }
        private static IWindow CreateWindow(ILogger logger)
        {
            var windowCreateInfo = new WindowCreateInfo(new Point(100, 100), new Size(640, 480), WindowState.Normal, "Glfw Example", ContextAPI.NoAPI);
            var window = new GlfwWindow(logger, windowCreateInfo, new GlfwWindowCreateInfo(IntPtr.Zero, false));
            window.TextInput += (w, args) =>
            {
                Console.Write($"{(char)args.Unicode}");
            };

            return window;
        }

        private static IDevice CreateDevice(ILogger logger, IWindow window)
        {
            var deviceCreateInfo = new DeviceCreateInfo
            {
                Debug = true,
                DepthFormat = PixelFormat.D24UNormS8UInt,
                IsSrgbFormat = true,
                IsVSync = true,
            };

            return new Device(
                logger,
                deviceCreateInfo,
                window.GetVulkanRequiredExtensions(),
                new SwapchainCreateInfo(window.GetSwapchainSource(), window.FramebufferSize, null, true),
                (dvs) => dvs.First());
        }
    }
}