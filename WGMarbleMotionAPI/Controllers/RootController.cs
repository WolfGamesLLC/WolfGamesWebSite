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
    public class RootController : Controller
    {
        /// <summary>
        /// The root action of the API. Follows the HATEOAS 
        /// standard.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null)
            };
 
            return Ok(response);
        }
    }
}
