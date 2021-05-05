using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.api.EndPoints.CreateLead
{

    [Serializable]
    public class CreateLeadModel
    {
        public string LeadProducts { get; set; }
        public string PhoneNumber { get; set; }
        public string DelivaryAddress { get; set; } 
        public string Email { get; set; }
    }
}
