using crm.common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.common.DTOs.GetLeads
{
    public class LeadModel : LinkResourceBase
    {
        public LeadModel() : base() { }

        public LeadModel(Guid id, string phoneNumber, int leadStage, DateTime catchLead)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            LeadStage = leadStage;
            CatchLead = catchLead;
        }

        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public int LeadStage { get; set; }
        public DateTime CatchLead { get; set; }
    }
}
