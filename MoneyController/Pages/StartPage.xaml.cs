using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyController
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {
        public StartPage()
        {
            this.InitializeComponent();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            var page = new MainPage();
            StartPageMainFrame.Content = page;
        }

        private void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            var page = new AddExpensePage();
            StartPageMainFrame.Content = page;
        }

        private void AddIncome_Click(object sender, RoutedEventArgs e)
        {
            var page = new AddIncomePage();
            StartPageMainFrame.Content = page;
        }

        private void Analytics_Click(object sender, RoutedEventArgs e)
        {
            var page = new AnalyticsPage();
            StartPageMainFrame.Content = page;
        }
    }
}
