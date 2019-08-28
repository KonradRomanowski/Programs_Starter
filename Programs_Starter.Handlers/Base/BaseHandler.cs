using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Handlers.Base
{
    public abstract class BaseHandler
    {
        public string Name { get; private set; }

        protected BaseHandler(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
