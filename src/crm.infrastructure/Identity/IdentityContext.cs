using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure.Identity
{
    public class IdentityContext : IdentityDbContext<User>
    {
        private readonly IConfiguration _config;
        public IdentityContext(IConfiguration config) : base()
        { _config = config; }

        public DbSet<Permission> Perrmissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("LeadConnectionString"));
            base.OnConfiguring(optionsBuilder);
        }

    }
}
