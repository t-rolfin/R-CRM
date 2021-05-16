using crm.common.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace crm.common.DTOs
{
    public class NoteModel : LinkResourceBase
    {
        public NoteModel() : base() { }

        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}
