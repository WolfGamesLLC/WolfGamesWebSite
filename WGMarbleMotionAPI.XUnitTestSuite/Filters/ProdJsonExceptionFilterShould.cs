using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using WGMarbleMotionAPI.Filters;
using WolfGamesWebSite.DAL.Models;

namespace WGMarbleMotionAPI.XUnitTestSuite.Filters
{
    /// <summary>
    /// Test the <see cref="JsonExceptionFilter"/> in a production environment 
    /// </summary>
    public class ProdJsonExceptionFilterShould : JsonExceptionFilterShould
    {
        public ProdJsonExceptionFilterShould()
            : base()
        {
            HostingEnvironment = new HostingEnvironment();

            ExpectedError = new ApiError
            {
                Message = "A server error occurred.",
                Detail = ExceptionContext.Exception.Message
            };

            ExceptionFilter = new JsonExceptionFilter(HostingEnvironment);
        }
    }
}
