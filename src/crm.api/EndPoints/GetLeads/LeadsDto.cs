using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.api.EndPoints.GetLeads
{
    public class LeadsDto
    {
        public List<LeadDto> Leads { get; set; } = new List<LeadDto>();
    }
}
