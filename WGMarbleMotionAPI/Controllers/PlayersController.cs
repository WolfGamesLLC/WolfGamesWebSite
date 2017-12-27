using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WGMarbleMotionAPI.Controllers
{
    /// <summary>
    /// The wolf games <see cref="WGMarbleMotionAPI.Controllers.PlayersController"/> 
    /// allows access to the player data for the Marble Motion game.
    /// </summary>
    [Route("/[controller]")]
    [ApiVersion("1.0")]
    public class PlayersController : Controller
    {
        /// <summary>
        /// Get a list of all the players of the Marble Motion game.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPlayers))]
        public IActionResult GetPlayers(long? id)
        {
            var response = new
            {
                href = Url.Link(nameof(GetPlayers), null)
            };

            return Ok(response);
        }
    }
}
