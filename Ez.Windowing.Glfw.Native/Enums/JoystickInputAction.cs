using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Windowing.GLFW.Native.Enums
{
    /// <summary>
    /// Defines event information for <see cref="Glfw.GetJoystickButtons"/> and <see cref="Glfw.GetJoystickButtonsRaw(int,out int)"/>.
    /// </summary>
    public enum JoystickInputAction : byte
    {
        /// <summary>
        /// The joystick button was released.
        /// </summary>
        Release = 0,

        /// <summary>
        /// The joystick button was pressed.
        /// </summary>
        Press = 1,
    }
}
