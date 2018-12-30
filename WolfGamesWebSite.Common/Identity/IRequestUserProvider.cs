using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WolfGamesWebSite.DAL.Models;

namespace WolfGamesWebSite.Common.Identity
{
    /// <summary>
    /// Interface for bridgeing to the Identity UserManager class
    /// </summary>
    public interface IRequestUserProvider
    {
        /// <summary>
        /// Get the user data for the specified claim principle
        /// </summary>
        /// <param name="principle">the claims principle</param>
        /// <returns>A user data object</returns>
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principle);
    }
}
