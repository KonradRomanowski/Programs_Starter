using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Models.Helpers
{
    public class ProgramStatus
    {
        private ProgramStatus(string value) { Value = value; }

        public string Value { get; set; }

        public static ProgramStatus Stopped { get { return new ProgramStatus("Stopped"); } }
        public static ProgramStatus Starting { get { return new ProgramStatus("Starting"); } }
        public static ProgramStatus Running { get { return new ProgramStatus("Running"); } }
        public static ProgramStatus Error { get { return new ProgramStatus("Error"); } }
        public static ProgramStatus Unknown { get { return new ProgramStatus("Unknown"); } }
        public static ProgramStatus Pending { get { return new ProgramStatus("Pending"); } }
    }
}
