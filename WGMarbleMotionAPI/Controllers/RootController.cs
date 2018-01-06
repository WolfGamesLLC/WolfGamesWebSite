using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WGMarbleMotionAPI.Controllers
{
    /// <summary>
    /// The wolf games marble motion API root controller
    /// </summary>
    [Route("/")]
    [ApiVersion("1.0")]
    public class RootController : Controller
    {
        /// <summary>
        /// The root action of the API. Follows the HATEOAS 
        /// standard and the ION specification.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                players = new { href = Url.Link(nameof(PlayersController.GetPlayersAsync), null), },
            };
 
            return Ok(response);
        }
    }
}
