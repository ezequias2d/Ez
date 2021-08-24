using System;

namespace Ez.Tests.Helpers
{
    internal class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(object customValue)
        {
            CustomValue = customValue;
        }
        public object CustomValue { get; }
    }
}
