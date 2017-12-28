using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using WGMarbleMotionAPI.Filters;

namespace WGMarbleMotionAPI.XUnitTestSuite.Filters
{
    /// <summary>
    /// Test the <see cref="JsonExceptionFilter"/> in a development environment 
    /// </summary>
    public class DevJsonExceptionFilterShould : JsonExceptionFilterShould
    {
        public DevJsonExceptionFilterShould()
            : base()
        {
            HostingEnvironment = new HostingEnvironment()
            {
                EnvironmentName = "Development"
            };
        }
    }
}
