namespace MoneyController.ViewModels
{
    using System.ComponentModel;

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChange(string propertyName)
        {
            if (this.PropertyChanged == null)
            {
                return;
            }
            
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
