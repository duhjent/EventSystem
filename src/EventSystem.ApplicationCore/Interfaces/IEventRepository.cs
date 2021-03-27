using EventSystem.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSystem.ApplicationCore.Interfaces
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAll();
        Task<Event> GetById(int id);
        Task<Event> Save(Event item);
        Task<Event> Update(Event item);
        Task<Event> AddUser(string id, int eventId);
        Task DeleteById(int id);
    }
}
