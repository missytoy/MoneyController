namespace MoneyController.ViewModels
{
    using System;

    public class ExpenseViewModel : ViewModelBase
    {
        public decimal Price { get; set; }

        public DateTime DateAndTimeOfExpence { get; set; }

        public string Description { get; set; }

        public Type ExpenseCategoryType { get { return typeof(ExpenseType); } }

        public ExpenseType CategoryExpense { get; set; }

        public string Place { get; set; }

        public string Photo { get; set; }
    }
}
