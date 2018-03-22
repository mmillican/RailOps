using Microsoft.EntityFrameworkCore;
using RailOps.Api.Entities.Roster;

namespace RailOps.Api.Data
{
    public class RailOpsContext : DbContext
    {
        public DbSet<Road> Roads { get; set; }
        
        public DbSet<EngineModel> EngineModels { get; set; }
        public DbSet<EngineType> EngineTypes { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<LoadType> LoadTypes { get; set; }
        public DbSet<Car> Cars { get; set; }


        public RailOpsContext(DbContextOptions<RailOpsContext> options)
            : base(options)
        {
            
        }
    }
}