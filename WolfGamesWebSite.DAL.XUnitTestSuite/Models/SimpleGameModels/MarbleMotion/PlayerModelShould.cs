using System;
using System.Collections.Generic;
using System.Text;
using WolfGamesWebSite.DAL.Models.SimpleGameModels.MarbleMotion;
using Xunit;

namespace WolfGamesWebSite.DAL.XUnitTestSuite.Models.SimpleGameModels.MarbleMotion
{
    /// <summary>
    /// Test of the <see cref="PlayerModel"/> db access object
    /// </summary>
    public class PlayerModelShould
    {
        private PlayerModel _player;

        /// <summary>
        /// Initialize the test suite
        /// </summary>
        public PlayerModelShould()
        {
            _player = new PlayerModel();
        }

        /// <summary>
        /// verify that an instance of PlayerModel can be created
        /// </summary>
        [Fact]
        public void CreatePlayerModel()
        {
            Assert.NotNull(new PlayerModel());
        }

        /// <summary>
        /// The id should be set and retrieved
        /// </summary>
        [Fact]
        public void ShouldSetAndGetId()
        {
            Guid expected = new Guid();
            _player.Id = expected;
            Assert.Equal(expected, _player.Id);
        }

        /// <summary>
        /// The score should be set and retrieved
        /// </summary>
        [Fact]
        public void ShouldSetAndGetScore()
        {
            long expected = 12345;
            _player.Score = expected;
            Assert.Equal(expected, _player.Score);
        }

        /// <summary>
        /// The X position should be set and retrieved
        /// </summary>
        [Fact]
        public void ShouldSetAndGetXPosition()
        {
            int expected = 12345;
            _player.XPosition = expected;
            Assert.Equal(expected, _player.XPosition);
        }

        /// <summary>
        /// The Y position should be set and retrieved
        /// </summary>
        [Fact]
        public void ShouldSetAndGetYPosition()
        {
            int expected = 12345;
            _player.ZPosition = expected;
            Assert.Equal(expected, _player.ZPosition);
        }
    }
}
