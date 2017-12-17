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
        /// <summary>
        /// Require that the default constructor take a valid
        /// instance of a JsonOutputFormatter
        /// </summary>
        [Fact]
        public void RequireJsonOutputFormatterAtConstruction()
        {
            var mockSerializer = new Mock<JsonSerializerSettings>();
            var mockArray = new Mock<ArrayPool<char>>();
            var jsonOutputFormatter = new JsonOutputFormatter(mockSerializer.Object, mockArray.Object);
            Assert.NotNull(new IonOutputFormatter(jsonOutputFormatter));
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
    }
}
