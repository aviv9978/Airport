using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EventHandlers.Interfaces.DAL
{
    public interface IDalBasicBasicEventHnadler<out T> where T : IBaseEntity
    {
    }
}
