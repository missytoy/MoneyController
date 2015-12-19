namespace MoneyController.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using Helpers.Models;

    public class ExpenseViewModel : ViewModelBase
    {
        public decimal Price { get; set; }

        public DateTime DateAndTimeOfExpence { get; set; }

        public string Description { get; set; }

        public Type ExpenseCategoryType { get { return typeof(ExpenseType); } }

        public ExpenseType CategoryExpense { get; set; }

        public string Place { get; set; }

        public ObservableCollection<Place> Places { get; set; }

        public string Photo { get; set; }
    }
}
