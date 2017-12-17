using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace WolfGamesWebSite.Integration.XUnitTestSuite.WGMarbleMotionAPI
{
    /// <summary>
    /// Integratyion Test suite for the <see cref="WGMarbleMotionAPI"/>
    /// </summary>
    public class DefaultRequestShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public DefaultRequestShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<global::WGMarbleMotionAPI.Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task ReturnOKResponse()
        {
            // Act
            var response = await _client.GetAsync("/");
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("OK", response.ReasonPhrase);
        }

        [Fact]
        public async Task HaveIonPlusJsonContentType()
        {
            // Act
            var response = await _client.GetAsync("/");
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("application/ion+json", response.Content.Headers.ContentType.MediaType);
        }
    }
}
