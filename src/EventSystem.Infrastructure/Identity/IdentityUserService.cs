using EventSystem.ApplicationCore.Dtos;
using EventSystem.ApplicationCore.Entities;
using EventSystem.ApplicationCore.Exceptions;
using EventSystem.ApplicationCore.Interfaces;
using EventSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventSystem.Infrastructure.Identity
{
    public class IdentityUserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _ctx;

        public IdentityUserService(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _ctx = ctx;
        }

        public async Task<UserViewModel> FindByUserName(string username)
        {
            var user = await _userManager.Users
                .Include(au => au.DomainUser).ThenInclude(du => du.OrganizedEvents)
                .Include(au => au.DomainUser).ThenInclude(du => du.ConnectedEvents).ThenInclude(ep => ep.Event)
                .FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
            {
                throw new ItemNotFoundException<User>($"User with username {username} not found");
            }

            return new UserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                OrganizedEvents = user.DomainUser.OrganizedEvents.Select(x => new EventShortViewModel
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    EventDate = x.EventDate
                }).ToList(),
                ConnectedEvents = user.DomainUser.ConnectedEvents.Select(x => new EventShortViewModel
                {
                    Id = x.Event.Id,
                    Name = x.Event.Name,
                    Description = x.Event.Description,
                    EventDate = x.Event.EventDate
                }).ToList()
            };
        }

        public async Task<User> FindDomainUserByUserName(string username)
        {
            var result = await _userManager.Users
                .Include(au => au.DomainUser).ThenInclude(du => du.OrganizedEvents)
                .Include(au => au.DomainUser).ThenInclude(du => du.ConnectedEvents).ThenInclude(ep => ep.Event)
                .FirstOrDefaultAsync(x => x.UserName == username);

            if (result == null)
            {
                throw new ItemNotFoundException<User>($"User with {username} not found");
            }

            return result.DomainUser;
        }

        public async Task<UserShortViewModel> FindShortById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new ItemNotFoundException<User>($"User with {id} not found");
            }

            return new UserShortViewModel
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task<UserShortViewModel> FindShortByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new ItemNotFoundException<User>($"User with {username} not found");
            }

            return new UserShortViewModel
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task RegisterUser(RegistrationBindingModel registrationModel)
        {
            var user = new ApplicationUser
            {
                UserName = registrationModel.UserName,
                Email = registrationModel.Email,
                DomainUser = new User { }
            };
            var result = await _userManager.CreateAsync(user, registrationModel.Password);
            if (!result.Succeeded)
            {
                throw new ArgumentException();
            }

            var roleName = registrationModel.Role == null
                || !Enum.TryParse(typeof(AvailableRoles), registrationModel.Role, out _)
                ? "User" : registrationModel.Role;

            await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}
