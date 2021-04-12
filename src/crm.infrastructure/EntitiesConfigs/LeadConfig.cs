using crm.domain.LeadAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure.EntitiesConfigs
{
    public class LeadConfig : IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.OwnsOne(x => x.DelivaryAddress);

            builder.HasOne(x => x.Client)
                .WithOne()
                .HasForeignKey<Customer>(x => x.LeadId);

            builder.HasMany(x => x.Notes)
                .WithOne();
        }
    }
}
