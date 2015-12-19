namespace MoneyController.ViewModels
{
    using MoneyController.Extensions;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class IncomeContentViewModel : ViewModelBase
    {
        private ObservableCollection<IncomeViewModel> incomeModel;

        public IEnumerable<IncomeViewModel> IncomeModels
        {
            get
            {
                if (this.incomeModel == null)
                {
                    this.incomeModel = new ObservableCollection<IncomeViewModel>();
                }

                return this.incomeModel;
            }
            set
            {
                if (this.incomeModel == null)
                {
                    this.incomeModel = new ObservableCollection<IncomeViewModel>();
                }

                this.incomeModel.Clear();

                value.ForEach(this.incomeModel.Add);
            }
        }
    }
}