using Programs_Starter.Handlers.Base;
using Programs_Starter.Models;
using Programs_Starter.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        public delegate void FinishedStartingProcedureDelegate(bool wasSuccessful);
        /// <summary>Delegate called when starting procedure is done</summary>
        public FinishedStartingProcedureDelegate FinishedStartingProcedure;

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

        /// <summary>
        /// Tries to move ProgramToStart at the given index in the dictionary
        /// Recalculates keys for all other programs
        /// </summary>
        /// <param name="oldIndex">Index from which program should be moved (order)</param>
        /// <param name="newIndex">Index to which program should be moved (order)</param>
        /// <returns>True if program was succesfuly moved, false if there were errors</returns>
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

        /// <summary>
        /// StartingProgramsHandler will begin to start all programs
        /// </summary>
        /// <param name="gapBetween">Time gap in seconds between two programs to start</param>
        public async void StartPrograms(int gapBetween)
        {
            // check if programs dict is empty
            if (ProgramsToStart.Count == 0)
            {
                Logger.DoErrorLog("StartPrograms method called for empty ProgramsToStart dictionary!");
                FinishedStartingProcedure?.Invoke(false);
                return;
            }
            // check if gapBerween is below zero
            if (gapBetween < 0)
            {
                Logger.DoErrorLog("StartPrograms method called with gapBetween parameter below 0!");
                FinishedStartingProcedure?.Invoke(false);
                return;
            }

            try
            {
                // Set all programs status to pending
                foreach (var program in ProgramsToStart)
                {
                    program.Value.SetProgramStatus(ProgramStatus.Pending);
                }

                // Start all programs and await gapBetween
                foreach (var program in ProgramsToStart.OrderBy(x => x.Key))
                {
                    StartProgram(program.Value);
                    await Task.Delay(gapBetween * 1000);
                }

                // Finish starting procedure
                FinishedStartingProcedure?.Invoke(true);
            }
            catch (Exception ex)
            {
                Logger.DoErrorLogKV("Error in StartPrograms method", "Error", ex.Message);

                foreach (var program in ProgramsToStart)
                {
                    if (program.Value.ProgramStatus.Value == ProgramStatus.Pending.Value)
                        program.Value.SetProgramStatus(ProgramStatus.Error);
                }
                FinishedStartingProcedure?.Invoke(false);
            }
        }

        /// <summary>
        /// This method is starting given program
        /// </summary>
        /// <param name="program">Program which needs to be started</param>
        private void StartProgram(ProgramToStart program)
        {
            // Check if given program is valid
            if (program == null || string.IsNullOrWhiteSpace(program.Name) || string.IsNullOrWhiteSpace(program.Path))
            {
                Logger.DoErrorLog("StartProgram called with null parameter!");
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Started, false, string.Empty);
                return;
            }
            // Check if file exist
            if (!File.Exists(program.Path))
            {
                Logger.DoErrorLogKV("Program does not exist at given path", "Program", program.ToString(), "Path", program.Path);
                program.SetProgramStatus(ProgramStatus.Error);
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Started, false, program.Name);
                return;
            }
            
            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo();

                //Check if file is an Excel sheet and Excel is installed then use default process
                if (FileIsExcelSheet(program.Path) && ExcelIsInstalled())
                {
                    processInfo.FileName = "Excel";
                    processInfo.Arguments = program.Path;

                    Process process = Process.Start(processInfo);
                }
                else
                {
                    //Start the program from it's path
                    Process process = Process.Start(program.Path);
                }

                program.SetProgramStatus(ProgramStatus.Starting);
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Started, true, program.Name);
            }
            catch (Exception ex)
            {
                Logger.DoErrorLogKV("Error while starting program!", "Program", program.ToString(), "Error", ex.Message);
                program.SetProgramStatus(ProgramStatus.Error);
                ProgramsToStartCollectionChanged?.Invoke(OperationType.Started, false, program.Name);
            }
        }

        /// <summary>
        /// Returns integer value with percent number of started programs from ProgramsToStart dict
        /// </summary>
        /// <returns></returns>
        public int GetPercentOfStartedPrograms()
        {
            return (int)((
                (float) ProgramsToStart.Count(x => x.Value.ProgramStatus.Value != ProgramStatus.Pending.Value) / ProgramsToStart.Count()
                ) * 100);
        }

        /// <summary>
        /// This method is checking if file is an Excel sheet
        /// </summary>
        /// <returns></returns>
        private bool FileIsExcelSheet(string _path)
        {
            string extenstion = Path.GetExtension(_path);

            return (extenstion == ".xls" || extenstion == ".xlsx" || extenstion == ".xlsm") ? true : false;
        }

        /// <summary>
        /// This method is checking if Excel software is installed on machine
        /// </summary>
        /// <returns></returns>
        private bool ExcelIsInstalled()
        {
            Type officeType = Type.GetTypeFromProgID("Excel.Application");

            return (officeType == null) ? false : true;
        }

        private int GetNewIndexForProgramToStart()
        {
            return ProgramsToStart.Count + 1;
        }
    }
}
