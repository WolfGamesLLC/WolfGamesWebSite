using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WolfGamesWebSite.Common.Identity;
using WolfGamesWebSite.DAL.Models;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WolfGamesWebSite.Common.XUnitTest.Identity
{
    /// <summary>
    /// Verify the request user provider bridge behaviors
    /// </summary>
    public class RequestUserProviderShould
    {
        /// <summary>
        /// Verify creation
        /// </summary>
        [Fact(Skip = "This test really doesn't make sense until I start to verify arguments")]
        public void CreateRequestProvider()
        {
            RequestUserProvider provider = new RequestUserProvider(null);
            Assert.IsAssignableFrom<IRequestUserProvider>(provider);
        }

        /// <summary>
        /// Verify the user can be retrieved from a principle
        /// </summary>
        [Fact]
        public void GetUserAsyncWithPrinciple()
        {
            var _store = new Mock<IUserStore<ApplicationUser>>();
            var manager = new UserManager<ApplicationUser>(_store.Object, null, null, null, null, null, null, null, null);

            RequestUserProvider provider = new RequestUserProvider(manager);
            ClaimsPrincipal principle = new ClaimsPrincipal();
            var user = provider.GetUserAsync(principle);

            Assert.Equal("1", user.Id.ToString());
        }
    }
}
