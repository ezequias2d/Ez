using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Windowing.GLFW.Native
{
    /// <summary>
    /// Opaque handle to a GLFW cursor.
    /// </summary>
    public struct Cursor
    {
        private IntPtr _handle;

        public bool IsEmpty => _handle == IntPtr.Zero;

        public static implicit operator IntPtr(Cursor cursor) => cursor._handle;
        public static implicit operator Cursor(IntPtr handle) => new Cursor { _handle = handle };
    }
}
