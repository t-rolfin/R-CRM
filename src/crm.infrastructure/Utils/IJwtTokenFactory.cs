using crm.infrastructure.Identity;

namespace crm.infrastructure.Utils
{
    public interface IJwtTokenFactory
    {
        string GenerateToken(User user);
        string GenerateIdToken(User user);
    }
}