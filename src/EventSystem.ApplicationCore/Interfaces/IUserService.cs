using EventSystem.ApplicationCore.Dtos;
using System.Threading.Tasks;

namespace EventSystem.ApplicationCore.Interfaces
{
    interface IUserService
    {
        Task<UserViewModel> FindByUserName(string username);
        Task RegisterUser(RegistrationBindingModel registrationModel);
    }
}
