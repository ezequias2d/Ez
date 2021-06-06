using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ez.Graphics.Contexts
{
    /// <summary>
    /// Encapsulates a Vulkan context.
    /// </summary>
    public struct VulkanContext
    {
        /// <summary>
        /// A delegate which can be used to retrieve Vulkan function pointers by VkInstance and name.
        /// </summary>
        public Func<IntPtr, string, IntPtr> GetInstanceProcAddress { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="VulkanContext"/> struct.
        /// </summary>
        /// <param name="getInstanceProcAddress">A delegate which can be used to retrieve Vulkan function pointers by VkInstance and name.</param>
        public VulkanContext(Func<IntPtr, string, IntPtr> getInstanceProcAddress)
        {
            GetInstanceProcAddress = getInstanceProcAddress;
        }
    }
}
