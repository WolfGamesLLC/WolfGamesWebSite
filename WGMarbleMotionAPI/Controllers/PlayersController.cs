using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WolfGamesWebSite.DAL.Data;
using WolfGamesWebSite.DAL.Models;
using WolfGamesWebSite.DAL.Models.SimpleGameModels.MarbleMotion;

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
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Allow dependencies to be injected by the ASP.NET pipeline
        /// </summary>
        /// <param name="context">The DB context to use when accessing data</param>
        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all the players of the Marble Motion game.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPlayers))]
        public IActionResult GetPlayers(long? id)
        {
            if (id == 1000)
            {
//                if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));
                throw new ArgumentException();
            }

            var response = new
            {
                href = Url.Link(nameof(GetPlayers), null)
            };

            return Ok(response);
        }

        /// <summary>
        /// Retrieve a specific player's data from the context
        /// </summary>
        /// <param name="guid">the player's guid</param>
        /// <param name="ct">the concellation token</param>
        /// <returns>Player's data or NotFound</returns>
        // /players/{playerId}
        [HttpGet("{playerId}", Name = nameof(GetPlayerAsync))]
        public async Task<IActionResult> GetPlayerAsync(Guid playerId, CancellationToken ct)
        {
            var entity = await _context.PlayerModel.SingleOrDefaultAsync(r => r.Id == playerId, ct);
            if (entity == null) return NotFound();

            var player = new PlayerModelResource()
            {
                Href = "hello",
//                Href = Url.Link(nameof(GetPlayerAsync), new { playerId = entity.Id }),
                Score = entity.Score
            };

            return Ok(player);
        }
    }
}
