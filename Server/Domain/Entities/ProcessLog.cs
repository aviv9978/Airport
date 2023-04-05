using Core.Entities.Terminal;
using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ProcessLog : BaseEntity
    {
        public virtual Flight? Flight { get; set; }
        public int LegNum { get; set; }
        public DateTime? EnterTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public string? Message { get; set; }
    }
}
