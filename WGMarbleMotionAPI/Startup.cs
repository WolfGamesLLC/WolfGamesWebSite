using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WGMarbleMotionAPI
{
    /// <summary>
    /// Main Startup class provided by the MS template
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Default constructor provided by the MS template
        /// </summary>
        /// <param name="configuration">An object that implements IConfiguration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// An get accessor of the Startup objects Configuration member
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The configure services method to allow services to be added to the 
        /// MVC pipeline
        /// </summary>
        /// <param name="services">An object that implements IServiceCollection</param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddMvc();
        }

        /// <summary>
        /// The configure method allows the MVC pipeline to be configured
        /// </summary>
        /// <param name="app">An object that implements IApplicationBuilder</param>
        /// <param name="env">An object that implements IHostingEnvironment</param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
