using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ProcessLog : BaseEntity
    {
        public virtual Flight? Flight { get; set; }
        [Required]
        public DateTime? EnterTime { get; set; }
        [Required]
        public DateTime? ExitTime { get; set; }
        [Required]
        public string? Message { get; set; }
    }
}
