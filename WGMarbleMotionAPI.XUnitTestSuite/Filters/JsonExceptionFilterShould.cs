using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Collections.Generic;
using System.Net;
using WGMarbleMotionAPI.Filters;
using WolfGamesWebSite.DAL.Models;
using Xunit;

namespace WGMarbleMotionAPI.XUnitTestSuite.Filters
{
    /// <summary>
    /// Test suite for the standard <see cref="JsonExceptionFilter"/>
    /// </summary>
    public class JsonExceptionFilterShould
    {
        private ExceptionContext _context;
        private JsonExceptionFilter _exceptionFilter;
        private ApiError _expectedError;

        /// <summary>
        /// Get the ApiError object from the context
        /// </summary>
        protected ApiError Error
        {
            get { return (ApiError)((ObjectResult)_context.Result).Value; }
        }

        public JsonExceptionFilterShould()
        {
            var mockContext = new Mock<HttpContext>();
            var mockRoute = new Mock<RouteData>();
            var mockDescriptor = new Mock<ActionDescriptor>();
            var mockAction = new Mock<ActionContext>();
            mockAction.Object.HttpContext = mockContext.Object;
            mockAction.Object.RouteData = mockRoute.Object;
            mockAction.Object.ActionDescriptor = mockDescriptor.Object;

            _context = new ExceptionContext(mockAction.Object, new List<IFilterMetadata>());
            _exceptionFilter = new JsonExceptionFilter();

            _expectedError = new ApiError();
            _expectedError.Message = "error text";
        }

        /// <summary>
        /// The context's resulting StatusCode should be set to 
        /// <see cref="HttpStatusCode.InternalServerError"/> when
        /// the <see cref="JsonExceptionFilter.OnException(ExceptionContext)"/> is called
        /// </summary>
        [Fact]
        public void SetInternalErrorStatusCodeOnException()
        {
            _exceptionFilter.OnException(_context);
            Assert.Equal((int)HttpStatusCode.InternalServerError, ((ObjectResult)_context.Result).StatusCode);
        }

        /// <summary>
        /// The context's resulting message text should be set to 
        /// ??? when
        /// the <see cref="JsonExceptionFilter.OnException(ExceptionContext)"/> is called
        /// </summary>
        [Fact]
        public void SetErrorMessageTextOnException()
        {
            _exceptionFilter.OnException(_context);
            Assert.Equal(_expectedError.Message, Error.Message);
        }
    }
}
