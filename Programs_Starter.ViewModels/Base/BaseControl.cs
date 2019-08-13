using System.Drawing;

namespace Programs_Starter.ViewModels.Base
{
    /// <summary>
    /// Class for the definition of base Control
    /// </summary>
    public abstract class BaseControl : BaseNotificator
    {
        private Color foregroundColor;
        public Color ForegroundColor
        {
            get { return foregroundColor; }
            set { foregroundColor = value; OnPropertyChanged(nameof(ForegroundColor)); }
        }

        private Color backgroundColor;
        public Color BackgroundColor
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
