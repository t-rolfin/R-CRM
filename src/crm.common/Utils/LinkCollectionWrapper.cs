using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.common.Utils
{
    public class LinkCollectionWrapper<T> : LinkResourceBase
        where T : LinkResourceBase
    {
        public List<T> Value { get; set; }
            = new();

        public LinkCollectionWrapper(List<T> value)
        {
            Value = value;
        }
    }
}
