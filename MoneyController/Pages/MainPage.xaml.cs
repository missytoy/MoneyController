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
        private object MainPagePage;
        private AddExpensePage AddExpensePagePage;
        private AddIncomePage AddIncomePagePage;
        private AnalyticsPage AnalyticsPagePage;
        private OptionsPage OptionsPagePage;
        private Point initialpoint;
        string currentPage;

        public MainPage()
        {
            this.InitializeComponent();
            MainPagePage = MainPageFrame.Content;
            this.currentPage = "MainPage";
            this.AddExpensePagePage = new AddExpensePage(this);
            this.AddIncomePagePage = new AddIncomePage(this);
            this.AnalyticsPagePage = new AnalyticsPage(this);
            this.OptionsPagePage = new OptionsPage(this);

        }

        private void MainPage_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            this.ManipulationStarted -= MainPage_ManipulationStarted;
            this.ManipulationDelta -= MainPage_ManipulationDelta;
            this.ManipulationCompleted -= MainPage_ManipulationCompleted;
        }

        void MainPage_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            initialpoint = e.Position;
        }

        void MainPage_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (e.IsInertial)
            {
                Point currentpoint = e.Position;

                if (currentpoint.X - initialpoint.X >= 300)
                {
                    System.Diagnostics.Debug.WriteLine("Swipe Right");

                    if (currentPage == "MainPage")
                    {
                        OnAddIncomeButtonClick(null, null);
                    }
                    else if (currentPage == "AddExpensePage")
                    {
                        NavigateToMainPage();
                    }
                }
                if (currentpoint.X - initialpoint.X <= -300)
                {
                    System.Diagnostics.Debug.WriteLine("Swipe Left");

                    if (currentPage == "MainPage")
                    {
                        OnAddExpenseButtonClick(null, null);
                    }
                    else if (currentPage == "AddIncomePage")
                    {
                        NavigateToMainPage();
                    }
                }
                e.Complete();
                System.Diagnostics.Debug.WriteLine(currentPage);
            }

        }

        public void NavigateToMainPage()
        {
            currentPage = "MainPage";
            MainPageFrame.Content = MainPagePage;
        }

        private void OnAddIncomeButtonClick(object sender, RoutedEventArgs e)
        {
            this.currentPage = "AddIncomePage";
            MainPageFrame.Content = this.AddIncomePagePage;
        }

        private void OnAddExpenseButtonClick(object sender, RoutedEventArgs e)
        {
            this.currentPage = "AddExpensePage";
            MainPageFrame.Content = this.AddExpensePagePage;
        }

        private void OnAnalyticsButtonClick(object sender, RoutedEventArgs e)
        {
            this.currentPage = "AnalyticsPage";
            MainPageFrame.Content = this.AnalyticsPagePage; //TODO:  //charts for day,for month, for year...
        }

        private void OnOptionsButtonClick(object sender, RoutedEventArgs e)
        {
            this.currentPage = "OptionsPage";
            MainPageFrame.Content = this.OptionsPagePage; //stop notifications
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigateToMainPage();
            this.TopAppBar.IsOpen = !this.TopAppBar.IsOpen;
        }

        private void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            OnAddExpenseButtonClick(null, null);
            this.TopAppBar.IsOpen = !this.TopAppBar.IsOpen;
        }

        private void AddIncome_Click(object sender, RoutedEventArgs e)
        {
            OnAddIncomeButtonClick(null, null);
            this.TopAppBar.IsOpen = !this.TopAppBar.IsOpen;
        }

        private void Analytics_Click(object sender, RoutedEventArgs e)
        {
            OnAnalyticsButtonClick(null, null);
            this.TopAppBar.IsOpen = !this.TopAppBar.IsOpen;
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            OnOptionsButtonClick(null, null);
            this.TopAppBar.IsOpen = !this.TopAppBar.IsOpen;
        }
    }
}
