using crm.common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.common.DTOs
{
    public class NoteCollectionModel
    {
        public List<Link> Links { get; set; }
            = new List<Link>();

        public List<NoteModel> Notes { get; set; }
    }
}
