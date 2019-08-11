using Programs_Starter.Models.Base;

namespace Programs_Starter.Models
{
    /// <summary>
    /// Program class
    /// </summary>
    public class ProgramToStart : BaseProgramDefinition
    {

        /// <summary>
        /// Program class
        /// </summary>
        /// <param name="name">Program Name</param>
        /// <param name="path">Path to the program</param>
        public ProgramToStart(string name, string path) : base(name, path)
        {
        }
    }
}
