using crm.common.DTOs;
using crm.common.DTOs.GetLeads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.common
{
    public interface ILeadQueryRepository
    {
        Task<LeadsModel> GetAll();
        Task<LeadDetailsModel> GetDetails(Guid leadId);
        Task<ClientDetailsModel> GetLeadClientDetails(Guid leadId);
    }
}
