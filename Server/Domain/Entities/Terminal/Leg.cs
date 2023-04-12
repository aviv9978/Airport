using Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Terminal
{
    public class Leg : BaseEntity
    {
        [Required]
        public LegNumber CurrentLeg { get; set; }
        [Required]
        public LegNumber NextPosibbleLegs { get; set; }
        [Required]
        public LegType LegType { get; set; }
        [Required]
        public int PauseTime { get; set; }
        [NotMapped]
        public bool IsOccupied { get; set; }
        [NotMapped]
        public virtual Flight? Flight { get; set; }
    }
}
