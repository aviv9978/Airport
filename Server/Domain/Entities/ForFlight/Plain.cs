namespace Core.Entities.ForFlight
{
    public class Plain : BaseEntity
    {
        public virtual Company? Company { get; set; }
        public string? Model { get; set; }
        public int PassangerCount { get; set; }
    }
}
