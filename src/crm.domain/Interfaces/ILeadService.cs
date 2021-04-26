using crm.domain.LeadAggregate;
using Rolfin.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace crm.domain.Interfaces
{
    public interface ILeadService
    {
        Task<Result<Lead>> AssignExistingClient(Lead lead, string customerPhoneNumber, CancellationToken cancellationToken);
    }
}
