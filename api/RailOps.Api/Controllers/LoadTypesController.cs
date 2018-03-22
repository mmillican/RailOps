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
    public class LoadTypesController : Controller
    {
        private readonly RailOpsContext _context;

        public LoadTypesController(RailOpsContext context)
        {
            _context = context;
        }

        // GET: api/LoadTypes
        [HttpGet]
        public IEnumerable<LoadType> GetLoadTypes()
        {
            return _context.LoadTypes;
        }

        // GET: api/LoadTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoadType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loadType = await _context.LoadTypes.FindAsync(id);

            if (loadType == null)
            {
                return NotFound();
            }

            return Ok(loadType);
        }

        // PUT: api/LoadTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoadType([FromRoute] int id, [FromBody] LoadType loadType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loadType.Id)
            {
                return BadRequest();
            }

            _context.Entry(loadType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoadTypeExists(id))
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

        // POST: api/LoadTypes
        [HttpPost]
        public async Task<IActionResult> PostLoadType([FromBody] LoadType loadType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.LoadTypes.Add(loadType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoadType", new { id = loadType.Id }, loadType);
        }

        // DELETE: api/LoadTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoadType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loadType = await _context.LoadTypes.FindAsync(id);
            if (loadType == null)
            {
                return NotFound();
            }

            _context.LoadTypes.Remove(loadType);
            await _context.SaveChangesAsync();

            return Ok(loadType);
        }

        private bool LoadTypeExists(int id)
        {
            return _context.LoadTypes.Any(e => e.Id == id);
        }
    }
}