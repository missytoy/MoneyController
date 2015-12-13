using System;

namespace MoneyController.ViewModels
{
    public class ExpenceViewModel : ViewModelBase
    {
        public decimal Price { get; set; }

        public DateTime DateAndTimeOfExpence { get; set; }

        public string Description { get; set; }

        public ExpenceType ExpenceType { get; set; }

        public string Gelocation { get; set; }

        public string Photo { get; set; }
    }
}
