using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using WGXUnit.Facts;
using Xunit.Abstractions;

namespace WolfGamesWebSite.Common.XUnitTest.Controllers
{
    /// <summary>
    /// Basic tests members and facts that all controller test suites require
    /// </summary>
    public abstract class BaseControllerShould<T> : FactWriteToStdOut
    {
        public BaseControllerShould(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        protected T Controller;
        protected ViewResult Result;
    }
}
