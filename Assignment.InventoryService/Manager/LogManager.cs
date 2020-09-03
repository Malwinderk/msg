using Assignment.InventoryService.IManager;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.InventoryService.Manager
{
    public class LogManager:ILogManager
    {
        private readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public void LogInfo(string logMessage)
        {
            _logger.Log(LogLevel.Info, logMessage);
        }

        public void LogDebug(string logMessage)
        {
            _logger.Log(LogLevel.Debug, logMessage);
        }

        public void LogExceptionAndInnerException(Exception ex)
        {
            LogError(ex.ToString());

            if (ex.InnerException != null)
            {
                LogError(ex.InnerException.ToString());
            }
        }

        public void LogError(string logMessage, Exception logException)
        {
            _logger.Log(LogLevel.Error, logException, logMessage);
        }

        public void LogError(string logMessage)
        {
            _logger.Log(LogLevel.Error, logMessage);
        }
    }
}

