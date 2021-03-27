using System.Threading.Tasks;

namespace EventSystem.ApplicationCore.Interfaces
{
    public interface ITokenClaimsService
    {
        Task<string> GenerateTokenAsync(string username);
    }
}
