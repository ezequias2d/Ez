using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ez.Threading
{
    public class EzThreadException : Exception
    {
        public EzThreadException()
        {
        }

        public EzThreadException(string message) : base(message)
        {
        }

        public EzThreadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EzThreadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
