using EventSystem.ApplicationCore.Dtos;
using EventSystem.ApplicationCore.Entities;
using EventSystem.ApplicationCore.Interfaces;
using EventSystem.ApplicationCore.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSystem.ApplicationCore.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;
        private readonly IUserService _userService;

        public EventService(IEventRepository repo, IUserService userService)
        {
            _repo = repo;
            _userService = userService;
        }

        public async Task<EventViewModel> AddUser(string username, int eventId)
        {
            var user = await _userService.FindDomainUserByUserName(username);
            var evtEntity = await _repo.AddUser(user.IdentityUserId, eventId);

            return await EventMapper.Map(evtEntity, _userService);
        }

        public async Task DeleteById(int id)
        {
            await _repo.DeleteById(id);
        }

        public async Task<List<EventViewModel>> GetAll()
        {
            var entities = await _repo.GetAll();

            var result = entities
                .Select(async x => await EventMapper.Map(x, _userService))
                .Select(x => x.Result)
                .ToList();

            return result;
        }

        public async Task<EventViewModel> GetById(int id)
        {
            var entity = await _repo.GetById(id);

            var result = await EventMapper.Map(entity, _userService);

            return result;
        }

        public async Task<List<UserShortViewModel>> GetConnectedUsers(int eventId)
        {
            var evtEntity = await _repo.GetById(eventId);

            var result = evtEntity.Participants
                .Select(async ep => await _userService.FindShortById(ep.UserId))
                .Select(x => x.Result)
                .ToList();

            return result;
        }

        public async Task<EventViewModel> Save(EventViewModel eventModel)
        {
            var entity = new Event
            {
                Name = eventModel.Name,
                Description = eventModel.Description,
                EventDate = eventModel.EventDate,
                Organizer = await _userService.FindDomainUserByUserName(eventModel.Organizer.UserName)
            };

            await _repo.Save(entity);

            return await EventMapper.Map(entity, _userService);
        }

        public async Task<EventViewModel> Update(EventViewModel eventModel)
        {
            var entity = await EventMapper.MapReverse(eventModel, _userService);
            var newEntity = await _repo.Update(entity);
            var result = await EventMapper.Map(newEntity, _userService);

            return result;
        }
    }
}
