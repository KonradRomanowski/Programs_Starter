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
        
        /// <summary>Dictionary with programs to start</summary>
        public Dictionary<int, ProgramToStart> ProgramsToStart { get; private set; }

        public delegate void ProgramsToStartCollectionChangedDelegate(OperationType operation, bool wasSuccesful, string programName);
        /// <summary>Delegate called when ProgramsToStart dictionary is somehow changed</summary>
        public ProgramsToStartCollectionChangedDelegate ProgramsToStartCollectionChanged;

        public delegate void ProgramsToStartLoadedSuccesfullyDelegate();
        /// <summary>Delegate called when programs to start are succesfully loaded to ProgramsToStart dictionary</summary>
        public ProgramsToStartLoadedSuccesfullyDelegate ProgramsToStartLoadedSuccesfully;

        public StartingProgramsHandler() : base(NAME)
        {
            ProgramsToStart = new Dictionary<int, ProgramToStart>();
        }

        /// <summary>
        /// Initializes ProgramsToStart dictionary with given programs
        /// </summary>
        /// <param name="programs">Dictionary with programs to initialize</param>
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
            if (program == null || string.IsNullOrWhiteSpace(program.Name) || string.IsNullOrWhiteSpace(program.Path))
            {
                Logger.DoErrorLog("TryAddProgramToStart called with null parameter!");
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Added, false, string.Empty);
                return false;
            }
            int i = GetNewIndexForProgramToStart();

            try
            {
                ProgramsToStart.Add(i, program);
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Added, true, program.Name);
                return true;
            }
            catch (Exception ex)
            {
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Added, false, program.Name);
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
            if (program == null || string.IsNullOrWhiteSpace(program.Name) || string.IsNullOrWhiteSpace(program.Path))
            {
                Logger.DoErrorLog("TryInsertProgramToStart called with null parameter!");
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Added, false, string.Empty);
                return false;
            }
            if (index <= 0)
            {
                Logger.DoErrorLogKV("TryInsertProgramToStart called with index below or equal 0!", "Index", index.ToString(),
                    "Program", program.ToString());
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Added, false, program.Name);
                return false;
            }
            if (index >= GetNewIndexForProgramToStart())
            {
                return TryAddProgramToStart(program);
            }

            try
            {
                ProgramsToStart.InsertProgramToStart(program, index);
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Added, true, program.Name);
                return true;
            }
            catch (Exception ex)
            {
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Added, false, program.Name);
                Logger.DoErrorLogKV("Error while trying to insert new program to ProgramsToStart dictionary: ",
                    "Program", program.ToString(), "Error", ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Tries to remove ProgramToStart at the given index from the dictionary
        /// Recalculates keys for all other programs
        /// </summary>
        /// <param name="index">Index from which program should be removed (order)</param>
        /// <returns>True if program was succesfuly removed, false if there were errors</returns>
        public bool TryRemoveProgramToStart(int index)
        {
            if (index <= 0)
            {
                Logger.DoErrorLogKV("TryRemoveProgramToStart called with index below or equal 0!", "Index", index.ToString());
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Removed, false, string.Empty);
                return false;
            }
            if (index.IsGreaterThan(ProgramsToStart.Count))
            {
                Logger.DoErrorLogKV("TryRemoveProgramToStart called with index greater then ProgramsToStart.Count!", 
                    "Index", index.ToString(), "ProgramsToStart.Count", ProgramsToStart.Count.ToString());
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Removed, false, string.Empty);
                return false;
            }

            ProgramToStart program = ProgramsToStart[index];

            try
            {
                ProgramsToStart.RemoveProgramToStart(index);
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Removed, true, program.Name);
                return true;
            }
            catch (Exception ex)
            {
                Logger.DoErrorLogKV("Error while trying to remove program from ProgramsToStart dictionary: ",
                    "Index", index.ToString(), "Error", ex.Message);
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Removed, false, program.Name);
            }

            return false;
        }

        public bool TryChangeProgramToStartIndex(int oldIndex, int newIndex)
        {
            if (newIndex <= 0)
            {
                Logger.DoErrorLogKV("TryChangeProgramToStartIndex called with newIndex below or equal 0!", "newIndex", newIndex.ToString(),
                    "Program", string.Empty);
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Moved, false, string.Empty);
                return false;
            }
            if (oldIndex <= 0)
            {
                Logger.DoErrorLogKV("TryChangeProgramToStartIndex called with oldIndex below or equal 0!", "oldIndex", oldIndex.ToString(),
                    "Program", string.Empty);
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Moved, false, string.Empty);
                return false;
            }

            ProgramToStart program = ProgramsToStart[oldIndex];

            try
            {
                ProgramsToStart.ChangeProgramToStartIndex(oldIndex, newIndex);
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Moved, true, program.Name);
                return true;
            }
            catch (Exception ex)
            {
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Moved, false, program.Name);
                Logger.DoErrorLogKV("Error while trying to change items index in ProgramsToStart dictionary: ",
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
