using crm.domain.LeadAggregate;
using Rolfin.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.domain.Interfaces
{
    public interface IRepository<T>
        where T : IAggregateRoot
    {
        Result<Lead> GetResult(Guid leadId);
        Result<bool> Create(T obj);
        Result<bool> Update(T obj);
    }
}
