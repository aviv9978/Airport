using Core.Enums;
using Core.Interfaces.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Terminal
{
    public class Leg : BaseEntity, IObserver
    {
        [Required]
        public LegNumber CurrentLeg { get; set; }
        [Required]
        public LegNumber NextPosibbleLegs { get; set; }
        [Required]
        public LegType LegType { get; set; }
        [Required]
        public int PauseTime { get; set; }
        public bool IsOccupied { get; set; }
        public virtual Flight? Flight { get; set; }
        public AsyncEvent<EventArgs>? ClearedLeg;
        public void Update()
        {
            ClearedLeg?.InvokeAsync(this, EventArgs.Empty);
        }
    }
}
