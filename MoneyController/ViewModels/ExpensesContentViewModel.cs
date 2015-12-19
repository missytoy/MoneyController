namespace MoneyController.ViewModels
{
    using MoneyController.Extensions;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class ExpensesContentViewModel : ViewModelBase
    {
        public ObservableCollection<ExpenseViewModel> expenses;

        public IEnumerable<ExpenseViewModel> Expenses
        {
            get
            {
                if (this.expenses == null)
                {
                    this.expenses = new ObservableCollection<ExpenseViewModel>();
                }

                return this.expenses;
            }
            set
            {
                if (this.expenses == null)
                {
                    this.expenses = new ObservableCollection<ExpenseViewModel>();
                }

                this.expenses.Clear();

                value.ForEach(this.expenses.Add);
            }
        }
    }
}