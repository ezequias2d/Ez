using Ez.Graphics;
using Ez.Graphics.API;
using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;
using Ez.Graphics.API.Vulkan;
using Ez.Windowing;
using Ez.Windowing.GLFW;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace VulkanExample
{
    class Program
    {

        private static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Device>>();

            var example = new VertexBufferExample(logger);
            example.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);
        }
    }
}
