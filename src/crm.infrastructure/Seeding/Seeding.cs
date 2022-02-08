using crm.infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure.Seeding
{
    public class Seeding
    {
        public static void SeedPermissions(IdentityContext context)
        {
            if (context.Perrmissions.Any())
                return;


            context.Perrmissions.AddRange(
                    new List<Permission>
                    {
                        new Permission("Leads", "read:leads"),
                        new Permission("Leads", "write:leads")
                    }
                );
        }
    }
}
