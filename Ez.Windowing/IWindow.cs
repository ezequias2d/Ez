﻿using System;
using System.Drawing;
using System.Numerics;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ez.Graphics.Contexts;

namespace Ez.Windowing
{
    /// <summary>
    /// Describes a desktop window.
    /// </summary>
    public interface IWindow : IDisposable
    {

        #region Properties
        /// <summary>
        /// Gets or sets the X position of the <see cref="IWindow"/>.
        /// </summary>
        int X { get; set; }
        /// <summary>
        /// Gets or sets the Y position of the <see cref="IWindow"/>.
        /// </summary>
        int Y { get; set; }

        /// <summary>
        /// Gets or sets the position of the <see cref="IWindow"/>.
        /// </summary>
        Point Position { get; set; }

        /// <summary>
        /// Gets an <see cref="IMouseState"/> that describes the mouse state in the <see cref="IWindow"/>.
        /// </summary>
        IMouseState MouseState { get; }

        /// <summary>
        /// Gets or sets the width of the <see cref="IWindow"/>.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the <see cref="IWindow"/>.
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// Gets or sets the size of the <see cref="IWindow"/>.
        /// </summary>
        Size Size { get; set; }

        /// <summary>
        /// Gets the native <see cref="IWindow"/> pointer.
        /// </summary>
        IntPtr Handle { get; }
        
        /// <summary>
        /// Gets or sets the title of the <see cref="IWindow"/>.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the window state of the <see cref="IWindow"/>.
        /// </summary>
        WindowState WindowState { get; set; }

        /// <summary>
        /// Gets or sets the window border of the <see cref="IWindow"/>.
        /// </summary>
        WindowBorder WindowBorder { get; set; }

        /// <summary>
        /// Gets the API of the <see cref="IWindow"/> when it was been created.
        /// </summary>
        ContextAPI API { get; }

        /// <summary>
        /// Gets the API version of the <see cref="API"/>.
        /// </summary>
        Version APIVersion { get; }

        /// <summary>
        /// Gets the flags of the <see cref="IWindow"/>.
        /// </summary>
        ContextFlags Flags { get; }

