using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Collections.Generic;
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
        [Fact]
        public void SetInternalErrorStatusCode()
        {
            var expRes = new ObjectResult(new ApiError())
            {
                StatusCode = 500
            };

            var mockContext = new Mock<HttpContext>();
            var mockRoute = new Mock<RouteData>();
            var mockDescriptor = new Mock<ActionDescriptor>();
            var mockAction = new Mock<ActionContext>();
            mockAction.Object.HttpContext = mockContext.Object;
            mockAction.Object.RouteData = mockRoute.Object;
            mockAction.Object.ActionDescriptor = mockDescriptor.Object;
            var context = new ExceptionContext(mockAction.Object, new List<IFilterMetadata>());
            var sut = new JsonExceptionFilter();

            sut.OnException(context);
            Assert.Equal(expRes.ToString(), context.Result.ToString());
        }
    }
}
