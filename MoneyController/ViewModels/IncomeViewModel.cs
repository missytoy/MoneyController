namespace MoneyController.ViewModels
{
    using Models;
    using System;
    using System.Linq.Expressions;

    public class IncomeViewModel : ViewModelBase
    {
        private decimal price;
        private string categoryIncomeString;

        public static Expression<Func<IncomeItem, IncomeViewModel>> FromModel
        {
            get
            {
                return model => new IncomeViewModel
                {
                    Description = model.Description,
                    DateOfIncome = model.DateOfIncome,
                    CategoryIncomeString = model.IncomeCategory,                    
                    Price = model.Price
                };
            }
        }

        public decimal Price
        {
            get { return this.price; }
            set
            {
                this.price = value;
                RaisePropertyChange("Price");
            }
        }

        public DateTime DateOfIncome { get; set; }

        public string Description { get; set; }

        public Type IncomeCategoryTypes { get { return typeof(IncomeType); } }

        public IncomeType IncomeCategory { get; set; }

        public string CategoryIncomeString
        {
            get { return this.categoryIncomeString; }
            set
            {
                this.categoryIncomeString = value;
                RaisePropertyChange("CategoryIncomeString");
            }
        }
    }
}
