using Core.EventHandlers.Enums;
using Core.Interfaces;

namespace Core.EventHandlers.Interfaces.DAL
{
    public interface IDalBasicEventHandler<T> : IDalBasicBasicEventHnadler<T> where T : IBaseEntity
    {
        public DalTopic DalTopic { get; set; }
        Task NotifyAsync(T entity) ;
    }
}
