using FlightSimulator.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightSimulator.Dal
{
    public class AirportDataContext : DbContext
    {
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Pilot> Pilots { get; set; }
        public virtual DbSet<Leg> Legs { get; set; }
        public virtual DbSet<ProcessLog> ProcessLogger { get; set; }

        public AirportDataContext(DbContextOptions<AirportDataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
                .HasOne(a => a.Leg)
                .WithOne(b => b.Flight)
                 .HasForeignKey<Leg>(b => b.Id);

        }

    }
}
