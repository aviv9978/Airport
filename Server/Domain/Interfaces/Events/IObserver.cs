using Core.Entities.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Events
{
    public interface IObserver
    {
        // Receive update from subject
        void Update();
    }
}
