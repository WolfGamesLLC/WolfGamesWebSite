using Microsoft.AspNetCore.Mvc.Formatters;
using Moq;
using Newtonsoft.Json;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using WGMarbleMotionAPI.Infrastructure;
using Xunit;

namespace WGMarbleMotionAPI.XUnitTestSuite.Infrastructure
{
    /// <summary>
    /// Test suite for the <see cref="IonOutputFormatter"/> class
    /// </summary>
    public class IonOutputFormatterShould
    {
        private JsonOutputFormatter _jsonOutputFormatter;

        /// <summary>
        /// Setup for IonOutputFormatter testing
        /// </summary>
        public IonOutputFormatterShould()
        {
            var mockSerializer = new Mock<JsonSerializerSettings>();
            var mockArray = new Mock<ArrayPool<char>>();
            _jsonOutputFormatter = new JsonOutputFormatter(mockSerializer.Object, mockArray.Object);
        }

        /// <summary>
        /// Require that the default constructor take a valid
        /// instance of a JsonOutputFormatter
        /// </summary>
        [Fact]
        public void RequireJsonOutputFormatterAtConstruction()
        {
            Assert.NotNull(new IonOutputFormatter(_jsonOutputFormatter));
        }

        /// <summary>
        /// The default constructor throws an error if null is 
        /// passed as an argument
        /// </summary>
        [Fact]
        public void ThrowArgumentNullExceptionFromConstructor()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new IonOutputFormatter(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: jsonOutputFormatter", ex.Message);
        }

        /// <summary>
        /// The default constructor should add the ion+json media type to 
        /// the supported media type collection
        /// </summary>
        [Fact]
        public void AddIonPlusJsonToMediaTypes()
        {
            var sut = new IonOutputFormatter(_jsonOutputFormatter);
            Assert.Contains("application/ion+json", sut.GetSupportedContentTypes("application/ion+json", null));
        }

        /// <summary>
        /// The default constructor should add support for UTF 8 encoding
        /// </summary>
        [Fact]
        public void AddUtf8ToEncodings()
        {
            var sut = new IonOutputFormatter(_jsonOutputFormatter);
            Assert.True(sut.SupportedEncodings.Contains(Encoding.UTF8));
        }
    }
}
