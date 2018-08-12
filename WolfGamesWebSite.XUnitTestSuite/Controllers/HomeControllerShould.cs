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
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using System.Collections.Generic;
using System.IO;

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
        private Mock<IUserStore<ApplicationUser>> _store;

        /// <summary>
        /// The test initializer for the suite
        /// </summary>
        public HomeControllerShould(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            _store = new Mock<IUserStore<ApplicationUser>>();
            var manager = new UserManager<ApplicationUser>(_store.Object, null, null, null, null, null, null, null, null);
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

        /// <summary>
        /// The MarbleMotion action returns a RedirectResult
        /// </summary>
        [Fact]
        public async void MarbleMotionReturnsViewResultWithAboutMessage()
        {
            var mockStore = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            var user = new ApplicationUser() { Id = "1" };

            mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .Returns(Task.FromResult(user)); 

            var sut = new HomeController(mockUserManager.Object);
            sut.ControllerContext = new ControllerContext();
            var mockContext = new Mock<HttpContext>();
            mockContext.Setup(x => x.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>()));

            sut.ControllerContext.HttpContext = mockContext.Object;
            var Result = await sut.MarbleMotion() as RedirectResult;

            Assert.IsType<RedirectResult>(Result);
            Assert.Equal("../SimpleGames/WebGl/MarbleMotion/index.html", Result.Url);
            mockContext.Verify(x => x.Response.Cookies.Append("id", user.Id));
        }
    }
}
