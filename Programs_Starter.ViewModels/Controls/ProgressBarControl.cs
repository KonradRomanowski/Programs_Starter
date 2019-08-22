using Programs_Starter.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.ViewModels.Controls
{
    public class ProgressBarControl : BaseControl
    {
        private int _value;
        public int Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged(nameof(Value)); }
        }        
    }
}
