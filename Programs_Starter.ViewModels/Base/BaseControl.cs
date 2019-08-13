using System.Drawing;

namespace Programs_Starter.ViewModels.Base
{
    /// <summary>
    /// Class for the definition of base Control
    /// </summary>
    public abstract class BaseControl : BaseNotificator
    {
        private string foregroundColor;
        public string ForegroundColor
        {
            get { return foregroundColor; }
            set { foregroundColor = value; OnPropertyChanged(nameof(ForegroundColor)); }
        }

        private string backgroundColor;
        public string BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; OnPropertyChanged(nameof(BackgroundColor)); }
        }

        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; OnPropertyChanged(nameof(IsVisible)); }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(nameof(Text)); }
        }
    }
}
