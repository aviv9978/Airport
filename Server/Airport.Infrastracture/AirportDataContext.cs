using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Airport.Infrastracture
{
    public class AirportDataContext : DbContext
    {
        public AirportDataContext(DbContextOptions<AirportDataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Leg>().HasData(
                new Leg { Id = 1, CurrentLeg = (Core.Enums.LegNumber.One), NextPosibbleLegs = Core.Enums.LegNumber.Two, LegType = Core.Enums.LegType.Land, PauseTime = 3 },
                new Leg { Id = 2, CurrentLeg = (Core.Enums.LegNumber.Two), NextPosibbleLegs = Core.Enums.LegNumber.Thr, LegType = Core.Enums.LegType.Land, PauseTime = 4 },
                new Leg { Id = 3, CurrentLeg = (Core.Enums.LegNumber.Thr), NextPosibbleLegs = Core.Enums.LegNumber.Fou, LegType = Core.Enums.LegType.Land, PauseTime = 5 },
                new Leg { Id = 4, CurrentLeg = (Core.Enums.LegNumber.Fou), NextPosibbleLegs = Core.Enums.LegNumber.Fiv | Core.Enums.LegNumber.Nin, LegType = Core.Enums.LegType.Process, PauseTime = 4 },
                new Leg { Id = 5, CurrentLeg = (Core.Enums.LegNumber.Fiv), NextPosibbleLegs = Core.Enums.LegNumber.Six | Core.Enums.LegNumber.Sev, LegType = Core.Enums.LegType.Process, PauseTime = 3 },
                new Leg { Id = 6, CurrentLeg = (Core.Enums.LegNumber.Six), NextPosibbleLegs = Core.Enums.LegNumber.Eig, LegType = Core.Enums.LegType.Departure, PauseTime = 4 },
                new Leg { Id = 7, CurrentLeg = (Core.Enums.LegNumber.Sev), NextPosibbleLegs = Core.Enums.LegNumber.Eig, LegType = Core.Enums.LegType.Departure, PauseTime = 4 },
                new Leg { Id = 8, CurrentLeg = (Core.Enums.LegNumber.Eig), NextPosibbleLegs = Core.Enums.LegNumber.Fou, LegType = Core.Enums.LegType.Land, PauseTime = 5 },
                new Leg { Id = 9, CurrentLeg = (Core.Enums.LegNumber.Nin), NextPosibbleLegs = Core.Enums.LegNumber.Air, LegType = Core.Enums.LegType.Process, PauseTime = 3 }
                );
        }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Pilot> Pilots { get; set; }
        public virtual DbSet<Leg> Legs { get; set; }
        public virtual DbSet<ProcessLog> ProcessLogger { get; set; }

    }
}
