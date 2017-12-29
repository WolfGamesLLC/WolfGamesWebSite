using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WolfGamesWebSite.Test.Framework.Facts;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace WolfGamesWebSite.Integration.XUnitTestSuite.MarbleMotionApi
{
    /// <summary>
    /// Integration Test suite for the <see cref="WGMarbleMotionApi.Controller.PlayersController"/>
    /// </summary>
    public class PlayersRequestShould : DefaultRequestShould
    {
        /// <summary>
        /// Varify a default request
        /// </summary>
        public PlayersRequestShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<WGMarbleMotionAPI.Startup>());
            _client = _server.CreateClient();

            Route = "https://localhost:44340/Players";
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
        /// A request for a non-existent player should return an 
        /// error ObjectResult and Json data of the exception
        /// </summary>
        /// <returns>A task</returns>
        [Fact]
        public async Task ReturnErrorResponseWithJsonDetail()
        {
            Route += "?id=1000";
            
            // Act
            HttpResponseMessage response = await Request(Route);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
