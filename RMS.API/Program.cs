namespace RMS.API
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using System.IO;
    using System.Reflection;

    public class Program
    {
        /// <summary>
        /// The main method of the application
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
#if DEBUG
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSetting("detailedErrors", "true")
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    if (env.IsDevelopment())
                    {
                        var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                        if (appAssembly != null)
                        {
                            config.AddUserSecrets(appAssembly, optional: true);
                        }
                    }

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .UseIISIntegration()
                .UseStartup<Startup>()
                .CaptureStartupErrors(true)
                .Build();

            host.Run();
#else
            CreateWebHostBuilder(args).Build().Run();
#endif
        }

        /// <summary>
        /// Method for creating IWebHostBuilder
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>A builder for IWebHost</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
        }
    }
}