using Programs_Starter.Handlers.Base;
using Programs_Starter.Models;
using Programs_Starter.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Handlers
{
    public class StartingProgramsHandler : BaseLoggingHandler
    {
        private const string NAME = "StartingProgramsHandler";
        
        public Dictionary<int, ProgramToStart> ProgramsToStart { get; private set; }

        public delegate void ProgramsToStartCollectionChangedDelegate();
        public ProgramsToStartCollectionChangedDelegate ProgramsToStartCollectionChanged;

        public delegate void ProgramsToStartLoadedSuccesfullyDelegate();
        public ProgramsToStartLoadedSuccesfullyDelegate ProgramsToStartLoadedSuccesfully;

        public StartingProgramsHandler() : base(NAME)
        {
            ProgramsToStart = new Dictionary<int, ProgramToStart>();
        }

        public void InitializeProgramsToStartDictionary(Dictionary<int, ProgramToStart> programs)
        {
            ProgramsToStart = programs;
            ProgramsToStartLoadedSuccesfully?.Invoke();
        }

        public bool TryAddProgramToStart(ProgramToStart program)
        {
            int i = GetNewIndexForProgramToStart();

            try
            {
                ProgramsToStart.Add(i, program);
                ProgramsToStartCollectionChanged?.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                Logger.DoErrorLogKV("Error while trying to add new program to ProgramsToStart dictionary: ", 
                    "Program", program.ToString(), "Error", ex.Message);
            }

            return false;
        }

        private int GetNewIndexForProgramToStart()
        {
            return ProgramsToStart.Count + 1;
        }
    }
}
