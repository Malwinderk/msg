using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryManagementService.IManager
{
    public interface ILogManager
    {
        void LogInfo(string logMessage);
        void LogDebug(string logMessage);
        void LogExceptionAndInnerException(Exception ex);
        void LogError(string logMessage, Exception logException);
        void LogError(string logMessage);
    }
}
