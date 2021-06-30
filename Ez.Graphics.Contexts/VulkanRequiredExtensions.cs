using System.Collections.Generic;
using System.Linq;

namespace Ez.Graphics.Contexts
{
    /// <summary>
    /// Encapsulates a Vulkan required extensions.
    /// </summary>
    public readonly struct VulkanRequiredExtensions
    {
        /// <summary>
        /// The required extensions of vulkan instance.
        /// </summary>
        public ISet<string> RequiredInstanceExtensions { get; }

        /// <summary>
        /// The required extensions of vulkan device.
        /// </summary>
        public ISet<string> RequiredDeviceExtensions { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="VulkanRequiredExtensions"/> struct.
        /// </summary>
        /// <param name="requiredInstanceExtensions">Required instance extensions for the vulkan.</param>
        /// <param name="requiredDeviceExtensions">Required device extensions for the vulkan.</param>
        public VulkanRequiredExtensions(IEnumerable<string> requiredInstanceExtensions, IEnumerable<string> requiredDeviceExtensions)
        {
            RequiredInstanceExtensions = new HashSet<string>(requiredInstanceExtensions.ToArray());
            RequiredDeviceExtensions = new HashSet<string>(requiredDeviceExtensions.ToArray());
        }
    }
}
