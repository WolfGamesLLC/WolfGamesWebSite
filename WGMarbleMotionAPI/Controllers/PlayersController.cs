using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    [Route("/api/[controller]")]
    [ApiVersion("1.0")]
//    [Authorize]
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
        [HttpGet(Name = nameof(GetPlayersAsync))]
        public IActionResult GetPlayersAsync()
        {
            return Ok(new Collection<PlayerModelResource>
            {
                new PlayerModelResource()
                {
                    Href = Url.Link(nameof(GetPlayersAsync), null)
                }
            });
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
                Href = Url.Link(nameof(GetPlayerAsync), new { playerId = entity.Id }),
                Score = entity.Score,
                XPosition = entity.XPosition,
                ZPosition = entity.ZPosition
            };

            return Ok(player);
        }

        /// <summary>
        /// Create a player's data in the database
        /// </summary>
        /// <param name="playerModelResource">The player model resource contaaining the player data</param>
        /// <param name="ct">The cancellation token used by the db server</param>
        /// <returns>The player's data</returns>
        [HttpPut("create", Name = nameof(CreatePlayerAsync))]
        public async Task<IActionResult> CreatePlayerAsync(PlayerModelResource playerModelResource, CancellationToken ct)
        {
            var player = _context.Add(new PlayerModel()
            {
                Score = playerModelResource.Score,
                XPosition = playerModelResource.XPosition,
                ZPosition = playerModelResource.ZPosition
            });

            var created = await _context.SaveChangesAsync(ct);
            if (created < 0) throw new InvalidOperationException("Could not create the player.");

            return Created(Url.Link(nameof(UpdatePlayerAsync), new { player.Entity.Id }), player.Entity.Id);
        }

        /// <summary>
        /// Update a player's data in the database
        /// </summary>
        /// <param name="newId">the id to be updated</param>
        /// <param name="playerModelResource">The player model resource contaaining the player data</param>
        /// <param name="ct">The cancellation token used by the db server</param>
        /// <returns>The player's data</returns>
        [HttpPut("update", Name = nameof(UpdatePlayerAsync))]
        public async Task<IActionResult> UpdatePlayerAsync(Guid newId, PlayerModelResource playerModelResource, CancellationToken ct)
        {
            var player = await _context.PlayerModel.SingleOrDefaultAsync(r => r.Id == newId);
            if (player == null)
                return new NotFoundResult();

            player.Score = playerModelResource.Score;
            player.XPosition = playerModelResource.XPosition;
            player.ZPosition = playerModelResource.ZPosition;

            var created = await _context.SaveChangesAsync(ct);
            if (created < 0) throw new InvalidOperationException("Could not create the player.");

            return Created(Url.Link(nameof(UpdatePlayerAsync), new { newId }), null);
        }
    }
}
