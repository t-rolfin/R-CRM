using crm.common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.common.DTOs
{
    public class LeadDetailsModel : LinkResourceBase
    {

        public LeadDetailsModel()
        {
            Notes = new List<NoteModel>();
        }

        public Guid Id { get; set; }
        public DateTime CloseLeadDate { get;  set; }
        public DateTime CatchLead { get; set; }
        public string LeadProducts { get; set; }
        public int LeadStage { get; set; }
        public string CloseStatus { get; set; }
        public string DelivaryAddress { get; set; }
        public decimal ProductsValue { get; set; }
        public List<NoteModel> Notes { get; set; }
    }
}
