using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WolfGamesWebSite.Data;
using WolfGamesWebSite.Models.SimpleGameModels;
using WolfGamesWebSite.Data.DAL;

namespace WolfGamesWebSite.Controllers
{
    /// <summary>
    /// The Marble Motion Game API controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/MarbleMotionModels")]
    public class MarbleMotionModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// The default constructor for the marble motion controller
        /// </summary>
        /// <param name="context"></param>
        public MarbleMotionModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The basic get all records action
        /// </summary>
        /// <returns>An enumerable list of all marble motion records</returns>
        // GET: api/MarbleMotionModels
        [HttpGet]
        public IEnumerable<MarbleMotionModel> GetMarbleMotionModel()
        {
            return _context.MarbleMotionModel;
        }

        /// <summary>
        /// The basic get of a single record action
        /// </summary>
        /// <param name="id">The id of the record to be retrieved</param>
        /// <returns>An action result containing the results of the operation</returns>
        // GET: api/MarbleMotionModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMarbleMotionModel([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var marbleMotionModel = await _context.MarbleMotionModel.SingleOrDefaultAsync(m => m.Id == id);

            if (marbleMotionModel == null)
            {
                return NotFound();
            }

            return Ok(marbleMotionModel);
        }

        /// <summary>
        /// The basic update action
        /// </summary>
        /// <param name="id">The id of the record to be updated</param>
        /// <param name="marbleMotionModel">The values of the new record</param>
        /// <returns>An action result containing the results of the operation</returns>
        // PUT: api/MarbleMotionModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarbleMotionModel([FromRoute] long id, [FromBody] MarbleMotionModel marbleMotionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marbleMotionModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(marbleMotionModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarbleMotionModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// The basic create action
        /// </summary>
        /// <param name="marbleMotionModel">The values to be added as a new record</param>
        /// <returns>An action result containing the results of the operation</returns>
        // POST: api/MarbleMotionModels
        [HttpPost]
        public async Task<IActionResult> PostMarbleMotionModel([FromBody] MarbleMotionModel marbleMotionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MarbleMotionModel.Add(marbleMotionModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarbleMotionModel", new { id = marbleMotionModel.Id }, marbleMotionModel);
        }

        /// <summary>
        /// The basic delete action
        /// </summary>
        /// <param name="id">The id of the record to be deleted</param>
        /// <returns>An action result containing the results of the operation</returns>
        // DELETE: api/MarbleMotionModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarbleMotionModel([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var marbleMotionModel = await _context.MarbleMotionModel.SingleOrDefaultAsync(m => m.Id == id);
            if (marbleMotionModel == null)
            {
                return NotFound();
            }

            _context.MarbleMotionModel.Remove(marbleMotionModel);
            await _context.SaveChangesAsync();

            return Ok(marbleMotionModel);
        }

        private bool MarbleMotionModelExists(long id)
        {
            return _context.MarbleMotionModel.Any(e => e.Id == id);
        }
    }
}