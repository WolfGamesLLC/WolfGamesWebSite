using System;
using System.Collections.Generic;
using System.Text;
using WolfGamesWebSite.DAL.Models.SimpleGameModels.MarbleMotion;
using Xunit;

namespace WolfGamesWebSite.DAL.XUnitTestSuite.Models.SimpleGameModels.MarbleMotion
{
    /// <summary>
    /// Test suite for the <see cref="PlayerModel"/>
    /// model
    /// </summary>
    public class PlayerModelShould : ApiResourceShould
    {
        /// <summary>
        /// Initialize the test suite
        /// </summary>
        public PlayerModelShould()
        {
            Model = new PlayerModel();
        }

        /// <summary>
        /// Verify the model can be created
        /// </summary>
        [Fact]
        public void ShouldCreateAMarbleMotionModel()
        {
            Assert.NotNull(new PlayerModel());
        }
    }
}
