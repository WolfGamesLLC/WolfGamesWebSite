using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
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
    public abstract class JsonExceptionFilterShould
    {
        private ExceptionContext _exceptionContext;
        private JsonExceptionFilter _exceptionFilter;
        private ApiError _expectedError;

        private IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Allow the testing host environment to be modified
        /// </summary>
        protected IHostingEnvironment HostingEnvironment
        {
            get { return _hostingEnvironment; }
            set { _hostingEnvironment = value; }
        }


        /// <summary>
        /// Get the ApiError object from the context
        /// </summary>
        protected ApiError Error
        {
            get { return (ApiError)((ObjectResult)_exceptionContext.Result).Value; }
        }

        public JsonExceptionFilterShould()
        {
            var mockHttpContext = new Mock<HttpContext>();
            var mockRoute = new Mock<RouteData>();
            var mockDescriptor = new Mock<ActionDescriptor>();
            var mockAction = new Mock<ActionContext>();

            mockAction.Object.HttpContext = mockHttpContext.Object;
            mockAction.Object.RouteData = mockRoute.Object;
            mockAction.Object.ActionDescriptor = mockDescriptor.Object;

            _exceptionContext = new ExceptionContext(mockAction.Object, new List<IFilterMetadata>());
            try
            {
                throw new ArgumentException();
            }
            catch (ArgumentException e)
            {
                _exceptionContext.Exception = e;
            }

            _exceptionFilter = new JsonExceptionFilter(_hostingEnvironment);

            _expectedError = new ApiError
            {
                Message = _exceptionContext.Exception.Message,
                Detail = _exceptionContext.Exception.StackTrace
            };
        }

        /// <summary>
        /// The context's resulting StatusCode should be set to 
        /// <see cref="HttpStatusCode.InternalServerError"/> when
        /// the <see cref="JsonExceptionFilter.OnException(ExceptionContext)"/> is called
        /// </summary>
        [Fact]
        public void SetInternalErrorStatusCodeOnException()
        {
            _exceptionFilter.OnException(_exceptionContext);
            Assert.Equal((int)HttpStatusCode.InternalServerError, ((ObjectResult)_exceptionContext.Result).StatusCode);
        }

        /// <summary>
        /// The context's resulting message text should be set to 
        /// the context's exception message text when
        /// the <see cref="JsonExceptionFilter.OnException(ExceptionContext)"/> is called
        /// </summary>
        [Fact]
        public void SetErrorMessageTextOnException()
        {
            _exceptionFilter.OnException(_exceptionContext);
            Assert.Equal(_expectedError.Message, Error.Message);
        }

        /// <summary>
        /// The context's resulting detail text should be set to 
        /// the context's exception stack trace when
        /// the <see cref="JsonExceptionFilter.OnException(ExceptionContext)"/> is called
        /// </summary>
        [Fact]
        public void SetErrorDetailTextOnException()
        {
            _exceptionFilter.OnException(_exceptionContext);
            Assert.Equal(_expectedError.Detail, Error.Detail);
        }
    }
}
