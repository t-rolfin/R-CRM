using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.api.EndPoints.UpdateNote
{
    public class UpdateNoteModel
    {
        [FromRoute(Name = "leadid")]
        public Guid LeadId { get; set; }

        [FromRoute(Name = "noteid")]
        public Guid NoteId { get; set; }

        [FromForm]
        public string NewContent { get; set; }
    }
}
