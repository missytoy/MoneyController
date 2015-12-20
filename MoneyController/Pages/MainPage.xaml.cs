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
        public object MainPagePage;
        public AddExpensePage AddExpensePagePage;
        public AddIncomePage AddIncomePagePage;
        public AnalyticsPage AnalyticsPagePage;
        //public Page OptionsPagePage;
        

        public MainPage()
        {
            this.InitializeComponent();
            MainPagePage = MainPageFrame.Content;
            this.AddExpensePagePage = new AddExpensePage(this);
            this.AddIncomePagePage = new AddIncomePage(this);
            this.AnalyticsPagePage = new AnalyticsPage(this);
            //this.OptionsPagePage = new OptionsPage(this);
        }

        public void NavigateToMainPage()
        {
            MainPageFrame.Content = MainPagePage;
        }

        private void OnAddIncomeButtonClick(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = this.AddIncomePagePage;
        }

        private void OnAddExpenseButtonClick(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = this.AddExpensePagePage;
        }

        private void OnAnalyticsButtonClick(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = this.AnalyticsPagePage; //TODO:  //charts for day,for month, for year...
        }

        private void OnOptionsButtonClick(object sender, RoutedEventArgs e)
        {
            //TODO: MainPageFrame.Content = this.OptionsPage; //stop notifications
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigateToMainPage();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(MainPage));
        }

        private void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = this.AddExpensePagePage;
        }

        private void AddIncome_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = this.AddIncomePagePage;
        }

        private void Analytics_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = this.AnalyticsPagePage;
        }


    }
}
