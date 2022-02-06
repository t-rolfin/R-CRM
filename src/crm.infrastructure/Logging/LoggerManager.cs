using Microsoft.Extensions.Logging;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure.Logging;

public partial class LoggerManager : ILoggerManager
{
    private readonly ILogger<LoggerManager> logger;

    public LoggerManager(ILogger<LoggerManager> logger)
    {
        this.logger = logger;
    }

    [LoggerMessage(0, LogLevel.Information, "{message}")]
    public partial void LogError(string message);
}
