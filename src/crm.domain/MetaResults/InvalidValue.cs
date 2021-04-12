using Rolfin.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.domain.MetaResults
{
    public class InvalidValue : IMetaResult
    {
        public int Code => 6;

        public string Name => "InvalidValue";

        public string Message { get; set; }
    }
}
