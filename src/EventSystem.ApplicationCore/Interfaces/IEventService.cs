using EventSystem.ApplicationCore.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSystem.ApplicationCore.Interfaces
{
    public interface IEventService
    {
        Task<EventViewModel> Save(EventViewModel eventModel, string organizerUserName);
        Task<List<EventViewModel>> GetAll();
        Task<EventViewModel> GetById(int id);
        Task<EventViewModel> Update(EventViewModel eventModel);
        Task DeleteById(int id);
        Task<EventViewModel> AddUser(string username, int eventId);
        Task<List<UserShortViewModel>> GetConnectedUsers(int eventId);
        Task<List<EventShortViewModel>> GetConnected(string userName);
        Task<List<EventShortViewModel>> GetOrganized(string userName);
    }
}
