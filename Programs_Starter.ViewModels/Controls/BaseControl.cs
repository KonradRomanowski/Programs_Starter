﻿using Programs_Starter.ViewModels.Base;
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
