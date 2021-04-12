using crm.domain.Interfaces;
using crm.domain.LeadAggregate;
using Rolfin.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure
{
    public class LeadRepository : ILeadRepository
    {

        private readonly IUnitOfWork unitOfWork;

        public LeadRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Result<bool> Create(Lead obj)
        {
            throw new NotImplementedException();
        }

        public Result<Lead> GetLead(Guid leadId)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Update(Lead obj)
        {
            throw new NotImplementedException();
        }
    }
}
