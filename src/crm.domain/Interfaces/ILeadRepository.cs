using crm.domain.LeadAggregate;
using Rolfin.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.domain.Interfaces
{
    public class ILeadRepository : IUnitOfWork, IRepository<Lead>
    {
        public Result<bool> Create(Lead obj)
        {
            throw new NotImplementedException();
        }

        public Result<Lead> GetResult(Guid leadId)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Update(Lead obj)
        {
            throw new NotImplementedException();
        }
    }
}
