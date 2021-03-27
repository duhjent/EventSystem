using EventSystem.ApplicationCore.Dtos;
using EventSystem.ApplicationCore.Entities;
using System.Threading.Tasks;

namespace EventSystem.ApplicationCore.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> FindByUserName(string username);
        Task<UserShortViewModel> FindShortByUserName(string username);
        Task<UserShortViewModel> FindShortById(string id);
        Task<User> FindDomainUserByUserName(string username);
        Task RegisterUser(RegistrationBindingModel registrationModel);
    }
}
