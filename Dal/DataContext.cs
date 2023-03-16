using FlightSimulator.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightSimulator.Dal
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Flight> Fligts { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
