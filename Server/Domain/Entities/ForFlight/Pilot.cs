using Core.Enums;

namespace Core.Entities.ForFlight
{
    public class Pilot : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public Rank Rank { get; set; }
    }
}
