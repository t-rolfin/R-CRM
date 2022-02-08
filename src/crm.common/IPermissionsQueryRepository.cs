using crm.common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.common
{
    public interface IPermissionsQueryRepository
    {
        Task<IEnumerable<PermissionModel>> GetPermissionsAsync();
    }
}
