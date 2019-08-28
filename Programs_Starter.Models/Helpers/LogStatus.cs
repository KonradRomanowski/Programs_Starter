using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Models.Helpers
{
    public class LogStatus
    {
        private LogStatus(string value) { Value = value; }

        public string Value { get; set; }

        public static LogStatus Trace { get { return new LogStatus("Trace"); } }
        public static LogStatus Debug { get { return new LogStatus("Debug"); } }
        public static LogStatus Info { get { return new LogStatus("Info"); } }
        public static LogStatus Warning { get { return new LogStatus("Warning"); } }
        public static LogStatus Error { get { return new LogStatus("Error"); } }
    }
}
