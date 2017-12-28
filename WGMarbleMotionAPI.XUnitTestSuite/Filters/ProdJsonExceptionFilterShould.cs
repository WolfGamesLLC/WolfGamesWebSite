using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }
}
