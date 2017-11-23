using System;
using System.Collections.Generic;
using System.Text;
using WolfGamesWebSite.DAL.Models.SimpleGameModels;
using Xunit;

namespace WolfGamesWebSite.XUnitTestSuite.Models.SimpleGameModels
{
    /// <summary>
    /// Test suite for the Marble Motion base table
    /// </summary>
    public class MarbleMotionModelShould
    {
        /// <summary>
        /// Standard object under test
        /// </summary>
        public MarbleMotionModel MarbleMotionModel { get; set; }

        /// <summary>
        /// Initialize the test suite
        /// </summary>
        public MarbleMotionModelShould()
        {
            MarbleMotionModel = new MarbleMotionModel();
        }

        /// <summary>
        /// Verify the model can be created
        /// </summary>
        [Fact]
        public void ShouldCreateAMarbleMotionModel()
        {
            Assert.NotNull(new MarbleMotionModel());
        }

        /// <summary>
        /// The id should be set and retrieved
        /// </summary>
        [Fact]
        public void ShouldSetAndGetId()
        {
            long expected = 12345;
            MarbleMotionModel.Id = expected;
            Assert.Equal(expected, MarbleMotionModel.Id);
        }

        /// <summary>
        /// The score should be set and retrieved
        /// </summary>
        [Fact]
        public void ShouldSetAndGetScore()
        {
            long expected = 12345;
            MarbleMotionModel.Score = expected;
            Assert.Equal(expected, MarbleMotionModel.Score);
        }

        /// <summary>
        /// The X position should be set and retrieved
        /// </summary>
        [Fact]
        public void ShouldSetAndGetXPosition()
        {
            int expected = 12345;
            MarbleMotionModel.XPosition = expected;
            Assert.Equal(expected, MarbleMotionModel.XPosition);
        }

        /// <summary>
        /// The Y position should be set and retrieved
        /// </summary>
        [Fact]
        public void ShouldSetAndGetYPosition()
        {
            int expected = 12345;
            MarbleMotionModel.ZPosition = expected;
            Assert.Equal(expected, MarbleMotionModel.ZPosition);
        }
    }
}
