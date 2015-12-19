namespace MoneyController.ViewModels
{
    using MoneyController.Extensions;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class ExpensesContentViewModel : ViewModelBase
    {
        public ObservableCollection<ExpenseViewModel> expensesModel;

        public IEnumerable<ExpenseViewModel> ExpensesModel
        {
            get
            {
                if (this.expensesModel == null)
                {
                    this.expensesModel = new ObservableCollection<ExpenseViewModel>();
                }

                return this.expensesModel;
            }
            set
            {
                if (this.expensesModel == null)
                {
                    this.expensesModel = new ObservableCollection<ExpenseViewModel>();
                }

                this.expensesModel.Clear();

                value.ForEach(this.expensesModel.Add);
            }
        }
    }
}