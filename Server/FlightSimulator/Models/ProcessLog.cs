using Microsoft.Build.Framework;

namespace FlightSimulator.Models
{
    public class ProcessLog
    {
        public int Id { get; set; }
        public virtual Flight? Flight { get; set; }
        [Required]
        public DateTime? In { get; set; }
        [Required]
        public DateTime? Out { get; set; }
        [Required]
        public string? Message { get; set; }
    }
}
