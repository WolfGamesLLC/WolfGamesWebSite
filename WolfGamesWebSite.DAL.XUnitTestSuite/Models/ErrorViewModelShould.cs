using System;
using System.Collections.Generic;
using System.Text;
using WolfGamesWebSite.DAL.Models;
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
        public ErrorViewModel ErrorViewModel { get; set; }

        /// <summary>
        /// Initialize the test suite
        /// </summary>
        public ErrorViewModelShould()
        {
            ErrorViewModel = new ErrorViewModel();
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
            ErrorViewModel.RequestId = expected;
            Assert.Equal(expected, ErrorViewModel.RequestId);
        }

        /// <summary>
        /// Show request id should return false when it is NULL
        /// </summary>
        [Fact]
        public void NotShowRequestIdWhenRequestIdIsNull()
        {
            Assert.False(ErrorViewModel.ShowRequestId);
        }

        /// <summary>
        /// Show request id should return false when it is empty
        /// </summary>
        [Fact]
        public void NotShowRequestIdWhenRequestIdIsEmpty()
        {
            ErrorViewModel.RequestId = "";
            Assert.False(ErrorViewModel.ShowRequestId);
        }

        /// <summary>
        /// Show request id should return true when it is a single space
        /// </summary>
        [Fact]
        public void ShowRequestIdWhenRequestIdIsSpace()
        {
            ErrorViewModel.RequestId = " ";
            Assert.True(ErrorViewModel.ShowRequestId);
        }

        /// <summary>
        /// Show request id should return true when it is a single character
        /// </summary>
        [Fact]
        public void ShowRequestIdWhenRequestIdIsA()
        {
            ErrorViewModel.RequestId = "a";
            Assert.True(ErrorViewModel.ShowRequestId);
        }
    }
}
