using Programs_Starter.ViewModels.Helpers;
using System;
using System.ComponentModel;

namespace Programs_Starter.ViewModels.Base
{
    /// <summary>
    /// Base class implementing INotifyPropertyChanged inteface
    /// </summary>
    public abstract class BaseNotificator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (propertyName == null)
                throw new ArgumentNullException("propertyExpression");

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected BaseNotificator()
        {
            SimpleCommandManager.AssignOnPropertyChanged(ref this.PropertyChanged);
        }

    }
}
