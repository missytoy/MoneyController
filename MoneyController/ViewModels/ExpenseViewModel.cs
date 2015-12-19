﻿namespace MoneyController.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using Helpers.Models;
    using System.Linq.Expressions;
    using Models;
    public class ExpenseViewModel : ViewModelBase
    {
        private string photo;
        private string place;
        private ObservableCollection<Place> places;

        public ExpenseViewModel()
        {
            this.places = new ObservableCollection<Place>();
            this.places.Add(new Place() { IconLink = string.Empty, Name = "[Empty]" });
            this.places.Add(new Place() { IconLink = string.Empty, Name = "[Empty1]" });
        }

        public static Expression<Func<ExpenseItem, ExpenseViewModel>> FromModel
        {
            get
            {
                return model => new ExpenseViewModel
                {
                    Description=model.Description,
                    CategoryExpenseString = model.CategoryExpense,
                    DateAndTimeOfExpence = model.DateAndTimeOfExpence,
                    Photo = model.Photo,
                    Place = model.Gelocation,
                    Price = model.Price                    
                };
            }
        }

        public decimal Price { get; set; }

        public DateTime DateAndTimeOfExpence { get; set; }

        public string Description { get; set; }

        public Type ExpenseCategoryType { get { return typeof(ExpenseType); } }

        public ExpenseType CategoryExpense { get; set; }

        public string CategoryExpenseString { get; set; }
        public string Place
        {
            get
            {
                return this.place;
            }
            set
            {
                this.place = value;
                this.RaiseProperyChange("Place");
            }
        }

        public ObservableCollection<Place> Places
        {
            get
            {
                return this.places;
            }
            set
            {
                this.places = value;
                this.RaiseProperyChange("Places");
            }
        }
        public string Photo
        {
            get
            {
                return this.photo;
            }

            set
            {
                this.photo = value;
                this.RaiseProperyChange("Photo");
            }
        }
    }
}
