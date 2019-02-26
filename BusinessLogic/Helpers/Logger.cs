using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Logger));

        public Logger()
        {

        }
        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Error(string message, string requestToken = null, Exception ex = null)
        {
            StackTrace stackTrace = new StackTrace();
            var callerName = stackTrace.GetFrame(1)?.GetMethod()?.Name;
            Log.Error("Error in " + callerName + ": " + message + ", requestToken: " + requestToken
                + (ex != null ? Environment.NewLine + "Caugh Exception:" + Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace + Environment.NewLine + "Inner Exception: " + ex.InnerException : string.Empty)
                );
        }

        public void Info(string message, string requestToken = null, string sfmTag = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            StackTrace stackTrace = new StackTrace();
            var callerName = stackTrace.GetFrame(1)?.GetMethod()?.Name;
            Log.Info("[" + callerName + "]"
                + (!string.IsNullOrWhiteSpace(requestToken) ? "[" + requestToken + "]" : string.Empty)
                + (!string.IsNullOrWhiteSpace(sfmTag) ? ", SFM Tag: " + sfmTag + ", " : string.Empty)
                +(startDate.HasValue && endDate.HasValue ? ", call duration: " + (endDate - startDate) + ", " : string.Empty)
                + message);
        }

        public void Warning(string message)
        {
            Log.Warn(message);
        }
    }
}
