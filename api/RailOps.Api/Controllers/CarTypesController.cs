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
    public class CarTypesController : Controller
    {
        private readonly RailOpsContext _context;

        public CarTypesController(RailOpsContext context)
        {
            _context = context;
        }

        // GET: api/CarTypes
        [HttpGet]
        public IEnumerable<CarType> GetCarTypes()
        {
            return _context.CarTypes;
        }

        // GET: api/CarTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carType = await _context.CarTypes.FindAsync(id);

            if (carType == null)
            {
                return NotFound();
            }

            return Ok(carType);
        }

        // PUT: api/CarTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarType([FromRoute] int id, [FromBody] CarType carType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carType.Id)
            {
                return BadRequest();
            }

            _context.Entry(carType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarTypeExists(id))
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

        // POST: api/CarTypes
        [HttpPost]
        public async Task<IActionResult> PostCarType([FromBody] CarType carType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CarTypes.Add(carType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarType", new { id = carType.Id }, carType);
        }

        // DELETE: api/CarTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carType = await _context.CarTypes.FindAsync(id);
            if (carType == null)
            {
                return NotFound();
            }

            _context.CarTypes.Remove(carType);
            await _context.SaveChangesAsync();

            return Ok(carType);
        }

        private bool CarTypeExists(int id)
        {
            return _context.CarTypes.Any(e => e.Id == id);
        }
    }
}