        /// <summary>
        /// Gets the profile mode of the <see cref="API"/>.
        /// </summary>
        ContextProfile Profile { get; }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="IWindow"/> was disposed.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Gets or sets a value that indicates the <see cref="IWindow"/> is in exiting state.
        /// This can be used to override the user's attempt to close the window, or to signal that it should be closed.
        /// </summary>
        bool IsExiting { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="CursorMode"/> of the cursor in <see cref="IWindow"/>.
        /// </summary>
        CursorMode CursorMode { get; set; }

        /// <summary>
        /// Gets the bounds of the <see cref="IWindow"/>.
        /// </summary>
        Rectangle Bounds { get; }

        /// <summary>
        /// Gets or sets the <see cref="IWindow"/> opacity.
        /// </summary>
        float Opacity { get; set; }

        /// <summary>
        /// Gets a value that indicates the <see cref="IWindow"/> is focused by the operating system.
        /// </summary>
        bool IsFocused { get; }

        /// <summary>
        /// Gets the number of samples from the <see cref="IWindow"/>.
        /// </summary>
        byte Multisamples { get; }

        /// <summary>
        /// Gets or sets the clipboard string.
        /// </summary>
        string ClipboardString { get; set; }

        #endregion
        #region Events
        /// <summary>
        /// Occurs whenever the <see cref="IWindow"/> changes size.
        /// </summary>
        event Action<IWindow, Size> SizeChanged;

        /// <summary>
        /// Occurs when the <see cref="IWindow"/> is about to close.
        /// </summary>
        event Action<IWindow, CancelEventArgs> Closing;

        /// <summary>
        /// Occurs after the <see cref="IWindow"/> has closed.
        /// </summary>
        event Action<IWindow> Closed;

        /// <summary>
        /// Occurs after the <see cref="IWindow"/> got focus.
        /// </summary>
        event Action<IWindow> GotFocus;

        /// <summary>
        /// Occurs after the <see cref="IWindow"/> lost focus.
        /// </summary>
        event Action<IWindow> LostFocus;

        /// <summary>
        /// Occurs after the <see cref="WindowState"/> changes.
        /// </summary>
        event Action<IWindow, WindowState> WindowStadeChange;

        /// <summary>
        /// Occurs whenever the <see cref="IWindow"/> is moved.
        /// </summary>
        event Action<IWindow, Point> Moved;

        /// <summary>
        /// Occurs whenever a Unicode code point is typed.
        /// </summary>
        event Action<IWindow, TextInputEventArgs> TextInput;

        /// <summary>
        /// Occurs whenever the mouse cursor enters the <see cref="Bounds"/>.
        /// </summary>
        event Action<IWindow, MouseEventArgs> MouseEntered;

        /// <summary>
        /// Occurs whenever the mouse cursor left the <see cref="Bounds"/>.
        /// </summary>
        event Action<IWindow, MouseEventArgs> MouseLeft;

        /// <summary>
        /// Occurs whenever a mouse wheel is moved.
        /// </summary>
        event Action<IWindow, MouseEventArgs> MouseWheel;

        /// <summary>
        /// Occurs whenever a mouse wheel is moved;
        /// </summary>
        event Action<IWindow, MouseEventArgs> MouseMove;

        /// <summary>
        /// Occurs when a <see cref="MouseButton"/> is pressed.
        /// </summary>
        event Action<IWindow, MouseEventArgs> MouseDown;

        /// <summary>
        /// Occurs when a <see cref="MouseButton"/> is released.
        /// </summary>
        event Action<IWindow, MouseEventArgs> MouseUp;

        /// <summary>
        /// Occurs whenever a <see cref="MouseButton"/> is repeated.
        /// </summary>
        event Action<IWindow, MouseEventArgs> MouseRepeat;

        /// <summary>
        /// Occurs when a <see cref="Key"/> is pressed.
        /// </summary>
        event Action<IWindow, KeyEventArgs> KeyDown;

        /// <summary>
        /// Occurs when a <see cref="Key"/> is released.
        /// </summary>
        event Action<IWindow, KeyEventArgs> KeyUp;

        /// <summary>
        /// Occurs whenever a <see cref="Key"/> is repeated.
        /// </summary>
        event Action<IWindow, KeyEventArgs> KeyRepeat;

        /// <summary>
        /// Occurs whenever one or more files are dropped on the <see cref="IWindow"/>.
        /// </summary>
        event Action<IWindow, DropEventArgs> Drop;

        /// <summary>
        /// Occurs when a joystick is connected or disconnected.
        /// </summary>
        event Action<IWindow, JoystickEventArgs> JoystickConnection;

        /// <summary>
        /// Occurs when the contents of a <see cref="IWindow"/> is damaged and needs to be refreshed.
        /// </summary>
        event Action<IWindow> Refresh;
        #endregion
        #region Functions
        /// <summary>
        /// Gets an <see cref="IJoystickState"/> that describes a joystick state in the <see cref="IWindow"/>.
        /// </summary>
        /// <param name="index">The joystick index to get the state.</param>
        /// <returns>The <see cref="IJoystickState"/>The <see cref="IJoystickState"/> related to the index 
        /// joystick provided.</returns>
        IJoystickState GetJoystickState(int index);

        /// <summary>
        /// Waits the window to close.
        /// </summary>
        void WaitClose();

        /// <summary>
        /// Processes pending <see cref="IWindow"/> events.
        /// </summary>
        void ProcessEvents();

        /// <summary>
        /// Processes pending <see cref="IWindow"/> events.
        /// </summary>
        Task ProcessEventsAsync();

        /// <summary>
        /// Gets a <see cref="OpenGLContext"/> needed to use OpenGL.
        /// </summary>
        /// <returns>A <see cref="OpenGLContext"/> instance.</returns>
        OpenGLContext GetOpenGLContext();

        /// <summary>
        /// Gets a <see cref="VulkanContext"/> needed to use Vulkan.
        /// </summary>
        /// <returns>A <see cref="VulkanContext"/> instance.</returns>
        VulkanContext GetVulkanContext();
        #endregion
    }
}