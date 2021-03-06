﻿using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WolfGamesWebSite.Test.Framework.Facts;
using Microsoft.AspNetCore.Http;
using System.Net;
using Moq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

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
            _server = new TestServer(new WebHostBuilder()
                .UseConfiguration(_configuration)
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
            Route += "/update?=1000";
            
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
            Route += "/update?id=11111111-1111-1111-1111-111111111112&score=1&xposition=2&zposition=3";

            // Act
            HttpResponseMessage response = await Request(Route);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
