using System.Threading.Tasks;

namespace EventSystem.ApplicationCore.Interfaces
{
    interface IClaimsTokenService
    {
        Task<string> GenerateTokenAsync(string username);
    }
}
