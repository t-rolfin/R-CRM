using Rolfin.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.domain.MetaResults
{
    public class NoContent : IMetaResult
    {
        public int Code => 5;

        public string Name => "NoContent";

        public string Message { get; set; }
    }
}
