using System;
using Xunit;
using WolfGamesWebSite;
using WolfGamesWebSite.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WolfGamesWebSite.XUnitTestSuite
{
    /// <summary>
    /// Test suite for the home controller
    /// </summary>
    public class HomeControllerShould
    {
        /// <summary>
        /// The default constructor should create a <see cref="HomeController">home controller</see>
        /// </summary>
        [Fact]
        public void CreateHomeController()
        {
            Assert.IsType<HomeController>(new HomeController());
        }

        /// <summary>
        /// Index should return a <see cref="ViewResult"/>
        /// </summary>
        [Fact]
        public void IndexReturnsViewResult()
        {
            var result = new HomeController().Index();
            Assert.IsType<ViewResult>(result);
        }

        /// <summary>
        /// DevCorner should return a <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/>
        /// </summary>
        [Fact]
        public void DevCornerReturnsViewResult()
        {
            var result = new HomeController().DevCorner();
            Assert.IsType<ViewResult>(result);
        }
    }
}
