namespace MoneyController.ViewModels
{
    using System;

    public class IncomeViewModel : ViewModelBase
    {
        public decimal Price { get; set; }

        public DateTime DateOfIncome { get; set; }

        public string Description { get; set; }

        public Type IncomeCategoryTypes { get { return typeof(IncomeType); } }

        public IncomeType IncomeCategory { get; set; }
    }
}
