using Core.DTOs.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.ILogicServices
{
    public interface ILegStatusService
    {
        List<LegStatusOutDTO> GetLegsStatus();
    }
}
