using crm.domain.Interfaces;
using crm.domain.LeadAggregate;
using Microsoft.EntityFrameworkCore;
using Rolfin.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace crm.infrastructure
{
    public class LeadRepository : ILeadRepository
    {

        private readonly LeadDbContext unitOfWork;

        public LeadRepository(LeadDbContext unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork
            => this.unitOfWork;

        public async Task<Result<Lead>> GetLead(Guid leadId)

        {
            return await unitOfWork.Leads.Where(x => x.Id == leadId)
                .Include(x => x.Notes).FirstOrDefaultAsync();
        }

        public async Task<Result<bool>> Create(Lead obj, CancellationToken cancellationToken)
        {
            if (obj is null)
                return Result<bool>.Invalid(false);
            else
            {
                await unitOfWork.Leads.AddAsync(obj);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> Update(Lead obj, CancellationToken cancellationToken)
        {
            if (obj is null)
                return Result<bool>.Invalid(false);
            else
            {
                unitOfWork.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return Result<bool>.Success(true);
        }
    }
}
