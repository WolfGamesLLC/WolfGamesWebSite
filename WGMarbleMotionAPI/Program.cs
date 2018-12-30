using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WGMarbleMotionAPI
{
    /// <summary>
    /// The main Program object provided by the MS template
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The Main function of the program
        /// </summary>
        /// <param name="args">An array of strings containing the command line args</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// The builder method used to create the web service
        /// </summary>
        /// <param name="args">An array of strings containing the command line args</param>
        /// <returns>An object that implements IWebHost</returns>
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                            .ConfigureServices(s => s.AddSingleton<IStartupConfigurationService, StartupConfigurationService>())
                            .UseStartup<Startup>()
                            .Build();
        }
    }
}
