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
    public interface IRepository<T>
        where T : IAggregateRoot
    {
        Task<Result<Lead>> GetLead(Guid leadId);
        Task<Result<bool>> Create(T obj, CancellationToken cancellationToken);
        Task<Result<bool>> Update(T obj, CancellationToken cancellationToken);
    }
}
