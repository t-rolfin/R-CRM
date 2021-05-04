using crm.domain.Interfaces;
using crm.domain.LeadAggregate;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Lead> GetAsync(Guid leadId)

        {
            return await unitOfWork.Leads.Where(x => x.Id == leadId)
                .Include(x => x.Notes)
                .Include(x => x.Client)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(Lead obj, CancellationToken cancellationToken)
        {
            if (obj is null)
                return false;
            else
            {
                await unitOfWork.Leads.AddAsync(obj);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return true;
        }

        public async Task<bool> UpdateAsync(Lead obj, CancellationToken cancellationToken)
        {
            if (obj is null)
                return false;
            else
            {
                unitOfWork.Entry(obj).State = EntityState.Modified;
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return true;
        }

        public async Task<Lead> GetByClientPhoneNumber(string phoneNumber, CancellationToken cancellationToken)
        {
            var lead = await unitOfWork.Leads.Include(x => x.Client)
                .Where(x => x.Client.PhoneNumber == phoneNumber)
                .FirstOrDefaultAsync();

            if (lead is not null)
                return lead;
            else
                return null;
        }
    }
}
