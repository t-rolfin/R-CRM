using System;
using System.Collections.Generic;
using System.Text;

namespace crm.common.DTOs
{
    public class NoteModel
    {
        public NoteModel() { }

        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}
