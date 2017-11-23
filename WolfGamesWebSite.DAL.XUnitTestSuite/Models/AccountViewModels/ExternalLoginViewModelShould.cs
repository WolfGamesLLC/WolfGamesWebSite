using System;
using System.Collections.Generic;
using System.Text;
using WGSystem.Collections.Generic;
using WGSystem.ComponentModel.DataAnnotations;
using WolfGamesWebSite.Models.AccountViewModels;
using Xunit;

namespace WolfGamesWebSite.XUnitTestSuite.Models.AccountViewModels
{
    /// <summary>
    /// Test suite for the ExternalLoginViewModel
    /// </summary>
    public class ExternalLoginViewModelShould
    {
        //        private readonly ITestOutputHelper output;

        /// <summary>
        /// Holds the test object
        /// </summary>
        public ExternalLoginViewModel LoginViewModel { get; set; }

        /// <summary>
        /// Holds the generi collections factory
        /// </summary>
        public WGGenericCollectionsFactory CollectionsFactory { get; set; }

        /// <summary>
        /// Initialize the test environment
        /// </summary>
        public ExternalLoginViewModelShould()
        {
            LoginViewModel = new ExternalLoginViewModel();
            CollectionsFactory = new WGGenericCollectionsFactory();
        }

        /// <summary>
        /// Create a default object
        /// </summary>
        [Fact]
        public void CreateAnExternalLoginViewModel()
        {
            Assert.NotNull(new ExternalLoginViewModel());
        }

        /// <summary>
        /// Set the Email property
        /// </summary>
        [Fact]
        public void SetEmail()
        {
            string expectedEmail = "some@email.com";
            LoginViewModel = new ExternalLoginViewModel() { Email = expectedEmail };
            Assert.Equal(expectedEmail, LoginViewModel.Email);
        }

        /// <summary>
        /// Test that a missing email fails validation
        /// This is an integration test
        /// </summary>
        [Fact]
        public void FailValidationIfEmailIsEmpty()
        {
            // arrange
            IWGValidation basicValidation = new WGBasicValidation(new WGAspNetCore2Validation(CollectionsFactory));
            IList<IWGValidationResult> validationResults = (IList<IWGValidationResult>)basicValidation.Result;
            LoginViewModel = new ExternalLoginViewModel();

            // apply
            Assert.False(basicValidation.TryValidateObject(LoginViewModel));

            // assert
            Assert.Equal(1, validationResults.Count);
            Assert.Equal("The Email field is required.", validationResults[0].ErrorMessage);
            Assert.Null(validationResults[0].MemberNames);
            Assert.Equal(false, validationResults[0].Success);
        }
    }
}
