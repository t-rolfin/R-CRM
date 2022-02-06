using Microsoft.Extensions.Logging;

namespace crm.infrastructure.Logging
{
    public interface ILoggerManager
    {
        void LogError(string message);
    }
}