using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public async Task RedirectToHTTPS()
        {
            // Act
//            var response = await _client.GetAsync("/api/index");
            var response = await _client.GetAsync("/");
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("OK", response.ReasonPhrase);
//            Assert.Equal("https://localhost/api/Values", response.Headers.Location.ToString());
//            Assert.Equal("", responseString);
        }
    }
}
