using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
    }
}
