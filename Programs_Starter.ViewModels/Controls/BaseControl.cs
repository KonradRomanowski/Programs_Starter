using Programs_Starter.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Programs_Starter.ViewModels.Controls
{
    /// <summary>
    /// Class for the definition of base Control
    /// </summary>
    public abstract class BaseControl : BaseViewModel
    {
        public Color ForegroundColor { get; set; }
        public Color BackgroundColor { get; set; }
        public bool Visibility { get; set; }

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(nameof(Text)); }
        }
    }
}
