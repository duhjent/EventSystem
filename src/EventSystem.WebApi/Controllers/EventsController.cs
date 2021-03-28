using EventSystem.ApplicationCore.Dtos;
using EventSystem.ApplicationCore.Entities;
using EventSystem.ApplicationCore.Exceptions;
using EventSystem.ApplicationCore.Interfaces;
using EventSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSystem.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public EventsController(IEventService eventService, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _eventService = eventService;
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventViewModel>>> GetAll() => await _eventService.GetAll();

        [HttpGet("connected")]
        public async Task<ActionResult<List<EventShortViewModel>>> GetConnected()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _eventService.GetConnected(user.UserName);

            return result;
        }

        [HttpGet("organized")]
        public async Task<ActionResult<List<EventShortViewModel>>> GetOrganized()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _eventService.GetOrganized(user.UserName);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventViewModel>> GetById(int id)
        {
            try
            {
                var result = await _eventService.GetById(id);
                return result;
            }
            catch (ItemNotFoundException<Event>)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<EventViewModel>> AddEvent([FromBody] EventViewModel eventModel)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _eventService.Save(eventModel, user.UserName);

            return result;
        }

        [HttpPut]
        public async Task<ActionResult<EventViewModel>> UpdateEvent([FromBody] EventViewModel eventModel)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.UserName != eventModel.Organizer.UserName)
            {
                return Forbid();
            }

            var result = await _eventService.Update(eventModel);

            return result;
        }

        [HttpPost("adduser")]
        public async Task<ActionResult<EventViewModel>> AddUserToEvent(int id, string userName)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var eventModel = await _eventService.GetById(id);
            if (currentUser.UserName != eventModel.Organizer.UserName)
            {
                return Forbid();
            }

            var result = await _eventService.AddUser(userName, id);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var eventModel = await _eventService.GetById(id);
            if (currentUser.UserName != eventModel.Organizer.UserName)
            {
                return Forbid();
            }

            await _eventService.DeleteById(id);

            return Ok();
        }
    }
}
