using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace WolfGamesWebSite.Services
{
    /// <summary>
    /// The email sending service
    /// </summary>
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// Default constructor for the email sending service
        /// </summary>
        /// <param name="optionsAccessor">The options access object</param>
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        /// <summary>
        /// The options access object
        /// </summary>
        public AuthMessageSenderOptions Options { get; }

        /// <summary>
        /// Send an email asyncronously - this comes from a sample and really should be refactored
        /// </summary>
        /// <param name="email">The email to send the message to</param>
        /// <param name="subject">The subject line of the email</param>
        /// <param name="message">The message to be sent</param>
        /// <returns>The asyncronous task operation that is performing the service</returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        /// <summary>
        /// The actual send operation - this comes from a sample and really should be refactored
        /// </summary>
        /// <param name="apiKey">The users key for the smtp provider</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="message">The message to be sent</param>
        /// <param name="email">The address to send to</param>
        /// <returns>The asynchronous task operation that is performing the service</returns>
        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("Contact@wolfgamesllc.com", "Wolf Games"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }
    }
}
