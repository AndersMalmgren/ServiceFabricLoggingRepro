using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace VanillaApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder => builder.AddTestConfiguration()) //Normally this is done through .json config. But douing it this way to prove it works when done through vanilla pipeline not service fabric
                .UseStartup<Startup>();
    }

    internal class VanillaCoreConfigurationProvider : ConfigurationProvider
    {

        public override void Load()
        {
            Data["Logging:LogLevel:Default"] = "Information";
            Data["Logging:Debug:LogLevel:Default"] = "Information";

        }
    }

    internal class VanillaCoreConfigurationSource : IConfigurationSource
    {


        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new VanillaCoreConfigurationProvider();
        }
    }

    public static class VanillaCoreConfigurationExtensions
    {
        public static IConfigurationBuilder AddTestConfiguration(this IConfigurationBuilder builder)
        {
            builder.Add(new VanillaCoreConfigurationSource());
            return builder;
        }
    }
}
