using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.common.DTOs.GetLeads
{
    public class LeadsModel
    {
        public LeadsModel()
        {
            Leads = new List<LeadModel>();
        }

        public LeadsModel(IEnumerable<LeadModel> leads)
        {
            Leads = leads;
        }

        public IEnumerable<LeadModel> Leads { get; set; }
    }
}
