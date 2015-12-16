﻿namespace MoneyController.Models
{
    using SQLite.Net.Attributes;
    using System;

    public class IncomeItem
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime DateOfIncome { get; set; }

        public string Description { get; set; }
        
        public string IncomeCategory { get; set; }
        
        public override string ToString()
        {
            if (this.Description=="")
            {
                return $"Category: {this.IncomeCategory}, Amount: {this.Price}";
            }
            else
            {
                return $"{this.Description}, Category: {this.IncomeCategory}, Amount: {this.Price}";
            }
        }
    }
}
