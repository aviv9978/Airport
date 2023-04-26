﻿using Core.EventHandlers.Interfaces;
using Core.Interfaces.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.EventHandlers
{
    internal class FlightIncomingHandler : IBaseAirportHandler
    {
        private readonly IISUbject _subject;

        public FlightIncomingHandler(IISUbject isubject)
        {
            _subject = isubject;
        }
        public async Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
