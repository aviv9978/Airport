using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EventHandlers.Interfaces.DAL
{
    public interface IDalHandler<T> where T : BaseEntity
    {
        Task UpdateAsync(T entity);
    }
}
