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
using WolfGamesWebSite.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using WolfGamesWebSite.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using WolfGamesWebSite.DAL.Models.SimpleGameModels.MarbleMotion;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WGMarbleMotionAPI
{
    public interface IStartupConfigurationService
    {
        void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory);

        void ConfigureEnvironment(IHostingEnvironment env);

        void ConfigureService(IServiceCollection services, IConfiguration configuration);
    }
    #region TEST Code should be movede to test name space
    public class AuthenticatedTestRequestMiddleware

    {
        public const string TestingCookieAuthentication = "TestCookieAuthentication";
        public const string TestingHeader = "X-Integration-Testing";
        public const string TestingHeaderValue = "abcde-12345";
        public const string TestingHeaderName = "my_name";
        public const string TestingHeaderId = "my_id";
        private readonly RequestDelegate _next;

        public AuthenticatedTestRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.Keys.Contains(TestingHeader) &&
                context.Request.Headers[TestingHeader].First().Equals(TestingHeaderValue))
            {
                if (context.Request.Headers.Keys.Contains(TestingHeaderName))
                {
                    var name =
                        context.Request.Headers[TestingHeaderName].First();
                    var id =
                        context.Request.Headers.Keys.Contains(TestingHeaderId)
                            ? context.Request.Headers[TestingHeaderId].First() : "";

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, name),
                        new Claim(ClaimTypes.NameIdentifier, id),
                    }, TestingCookieAuthentication);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    context.User = claimsPrincipal;
                }
            }
            await _next(context);
        }
    }

    public class TestStartupConfigurationService<TDbContext> : IStartupConfigurationService
    where TDbContext : DbContext
    {
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            SetupStore(app);

            app.UseMiddleware<AuthenticatedTestRequestMiddleware>();
        }

        public virtual void ConfigureEnvironment(IHostingEnvironment env)
        {
            env.EnvironmentName = "Test";
        }

        public virtual void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureStore(services);
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        protected virtual void SetupStore(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<TDbContext>();

                dbContext.Database.OpenConnection();
                dbContext.Database.EnsureCreated();

                dbContext.Add(new PlayerModel()
                {
                    Score = 0,
                    XPosition = 1,
                    ZPosition = 2
                });

                dbContext.Add(new PlayerModel()
                {
                    Score = 10,
                    XPosition = 11,
                    ZPosition = 12
                });

                if (!dbContext.SaveChangesAsync(new CancellationToken()).IsCompletedSuccessfully)
                    throw new InvalidOperationException("Could not create the player.");
            }
        }

        protected virtual void ConfigureStore(IServiceCollection services)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            services.AddDbContext<TDbContext>(options => options.UseSqlite(connection));
        }
    }
    #endregion

    public class StartupConfigurationService : IStartupConfigurationService
    {
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) { }

        public virtual void ConfigureEnvironment(IHostingEnvironment env) { }

        public virtual void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("WolfGamesWebSite")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }

    public class Startup
    {
        private IStartupConfigurationService externalStartupConfiguration;
        private IConfiguration configuration;

        public Startup(IHostingEnvironment env, IConfiguration configuration, IStartupConfigurationService externalStartupConfiguration)
        {
            this.configuration = configuration;
            this.externalStartupConfiguration = externalStartupConfiguration;
            this.externalStartupConfiguration.ConfigureEnvironment(env);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Pass configuration (IConfigurationRoot) to the configuration service if needed
            this.externalStartupConfiguration.ConfigureService(services, configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAuthentication();

            this.externalStartupConfiguration.Configure(app, env, loggerFactory);

            //      using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //      {
            //          serviceScope.ServiceProvider.GetService <ApplicationDbContext>().Database.EnsureCreated();
            //      }

            app.UseMvc();
        }
    }

    //    /// <summary>
    //    /// Main Startup class provided by the MS template
    //    /// </summary>
    //    public class Startup
    //    {
    //        /// <summary>
    //        /// Default constructor provided by the MS template
    //        /// </summary>
    //        /// <param name="configuration">An object that implements IConfiguration</param>
    //        public Startup(IConfiguration configuration)
    //        {
    //            Configuration = configuration;
    //        }
    //
    //        /// <summary>
    //        /// An get accessor of the Startup objects Configuration member
    //        /// </summary>
    //        public IConfiguration Configuration { get; }
    //
    //        /// <summary>
    //        /// The configure services method to allow services to be added to the 
    //        /// MVC pipeline
    //        /// </summary>
    //        /// <param name="services">An object that implements IServiceCollection</param>
    //        // This method gets called by the runtime. Use this method to add services to the container.
    //        public void ConfigureServices(IServiceCollection services)
    //        {
    ////            services.AddDbContext<ApplicationDbContext>(options =>
    ////                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("WolfGamesWebSite")));
    ////
    ////            services.AddCors();            
    ////            services.AddRouting(opt => opt.LowercaseUrls = true);
    ////
    ////            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
    ////            {
    ////                config.SignIn.RequireConfirmedEmail = true;
    ////            })
    ////                .AddEntityFrameworkStores<ApplicationDbContext>()
    ////                .AddDefaultTokenProviders();
    ////
    ////            services.AddMvc(opt =>
    ////            {
    ////                opt.Filters.Add(typeof(JsonExceptionFilter));
    //////                opt.Filters.Add(typeof(LinkRewritingFilter));
    //////
    //////                // Require HTTPS for all controllers
    //////                opt.SslPort = _httpsPort;
    ////                opt.Filters.Add(typeof(RequireHttpsAttribute));
    ////
    ////                var jsonFormatter = opt.OutputFormatters.OfType<JsonOutputFormatter>().Single();
    ////                opt.OutputFormatters.Remove(jsonFormatter);
    ////                opt.OutputFormatters.Add(new IonOutputFormatter(jsonFormatter));
    ////
    //////                opt.CacheProfiles.Add("Static", new CacheProfile { Duration = 86400 });
    //////                opt.CacheProfiles.Add("Collection", new CacheProfile { Duration = 60 });
    //////                opt.CacheProfiles.Add("Resource", new CacheProfile { Duration = 180 });
    ////            });
    ////
    ////            services.AddApiVersioning(opt =>
    ////            {
    ////                opt.ApiVersionReader = new MediaTypeApiVersionReader();
    ////                opt.AssumeDefaultVersionWhenUnspecified = true;
    ////                opt.ReportApiVersions = true;
    ////                opt.DefaultApiVersion = new ApiVersion(1, 0);
    ////                opt.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opt);
    ////            });
    ////
    ////            // Register the Swagger generator, defining one or more Swagger documents
    ////            services.AddSwaggerGen(c =>
    ////            {
    ////                c.SwaggerDoc("v1", new Info { Title = "WG MarbleMotion API", Version = "v1" });
    ////            });
    //        }
    //
    //        /// <summary>
    //        /// The configure method allows the MVC pipeline to be configured
    //        /// </summary>
    //        /// <param name="app">An object that implements IApplicationBuilder</param>
    //        /// <param name="env">An object that implements IHostingEnvironment</param>
    //        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    //        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    //        {
    ////            if (env.IsDevelopment())
    ////            {
    ////                app.UseDeveloperExceptionPage();
    ////            }
    ////
    ////            app.UseHsts(opt => 
    ////            {
    ////                opt.MaxAge(days: 365);
    ////                opt.IncludeSubdomains();
    ////                opt.Preload();
    ////            });
    ////
    //////            app.UseCors(builder => builder.WithOrigins("https://localhost:44357")
    //////                                    .AllowAnyMethod()
    //////                                    .AllowAnyHeader());
    //////            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    ////
    ////            // Enable middleware to serve generated Swagger as a JSON endpoint.
    ////            app.UseSwagger();
    ////
    ////            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
    ////            app.UseSwaggerUI(c =>
    ////            {
    ////                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Marble Motion V1");
    ////            });
    ////
    ////            app.UseMvc();
    //        }
    //    }
}
