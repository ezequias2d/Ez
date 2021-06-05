using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ez.Graphics
{
    /// <inheritdoc/>
    public class GraphicsException : Exception
    {
        /// <inheritdoc/>
        public GraphicsException()
        {
        }

        /// <inheritdoc/>
        public GraphicsException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public GraphicsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        protected GraphicsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
