using Ez.Windowing.GLFW.Native.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Windowing.GLFW.Native
{
    internal static class KeyExtensions
    {
        public static Key ToKey(this Keys key)
        {
            switch (key)
            {
                case Keys.A:
                    return Key.A;
                case Keys.Apostrophe:
                    return Key.Apostrophe;
                case Keys.B:
                    return Key.B;
                case Keys.Backslash:
                    return Key.Backslash;
                case Keys.Backspace:
                    return Key.Backspace;
                case Keys.C:
                    return Key.C;
                case Keys.CapsLock:
                    return Key.CapsLock;
                case Keys.Comma:
                    return Key.Comma;
                case Keys.D:
                    return Key.D;
                case Keys.D0:
                    return Key.D0;
                case Keys.D1:
                    return Key.D1;
                case Keys.D2:
                    return Key.D2;
                case Keys.D3:
                    return Key.D3;
                case Keys.D4:
                    return Key.D4;
                case Keys.D5:
                    return Key.D5;
                case Keys.D6:
                    return Key.D6;
                case Keys.D7:
                    return Key.D7;
                case Keys.D8:
                    return Key.D8;
                case Keys.D9:
                    return Key.D9;
                case Keys.Delete:
                    return Key.Delete;
                case Keys.Down:
                    return Key.Down;
                case Keys.E:
                    return Key.E;
                case Keys.End:
                    return Key.End;
                case Keys.Enter:
                    return Key.Enter;
                case Keys.Equal:
                    return Key.Equal;
                case Keys.Escape:
                    return Key.Escape;
                case Keys.F:
                    return Key.F;
                case Keys.F1:
                    return Key.F1;
                case Keys.F10:
                    return Key.F10;
                case Keys.F11:
                    return Key.F11;
                case Keys.F12:
                    return Key.F12;
                case Keys.F13:
                    return Key.F13;
                case Keys.F14:
                    return Key.F14;
                case Keys.F15:
                    return Key.F15;
                case Keys.F16:
                    return Key.F16;
                case Keys.F17:
                    return Key.F17;
                case Keys.F18:
                    return Key.F18;
                case Keys.F19:
                    return Key.F19;
                case Keys.F2:
                    return Key.F2;
                case Keys.F20:
                    return Key.F20;
                case Keys.F21:
                    return Key.F21;
                case Keys.F22:
                    return Key.F22;
                case Keys.F23:
                    return Key.F23;
                case Keys.F24:
                    return Key.F24;
                case Keys.F25:
                    return Key.F25;
                case Keys.F3:
                    return Key.F3;
                case Keys.F4:
                    return Key.F4;
                case Keys.F5:
                    return Key.F5;
                case Keys.F6:
                    return Key.F6;
                case Keys.F7:
                    return Key.F7;
                case Keys.F8:
                    return Key.F8;
                case Keys.F9:
                    return Key.F9;
                case Keys.G:
                    return Key.G;
                case Keys.GraveAccent:
                    return Key.GraveAccent;
                case Keys.H:
                    return Key.H;
                case Keys.Home:
                    return Key.Home;
                case Keys.I:
                    return Key.I;
                case Keys.Insert:
                    return Key.Insert;
                case Keys.J:
                    return Key.J;
                case Keys.K:
                    return Key.K;
                case Keys.KeyPad0:
                    return Key.KeyPad0;
                case Keys.KeyPad1:
                    return Key.KeyPad1;
                case Keys.KeyPad2:
                    return Key.KeyPad2;
                case Keys.KeyPad3:
                    return Key.KeyPad3;
                case Keys.KeyPad4:
                    return Key.KeyPad4;
                case Keys.KeyPad5:
                    return Key.KeyPad5;
                case Keys.KeyPad6:
                    return Key.KeyPad6;
                case Keys.KeyPad7:
                    return Key.KeyPad7;
                case Keys.KeyPad8:
                    return Key.KeyPad8;
                case Keys.KeyPad9:
                    return Key.KeyPad9;
                case Keys.KeyPadAdd:
                    return Key.KeyPadAdd;
                case Keys.KeyPadDecimal:
                    return Key.KeyPadDecimal;
                case Keys.KeyPadDivide:
                    return Key.KeyPadDivide;
                case Keys.KeyPadEnter:
                    return Key.KeyPadEnter;
                case Keys.KeyPadEqual:
                    return Key.KeyPadEqual;
                case Keys.KeyPadMultiply:
                    return Key.KeyPadMultiply;
                case Keys.KeyPadSubtract:
                    return Key.KeyPadSubtract;
                case Keys.L:
                    return Key.L;
                case Keys.Left:
                    return Key.Left;
                case Keys.LeftAlt:
                    return Key.LeftAlt;
                case Keys.LeftBracket:
                    return Key.LeftBracket;
                case Keys.LeftControl:
                    return Key.LeftControl;
                case Keys.LeftShift:
                    return Key.LeftShift;
                case Keys.LeftSuper:
                    return Key.LeftSuper;
                case Keys.M:
                    return Key.M;
                case Keys.Menu:
                    return Key.Menu;
                case Keys.Minus:
                    return Key.Minus;
                case Keys.N:
                    return Key.N;
                case Keys.NumLock:
                    return Key.NumLock;
                case Keys.O:
                    return Key.O;
                case Keys.P:
                    return Key.P;
                case Keys.PageDown:
                    return Key.PageDown;
                case Keys.PageUp:
                    return Key.PageUp;
                case Keys.Pause:
                    return Key.Pause;
                case Keys.Period:
                    return Key.Period;
                case Keys.PrintScreen:
                    return Key.PrintScreen;
                case Keys.Q:
                    return Key.Q;
                case Keys.R:
                    return Key.R;
                case Keys.Right:
                    return Key.Right;
                case Keys.RightAlt:
                    return Key.RightAlt;
                case Keys.RightBracket:
                    return Key.RightBracket;
                case Keys.RightControl:
                    return Key.RightControl;
                case Keys.RightShift:
                    return Key.RightShift;
                case Keys.RightSuper:
                    return Key.RightSuper;
                case Keys.S:
                    return Key.S;
                case Keys.ScrollLock:
                    return Key.ScrollLock;
                case Keys.Semicolon:
                    return Key.Semicolon;
                case Keys.Slash:
                    return Key.Slash;
                case Keys.Space:
                    return Key.Space;
                case Keys.T:
                    return Key.T;
                case Keys.Tab:
                    return Key.Tab;
                case Keys.U:
                    return Key.U;
                case Keys.Unknown:
                    return Key.Unknown;
                case Keys.Up:
                    return Key.Up;
                case Keys.V:
                    return Key.V;
                case Keys.W:
                    return Key.W;
                case Keys.X:
                    return Key.X;
                case Keys.Y:
                    return Key.Y;
                case Keys.Z:
                    return Key.Z;
                case Keys.World1:
                    return Key.LastKey + 1;
                case Keys.World2:
                    return Key.LastKey + 2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key));
            }
        }

        public static KeyModifiers ToKeyModifiers(this Enums.KeyModifiers modifiers)
        {
            KeyModifiers output = KeyModifiers.None;

            if (modifiers.HasFlag(Enums.KeyModifiers.Alt))
                output |= KeyModifiers.Alt;

            if (modifiers.HasFlag(Enums.KeyModifiers.CapsLock))
                output |= KeyModifiers.CapsLock;

            if (modifiers.HasFlag(Enums.KeyModifiers.Control))
                output |= KeyModifiers.Control;

            if (modifiers.HasFlag(Enums.KeyModifiers.NumLock))
                output |= KeyModifiers.NumLock;

            if (modifiers.HasFlag(Enums.KeyModifiers.Shift))
                output |= KeyModifiers.Shift;

            if (modifiers.HasFlag(Enums.KeyModifiers.Super))
                output |= KeyModifiers.Super;

            return output;
        }

        public static KeyEvent ToKeyEvent(this InputAction inputAction)
        {
            switch (inputAction)
            {
                case InputAction.Press:
                    return KeyEvent.Down;
                case InputAction.Release:
                    return KeyEvent.Up;
                case InputAction.Repeat:
                    return KeyEvent.Repeat;
                default:
                    throw new ArgumentOutOfRangeException(nameof(inputAction));
            }
        }

        public static MouseButton ToMouseButton(this Enums.MouseButton mouseButton)
        {
            switch (mouseButton)
            {
                case Enums.MouseButton.Button1:
                    return MouseButton.Button1;
                case Enums.MouseButton.Button2:
                    return MouseButton.Button2;
                case Enums.MouseButton.Button3:
                    return MouseButton.Button3;
                case Enums.MouseButton.Button4:
                    return MouseButton.Button4;
                case Enums.MouseButton.Button5:
                    return MouseButton.Button5;
                case Enums.MouseButton.Button6:
                    return MouseButton.Button6;
                case Enums.MouseButton.Button7:
                    return MouseButton.Button7;
                case Enums.MouseButton.Button8:
                    return MouseButton.Button8;
                default:
                    return MouseButton.None;
            }
        }

        public static CursorMode ToCursorMode(this CursorModeValue value)
        {
            switch (value)
            {
                case CursorModeValue.CursorDisabled:
                    return CursorMode.Disabled;
                case CursorModeValue.CursorHidden:
                    return CursorMode.Hidden;
                case CursorModeValue.CursorNormal:
                    return CursorMode.Visible;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }
    }
}
