using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WolfGamesWebSite.Services
{
    /// <summary>
    /// The interface defining the email service
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// The asyncronous email sending operation
        /// </summary>
        /// <param name="email">The address to send an email to</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="message">The message to be sent</param>
        /// <returns>The asyncronous task sending the email</returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}
