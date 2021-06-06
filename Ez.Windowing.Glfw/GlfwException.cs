using Ez.Windowing.GLFW.Native.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ez.Windowing.GLFW
{
    public class GlfwException : WindowingException
    {
        public GlfwException()
        {
        }

        public GlfwException(string message) : base(message)
        {

        }

        public GlfwException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected GlfwException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
