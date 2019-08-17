using Programs_Starter.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Programs_Starter.ViewModels.Controls
{
    public class DataGridControl<T> : BaseControl
    {
        public ObservableCollection<T> DataCollection { get; set; }
    }
}
