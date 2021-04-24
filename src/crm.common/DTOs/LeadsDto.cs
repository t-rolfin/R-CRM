using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.common.DTOs.GetLeads
{
    public class LeadsDto
    {
        public LeadsDto()
        {
            Leads = new List<LeadDto>();
        }

        public LeadsDto(IEnumerable<LeadDto> leads)
        {
            Leads = leads;
        }

        public IEnumerable<LeadDto> Leads { get; set; }
    }
}
