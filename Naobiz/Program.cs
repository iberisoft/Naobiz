using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Topshelf;

namespace Naobiz
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var exitCode = HostFactory.Run(hostConfig =>
            {
                hostConfig.Service<Service>(serviceConfig =>
                {
                    serviceConfig.ConstructUsing(() => new Service(CreateHostBuilder(args).Build()));
                    serviceConfig.WhenStarted(service => service.Start());
                    serviceConfig.WhenStopped(service => service.Stop());
                });
                hostConfig.RunAsLocalService();
                hostConfig.SetServiceName("Naobiz");
                hostConfig.SetDescription("E-commerce portal");
            });
            return (int)exitCode;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .UseSerilog((hostContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        class Service
        {
            readonly IHost m_Host;

            public Service(IHost host) => m_Host = host;

            public void Start() => m_Host.Start();

            public void Stop() => m_Host.Dispose();
        }
    }
}
