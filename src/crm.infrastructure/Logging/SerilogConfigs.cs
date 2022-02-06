using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure.Logging
{
    public static class SerilogConfigs
    {
        public static void GetConfigs(HostBuilderContext context, IServiceProvider serviceProvider, LoggerConfiguration loggerConfigs)
        {
            loggerConfigs
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(serviceProvider)
                .Enrich.FromLogContext()
                .WriteTo.Console();
        }
    }
}
