namespace MoneyController.ViewModels
{
    using System;

    public class IncomeViewModel : ViewModelBase
    {
        public decimal Price { get; set; }

        public DateTime DateOfIncome { get; set; }

        public string Description { get; set; }

        public IncomeType IncomeType { get; set; }
    }
}
