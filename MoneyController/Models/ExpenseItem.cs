namespace MoneyController.Models
{
    using SQLite.Net.Attributes;
    using System;

    public class ExpenseItem 
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime DateAndTimeOfExpence { get; set; }

        public string Description { get; set; }
        
        public string CategoryExpense { get; set; }

        public string Gelocation { get; set; }

        public string Photo { get; set; }

        public override string ToString()
        {
            return $"Category: {this.CategoryExpense}, Amount: {this.Price}";           
        }
    }
}
