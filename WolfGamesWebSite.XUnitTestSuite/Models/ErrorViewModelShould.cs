using System;
using System.Collections.Generic;
using System.Text;
using WolfGamesWebSite.Models;
using Xunit;

namespace WolfGamesWebSite.XUnitTestSuite.Models
{
    /// <summary>
    /// Test suite for the standard error view model
    /// </summary>
    public class ErrorViewModelShould
    {
        /// <summary>
        /// Standard object under test
        /// </summary>
        public ErrorViewModel errorViewModel { get; set; }

        /// <summary>
        /// Initialize the test suite
        /// </summary>
        public ErrorViewModelShould()
        {
            errorViewModel = new ErrorViewModel();
        }

        /// <summary>
        /// Verify the model can be created
        /// </summary>
        [Fact]
        public void CreateAnErrorViewModel()
        {
            Assert.NotNull(new ErrorViewModel());
        }

        /// <summary>
        /// The request id should be set and retrieved
        /// </summary>
        [Fact]
        public void SetAndGetRequestId()
        {
            string expected = "RequestId";
            errorViewModel.RequestId = expected;
            Assert.Equal(expected, errorViewModel.RequestId);
        }

        /// <summary>
        /// Show request id should return false when it is NULL
        /// </summary>
        [Fact]
        public void NotShowRequestIdWhenRequestIdIsNull()
        {
            Assert.False(errorViewModel.ShowRequestId);
        }

        /// <summary>
        /// Show request id should return false when it is empty
        /// </summary>
        [Fact]
        public void NotShowRequestIdWhenRequestIdIsEmpty()
        {
            errorViewModel.RequestId = "";
            Assert.False(errorViewModel.ShowRequestId);
        }

        /// <summary>
        /// Show request id should return true when it is a single space
        /// </summary>
        [Fact]
        public void ShowRequestIdWhenRequestIdIsSpace()
        {
            errorViewModel.RequestId = " ";
            Assert.True(errorViewModel.ShowRequestId);
        }

        /// <summary>
        /// Show request id should return true when it is a single character
        /// </summary>
        [Fact]
        public void ShowRequestIdWhenRequestIdIsA()
        {
            errorViewModel.RequestId = "a";
            Assert.True(errorViewModel.ShowRequestId);
        }
    }
}
