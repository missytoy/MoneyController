namespace MoneyController.ViewModels
{
    using System.ComponentModel;

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaiseProperyChange(string propertyName)
        {
            if (this.PropertyChanged == null)
            {
                return;
            }

            var propertyChangeEventsArg = new PropertyChangedEventArgs(propertyName);
            this.PropertyChanged(this, propertyChangeEventsArg);
        }
    }
}
