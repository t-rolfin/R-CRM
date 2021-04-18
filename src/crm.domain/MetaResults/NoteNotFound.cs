using Rolfin.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.domain.MetaResults
{
    public class NoteNotFound : IMetaResult
    {
        public int Code => 4;

        public string Name => "NoteNotFound";

        public string Message { get; set; }
    }
}
