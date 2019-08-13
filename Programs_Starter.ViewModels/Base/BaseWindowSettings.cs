namespace Programs_Starter.ViewModels.Base
{
    /// <summary>
    /// Base class for windows
    /// </summary>
    public abstract class BaseWindowSettings : BaseNotificator
    {
        private int height;
        public int Height
        {
            get { return height; }
            set { height = value; OnPropertyChanged(nameof(Height)); }
        }
    }
}
