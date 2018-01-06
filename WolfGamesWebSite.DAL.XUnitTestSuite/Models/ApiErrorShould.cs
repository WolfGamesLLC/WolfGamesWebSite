using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WolfGamesWebSite.DAL.Models;
using Xunit;
using Newtonsoft.Json;

namespace WolfGamesWebSite.DAL.XUnitTestSuite.Models
{
    /// <summary>
    /// Test suite for the standard <see cref="ApiError"/>
    /// </summary>
    public class ApiErrorShould
    {
        /// <summary>
        /// Standard object under test
        /// </summary>
        public ApiError ApiError { get; set; }

        /// <summary>
        /// Initialize the test suite
        /// </summary>
        public ApiErrorShould()
        {
            ApiError = new ApiError();
        }

        /// <summary>
        /// Verify the model can be created
        /// </summary>
        [Fact]
        public void CreateAnApiError()
        {
            Assert.NotNull(new ApiError());
        }

        /// <summary>
        /// The <see cref="ApiError.Message"/> should be set and retrieved
        /// </summary>
        [Fact]
        public void SetAndGetMessage()
        {
            string expected = "Message";
            ApiError.Message = expected;
            Assert.Equal(expected, ApiError.Message);
        }

        /// <summary>
        /// The <see cref="ApiError.Detail"/> should be set and retrieved
        /// </summary>
        [Fact]
        public void SetAndGetDetail()
        {
            string expected = "Detail";
            ApiError.Detail = expected;
            Assert.Equal(expected, ApiError.Detail);
        }

        /// <summary>
        /// The <see cref="ApiError.StackTrace"/> of the error should be set and retrieved
        /// </summary>
        [Fact]
        public void SetAndGetStackTrace()
        {
            string expected = "trace";
            ApiError.StackTrace = expected;
            Assert.Equal(expected, ApiError.StackTrace);
        }

        /// <summary>
        /// The <see cref="ApiError.StackTrace"/> of the error should default to an empty string
        /// </summary>
        [Fact]
        public void SetStackTraceDefault()
        {
            string expected = "";

            // Gets the attributes for the property.
            AttributeCollection attributes =
               TypeDescriptor.GetProperties(ApiError)["StackTrace"].Attributes;

            /* Prints the default value by retrieving the DefaultValueAttribute 
             * from the AttributeCollection. */
            DefaultValueAttribute myAttribute =
               (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
            Assert.Equal(expected, myAttribute.Value.ToString());
        }

        /// <summary>
        /// The <see cref="ApiError.StackTrace"/> of the error's NullValueHandling json 
        /// property should be set to ignore
        /// </summary>
        [Fact]
        public void SetStackTraceNullValueJsonProperty()
        {
            // Gets the attributes for the property.
            AttributeCollection attributes =
               TypeDescriptor.GetProperties(ApiError)["StackTrace"].Attributes;

            /* Prints the default value by retrieving the DefaultValueAttribute 
             * from the AttributeCollection. */
            JsonPropertyAttribute myAttribute =
               (JsonPropertyAttribute)attributes[typeof(JsonPropertyAttribute)];
            Assert.Equal(NullValueHandling.Ignore, myAttribute.NullValueHandling);
        }

        /// <summary>
        /// The <see cref="ApiError.StackTrace"/> of the error's DefaultValueHandling json 
        /// property should be set to ignore
        /// </summary>
        [Fact]
        public void SetDefaultValueHandlingJsonProperty()
        {
            // Gets the attributes for the property.
            AttributeCollection attributes =
               TypeDescriptor.GetProperties(ApiError)["StackTrace"].Attributes;

            /* Prints the default value by retrieving the DefaultValueAttribute 
             * from the AttributeCollection. */
            JsonPropertyAttribute myAttribute =
               (JsonPropertyAttribute)attributes[typeof(JsonPropertyAttribute)];
            Assert.Equal(DefaultValueHandling.Ignore, myAttribute.DefaultValueHandling);
        }
    }
}
