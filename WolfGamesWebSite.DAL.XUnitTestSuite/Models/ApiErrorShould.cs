using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WolfGamesWebSite.DAL.Models;
using Xunit;

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
        /// The error message should be set and retrieved
        /// </summary>
        [Fact]
        public void SetAndGetMessage()
        {
            string expected = "Message";
            ApiError.Message = expected;
            Assert.Equal(expected, ApiError.Message);
        }

        /// <summary>
        /// The error detail should be set and retrieved
        /// </summary>
        [Fact]
        public void SetAndGetDetail()
        {
            string expected = "Detail";
            ApiError.Detail = expected;
            Assert.Equal(expected, ApiError.Detail);
        }

        /// <summary>
        /// The stack trace of the error should be set and retrieved
        /// </summary>
        [Fact]
        public void SetAndGetStackTrace()
        {
            string expected = "trace";
            ApiError.StackTrace = expected;
            Assert.Equal(expected, ApiError.StackTrace);
        }

        /// <summary>
        /// The stack trace of the error should be set and retrieved
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
    }
}
