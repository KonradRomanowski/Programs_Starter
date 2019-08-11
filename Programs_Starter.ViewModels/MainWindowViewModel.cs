using Programs_Starter.ViewModels.Base;
using Programs_Starter.ViewModels.Controls;
using Programs_Starter.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Programs_Starter.ViewModels
{
    /// <summary>
    /// ViewModel for MainWindow
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        public TextBlockControl MainMessage { get; set; }
        
        public ButtonControl CancelButton { get; set; }

        public MainWindowViewModel()
        {
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
        }

        private void CancelButtonCommand()
        {            
            MainMessage.Text = "Test przycisku Cancel udany!";
            CancelButton.IsVisible = false;
            MainMessage.ForegroundColor = Color.Red;
        }
    }
}
