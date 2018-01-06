using System;
using System.Collections.Generic;
using System.Text;
using WolfGamesWebSite.DAL.Models.SimpleGameModels.MarbleMotion;
using Xunit;

namespace WolfGamesWebSite.DAL.XUnitTestSuite.Models.SimpleGameModels.MarbleMotion
{
    /// <summary>
    /// Test suite for the <see cref="PlayerModelResource"/>
    /// model
    /// </summary>
    public class PlayerModelResourceShould : ApiResourceShould
    {
        /// <summary>
        /// Initialize the test suite
        /// </summary>
        public PlayerModelResourceShould()
        {
            Model = new PlayerModelResource();
        }

        /// <summary>
        /// Verify the model can be created
        /// </summary>
        [Fact]
        public void ShouldCreateAMarbleMotionModel()
        {
            Assert.NotNull(new PlayerModelResource());
        }

        /// <summary>
        /// The score should be set and retrieved
        /// </summary>
        [Fact]
        public void ShouldSetAndGetScore()
        {
            int expected = 100;
            ((PlayerModelResource)Model).Score = expected;
            Assert.Equal(expected, ((PlayerModelResource)Model).Score);
        }

        /// <summary>
        /// The X position should be set and retrieved
        /// </summary>
        [Fact]
        public void ShouldSetAndGetXPosition()
        {
            int expected = 12345;
            ((PlayerModelResource)Model).XPosition = expected;
            Assert.Equal(expected, ((PlayerModelResource)Model).XPosition);
        }

        /// <summary>
        /// The Y position should be set and retrieved
        /// </summary>
        [Fact]
        public void ShouldSetAndGetYPosition()
        {
            int expected = 12345;
            ((PlayerModelResource)Model).ZPosition = expected;
            Assert.Equal(expected, ((PlayerModelResource)Model).ZPosition);
        }
    }
}
