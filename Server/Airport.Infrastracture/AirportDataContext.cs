﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Airport.Infrastracture
{
    public class AirportDataContext : DbContext
    {
        public AirportDataContext(DbContextOptions<AirportDataContext> options) : base(options) 
        {
            _legs = Legs.ToList();
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Flight>()
        //        .HasOne(a => a.Leg)
        //        .WithOne(b => b.Flight)
        //         .HasForeignKey<Leg>(b => b.FlightId);

        //}
        protected readonly List<Leg> _legs;
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Pilot> Pilots { get; set; }
        public virtual DbSet<Leg> Legs { get; set; }
        public virtual DbSet<ProcessLog> ProcessLogger { get; set; }

    }
}
