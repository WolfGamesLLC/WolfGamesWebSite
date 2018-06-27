using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WolfGamesWebSite.Common.Identity
{
    /// <summary>
    /// Concrete implementation of the WG side of the Identity bridge 
    /// </summary>
    public class RequestUserProvider<T> : IRequestUserProvider<T>
    {
        /// <summary>
        /// Get the user data for the specified claim principle
        /// </summary>
        /// <param name="principle">the claims principle</param>
        /// <returns>A user data object</returns>
        public Task<T> GetUserAsync(ClaimsPrincipal principle)
        {
            throw new NotImplementedException();
        }
    }
}
