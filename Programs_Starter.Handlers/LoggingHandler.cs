using Programs_Starter.Handlers.Base;
using Programs_Starter.Models;
using Programs_Starter.Models.Base;
using Programs_Starter.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Handlers
{
    public class LoggingHandler : BaseHandler
    {
        private const string NAME = "LoggingHandler";

        private string parentClassName;

        public List<BaseLog> Logs { get; private set; }

        public LoggingHandler(string _parentClassName) : base(NAME)
        {
            parentClassName = _parentClassName;
            Logs = new List<BaseLog>();
        }

        public void DoWarningLog(string logContent)
        {
            WarningLog log = new WarningLog(DateTime.Now, parentClassName, logContent);

            StoreLog(log);
        }

        public void DoWarningLogKV(string logContent, params string[] values)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(logContent);

            for (int i = 0; i < values.Length; i++)
            {
                if (i.IsEven())
                    stringBuilder.Append(values[i]);
                else
                    stringBuilder.Append($"=<{values[i]}>");
            }

            WarningLog log = new WarningLog(DateTime.Now, parentClassName, stringBuilder.ToString());

            StoreLog(log);
        }

        public void DoErrorLog(string logContent)
        {
            ErrorLog log = new ErrorLog(DateTime.Now, parentClassName, logContent);

            StoreLog(log);            
        }
        
        public void DoErrorLogKV(string logContent, params string[] values)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(logContent);

            for (int i = 0; i < values.Length; i++)
            {
                if (i.IsEven())
                    stringBuilder.Append(values[i]);
                else
                    stringBuilder.Append($"=<{values[i]}>");
            }

            ErrorLog log = new ErrorLog(DateTime.Now, parentClassName, stringBuilder.ToString());

            StoreLog(log);
        }

        private void StoreLog(BaseLog log)
        {
            Logs.Add(log);
        }
    }
}
