using Programs_Starter.HandlersManaging;
using Programs_Starter.Models;
using Programs_Starter.ViewModels.Base;
using Programs_Starter.ViewModels.Controls;
using Programs_Starter.ViewModels.Helpers;
using Programs_Starter.ViewModels.Windows;
using Programs_Starter.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Programs_Starter.ViewModels
{
    /// <summary>
    /// ViewModel for MainWindow
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowSettings MainWindowSettings { get; set; }
        public DataGridControl<ProgramToStartWrapper> ProgramsToStart { get; set; }
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
            HandlersManager.StartingProgramsHandler.AddedNewProgram += NewProgramAddedToStartingProgramsHandler;
            HandlersManager.XMLConfigHandler.NoProgramsToStartFound += NoProgramsToStartFound;

            HandlersManager.LoadProgramsToStartFromConfig();

            InitializeControls();
        }

        private void NoProgramsToStartFound()
        {
            MainMessage.Text = "No programs to start added - add some programs first!";
            MainMessage.ForegroundColor = ControlsColors.RED;
        }

        private void NewProgramAddedToStartingProgramsHandler(int order, ProgramToStart program)
        {
            ProgramsToStart.DataCollection.Add(new ProgramToStartWrapper(program, order));        
        }

        private void CancelButtonCommand()
        {            
            MainMessage.Text = "Test przycisku Cancel udany!";
            CancelButton.IsVisible = false;
            MainMessage.ForegroundColor = ControlsColors.RED;

            StatusProgressBar.Value = 75;
            StatusProgressBar.Text = $"Progress: {StatusProgressBar.Value.ToString()}%";

            MainWindowSettings.Height = 400;
            ProgramsToStart.IsVisible = true;
        }

        private void SaveButtonCommand()
        {
            HandlersManager.SaveProgramsToStartToConfig();
        }

        private void StartNowButtonCommand()
        {
            HandlersManager.StartingProgramsHandler.TryAddProgramToStart(new ProgramToStart("test3", "D://test3.txt"));
        }

        private void InitializeControls()
        {
            MainWindowSettings = new MainWindowSettings
            {
                Height = 160
            };

            MainMessage = new TextBlockControl
            {
                ForegroundColor = ControlsColors.BLACK,
                Text = "Test tekstu udany!",
                IsVisible = true
            };

            CancelButton = new ButtonControl
            {
                ForegroundColor = ControlsColors.BLACK,
                Text = "Przycisk Cancel",
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
                Text = "Start",
                IsVisible = true,
                Command = new RelayCommand(StartNowButtonCommand)
            };

            StatusProgressBar = new ProgressBarControl
            {
                IsVisible = true,
                Value = 0
            };
            StatusProgressBar.Text = "Waiting...";

            AboutText = new TextBlockControl();
            AboutText.IsVisible = true;
            AboutText.Text = "v. 0.0.0.1  KR @ 2019";

            ProgramsToStart = new DataGridControl<ProgramToStartWrapper>();
            ProgramsToStart.IsVisible = false;
            ProgramsToStart.DataCollection = new ObservableCollection<ProgramToStartWrapper>();
            foreach (var program in HandlersManager.StartingProgramsHandler.ProgramsToStart)
            {
                ProgramsToStart.DataCollection.Add(new ProgramToStartWrapper(program.Value, program.Key));
            }
        }
    }
}
