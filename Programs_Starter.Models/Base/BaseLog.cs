using Programs_Starter.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Models.Base
{
    public abstract class BaseLog : BaseEntity
    {
        public DateTime DateAndTime { get; protected set; }
        public LogStatus LogStatus { get; protected set; }
        public string ClassName { get; protected set; }
        public string Log { get; protected set; }

        protected BaseLog(DateTime dateAndTime, LogStatus logStatus, string className, string log)
        {
            DateAndTime = dateAndTime;
            LogStatus = logStatus ?? throw new ArgumentNullException(nameof(logStatus));
            ClassName = className ?? throw new ArgumentNullException(nameof(className));
            Log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public override string ToString()
        {
            return $"{DateAndTime}: {LogStatus.Value} log from {ClassName}: {Log}";
        }
    }
}
