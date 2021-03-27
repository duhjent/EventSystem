using EventSystem.ApplicationCore.Dtos;
using EventSystem.ApplicationCore.Interfaces;
using EventSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EventSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenClaimsService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IUserService userService, ITokenClaimsService tokenService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<UserViewModel>> GetAll(string username) => await _userService.FindByUserName(username);

        [HttpPost]
        public async Task<ActionResult<string>> RegisterUser([FromBody] RegistrationBindingModel registrationModel)
        {
            try
            {
                await _userService.RegisterUser(registrationModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            var result = await _tokenService.GenerateTokenAsync(registrationModel.UserName);

            return result;
        }

        [HttpGet("login")]
        public async Task<ActionResult<string>> SignIn(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user == null)
            {
                return BadRequest();
            }

            var signInResult = await _userManager.CheckPasswordAsync(user, password);
            if(!signInResult)
            {
                return BadRequest();
            }
            var token = await _tokenService.GenerateTokenAsync(username);

            return token;
        }

        [HttpGet("short/{username}")]
        public async Task<ActionResult<UserShortViewModel>> GetShort(string username)
        {
            var result = await _userService.FindShortByUserName(username);

            if (result == null)
            {
                return BadRequest();
            }

            return result;
        }
    }
}
