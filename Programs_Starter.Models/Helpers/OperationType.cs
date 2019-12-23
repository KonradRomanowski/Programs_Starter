using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Models.Helpers
{
    public class OperationType
    {
        public string Value { get; set; }

        private OperationType(string value) { Value = value; }

        public static OperationType Added { get { return new OperationType("Added"); } }
        public static OperationType Removed { get { return new OperationType("Removed"); } }
        public static OperationType Moved { get { return new OperationType("Moved"); } }
    }
}
