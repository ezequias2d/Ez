using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Windowing.GLFW.Native
{
    /// <summary>
    /// Opaque handle to a GLFW window.
    /// </summary>
    public struct Window
    {
        private IntPtr _handle;

        public bool IsEmpty => _handle == IntPtr.Zero;
        
        public static implicit operator IntPtr(Window window) => window._handle;
        public static implicit operator Window(IntPtr handle) => new Window { _handle = handle };
    }
}
