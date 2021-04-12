using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ez
{
    /// <summary>
    /// Base exception class for exceptions of EzLib.
    /// </summary>
    public class EzException : Exception
    {
        public EzException()
        {
        }

        public EzException(string message) : base(message)
        {
        }

        public EzException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EzException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
