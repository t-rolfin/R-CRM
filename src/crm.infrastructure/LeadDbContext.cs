using crm.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace crm.infrastructure
{
    public class LeadDbContext : DbContext, IUnitOfWork
    {
        private readonly LeadConnString ConnectionString;

        public LeadDbContext(LeadConnString connectionString)
            :base()
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.Value);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
