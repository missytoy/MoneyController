namespace MoneyController.ViewModels
{
    using Models;
    using System;
    using System.Linq.Expressions;

    public class IncomeViewModel : ViewModelBase
    {
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

        public decimal Price { get; set; }

        public DateTime DateOfIncome { get; set; }

        public string Description { get; set; }

        public Type IncomeCategoryTypes { get { return typeof(IncomeType); } }

        public IncomeType IncomeCategory { get; set; }

        public string CategoryIncomeString { get; set; }
    }
}
