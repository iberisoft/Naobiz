using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Globalization;
using Topshelf;

namespace Naobiz
{
    public class Program
    {
        public static int Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-ES");

            var exitCode = HostFactory.Run(hostConfig =>
            {
#if !DEBUG
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
#endif
                hostConfig.Service<Service>(serviceConfig =>
                {
                    serviceConfig.ConstructUsing(() => new Service(CreateHostBuilder(args).Build()));
                    serviceConfig.WhenStarted(service => service.Start());
                    serviceConfig.WhenStopped(service => service.Stop());
                });
                hostConfig.RunAsLocalService();
                hostConfig.SetServiceName("Naobiz");
                hostConfig.SetDescription("E-commerce portal");
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var settings = config.Get<Settings>();
                hostConfig.DependsOn(settings.DependeeServiceName);
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
