using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Models.Base
{
    public abstract class BaseOption : BaseEntity
    {
        /// <summary>
        /// Option name
        /// </summary>
        public string Name { get; protected set; }
        
    }
}
