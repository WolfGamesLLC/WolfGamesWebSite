using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WolfGamesWebSite.DAL.Models
{
    /// <summary>
    /// Represents an error in an API
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// The text of the error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The text of the error's detail
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// The text of the error's stack trace
        /// </summary>
        [DefaultValue("")]
        public string StackTrace { get; set; }
    }
}
