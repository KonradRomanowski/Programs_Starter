using GongSolutions.Wpf.DragDrop;
using Microsoft.Win32;
using Programs_Starter.Handlers;
using Programs_Starter.HandlersManaging;
using Programs_Starter.Models;
using Programs_Starter.Models.Helpers;
using Programs_Starter.ViewModels.Base;
using Programs_Starter.ViewModels.Controls;
using Programs_Starter.ViewModels.Helpers;
using Programs_Starter.ViewModels.Windows;
using Programs_Starter.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Programs_Starter.ViewModels
{
    /// <summary>
    /// ViewModel for MainWindow
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        public const int MAIN_WINDOW_HEIGHT_SMALL = 160;
        public const int MAIN_WINDOW_HEIGHT_BIG = 400;        

        public MainWindowSettings MainWindowSettings { get; set; }
        public ProgramsToStartDataGridWrapper ProgramsToStart { get; set; }
        public TextBlockControl MainMessage { get; set; }
        public TextBlockControl AboutText { get; set; }
        public ButtonControl CancelButton { get; set; }
        public ButtonControl SaveButton { get; set; }
        public ButtonControl ThankYouButton { get; set; }
        public ButtonControl StartNowButton { get; set; }
        public ButtonControl DontStartButton { get; set; }
        public ButtonControl OptionsButton { get; set; }
        public ButtonControl ShowProgramsListButton { get; set; }
        public ProgressBarControl StatusProgressBar { get; set; }
        
        public MainWindowViewModel()
        {
            InitializeControls();
            InitializeDelegatesFromHandlersManager();

            HandlersManager.LoadOptionsFromConfig();
            HandlersManager.LoadProgramsToStartFromConfig();
        }

        private void InitializeDelegatesFromHandlersManager()
        {            
            HandlersManager.XMLConfigHandler.NoProgramsToStartFound += NoProgramsToStartFound;
            HandlersManager.XMLConfigHandler.ProgramsToStartSaved += ProgramsToStartSaved;
            HandlersManager.StartingProgramsHandler.ProgramsToStartCollectionChanged += ProgramsToStartCollectionChanged;
            HandlersManager.StartingProgramsHandler.FinishedStartingProcedure += StartingProcedureIsFinished;
        }

        private void StartingProcedureIsFinished(bool wasSuccessful)
        {
            if (wasSuccessful)
            {
                MainMessage.Text = "All programs successfully started!";
                MainMessage.ForegroundColor = ControlsColors.GREEN;
            }
            else
            {
                MainMessage.Text = "Some errors occured when starting programs";
                MainMessage.ForegroundColor = ControlsColors.RED;
            }

            StatusProgressBar.Value = HandlersManager.StartingProgramsHandler.GetPercentOfStartedPrograms();
            StatusProgressBar.Text = $"Finished!";
            CancelButton.IsVisible = false;
        }

        private void ProgramsToStartCollectionChanged(OperationType operation, bool wasSuccesful, string programName)
        {
            if (operation.Value == OperationType.Added.Value)
            {
                if (wasSuccesful)
                {
                    MainMessage.Text = string.IsNullOrWhiteSpace(programName) ? "New program added!" :
                        $"Program {programName} added!";
                    MainMessage.ForegroundColor = ControlsColors.GREEN;
                }
                else
                {
                    MainMessage.Text = string.IsNullOrWhiteSpace(programName) ? "Error when adding new program!" :
                        $"Error when adding program {programName}!";
                    MainMessage.ForegroundColor = ControlsColors.RED;
                }
            }

            if (operation.Value == OperationType.Removed.Value)
            {
                if (wasSuccesful)
                {
                    MainMessage.Text = string.IsNullOrWhiteSpace(programName) ? "Program removed!" :
                        $"Program {programName} removed!";
                    MainMessage.ForegroundColor = ControlsColors.GREEN;
                }
                else
                {
                    MainMessage.Text = string.IsNullOrWhiteSpace(programName) ? "Error when removing program!" :
                        $"Error when removing program {programName}!";
                    MainMessage.ForegroundColor = ControlsColors.RED;
                }
            }

            if (operation.Value == OperationType.Moved.Value)
            {
                if (wasSuccesful)
                {
                    MainMessage.Text = string.IsNullOrWhiteSpace(programName) ? "Program moved!" :
                        $"Program {programName} moved!";
                    MainMessage.ForegroundColor = ControlsColors.GREEN;
                }
                else
                {
                    MainMessage.Text = string.IsNullOrWhiteSpace(programName) ? "Error when moving program!" :
                        $"Error when moving program {programName}!";
                    MainMessage.ForegroundColor = ControlsColors.RED;
                }
            }
            
            if (operation.Value == OperationType.Started.Value)
            {
                if (wasSuccesful)
                {
                    MainMessage.Text = string.IsNullOrWhiteSpace(programName) ? "Program started!" :
                        $"Program {programName} started!";
                    MainMessage.ForegroundColor = ControlsColors.GREEN;
                }
                else
                {
                    MainMessage.Text = string.IsNullOrWhiteSpace(programName) ? "Error when starting program!" :
                        $"Error when starting program {programName}!";
                    MainMessage.ForegroundColor = ControlsColors.RED;
                }

                StatusProgressBar.Value = HandlersManager.StartingProgramsHandler.GetPercentOfStartedPrograms();
                StatusProgressBar.Text = $"Progress: {StatusProgressBar.Value.ToString()}%";
            }
        }

        private void ProgramsToStartSaved(bool wasSaveSuccesfull)
        {
            if (wasSaveSuccesfull)
            {
                MainMessage.Text = "Programs to start saved!";
                MainMessage.ForegroundColor = ControlsColors.GREEN;
            }
            else
            {
                MainMessage.Text = "Programs to start not saved - error!";
                MainMessage.ForegroundColor = ControlsColors.RED;
            }
        }        

        private void NoProgramsToStartFound()
        {
            MainMessage.Text = "Welcome in Programs Starter - add some programs first!";
            MainMessage.ForegroundColor = ControlsColors.BLACK;
        }
        
        private void CancelButtonCommand()
        {            
            MainMessage.Text = "Test przycisku Cancel udany!";
            CancelButton.IsVisible = false;
            MainMessage.ForegroundColor = ControlsColors.RED;

            StatusProgressBar.Value = 75;
            StatusProgressBar.Text = $"Progress: {StatusProgressBar.Value.ToString()}%";            
        }

        private void ShowProgramsListButtonCommand()
        {
            if (ProgramsToStart.IsVisible)
            {
                ProgramsToStart.IsVisible = false;
                MainWindowSettings.Height = MAIN_WINDOW_HEIGHT_SMALL;
            }
            else
            {
                ProgramsToStart.IsVisible = true;
                MainWindowSettings.Height = MAIN_WINDOW_HEIGHT_BIG;                
            }            
        }

        private void SaveButtonCommand()
        {
            HandlersManager.SaveProgramsToStartToConfig();
        }

        private void StartNowButtonCommand()
        {
            // Prepare Controls
            StartNowButton.IsVisible = false;
            DontStartButton.IsVisible = false;
            StatusProgressBar.Value = 0;
            StatusProgressBar.Text = "Starting...";

            // Start work
            HandlersManager.StartingProgramsHandler.StartPrograms(OptionsHandler.GapBetweenStartingPrograms.Value);
        }

        private void DontStartButtonCommand()
        {
            throw new NotImplementedException();
        }

        private void InitializeControls()
        {
            MainWindowSettings = new MainWindowSettings
            {
                Height = MAIN_WINDOW_HEIGHT_SMALL,
            };

            ShowProgramsListButton = new ButtonControl
            {
                ForegroundColor = ControlsColors.BLACK,
                Text = "Programs",
                IsVisible = true,
                Command = new RelayCommand(ShowProgramsListButtonCommand)
            };

            MainMessage = new TextBlockControl
            {
                ForegroundColor = ControlsColors.BLACK,
                Text = "Welcome in Programs Starter!",
                IsVisible = true
            };

            CancelButton = new ButtonControl
            {
                ForegroundColor = ControlsColors.BLACK,
                Text = "Cancel",
                IsVisible = true,
                Command = new RelayCommand(CancelButtonCommand)
            };

            SaveButton = new ButtonControl
            {
                ForegroundColor = ControlsColors.BLACK,
                Text = "Save",
                IsVisible = true,
                Command = new RelayCommand(SaveButtonCommand)
            };

            StartNowButton = new ButtonControl
            {
                ForegroundColor = ControlsColors.BLACK,
                Text = "Start Now",
                IsVisible = true,
                Command = new RelayCommand(StartNowButtonCommand)
            };

            DontStartButton = new ButtonControl
            {
                ForegroundColor = ControlsColors.BLACK,
                Text = "Don't start",
                IsVisible = true,
                Command = new RelayCommand(DontStartButtonCommand)
            };

            StatusProgressBar = new ProgressBarControl
            {
                IsVisible = true,
                Value = 0,
                Text = "Waiting...",
            };

            AboutText = new TextBlockControl();
            AboutText.IsVisible = true;
            AboutText.Text = "v. 0.0.0.1  KR @ 2019";

            ProgramsToStart = new ProgramsToStartDataGridWrapper();
            ProgramsToStart.IsVisible = false;            
        }
                
    }
}
