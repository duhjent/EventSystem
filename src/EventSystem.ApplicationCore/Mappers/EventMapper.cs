using EventSystem.ApplicationCore.Dtos;
using EventSystem.ApplicationCore.Entities;
using EventSystem.ApplicationCore.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace EventSystem.ApplicationCore.Mappers
{
    public static class EventMapper
    {
        public static async Task<EventViewModel> Map(Event input, IUserService userService)
        {
            var participants = input.Participants
                .Select(x => userService
                    .FindShortById(x.User.IdentityUserId)
                    .GetAwaiter()
                    .GetResult())
                .ToList();

            var result = new EventViewModel
            {
                Id = input.Id,
                Description = input.Description,
                EventDate = input.EventDate,
                Name = input.Name,
                Organizer = await userService.FindShortById(input.Organizer.IdentityUserId),
                Participants = participants
            };

            return result;
        }

        public static async Task<Event> MapReverse(EventViewModel input, IUserService userService)
        {
            var result = new Event
            {
                Name = input.Name,
                Description = input.Description,
                EventDate = input.EventDate,
                Organizer = await userService.FindDomainUserByUserName(input.Organizer.UserName)
            };

            return result;
        }
    }
}
