using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using WolfGamesWebSite.Common.Identity;
using WolfGamesWebSite.DAL.Models;
using Xunit;

namespace WolfGamesWebSite.Common.XUnitTest.Identity
{
    internal class MockUserManager : UserManager<ApplicationUser>
    {
        public MockUserManager()
        : base(new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null)
        { }

        public override Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return Task.FromResult<ApplicationUser>(new ApplicationUser() { Id = "18" });
        }
    }

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
        public async void GetUserAsyncWithPrinciple()
        {
            var manager = new MockUserManager();
            RequestUserProvider provider = new RequestUserProvider(manager);

            var user = await provider.GetUserAsync(new ClaimsPrincipal());

            Assert.Equal("18", user.Id);
        }
    }
}
