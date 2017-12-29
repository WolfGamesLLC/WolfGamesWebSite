using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        /// <summary>
        /// The order of the <see cref="ApiResource.Href"/> json 
        /// property should be set to ensure it is printed first
        /// </summary>
        [Fact]
        public void SetHrefOrderJsonProperty()
        {
            // Gets the attributes for the property.
            AttributeCollection attributes =
               TypeDescriptor.GetProperties(Model)["Href"].Attributes;

            /* Prints the default value by retrieving the DefaultValueAttribute 
             * from the AttributeCollection. */
            JsonPropertyAttribute myAttribute =
               (JsonPropertyAttribute)attributes[typeof(JsonPropertyAttribute)];
            Assert.Equal(-2, myAttribute.Order);
        }
    }
}
