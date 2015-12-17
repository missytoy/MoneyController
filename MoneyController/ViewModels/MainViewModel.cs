using MoneyController.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyController.ViewModels
{
   public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ExpenseItem> expenses;

        public IEnumerable<ExpenseItem> Expences
        {
            get
            {
                if (expenses == null)
                {
                    expenses = new ObservableCollection<ExpenseItem>();
                }

                return expenses;
            }
            set
            {
                if (expenses == null)
                {
                    expenses = new ObservableCollection<ExpenseItem>();
                }

                expenses.Clear();
                foreach (var item in value)
                {
                    expenses.Add(item);
                }
            }
        }
    }
}
