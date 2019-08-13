using Programs_Starter.ViewModels.Base;
using Programs_Starter.ViewModels.Controls;
using Programs_Starter.ViewModels.Helpers;
using Programs_Starter.ViewModels.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public TextBlockControl MainMessage { get; set; }        
        public ButtonControl CancelButton { get; set; }
        public ProgressBarControl StatusProgressBar { get; set; }

        public MainWindowViewModel()
        {
            MainWindowSettings = new MainWindowSettings();
            MainWindowSettings.Height = 200;

            MainMessage = new TextBlockControl();
            MainMessage.BackgroundColor = Color.Gray;
            MainMessage.ForegroundColor = Color.Red;
            MainMessage.Text = "Test tekstu udany!";
            MainMessage.IsVisible = true;

            CancelButton = new ButtonControl();
            CancelButton.BackgroundColor = Color.Orange;
            CancelButton.ForegroundColor = Color.Green;
            CancelButton.Text = "Przycisk Cancel";
            CancelButton.IsVisible = true;
            CancelButton.Command = new RelayCommand(CancelButtonCommand);

            StatusProgressBar = new ProgressBarControl();
            StatusProgressBar.IsVisible = true;
            StatusProgressBar.Value = 0;
            StatusProgressBar.Text = StatusProgressBar.Value.ToString();
        }

        private void CancelButtonCommand()
        {            
            MainMessage.Text = "Test przycisku Cancel udany!";
            CancelButton.IsVisible = false;
            MainMessage.ForegroundColor = Color.Red;

            StatusProgressBar.Value = 75;
            StatusProgressBar.Text = StatusProgressBar.Value.ToString();

            MainWindowSettings.Height = 400;
        }
    }
}
