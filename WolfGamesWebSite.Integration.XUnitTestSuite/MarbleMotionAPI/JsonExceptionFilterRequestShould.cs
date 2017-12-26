using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WolfGamesWebSite.Test.Framework.Facts;
using Xunit;

namespace WolfGamesWebSite.Integration.XUnitTestSuite.MarbleMotionAPI
{
    /// <summary>
    /// Verify that a request that generates an <see cref="WGMarbleMotionAPI.Filters.JsonExceptionFilter"/>
    /// </summary>
    public class JsonExceptionFilterRequestShould : DefaultRequestShould
    {
        /// <summary>
        /// Varify a default request
        /// </summary>
        public JsonExceptionFilterRequestShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<WGMarbleMotionAPI.Startup>());
            _client = _server.CreateClient();

            Route = "/NotARoute";
        }

        /// <summary>
        /// A default request should return an Ok response
        /// </summary>
        /// <returns>A task</returns>
        [Fact]
        public async Task ReturnInternalErrorResponse()
        {
            // Act
            HttpResponseMessage response = await Request(Route);

            // Assert
            Assert.Equal("500 Internal Error", response.ReasonPhrase);
        }
    }
}
