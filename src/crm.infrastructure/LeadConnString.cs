using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure
{
    public class LeadConnString
    {
        public LeadConnString(string value)
        {
            Value = value;
        }

        public string Value { get; init; }
    }
}
