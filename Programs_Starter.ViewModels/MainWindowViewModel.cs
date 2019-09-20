﻿using Programs_Starter.HandlersManaging;
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
        public const int MAIN_WINDOW_HEIGHT_SMALL = 160;
        public const int MAIN_WINDOW_HEIGHT_BIG = 400;        

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
            InitializeControls();
            InitializeDelegatesFromHandlersManager();

            HandlersManager.LoadOptionsFromConfig();
            HandlersManager.LoadProgramsToStartFromConfig();
        }

        private void InitializeDelegatesFromHandlersManager()
        {
            HandlersManager.StartingProgramsHandler.AddedNewProgram += NewProgramAddedToStartingProgramsHandler;
            HandlersManager.StartingProgramsHandler.ProgramsToStartLoadedSuccesfully += ProgramsToStartLoaded;
            HandlersManager.XMLConfigHandler.NoProgramsToStartFound += NoProgramsToStartFound;
        }

        private void ProgramsToStartLoaded()
        {
            ProgramsToStart.DataCollection = new ObservableCollection<ProgramToStartWrapper>();
            foreach (var program in HandlersManager.StartingProgramsHandler.ProgramsToStart)
            {
                ProgramsToStart.DataCollection.Add(new ProgramToStartWrapper(program.Value, program.Key));
            }
        }

        private void NoProgramsToStartFound()
        {
            MainMessage.Text = "No programs added - add some programs first!";
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
            HandlersManager.StartingProgramsHandler.TryAddProgramToStart(new ProgramToStart("test3", "D://test3.txt"));
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
        }
    }
}
