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

        /// <summary>
        /// Tries to add ProgramToStart at the end of the dictionary
        /// </summary>
        /// <param name="program">Program to add</param>
        /// <returns>True if program was succesfuly added, false if there were some errors</returns>
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

        /// <summary>
        /// Tries to insert ProgramToStart at the given index in the dictionary
        /// </summary>
        /// <param name="program">Program to add</param>
        /// <param name="index">Index at which program should be added (order)</param>
        /// <returns>True if program was succesfuly inserted, false if there were some errors</returns>
        public bool TryInsertProgramToStart(ProgramToStart program, int index)
        {
            if (index <= 0)
            {
                Logger.DoErrorLogKV("TryInsertProgramToStart called with index below or equal 0!", "Index", index.ToString(),
                    "Program", program.ToString());
                return false;
            }
            if (index >= GetNewIndexForProgramToStart())
            {
                return TryAddProgramToStart(program);
            }

            try
            {
                ProgramsToStart.Insert(program, index);
                ProgramsToStartCollectionChanged?.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                Logger.DoErrorLogKV("Error while trying to insert new program to ProgramsToStart dictionary: ",
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
