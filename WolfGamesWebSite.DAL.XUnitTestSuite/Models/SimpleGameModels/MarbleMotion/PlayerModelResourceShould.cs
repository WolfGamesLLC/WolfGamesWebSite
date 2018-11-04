using System;
using System.Collections;
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
        public void CreateAMarbleMotionModel()
        {
            Assert.NotNull(new PlayerModelResource());
        }

        /// <summary>
        /// The score should be set and retrieved
        /// </summary>
        [Fact]
        public void SetAndGetScore()
        {
            int expected = 100;
            ((PlayerModelResource)Model).Score = expected;
            Assert.Equal(expected, ((PlayerModelResource)Model).Score);
        }

        /// <summary>
        /// The X position should be set and retrieved
        /// </summary>
        [Fact]
        public void SetAndGetXPosition()
        {
            int expected = 12345;
            ((PlayerModelResource)Model).XPosition = expected;
            Assert.Equal(expected, ((PlayerModelResource)Model).XPosition);
        }

        /// <summary>
        /// The Y position should be set and retrieved
        /// </summary>
        [Fact]
        public void SetAndGetYPosition()
        {
            int expected = 12345;
            ((PlayerModelResource)Model).ZPosition = expected;
            Assert.Equal(expected, ((PlayerModelResource)Model).ZPosition);
        }

        private class PlayerModelResourceEqualTestGenerator : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new PlayerModelResource {Href="a"},
                    new PlayerModelResource {Href="a"},
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        };

        /// <summary>
        /// Verify equality operations
        /// </summary>
        /// <param name="playerModelResource"></param>
        [Theory]
        [ClassData(typeof(PlayerModelResourceEqualTestGenerator))]
        public void BeEqual(PlayerModelResource dut, PlayerModelResource expected)
        {
            Assert.Equal(expected, dut);
        }

        private class PlayerModelResourceNotEqualTestGenerator : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new PlayerModelResource {Href="a"},
                    null,
                };
                yield return new object[]
                {
                    new PlayerModelResource {},
                    new PlayerModelResource {Href="a"},
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        };
        /// <summary>
        /// Verify not equal operations
        /// </summary>
        /// <param name="playerModelResource"></param>
        [Theory]
        [ClassData(typeof(PlayerModelResourceNotEqualTestGenerator))]
        public void NotBeEqual(PlayerModelResource dut, PlayerModelResource expected)
        {
            Assert.NotEqual(expected, dut);
        }
    }
}
