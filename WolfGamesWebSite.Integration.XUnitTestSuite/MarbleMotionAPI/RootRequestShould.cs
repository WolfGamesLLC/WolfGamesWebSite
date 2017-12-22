using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WolfGamesWebSite.Test.Framework.Facts;

namespace WolfGamesWebSite.Integration.XUnitTestSuite.MarbleMotionApi
{
    /// <summary>
    /// Integration Test suite for the <see cref="WGMarbleMotionApi.Controller.RootController"/>
    /// </summary>
    public class RootRequestShould : DefaultRequestShould<WGMarbleMotionAPI.Startup>
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        /// <summary>
        /// The route to be used in base integration tests
        /// </summary>
        public string Route { get; set; }

        private async Task<HttpResponseMessage> Request(string route)
        {
            var response = await _client.GetAsync(route);
            var responseString = await response.Content.ReadAsStringAsync();
            return response;
        }

        /// <summary>
        /// Varify a default request
        /// </summary>
        public RootRequestShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<WGMarbleMotionAPI.Startup>());
            _client = _server.CreateClient();

            Route = "/";
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

        /// <summary>
        /// A default request should use an application/ion+json content type
        /// </summary>
        /// <returns>A task</returns>
        [Fact]
        public async Task UseIonPlusJsonContentType()
        {
            // Act
            HttpResponseMessage response = await Request(Route);

            // Assert
            Assert.Equal("application/ion+json", response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task HaveDefaultVersion()
        {
            // Act
            HttpResponseMessage response = await Request(Route);

            // Assert
            Assert.Contains("1.0", response.Headers.GetValues("api-supported-versions"));
        }
    }
}
