using crm.domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.api.EndPoints.CloseLead
{
    public class CloseLeadModel
    {
        [FromRoute( Name = "leadid")]
        public Guid LeadId { get; set; }

        [FromForm]
        public CloseStatus CloseStatus { get; set; }
    }
}
