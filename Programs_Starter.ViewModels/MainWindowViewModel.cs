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
        public ButtonControl ThankYouButton { get; set; }
        public ButtonControl StartNowButton { get; set; }
        public ButtonControl DontStartButton { get; set; }
        public ButtonControl OptionsButton { get; set; }
        public ButtonControl ShowProgramsListButton { get; set; }
        public ProgressBarControl StatusProgressBar { get; set; }
        
        public MainWindowViewModel()
        {
            MainWindowSettings = new MainWindowSettings();
            MainWindowSettings.Height = 160;

            MainMessage = new TextBlockControl();
            MainMessage.BackgroundColor = ControlsColors.ORANGE;
            MainMessage.ForegroundColor = ControlsColors.BLACK;
            MainMessage.Text = "Test tekstu udany!";
            MainMessage.IsVisible = true;

            CancelButton = new ButtonControl();
            CancelButton.BackgroundColor = ControlsColors.WHITE;
            CancelButton.ForegroundColor = ControlsColors.BLACK;
            CancelButton.Text = "Przycisk Cancel";
            CancelButton.IsVisible = true;
            CancelButton.Command = new RelayCommand(CancelButtonCommand);

            StatusProgressBar = new ProgressBarControl();
            StatusProgressBar.IsVisible = true;
            StatusProgressBar.Value = 0;
            StatusProgressBar.ForegroundColor = ControlsColors.BLACK;
            StatusProgressBar.Text = StatusProgressBar.Value.ToString();

            AboutText = new TextBlockControl();
            AboutText.IsVisible = true;
            AboutText.Text = "v. 0.0.0.1  KR @ 2019";

            ProgramsToStart = new DataGridControl<ProgramToStartWrapper>();
            ProgramsToStart.IsVisible = false;
            ProgramsToStart.DataCollection = new ObservableCollection<ProgramToStartWrapper>()
            {
                new ProgramToStartWrapper(new ProgramToStart("test1", "D://test1.txt")),
                new ProgramToStartWrapper(new ProgramToStart("test2", "D://test2.txt")),
                new ProgramToStartWrapper(new ProgramToStart("test3", "D://test3.txt")),
            };
        }

        private void CancelButtonCommand()
        {            
            MainMessage.Text = "Test przycisku Cancel udany!";
            CancelButton.IsVisible = false;
            MainMessage.ForegroundColor = ControlsColors.RED;

            StatusProgressBar.Value = 75;
            StatusProgressBar.Text = StatusProgressBar.Value.ToString();

            MainWindowSettings.Height = 400;
            ProgramsToStart.IsVisible = true;
        }
    }
}
