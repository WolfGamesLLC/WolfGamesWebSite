using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WolfGamesWebSite.DAL.Models;

namespace WolfGamesWebSite.Controllers
{
    /// <summary>
    /// The group of constants used to contain home controller related
    /// messages. Hopefully this will prevent the primitive obsession 
    /// smell, at least in this case
    /// </summary>
    public class HomeControllerMessages
    {
        private string message;

        /// <summary>
        /// The Message accessor
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
        }

        /// <summary>
        /// The default controller
        /// </summary>
        /// <param name="message">The text the message will be set to</param>
        public HomeControllerMessages(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// The about message accessor
        /// </summary>
        /// <returns>The about message</returns>
        public static string About()
        {
            return new HomeControllerMessages("About Wolf Games.").Message;
        }

        /// <summary>
        /// The contact message accessor
        /// </summary>
        /// <returns>the contact message</returns>
        public static string Contact()
        {
            return new HomeControllerMessages("We love to hear from you.").Message;
        }

        /// <summary>
        /// The error message accessor
        /// </summary>
        /// <returns>the error message</returns>
        public static string Error()
        {
            return new HomeControllerMessages("~/Views/Shared/Error.cshtml").Message;
        }

        /// <summary>
        /// The thank you message accessor
        /// </summary>
        /// <returns>the thank you message</returns>
        public static string ThankYou()
        {
            return new HomeControllerMessages("Thank you from all of us at Wolf Games.").Message;
        }

        /// <summary>
        /// The dev corner message accessor
        /// </summary>
        /// <returns>the dev corner message</returns>
        public static string DevCorner()
        {
            return new HomeControllerMessages("Welcome to Wolf Games' dev corner.").Message;
        }
    }

    /// <summary>
    /// The wolf games home page controller
    /// </summary>
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Construct the Home controller object
        /// </summary>
        /// <param name="userManager">The user manager to use</param>
        public HomeController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        /// <summary>
        /// The basic Index action
        /// </summary>
        /// <returns>The home page view </returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The about action
        /// </summary>
        /// <returns>The about page view</returns>
        public IActionResult About()
        {
            ViewData["Message"] = HomeControllerMessages.About();

            return View();
        }

        /// <summary>
        /// The contact action
        /// </summary>
        /// <returns>The contact page view</returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = HomeControllerMessages.Contact();

            return View();
        }

        /// <summary>
        /// The dev corner action
        /// </summary>
        /// <returns>The developer's corner page view</returns>
        public object DevCorner()
        {
            return View();
        }

        /// <summary>
        /// The error action
        /// </summary>
        /// <returns>The error page view</returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Launch the Marble Motion game
        /// </summary>
        /// <returns>The marble motion site</returns>
        public async Task<IActionResult> MarbleMotion()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Response.Cookies.Append("id", user.Id);
            }

            return Redirect("../SimpleGames/WebGl/MarbleMotion/index.html");
        }
    }
}
