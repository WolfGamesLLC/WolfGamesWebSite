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

            Route = "/Players";
        }
    }
}
