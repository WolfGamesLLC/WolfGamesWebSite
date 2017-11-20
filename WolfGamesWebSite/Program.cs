using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WolfGamesWebSite
{
    /// <summary>
    /// The main program class of the Wolf Games site - created by the MS template
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main function of the site
        /// </summary>
        /// <param name="args">List of args passed to the app</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// The static builder function microsoft generated to create the web host
        /// used by the Wolf Games site app
        /// </summary>
        /// <param name="args">The command line args are passed into here</param>
        /// <returns>An object that implements IWebHost</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .Build();
    }
}
