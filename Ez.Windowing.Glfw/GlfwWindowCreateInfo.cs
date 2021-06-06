using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Windowing.GLFW
{
    public struct GlfwWindowCreateInfo
    {

        public IntPtr MonitorHandle { get; set; }

        public bool IsEventDriven { get; set; }

        public GlfwWindowCreateInfo(IntPtr monitorHandle, bool isEventDriven)
        {
            MonitorHandle = monitorHandle;
            IsEventDriven = isEventDriven;
        }

        public static readonly GlfwWindowCreateInfo Default =
            new GlfwWindowCreateInfo(IntPtr.Zero, false);
    }
}
