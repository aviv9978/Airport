using Core.Entities;
using Core.EventHandlers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EventHandlers.Interfaces.DAL
{
    public interface IDalBasicEventHandler<T> where T : BaseEntity
    {
        public DalTopic DalTopic { get; set; }
        Task NotifyAsync(T entity);
    }
}
