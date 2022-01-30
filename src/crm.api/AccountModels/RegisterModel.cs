using System.Collections.Generic;

namespace crm.api.AccountModels
{
    public record RegisterModel(
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Email,
        string Password,
        string VerifyPassword,
        List<string> Roles = null
        );
}
