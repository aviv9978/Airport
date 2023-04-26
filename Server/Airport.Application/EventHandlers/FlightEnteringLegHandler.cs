using Core.EventHandlers.Interfaces;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.EventHandlers
{
    internal class FlightEnteringLegHandler : IBaseAirportHandler
    {
        private readonly IUnitOfWork _UOW;

        public FlightEnteringLegHandler(IUnitOfWork unitOfWork) 
        {
            _UOW = unitOfWork;
        }
        public async Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
