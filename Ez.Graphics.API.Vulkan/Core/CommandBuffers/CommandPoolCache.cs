using Ez.Graphics.API.Vulkan.Core.Cached;
using System.Threading;

namespace Ez.Graphics.API.Vulkan.Core.CommandBuffers
{
    internal class CommandPoolCache : Cache<Thread, CommandPool>
    {
        public CommandPoolCache(Device device)
        {
            Device = device;
        }

        public Device Device { get; }

        public override CommandPool CreateCached(in Thread thread) => new (Device, thread);
    }
}
