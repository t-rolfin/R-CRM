using crm.domain.Interfaces;
using crm.domain.LeadAggregate;
using Rolfin.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace crm.domain.Services
{
    public class LeadService : ILeadService
    {

        private readonly IRepository<Lead> _leadRepo;

        public LeadService(IRepository<Lead> leadRepo)
        {
            _leadRepo = leadRepo;
        }

        public async Task<Result<Lead>> AssignExistingClient(Lead lead, string customerPhoneNumber, CancellationToken cancellationToken)
        {

            var leadDetails = await _leadRepo
                .GetByClientPhoneNumber(customerPhoneNumber, cancellationToken);

            if (leadDetails is not null)
            {
                var result = lead
                    .UpdateClient(leadDetails.Client);

                return result.IsSuccess 
                    ? Result<Lead>.Success(lead)
                    : Result<Lead>.Invalid();
            }

            return Result<Lead>.Success(lead);
        }
    }
}
