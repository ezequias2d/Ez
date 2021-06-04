using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ez.Graphics
{
    public class GraphicsException : Exception
    {
        public GraphicsException()
        {
        }

        public GraphicsException(string message) : base(message)
        {
        }

        public GraphicsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GraphicsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
