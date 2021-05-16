using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.api.EndPoints.DeleteNote
{
    public class DeleteNoteModel
    {
        [FromRoute(Name = "leadid")]
        public Guid LeadId { get; set; }

        [FromRoute(Name = "noteid")]
        public Guid NoteId { get; set; }
    }
}
