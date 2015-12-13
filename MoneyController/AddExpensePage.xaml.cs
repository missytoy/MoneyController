﻿using MoneyController.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyController
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddExpensePage : Page
    {
        public AddExpensePage()
        {
            this.InitializeComponent();

            this.ViewModel = new ExpenceViewModel();
        }

        public ExpenceViewModel ViewModel
        {
            get { return this.DataContext as ExpenceViewModel; }
            set { this.DataContext = value; }
        }
    }
}
