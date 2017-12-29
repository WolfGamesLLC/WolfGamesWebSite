using System;
using System.Collections.Generic;
using System.Text;
using WolfGamesWebSite.DAL.Models;
using Xunit;

namespace WolfGamesWebSite.DAL.XUnitTestSuite.Models
{
    /// <summary>
    /// Test suite for the an <see cref="ApiResource"/>
    /// </summary>
    public abstract class ApiResourceShould
    {
        /// <summary>
        /// Standard object under test
        /// </summary>
        public ApiResource Model { get; set; }

        /// <summary>
        /// The id should be set and retrieved
        /// </summary>
        [Fact]
        public void ShouldSetAndGetHref()
        {
            string expected = "some link";
            Model.Href = expected;
            Assert.Equal(expected, Model.Href);
        }
    }
}
