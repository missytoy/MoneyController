using System;

namespace MoneyController.ViewModels
{
    public class ExpenceViewModel : ViewModelBase
    {
        public decimal Price { get; set; }

        public DateTime DateAndTimeOfExpence { get; set; }

        public string Description { get; set; }

        public Type ExpenseCategoryType { get { return typeof(ExpenceType); } }

        public ExpenceType CategoryExpence { get; set; }

        public string Gelocation { get; set; }

        public string Photo { get; set; }
    }
}
