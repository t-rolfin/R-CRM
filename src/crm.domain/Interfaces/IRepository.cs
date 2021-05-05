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
        Task<T> GetAsync(Guid leadId);
        Task<T> GetByClientPhoneNumber(string phoneNumber, CancellationToken cancellationToken);
        Task<bool> CreateAsync(T obj, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(T obj, CancellationToken cancellationToken);
    }
}
