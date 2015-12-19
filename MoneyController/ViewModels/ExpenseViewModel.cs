namespace MoneyController.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using Helpers.Models;
    using System.Diagnostics;
    public class ExpenseViewModel : ViewModelBase
    {
        private string photo;
        private string place;
        private ObservableCollection<Place> places;

        public ExpenseViewModel()
        {
            //var localPlaces = new ObservableCollection<Place>();
            //localPlaces.Add(new Place() { IconLink = string.Empty, Name = "[Empty]" });
            //localPlaces.Add(new Place() { IconLink = string.Empty, Name = "[Empty1]" });
            //this.Places = localPlaces;
        }

        public decimal Price { get; set; }

        public DateTime DateAndTimeOfExpence { get; set; }

        public string Description { get; set; }

        public Type ExpenseCategoryType { get { return typeof(ExpenseType); } }

        public ExpenseType CategoryExpense { get; set; }

        public string Place
        {
            get
            {
                return this.place;
            }
            set
            {
                this.place = value;
                this.RaisePropertyChange("Place");
            }
        }

        public ObservableCollection<Place> Places
        {
            get
            {
                if (this.places == null)
                {
                    this.places = new ObservableCollection<Place>();
                }

                return this.places;
            }
            set
            {
                if (this.places == null)
                {
                    this.places = new ObservableCollection<Place>();
                }

                this.places.Clear();
                foreach (var place in value)
                {
                    this.places.Add(place);
                }
                this.RaisePropertyChange("Places");
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
                this.RaisePropertyChange("Photo");
            }
        }
    }
}
