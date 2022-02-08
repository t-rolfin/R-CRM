using crm.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure.Identity
{
    public interface IPermissionRepository : IRepository<Permission>
    {
    }
}
