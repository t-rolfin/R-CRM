using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.common.DTOs.GetLeads
{
    public class LeadDto
    {
        public LeadDto() { }

        public LeadDto(Guid id, string phoneNumber, string leadStage, DateTime catchLead)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            LeadStage = leadStage;
            CatchLead = catchLead;
        }

        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string LeadStage { get; set; }
        public DateTime CatchLead { get; set; }
    }
}
