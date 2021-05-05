using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.api.EndPoints.AddNote
{
    public class AddNoteModel
    {

        [FromRoute(Name = "leadid")]  
        public Guid LeadId { get; set; }

        [FromForm]
        public string Note { get; set; }
    }
}
