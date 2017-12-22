using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WolfGamesWebSite.Test.Framework.Facts
{
    /// <summary>
    /// Base integration test suite for web apps
    /// </summary>
    public abstract class DefaultRequestShould
    {
        protected TestServer _server;
        protected HttpClient _client;

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
