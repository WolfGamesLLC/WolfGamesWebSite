using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using WolfGamesWebSite.Test.Framework.Facts;
using WolfGamesWebSite.DAL.Data;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using System;
using WGMarbleMotionAPI;
using WolfGamesWebSite.DAL.Models.SimpleGameModels.MarbleMotion;
using Newtonsoft.Json;
using WolfGamesWebSite.Test.Framework.Fixtures;
using System.Collections.Generic;

namespace WolfGamesWebSite.Integration.XUnitTestSuite.MarbleMotionApi
{
    /// <summary>
    /// Integration Test suite for the <see cref="WGMarbleMotionApi.Controller.PlayersController"/>
    /// </summary>
    public class PlayersRequestShould : DefaultRequestShould
    {
        /// <summary>
        /// Verify a default request
        /// </summary>
        public PlayersRequestShould()
        {
            var webHostBuilder = new WebHostBuilder();
            webHostBuilder.ConfigureServices(
                s => s.AddSingleton<IStartupConfigurationService, TestStartupConfigurationService<ApplicationDbContext>>());
            webHostBuilder.UseConfiguration(_configuration);
            webHostBuilder.UseStartup<WGMarbleMotionAPI.Startup>();
            _server = new TestServer(webHostBuilder);
            _client = _server.CreateClient();

            Route = "https://localhost:44340/api/players";
        }

        /// <summary>
        /// A default request should return an Ok response
        /// </summary>
        /// <returns>A task</returns>
        [Fact]
        public async Task ReturnOKResponse()
        {
            // apply
            SetupRequestAuthenticationHeaders();
            _client.BaseAddress = new Uri("https://localhost:44357");

            // Act
            HttpResponseMessage response = await Request(Route);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// A request that throws an error should return an 
        /// error ObjectResult and Json data of the exception
        /// </summary>
        /// <returns>A task</returns>
        [Fact]
        public async Task ReturnErrorResponseWithJsonDetail()
        {
            SetupRequestAuthenticationHeaders();
            Route += "https://localhost/api/update?=1000";
            
            // Act
            HttpResponseMessage response = await Request(Route);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        /// <summary>
        /// A create player request should return an Created response
        /// </summary>
        /// <returns>A task</returns>
        [Fact (Skip = "Until I can add an in memory db to be used by these tests")]
        public async Task ReturnCreatedResponse()
        {
            SetupRequestAuthenticationHeaders();
            Route += "/create?score=1&xposition=2&zposition=3";

            // Act
            HttpResponseMessage response = await Request(Route);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        /// <summary>
        /// An update player request should return an Created response
        /// </summary>
        /// <returns>A task</returns>
        [Fact (Skip = "Until I can add an in memory db to be used by these tests")]
        public async Task ReturnUpdatedResponse()
        {
            SetupRequestAuthenticationHeaders();
            Route += "/update?id=11111111-1111-1111-1111-111111111112&score=1&xposition=2&zposition=3";

            // Act
            HttpResponseMessage response = await Request(Route);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public void ReturnPlayerModelResourcesFromGet()
        {
            PlayerModelResource[] data =
            {
                new PlayerModelResource()
                {
                    Href = "https://localhost/api/players",
                    Score = 0,
                    XPosition = 1,
                    ZPosition = 2
                },

                new PlayerModelResource()
                {
                    Href = "https://localhost/api/players",
                    Score = 10,
                    XPosition = 11,
                    ZPosition = 12
                },
            };

            SetupRequestAuthenticationHeaders();

            var response = _client.GetAsync("https://localhost/api/players").Result;
            response.EnsureSuccessStatusCode();

            IEnumerable<string> iList;
            Assert.True(response.Headers.TryGetValues("api-supported-versions", out iList));

            var result = JsonConvert.DeserializeObject<PlayerModelResource[]>(response.Content.ReadAsStringAsync().Result);
            Assert.Equal(data.Length, result.Length);
            for (int i = 0; i < data.Length; i++)
            {
                Assert.Equal(data[i], result[i]);
            }
        }
    }
}
