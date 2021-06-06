using System;

namespace Ez.Graphics.Contexts
{
    /// <summary>
    /// Encapsulates various pieces of OpenGL context, necessary for creating a connection between OpenGL and a Window.
    /// </summary>
    public struct OpenGLContext
    {
        /// <summary>
        /// The OpenGL context handle.
        /// </summary>
        public IntPtr Handle { get; }

        /// <summary>
        /// A delegate which can be used to retrieve OpenGL function pointers by name.
        /// </summary>
        public Func<string, IntPtr> GetProcAddress { get; }

        /// <summary>
        /// A delegate which can be used to make the given OpenGL context current on the calling thread.
        /// </summary>
        public Action<IntPtr> MakeCurrent { get; }

        /// <summary>
        /// A delegate which can be used to retrieve the calling thread's active OpenGL context.
        /// </summary>
        public Func<IntPtr> GetCurrentContext { get; }

        /// <summary>
        /// A delegate which can be used to clear the calling thread's GL context.
        /// </summary>
        public Action ClearCurrentContext { get; }

        /// <summary>
        /// A delegate which can be used to swap the main back buffer associated with the OpenGL context.
        /// </summary>
        public Action<IntPtr> SwapBuffers { get; }

        /// <summary>
        /// A delegate which can be used to set the synchronization behavior of the OpenGL context.
        /// </summary>
        public Action<bool> SetVSync { get; }

        /// <summary>
        /// A delegate which is invoked when the main Swapchain is resized.
        /// This may be null, in which case no special action is taken when the Swapchain is resized.
        /// </summary>
        public Action<uint, uint> ResizeSwapchain { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="OpenGLContext"/> struct.
        /// </summary>
        /// <param name="handle">The OpenGL context handle.</param>
        /// <param name="getProcAddress">A delegate which can be used to retrieve OpenGL function pointers by name.</param>
        /// <param name="makeCurrent">A delegate which can be used to make the given OpenGL context current on the calling
        /// thread.</param>
        /// <param name="getCurrentContext">A delegate which can be used to retrieve the calling thread's active OpenGL context.</param>
        /// <param name="clearCurrentContext">A delegate which can be used to clear the calling thread's GL context.</param>
        /// <param name="swapBuffers">A delegate which can be used to swap the main back buffer associated with the OpenGL
        /// context.</param>
        /// <param name="setSyncToVerticalBlank">A delegate which can be used to set the synchronization behavior of the OpenGL
        /// context.</param>
        /// <param name="resizeSwapchain">A delegate which is invoked when the main Swapchain is resized. This may be null,
        /// in which case no special action is taken when the Swapchain is resized.</param>
        public OpenGLContext(
            IntPtr handle,
            Func<string, IntPtr> getProcAddress,
            Action<IntPtr> makeCurrent,
            Func<IntPtr> getCurrentContext,
            Action clearCurrentContext,
            Action<IntPtr> swapBuffers,
            Action<bool> setSyncToVerticalBlank,
            Action<uint, uint> resizeSwapchain)
        {
            Handle = handle;
            GetProcAddress = getProcAddress;
            MakeCurrent = makeCurrent;
            GetCurrentContext = getCurrentContext;
            ClearCurrentContext = clearCurrentContext;
            SwapBuffers = swapBuffers;
            SetVSync = setSyncToVerticalBlank;
            ResizeSwapchain = resizeSwapchain;
        }
    }
}
