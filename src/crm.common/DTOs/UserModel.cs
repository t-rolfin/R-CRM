using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.common.DTOs
{
    public record UsersListModel(List<UserModel> Users);
    public record UserModel(
            string Id,
            string Name,
            string Email,
            string PhoneNumber,
            string Roles,
            string CreatedBy
        );

    public record UpdateUserModel(
            string Id,
            string FirstName,
            string LastName,
            string Email,
            string PhoneNumber,
            string Roles
        );
}
