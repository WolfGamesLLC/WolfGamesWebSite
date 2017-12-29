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
using WGMarbleMotionAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using WGMarbleMotionAPI.Filters;

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
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(JsonExceptionFilter));
//                opt.Filters.Add(typeof(LinkRewritingFilter));
//
//                // Require HTTPS for all controllers
//                opt.SslPort = _httpsPort;
                opt.Filters.Add(typeof(RequireHttpsAttribute));

                var jsonFormatter = opt.OutputFormatters.OfType<JsonOutputFormatter>().Single();
                opt.OutputFormatters.Remove(jsonFormatter);
                opt.OutputFormatters.Add(new IonOutputFormatter(jsonFormatter));

//                opt.CacheProfiles.Add("Static", new CacheProfile { Duration = 86400 });
//                opt.CacheProfiles.Add("Collection", new CacheProfile { Duration = 60 });
//                opt.CacheProfiles.Add("Resource", new CacheProfile { Duration = 180 });
            });

            services.AddApiVersioning(opt =>
            {
                opt.ApiVersionReader = new MediaTypeApiVersionReader();
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opt);
            });
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
