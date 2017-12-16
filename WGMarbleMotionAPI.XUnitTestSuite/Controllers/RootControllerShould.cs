using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WGMarbleMotionAPI.Controllers;
using WolfGamesWebSite.Common.XUnitTest.Controllers;
using Xunit;

namespace WGMarbleMotionAPI.XUnitTestSuite.Controllers
{
    /// <summary>
    /// Test suite for the <see cref="RootController"/>
    /// </summary>
    public class RootControllerShould : BaseControllerShould<RootController>
    {
        private Mock<IUrlHelper> mockUrl;

        /// <summary>
        /// The test initializer for the suite
        /// </summary>
        public RootControllerShould()
        {
            Controller = new RootController();
            mockUrl = new Mock<IUrlHelper>();
            mockUrl.Setup(r => r.Link("GetRoot",null)).Returns("http://GetRoot");
            Controller.Url = mockUrl.Object;
        }

        /// <summary>
        /// The <see cref="RootController.GetRoot"/> action should return a OkObjectResult
        /// </summary>
        [Fact]
        public void GetRootReturnsOkObjectResult()
        {
            Assert.IsType<OkObjectResult>(Controller.GetRoot());
        }

        /// <summary>
        /// The <see cref="RootController.GetRoot"/> action should return a ViewResult
        /// </summary>
        [Fact]
        public void GetRootReturnsStatusCodeValueHrefEqualGetRoot()
        {
            OkObjectResult response = Controller.GetRoot() as OkObjectResult;
            var exp = new { href = "http://GetRoot" };
            Assert.Equal(exp.ToString(), response.Value.ToString());
        }
    }
}
