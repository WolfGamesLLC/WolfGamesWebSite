using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace WolfGamesWebSite.Integration.XUnitTestSuite.MarbleMotionApi
{
    /// <summary>
    /// Integratyion Test suite for the <see cref="MarbleMotionApi"/>
    /// </summary>
    public class DefaultRequestShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        private async Task<HttpResponseMessage> Request(string route)
        {
            var response = await _client.GetAsync(route);
            var responseString = await response.Content.ReadAsStringAsync();
            return response;
        }

        /// <summary>
        /// Varify a default request
        /// </summary>
        public DefaultRequestShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<global::WGMarbleMotionAPI.Startup>());
            _client = _server.CreateClient();
        }

        /// <summary>
        /// A default request should return an Ok response
        /// </summary>
        /// <returns>A task</returns>
        [Fact]
        public async Task ReturnOKResponse()
        {
            // Act
            HttpResponseMessage response = await Request("/");

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
            HttpResponseMessage response = await Request("/");

            // Assert
            Assert.Equal("application/ion+json", response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task HaveDefaultVersion()
        {
            // Act
            HttpResponseMessage response = await Request("/");

            // Assert
            Assert.Contains("1.0", response.Headers.GetValues("api-supported-versions"));
        }
    }
}
