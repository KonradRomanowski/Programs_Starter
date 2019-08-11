using System;

namespace Programs_Starter.Models.Base
{
    /// <summary>
    /// Base Program definition class
    /// </summary>
    public abstract class BaseProgramDefinition : BaseEntity
    {       
        /// <summary>Program Name</summary>
        public string Name { get; private set; }
        /// <summary>Path to the program</summary>
        public string Path { get; private set; }

        /// <summary>
        /// Base Program definition
        /// </summary>
        /// <param name="name">Program Name</param>
        /// <param name="path">Path to the program</param>
        protected BaseProgramDefinition(string name, string path)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }
    }
}
