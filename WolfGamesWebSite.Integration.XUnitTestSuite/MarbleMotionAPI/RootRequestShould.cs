using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WolfGamesWebSite.Test.Framework.Facts;
using System.Net;
using System;
using WGMarbleMotionAPI;
using WolfGamesWebSite.Test.Framework.Fixtures;
using WolfGamesWebSite.DAL.Data;
using Microsoft.Extensions.DependencyInjection;

namespace WolfGamesWebSite.Integration.XUnitTestSuite.MarbleMotionApi
{
    /// <summary>
    /// Integration Test suite for the <see cref="WGMarbleMotionApi.Controller.RootController"/>
    /// </summary>
    public class RootRequestShould : DefaultRequestShould
    {
        /// <summary>
        /// Varify a default request
        /// </summary>
        public RootRequestShould()
        {
            // Arrange
            var webHostBuilder = new WebHostBuilder();
            webHostBuilder.ConfigureServices(
                s => s.AddSingleton<IStartupConfigurationService, TestStartupConfigurationService<ApplicationDbContext>>());
            webHostBuilder.UseConfiguration(_configuration);
            webHostBuilder.UseStartup<WGMarbleMotionAPI.Startup>();
            _server = new TestServer(webHostBuilder);
            _client = _server.CreateClient();

            Route = "https://localhost/api";
        }

        /// <summary>
        /// A default request should return an Ok response
        /// </summary>
        /// <returns>A task</returns>
        [Fact]
        public async Task ReturnOKResponse()
        {
            // Act
            HttpResponseMessage response = await Request(Route);

            // Assert
            Assert.Equal("OK", response.ReasonPhrase);
        }
    }
}
