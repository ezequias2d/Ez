using Silk.NET.Vulkan;
using System;
using System.Runtime.Serialization;

namespace Ez.Graphics.API.Vulkan
{
    public class VkResultErrorException : VkException
    {
        public VkResultErrorException()
        {
        }

        public VkResultErrorException(string message) : base(message)
        {
        }

        public VkResultErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VkResultErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public VkResultErrorException(Result result)
            : base($"Vulkan Result Exception: {result}")
        {

        }

        public VkResultErrorException(Result result, string message)
            : base($"Vulkan Result Exception: {result} with message '{message}'")
        {

        }
    }
}
