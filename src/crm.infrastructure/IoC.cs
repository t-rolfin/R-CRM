﻿using crm.domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure
{
    public static class IoC
    {
        public static ServiceCollection Infrastructure(this ServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(x =>
            {
                return new LeadConnString(configuration.GetConnectionString("LeadConnectionString"));
            });

            services.AddDbContext<LeadDbContext>();
            services.AddTransient<IUnitOfWork, LeadDbContext>();

            return services;
        }
    }
}