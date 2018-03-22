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
    public class EngineTypesController : Controller
    {
        private readonly RailOpsContext _context;

        public EngineTypesController(RailOpsContext context)
        {
            _context = context;
        }

        // GET: api/EngineTypes
        [HttpGet]
        public IEnumerable<EngineType> GetEngineTypes()
        {
            return _context.EngineTypes;
        }

        // GET: api/EngineTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEngineType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var engineType = await _context.EngineTypes.FindAsync(id);

            if (engineType == null)
            {
                return NotFound();
            }

            return Ok(engineType);
        }

        // PUT: api/EngineTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEngineType([FromRoute] int id, [FromBody] EngineType engineType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != engineType.Id)
            {
                return BadRequest();
            }

            _context.Entry(engineType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EngineTypeExists(id))
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

        // POST: api/EngineTypes
        [HttpPost]
        public async Task<IActionResult> PostEngineType([FromBody] EngineType engineType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EngineTypes.Add(engineType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEngineType", new { id = engineType.Id }, engineType);
        }

        // DELETE: api/EngineTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEngineType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var engineType = await _context.EngineTypes.FindAsync(id);
            if (engineType == null)
            {
                return NotFound();
            }

            _context.EngineTypes.Remove(engineType);
            await _context.SaveChangesAsync();

            return Ok(engineType);
        }

        private bool EngineTypeExists(int id)
        {
            return _context.EngineTypes.Any(e => e.Id == id);
        }
    }
}