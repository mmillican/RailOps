using System.ComponentModel.DataAnnotations.Schema;

namespace RailOps.Api.Entities.Roster
{
    public class Engine : RosterItem
    {
        public int ModelId { get; set; }
        public int TypeId { get; set; }

        [ForeignKey(nameof(ModelId))]
        public virtual EngineModel Model { get; set; }

        [ForeignKey(nameof(TypeId))]
        public virtual EngineType Type { get; set; }

        public int WeightTons { get; set; }

        // TODO: Location
    }

    public class EngineModel : BaseLookupEntity
    {
    }

    public class EngineType : BaseLookupEntity
    {
    }
    
}