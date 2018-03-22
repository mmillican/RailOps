using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RailOps.Api.Data;
using RailOps.Api.Entities.Roster;

namespace RailOps.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EngineModelsController : Controller
    {
        private readonly RailOpsContext _context;

        public EngineModelsController(RailOpsContext context)
        {
            _context = context;
        }

        // GET: api/EngineModels
        [HttpGet]
        public IEnumerable<EngineModel> GetEngineModels()
        {
            return _context.EngineModels;
        }

        // GET: api/EngineModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEngineModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var engineModel = await _context.EngineModels.FindAsync(id);

            if (engineModel == null)
            {
                return NotFound();
            }

            return Ok(engineModel);
        }

        // PUT: api/EngineModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEngineModel([FromRoute] int id, [FromBody] EngineModel engineModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != engineModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(engineModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EngineModelExists(id))
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

        // POST: api/EngineModels
        [HttpPost]
        public async Task<IActionResult> PostEngineModel([FromBody] EngineModel engineModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EngineModels.Add(engineModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEngineModel", new { id = engineModel.Id }, engineModel);
        }

        // DELETE: api/EngineModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEngineModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var engineModel = await _context.EngineModels.FindAsync(id);
            if (engineModel == null)
            {
                return NotFound();
            }

            _context.EngineModels.Remove(engineModel);
            await _context.SaveChangesAsync();

            return Ok(engineModel);
        }

        private bool EngineModelExists(int id)
        {
            return _context.EngineModels.Any(e => e.Id == id);
        }
    }
}