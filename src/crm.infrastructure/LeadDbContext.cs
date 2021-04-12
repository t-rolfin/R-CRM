using crm.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using crm.domain.LeadAggregate;
using System.Reflection;

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

        public DbSet<Lead> Leads { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.Value);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
