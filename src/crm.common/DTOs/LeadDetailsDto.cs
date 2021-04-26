using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.common.DTOs
{
    public class LeadDetailsDto
    {

        public LeadDetailsDto()
        {
            Notes = new List<NoteDto>();
        }

        public Guid Id { get; set; }
        public DateTime CloseLeadDate { get;  set; }
        public DateTime CatchLead { get; set; }
        public string LeadProducts { get; set; }
        public string LeadStage { get; set; }
        public string CloseStatus { get; set; }
        public string DelivaryAddress { get; set; }
        public decimal ProductsValue { get; set; }
        public List<NoteDto> Notes { get; set; }
    }
}
