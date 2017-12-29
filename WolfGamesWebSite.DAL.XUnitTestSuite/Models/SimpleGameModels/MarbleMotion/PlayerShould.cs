using System;
using System.Collections.Generic;
using System.Text;
using WolfGamesWebSite.DAL.Models.SimpleGameModels.MarbleMotion;
using Xunit;

namespace WolfGamesWebSite.DAL.XUnitTestSuite.Models.SimpleGameModels.MarbleMotion
{
    /// <summary>
    /// Test suite for the <see cref="Player"/>
    /// model
    /// </summary>
    public class PlayerShould : ApiResourceShould
    {
        /// <summary>
        /// Initialize the test suite
        /// </summary>
        public PlayerShould()
        {
            Model = new Player();
        }

        /// <summary>
        /// Verify the model can be created
        /// </summary>
        [Fact]
        public void ShouldCreateAMarbleMotionModel()
        {
            Assert.NotNull(new Player());
        }
    }
}
