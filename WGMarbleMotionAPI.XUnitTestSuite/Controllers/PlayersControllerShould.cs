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
    /// Test suite for the <see cref="PlayersController"/>
    /// </summary>
    public class PlayersControllerShould : BaseControllerShould<PlayersController>
    {
        private Mock<IUrlHelper> mockUrl;

        /// <summary>
        /// The test initializer for the suite
        /// </summary>
        public PlayersControllerShould()
        {
            Controller = new PlayersController();
            mockUrl = new Mock<IUrlHelper>();
            mockUrl.Setup(r => r.Link("GetPlayers",null)).Returns("http://GetPlayers");
            Controller.Url = mockUrl.Object;
        }

        /// <summary>
        /// The <see cref="PlayersController.GetPlayers"/> action should return a OkObjectResult
        /// </summary>
        [Fact]
        public void GetPlayersReturnsOkObjectResult()
        {
            Assert.IsType<OkObjectResult>(Controller.GetPlayers());
        }

        /// <summary>
        /// The <see cref="PlayersController.GetPlayers"/> action should return a ViewResult
        /// </summary>
        [Fact]
        public void GetPlayersReturnsStatusCodeValueHrefEqualGetPlayers()
        {
            OkObjectResult response = Controller.GetPlayers() as OkObjectResult;
            var exp = new { href = "http://GetPlayers" };
            Assert.Equal(exp.ToString(), response.Value.ToString());
        }
    }
}
