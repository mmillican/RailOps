using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RailOps.Api.Data;
using RailOps.Api.Entities.Roster;

namespace RailOps.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private readonly RailOpsContext _context;

        public CarsController(RailOpsContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public IEnumerable<Car> GetCars()
        {
            return _context.Cars
                .Include(x => x.Road)
                .Include(x => x.Type)
                .Include(x => x.Load);
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar([FromRoute] int id, [FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        [HttpPost]
        public async Task<IActionResult> PostCar([FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return Ok(car);
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync([FromForm] IFormFile form, [FromForm] bool deleteExisting = false) 
        {
            try 
            {
                if (deleteExisting) 
                {
                    var carsToDelete = await _context.Cars.ToListAsync();
                    foreach(var del in carsToDelete)
                    {
                        _context.Cars.Remove(del);
                    }

                    await _context.SaveChangesAsync();
                }

                var importCount = 0;
                using (var reader = new StreamReader(form.OpenReadStream()))
                {
                    var csvReaderConfig = new Configuration
                    {
                        HasHeaderRecord = true
                    };
                    
                    using (var csv = new CsvReader(reader, csvReaderConfig))
                    {
                        var items = csv.GetRecords<ImportCarModel>().ToList();

                        var roadNames = items.Select(x => x.Road).Distinct().ToList();
                        var roads = await GetRoads(roadNames);
                        var typeNames = items.Select(x => x.Type).Distinct().ToList();
                        var types = await GetCarTypes(typeNames);
                        var loadNames = items.Select(x => x.Load).Distinct().ToList();
                        var loads = await GetLoadTypes(loadNames);

                        foreach(var importItem in items) 
                        {
                            var car = new Car();
                            car.Type = types.FirstOrDefault(x => x.Name == importItem.Type);
                            car.Road = roads.FirstOrDefault(x => x.Name == importItem.Road);
                            car.RoadNumber = importItem.Number;
                            car.Length = importItem.Length;
                            car.Color = importItem.Color;
                            car.WeightOunces = importItem.Weight;
                            car.Load = loads.FirstOrDefault(x => x.Name == importItem.Load);

                            _context.Cars.Add(car);
                            importCount++;
                        }

                        await _context.SaveChangesAsync();

                    }
                }

                var totalCarCount = await _context.Cars.CountAsync();

                var result = new
                {
                    importCount,
                    totalCount = totalCarCount
                };

                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private async Task<List<CarType>> GetCarTypes(List<string> typeNames) 
        {
            var result = new List<CarType>();

            if (!typeNames.Any()) return result;

            var existingTypes = await _context.CarTypes
                .Where(x => typeNames.Contains(x.Name))
                .ToListAsync();
            var existingTypeNames = existingTypes.Select(x => x.Name);

            var typeNamesToCreate = typeNames.Where(x => !existingTypeNames.Contains(x));
            foreach(var tn in typeNamesToCreate) 
            {
                var carType = new CarType { Name = tn };
                _context.CarTypes.Add(carType);
            }

            await _context.SaveChangesAsync();

            result = await _context.CarTypes.ToListAsync();
            return result;
        }

        private async Task<List<Road>> GetRoads(List<string> roadNames) 
        {
            var result = new List<Road>();

            if (!roadNames.Any()) return result;

            var existingRoads = await _context.Roads
                .Where(x => roadNames.Contains(x.Name))
                .ToListAsync();
            var existingRoadNames = existingRoads.Select(x => x.Name);

            var roadNamesToCreate = roadNames.Where(x => !existingRoadNames.Contains(x));
            foreach(var tn in roadNamesToCreate) 
            {
                var road = new Road { Name = tn };
                _context.Roads.Add(road);
            }

            await _context.SaveChangesAsync();

            result = await _context.Roads.ToListAsync();
            return result;
        }

        private async Task<List<LoadType>> GetLoadTypes(List<string> loadNames)
        {
            var result = new List<LoadType>();

            if (!loadNames.Any()) return result;

            var existingLoads = await _context.LoadTypes
                .Where(x => loadNames.Contains(x.Name))
                .ToListAsync();
            var existingLoadNames = existingLoads.Select(x => x.Name);

            var loadNamesToCreate = loadNames.Where(x => !existingLoadNames.Contains(x));
            foreach(var tn in loadNamesToCreate)
            {
                var load = new LoadType { Name = tn };
                _context.LoadTypes.Add(load);
            }
            
            await _context.SaveChangesAsync();

            result = await _context.LoadTypes.ToListAsync();
            return result;
        }
    }

    public class ImportCarModel
    {
        public string Number { get; set; }
        public string Road { get; set; }

        public string Type { get; set; }

        public int Length { get; set; }

        public string Color { get; set; }

        public decimal Weight { get; set; }

        public string Load { get; set; }

    }
}