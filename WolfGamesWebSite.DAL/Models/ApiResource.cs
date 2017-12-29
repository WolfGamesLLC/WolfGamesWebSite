using System;
using System.Collections.Generic;
using System.Text;

namespace WolfGamesWebSite.DAL.Models
{
    /// <summary>
    /// Base class for all API resource models
    /// </summary>
    public abstract class ApiResource
    {
        /// <summary>
        /// Contains the link used to access the resource
        /// </summary>
        public string Href { get; set; }
    }
}
