using EventSystem.ApplicationCore.Entities;
using EventSystem.ApplicationCore.Exceptions;
using EventSystem.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSystem.Infrastructure.Data
{
    public class EfCoreEventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _ctx;

        public EfCoreEventRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Event> AddUser(string id, int eventId)
        {
            var eventParticipant = new EventParticipant
            {
                UserId = id,
                EventId = eventId
            };

            _ctx.EventParticipants.Add(eventParticipant);
            await _ctx.SaveChangesAsync();

            var evt = await _ctx.Events
                .Include(evt => evt.Organizer)
                .Include(evt => evt.Participants).ThenInclude(ep => ep.User)
                .FirstOrDefaultAsync(x => x.Id == eventId);

            return evt;
        }

        public async Task DeleteById(int id)
        {
            var entity = await _ctx.Events.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new ItemNotFoundException<Event>($"Item with id {id} not found");
            }

            _ctx.Remove(entity);
        }

        public async Task<List<Event>> GetAll()
        {
            var result = await _ctx.Events
                .Include(e => e.Organizer)
                .Include(e => e.Participants).ThenInclude(ep => ep.User)
                .ToListAsync();

            return result;
        }

        public async Task<Event> GetById(int id)
        {
            var entity = await _ctx.Events
                .Include(e => e.Organizer)
                .Include(e => e.Participants).ThenInclude(ep => ep.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(entity == null)
            {
                throw new ItemNotFoundException<Event>($"Item with {id} not found");
            }

            return entity;
        }

        public async Task<Event> Save(Event item)
        {
            _ctx.Events.Add(item);
            await _ctx.SaveChangesAsync();

            return item;
        }

        public async Task<Event> Update(Event item)
        {
            _ctx.Entry(item).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();

            return item;
        }
    }
}
