﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Application.ILogicServices
{
    public interface ITerminalService
    {
        Task StartDepartureAsync(Flight flight);
        Task StartLandAsync(Flight flight);
    }
}