using System.Collections.Generic;
using System.Threading.Tasks;

namespace crm.infrastructure.Identity
{
    public interface IIdentityService
    {
        Task<bool> RegisterUserAsync(User user, List<string> roles, string password);
        Task<(string token, string idToken, string refreshToken)> LogInAsync(string userName, string password);
    }
}