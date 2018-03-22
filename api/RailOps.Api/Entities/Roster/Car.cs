using System.ComponentModel.DataAnnotations.Schema;

namespace RailOps.Api.Entities.Roster
{
    public class Car : RosterItem
    {
        public int CarTypeId { get; set; }

        // TODO: Some of these could probably be on CarType
        public bool IsPassenger { get; set; }
        public bool IsCaboose { get; set; }
        public bool IsFRED { get; set; }
        public bool IsUtility { get; set; }
        public bool IsHazardous { get; set; }

        public decimal WeightOunces { get; set;}
        public int WeightTons { get; set; }

        // Lookup?
        public string Color { get; set; }

        public int LoadTypeId { get; set; }

        [ForeignKey(nameof(CarTypeId))]
        public virtual CarType Type { get; set;}

        [ForeignKey(nameof(LoadTypeId))]
        public virtual LoadType Load { get; set; }

        // TODO: Location
    }

    public class CarType : BaseLookupEntity
    {
    }

    public class LoadType : BaseLookupEntity 
    {
    }
}