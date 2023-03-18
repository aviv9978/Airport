using FlightSimulator.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightSimulator.Dal
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Flight> Flights { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
