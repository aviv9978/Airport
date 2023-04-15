using Core.DTOs.Outgoing;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.Interfaces
{
    public interface IProcLogService
    {
        Task<ICollection<ProcessLogOutDTO>> GetProcessLogsAsync();
    }
}
