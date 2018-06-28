using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WolfGamesWebSite.DAL.Models;

namespace WolfGamesWebSite.Common.Identity
{
    /// <summary>
    /// Concrete implementation of the WG side of the Identity bridge 
    /// </summary>
    public class RequestUserProvider : IRequestUserProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestUserProvider(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Get the user data for the specified claim principle
        /// </summary>
        /// <param name="principle">the claims principle</param>
        /// <returns>A user data object</returns>
        public Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principle)
        {
            return _userManager.GetUserAsync(principle);
        }
    }
}
