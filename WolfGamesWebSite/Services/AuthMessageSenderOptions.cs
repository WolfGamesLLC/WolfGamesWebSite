using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WolfGamesWebSite.Services
{
    /// <summary>
    /// AuthMessageSenderOptions holds the smpt provide authorization
    /// information
    /// </summary>
    public class AuthMessageSenderOptions
    {
        /// <summary>
        /// The users id for the smpt provider
        /// </summary>
        public string SendGridUser { get; set; }

        /// <summary>
        /// The users auth key for the smpt provider
        /// </summary>
        public string SendGridKey { get; set; }
    }
}
