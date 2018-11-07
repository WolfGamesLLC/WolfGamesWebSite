using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WolfGamesWebSite.DAL.Models;
using Microsoft.AspNetCore.Identity;
using WolfGamesWebSite.DAL.Data;
using WolfGamesWebSite.DAL.Models.SimpleGameModels.MarbleMotion;
using System.Threading;
using Microsoft.Data.Sqlite;
using WGMarbleMotionAPI;

namespace WolfGamesWebSite.Test.Framework.Fixtures
{
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
}
