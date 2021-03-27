using EventSystem.ApplicationCore.Dtos;
using EventSystem.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSystem.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventViewModel>>> GetAll() => await _eventService.GetAll();

        [HttpGet("{id}")]
        public async Task<ActionResult<EventViewModel>> GetById(int id) => await _eventService.GetById(id);

        [HttpPost]
        public async Task<ActionResult<EventViewModel>> AddEvent([FromBody] EventViewModel eventModel) => await _eventService.Save(eventModel);

        [HttpPut]
        public async Task<ActionResult<EventViewModel>> UpdateEvent([FromBody] EventViewModel eventModel) => await _eventService.Update(eventModel);

        [HttpPost("{id}")]
        public async Task<ActionResult<EventViewModel>> AddUserToEvent(int id, string userName) => await _eventService.AddUser(userName, id);

        [HttpDelete("{id}")]
        public async Task DeleteEvent(int id) => await _eventService.DeleteById(id);


    }
}
