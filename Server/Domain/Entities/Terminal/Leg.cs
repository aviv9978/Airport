using Core.Enums;
using Core.Events;
using Core.Interfaces.Events;
using Microsoft.VisualStudio.Threading;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

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
        public AsyncEventHandler? ClearedLeg;

        public async Task UpdateAsync(Leg nextLeg)
        {
            LegEventArgs legEventArgs = new LegEventArgs(nextLeg);
             await ClearedLeg.InvokeAsync(this, legEventArgs);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Leg other))
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
