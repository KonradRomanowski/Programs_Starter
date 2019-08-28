using Programs_Starter.Models.Base;
using Programs_Starter.Models.Helpers;

namespace Programs_Starter.Models
{
    /// <summary>
    /// Program class
    /// </summary>
    public class ProgramToStart : BaseProgramDefinition
    {
        public ProgramStatus ProgramStatus { get; private set; }

        /// <summary>
        /// Program class
        /// </summary>
        /// <param name="name">Program Name</param>
        /// <param name="path">Path to the program</param>
        public ProgramToStart(string name, string path) : base(name, path)
        {
            ProgramStatus = ProgramStatus.Unknown;
        }

        public void SetProgramStatus(ProgramStatus newStatus)
        {
            ProgramStatus = newStatus;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Path: {Path}";
        }
    }
}
