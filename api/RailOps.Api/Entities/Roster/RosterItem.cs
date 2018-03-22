using System.ComponentModel.DataAnnotations.Schema;

namespace RailOps.Api.Entities.Roster
{
    public abstract class RosterItem 
    {
        public int Id { get; set; }

        public int RoadId { get; set; }

        public string RoadNumber { get; set; }

        public int Length { get; set; }

        public string Comments { get; set; }

        [ForeignKey(nameof(RoadId))]
        public virtual Road Road { get; set; }
    }
}