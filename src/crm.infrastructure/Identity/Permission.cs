using crm.domain;
using crm.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure.Identity
{
    public class Permission : Entity<Guid>, IAggregateRoot
    {
        protected Permission() : base() { }

        public Permission(string group, string name) : base()
        {
            Group = group;
            Name = name;
        }

        public string Group { get; protected set; }
        public string Name { get; protected set; }
    }
}
