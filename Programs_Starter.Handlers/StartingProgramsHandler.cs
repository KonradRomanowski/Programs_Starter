using Programs_Starter.Handlers.Base;
using Programs_Starter.Models;
using Programs_Starter.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Handlers
{
    public class StartingProgramsHandler : BaseHandler
    {
        private const string NAME = "StartingProgramsHandler";
        
        public Dictionary<int, ProgramToStart> ProgramsToStart { get; private set; }

        public StartingProgramsHandler() : base(NAME)
        {
            ProgramsToStart = new Dictionary<int, ProgramToStart>();
        }

        public bool TryAddProgramToStart(ProgramToStart program)
        {
            return false;
        }
    }
}
