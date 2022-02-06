using System.Collections.Generic;

namespace crm.api.AccountModels
{
    public record RegisterModel(
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Email,
        string Password,
        bool GeneratePassword,
        List<string> Roles = null
        );
}
