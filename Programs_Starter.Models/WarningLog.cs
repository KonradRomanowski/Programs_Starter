using Programs_Starter.Models.Base;
using Programs_Starter.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Models
{
    public class WarningLog : BaseLog
    {
        public WarningLog(DateTime dateAndTime, string className, string log)
            : base(dateAndTime, LogStatus.Warning, className, log)
        {
        }
    }
}
