using EventSystem.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSystem.ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Save(T item);
        Task<T> Update(T item);
        Task DeleteById(int id);
    }
}
