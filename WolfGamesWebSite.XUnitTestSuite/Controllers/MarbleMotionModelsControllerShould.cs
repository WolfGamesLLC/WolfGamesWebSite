using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WolfGamesWebSite.Controllers;
using WolfGamesWebSite.DAL.Data;
using WolfGamesWebSite.DAL.Models.SimpleGameModels;
using Xunit;

namespace WolfGamesWebSite.XUnitTestSuite.Controllers
{
    /// <summary>
    /// Test suite for the Marble Motion API controller
    /// </summary>
    public class MarbleMotionModelsControllerShould
    {
        private string MARBLE_MOTION_TEST_DB = "MarbleMotionTestDB";
        MarbleMotionModelsController _controller;
        ApplicationDbContext _context;

        /// <summary>
        /// The test initializer for the suite
        /// </summary>
        public MarbleMotionModelsControllerShould()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseInMemoryDatabase(MARBLE_MOTION_TEST_DB);

            _context = new ApplicationDbContext(options.Options);

            _controller = new MarbleMotionModelsController(_context);

            if (_context.MarbleMotionModel.Count() == 0)
            {
                _context.MarbleMotionModel.Add(new MarbleMotionModel { Score = 1 });
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// The get action should return a list
        /// </summary>
        [Fact]
        public void GetReturnsMarbleMotionList()
        {
            Assert.IsType<InternalDbSet<MarbleMotionModel>>(_controller.GetMarbleMotionModel());
        }

        /// <summary>
        /// The get action should return a NotFoundResult
        /// </summary>
        [Fact]
        public async void GetReturnsNotFoundWhenNonIdRequested()
        {
            IActionResult actionResult = await _controller.GetMarbleMotionModel(5);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        /// <summary>
        /// The get action should return an IActionResult
        /// </summary>
        [Fact]
        public async void GetReturnsMarbleMotionIdRequested()
        {
            IActionResult actionResult = await _controller.GetMarbleMotionModel(1);
            Assert.IsType<OkObjectResult>(actionResult);

            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.IsType<MarbleMotionModel>(okObjectResult.Value);
        }

        /// <summary>
        /// The post action should create a MarbleMotion record
        /// </summary>
        [Fact]
        public async void PostCreatesMarbleMotionRecord()
        {
            var newMarble = new MarbleMotionModel { Id = 2, Score = 10, XPosition = 20, ZPosition = 30 };
            IActionResult actionResult = await _controller.PostMarbleMotionModel(newMarble);
            Assert.True(_context.MarbleMotionModel.Any(x => x == newMarble));
        }
    }
}
