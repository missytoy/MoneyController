using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NotificationsExtensions.Tiles;
using MoneyController.Helpers;

namespace MoneyController
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnAddIncomeButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddIncomePage));
        }

        private void OnAddExpenseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddExpensePage));
        }

        private void OnAnalyticsButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AnalyticsPage)); //TODO:  //charts for day,for month, for year...
        }

        private void OnOptionsButtonClick(object sender, RoutedEventArgs e)
        {
            //TODO: this.Frame.Navigate(typeof(Options)); //stop notifications
        }
        
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddExpensePage));
        }

        private void AddIncome_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddIncomePage));
        }

        private void Analytics_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AnalyticsPage));
        }

        
    }
}
