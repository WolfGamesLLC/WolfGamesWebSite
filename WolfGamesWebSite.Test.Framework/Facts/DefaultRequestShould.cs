using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
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
        protected IConfigurationRoot _configuration;

        /// <summary>
        /// Do basic initialization for all tests 
        /// </summary>
        public DefaultRequestShould()
        {
            var dict = new Dictionary<string, string>
            {
                { "ConnectionStrings:DefaultConnection", "inmemory.testdb" }
            };


            _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(dict)
            .Build();
        }

        /// <summary>
        /// The route to be used in base integration tests
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Helper function that does the actual request and reads the response
        /// </summary>
        /// <param name="route">The route (URL) to be tested</param>
        /// <returns>The response message object</returns>
        protected async Task<HttpResponseMessage> Request(string route)
        {
            var response = await _client.GetAsync(route);
            var responseString = await response.Content.ReadAsStringAsync();
            return response;
        }

        /// <summary>
        /// All requests should use an application/ion+json content type
        /// </summary>
        /// <returns>none</returns>
        [Fact]
        public async Task UseIonPlusJsonContentType()
        {
            // Act
            HttpResponseMessage response = await Request(Route);

            // Assert
            Assert.Equal("application/ion+json", response.Content.Headers.ContentType.MediaType);
        }

        /// <summary>
        /// All requests responses should have a default version set
        /// </summary>
        /// <returns>none</returns>
        [Fact]
        public async Task HaveDefaultVersion()
        {
            // Act
            HttpResponseMessage response = await Request(Route);

            // Assert
            Assert.Contains("1.0", response.Headers.GetValues("api-supported-versions"));
        }

        /// <summary>
        /// Ensure all http requests are routed to https
        /// </summary>
        /// <returns>none</returns>
        [Fact]
        public async Task RedirectNonSSLRequest()
        {
            HttpResponseMessage response = await Request("http://localhost");

            Assert.Equal(HttpStatusCode.Found, response.StatusCode);
            Assert.Equal(new Uri("https://localhost"), response.Headers.Location);
        }

        /// <summary>
        /// Ensure all responses contain an hsts header
        /// </summary>
        /// <returns>none</returns>
        [Fact]
        public async Task AddHstsHeaders()
        {
            // arrange
            var expectedValues = new WGSystem.Collections.Generic.WGGenericCollectionsFactory().CreateList<string>();
            expectedValues.Add("max-age=31536000; includeSubDomains; preload");

            // apply
            HttpResponseMessage response = await Request(Route);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedValues, response.Headers.GetValues("Strict-Transport-Security"));
        }
    }
}
