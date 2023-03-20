using FlightSimulator.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightSimulator.Dal
{
    public class AirportDataContext : DbContext
    {
        public virtual DbSet<Flight>? Flights { get; set; }
        public virtual DbSet<Pilot>? Pilots { get; set; }
        public virtual DbSet<Leg>? Legs { get; set; }

        public AirportDataContext(DbContextOptions<AirportDataContext> options) : base(options) { }
    }
}
