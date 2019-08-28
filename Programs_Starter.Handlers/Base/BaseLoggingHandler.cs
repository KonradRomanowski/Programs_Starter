using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Handlers.Base
{
    public abstract class BaseLoggingHandler : BaseHandler
    {
        protected LoggingHandler Logger { get; private set; }

        protected BaseLoggingHandler(string name) : base(name)
        {
            Logger = new LoggingHandler(name);
        }
    }
}
