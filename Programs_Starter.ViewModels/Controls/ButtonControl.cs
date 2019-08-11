using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Programs_Starter.ViewModels.Controls
{
    public class ButtonControl : BaseControl
    {
        public ICommand Command { get; set; }
    }
}
