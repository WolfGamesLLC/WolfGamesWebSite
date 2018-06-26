using System;
using Xunit;
using WolfGamesWebSite;
using WolfGamesWebSite.Controllers;
using Microsoft.AspNetCore.Mvc;
using WolfGamesWebSite.Common.XUnitTest.Controllers;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Identity;
using WolfGamesWebSite.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Moq;

namespace WolfGamesWebSite.XUnitTestSuite
{

    /// <summary>
    /// Test suite for the <see cref="HomeControllerMessages"/> object
    /// </summary>
    public class HomeControllerMessageShould
    {
        /// <summary>
        /// The default constructor should set message to text
        /// </summary>
        [Fact]
        public void HomeControllerMessagesTest()
        {
            string text = "test text";
            Assert.Equal(text, new HomeControllerMessages(text).Message);
        }

        /// <summary>
        /// The About method should return about text
        /// </summary>
        [Fact]
        public void AboutTest()
        {
            Assert.Equal("About Wolf Games.", HomeControllerMessages.About());
        }

        /// <summary>
        /// The Contact method should return contact text
        /// </summary>
        [Fact]
        public void ContactTest()
        {
            Assert.Equal("We love to hear from you.", HomeControllerMessages.Contact());
        }

        /// <summary>
        /// The Error method should return error text
        /// </summary>
        [Fact]
        public void ErrorTest()
        {
            Assert.Equal("~/Views/Shared/Error.cshtml", HomeControllerMessages.Error());
        }

        /// <summary>
        /// The ThankYou method should return thank you text
        /// </summary>
        [Fact]
        public void ThankYouTest()
        {
            Assert.Equal("Thank you from all of us at Wolf Games.", HomeControllerMessages.ThankYou());
        }

        /// <summary>
        /// The DevCorner method should return dev corner intro text
        /// </summary>
        [Fact]
        public void DevCornerTest()
        {
            Assert.Equal("Welcome to Wolf Games' dev corner.", HomeControllerMessages.DevCorner());
        }
    }

    /// <summary>
    /// Test suite for the <see cref="HomeController"/>
    /// </summary>
    public class HomeControllerShould : BaseControllerShould<HomeController>
    {
        /// <summary>
        /// The test initializer for the suite
        /// </summary>
        public HomeControllerShould(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            var manager = new UserManager<ApplicationUser>(store.Object, null, null, null, null, null, null, null, null);
            Controller = new HomeController(manager);
        }

        /// <summary>
        /// The index action should return a ViewResult
        /// </summary>
        [Fact]
        public void IndexReturnsViewResult()
        {
            Result = Controller.Index() as ViewResult;
            Assert.IsType<ViewResult>(Result);
        }

        /// <summary>
        /// The DevCorner action should return a ViewResult
        /// </summary>
        [Fact]
        public void DevCornerReturnsViewResult()
        {
            Result = Controller.DevCorner() as ViewResult;
            Assert.IsType<ViewResult>(Result);
        }

        /// <summary>
        /// The about action returns a ViewResult
        /// </summary>
        [Fact]
        public void AboutReturnsViewResultWithAboutMessage()
        {
            Result = Controller.About() as ViewResult;
            Assert.IsType<ViewResult>(Result);
            Assert.Equal(HomeControllerMessages.About(), Result.ViewData["Message"]);
        }

        /// <summary>
        /// The contact action returns a ViewResult
        /// </summary>
        [Fact]
        public void ContactReturnsViewResultWithAboutMessage()
        {
            Result = Controller.Contact() as ViewResult;
            Assert.IsType<ViewResult>(Result);
            Assert.Equal(HomeControllerMessages.Contact(), Result.ViewData["Message"]);
        }
    }
}
