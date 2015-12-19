using MoneyController.Models;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class AnalyticsPage : Page
    {
        public AnalyticsPage()
        {
            this.InitializeComponent();
        }

        private async void OnShowAllExpensesButtonClick(object sender, RoutedEventArgs e)
        {
            this.InitAsyncExpense();
            var expenseData = await this.GetAllExpensesAsync();
            var expenseDataAsString = new StringBuilder();
            foreach (var expenseItem in expenseData)
            {
                if (expenseItem.Description=="")
                {
                    expenseDataAsString.AppendLine($"Category: {expenseItem.CategoryExpense}, Amount: {expenseItem.Price}");
                }
                else
                {
                    expenseDataAsString.AppendLine($"Description: {expenseItem.Description}, Category: {expenseItem.CategoryExpense}, Amount: {expenseItem.Price}");
                }
            }

            this.resultTry.Text = expenseDataAsString.ToString();
            this.scrollViewer.Visibility = Visibility.Visible;

        }

        private async void OnAnalyticsExpensesButtonClick(object sender, RoutedEventArgs e)
        {
            this.InitAsyncExpense();
            var expenseData = await this.AnalyticsExpensesAsync();
            var expenseDataAsString = new StringBuilder();
            foreach (var expenseItem in expenseData)
            {
                expenseDataAsString.AppendLine($"Category: {expenseItem.CategoryExpense}, Amount: {expenseItem.Price}");
            }

            this.resultTry.Text = expenseDataAsString.ToString();
            this.scrollViewer.Visibility = Visibility.Visible;

        }

        private async void OnShowAllIncomesButtonClick(object sender, RoutedEventArgs e)
        {
            this.InitAsyncIncome();
            var incomeData = await this.GetAllIncomesAsync();
            var incomeDataAsString = new StringBuilder();
            
            foreach (var incomeItem in incomeData)
            {
                if (incomeItem.Description == "")
                {
                    incomeDataAsString.AppendLine($"Category: {incomeItem.IncomeCategory}, Amount: {incomeItem.Price}");
                }
                else
                {
                    incomeDataAsString.AppendLine($"Description: {incomeItem.Description}, Category: {incomeItem.IncomeCategory}, Amount: {incomeItem.Price}");
                }
            }


            this.resultTry.Text = incomeDataAsString.ToString();
            this.scrollViewer.Visibility = Visibility.Visible;
        }

        private async void OnAnalyticsIncomesButtonClick(object sender, RoutedEventArgs e)
        {
            this.InitAsyncIncome();
            var incomeData = await this.AnalyticsIncomesAsync();
            var incomeDataAsString = new StringBuilder();

            foreach (var incomeItem in incomeData)
            {
                incomeDataAsString.AppendLine($"Category: {incomeItem.IncomeCategory}, Amount: {incomeItem.Price}");
            }


            this.resultTry.Text = incomeDataAsString.ToString();
            this.scrollViewer.Visibility = Visibility.Visible;
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private SQLiteAsyncConnection GetDbConnectionAsync()
        {
            var dbFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            var connectionFactory =
                new Func<SQLiteConnectionWithLock>(
                    () =>
                    new SQLiteConnectionWithLock(
                        new SQLitePlatformWinRT(),
                        new SQLiteConnectionString(dbFilePath, storeDateTimeAsTicks: false)));

            var asyncConnection = new SQLiteAsyncConnection(connectionFactory);

            return asyncConnection;
        }


        private async void InitAsyncExpense()
        {
            var connection = this.GetDbConnectionAsync();
            await connection.CreateTableAsync<ExpenseItem>();
        }

        private async void InitAsyncIncome()
        {
            var connection = this.GetDbConnectionAsync();
            await connection.CreateTableAsync<IncomeItem>();
        }


        private async Task<List<ExpenseItem>> AnalyticsExpensesAsync()
        {
            var connection = this.GetDbConnectionAsync();
            var result = await connection.Table<ExpenseItem>().ToListAsync();

            result = result.GroupBy(ei => ei.CategoryExpense)
                .Select(item => new ExpenseItem
                {
                    Price = item.Sum(i => i.Price),
                    CategoryExpense = item.First().CategoryExpense,
                })
                .OrderByDescending(ei => ei.Price)
                .ToList();
            return result;
        }

        private async Task<List<ExpenseItem>> GetAllExpensesAsync()
        {
            var connection = this.GetDbConnectionAsync();
            var result = await connection.Table<ExpenseItem>()
            .OrderByDescending(x => x.DateAndTimeOfExpence)
            .ToListAsync();
            return result;
        }

        private async Task<List<IncomeItem>> AnalyticsIncomesAsync()
        {
            var connection = this.GetDbConnectionAsync();
            var result = await connection.Table<IncomeItem>().ToListAsync();
            result = result.GroupBy(ii => ii.IncomeCategory)
                 .Select(item => new IncomeItem
                 {
                     Price = item.Sum(i => i.Price),
                     IncomeCategory = item.First().IncomeCategory
                 })
                 .OrderByDescending(ei => ei.Price)
                 .ToList();
            return result;
        }

        private async Task<List<IncomeItem>> GetAllIncomesAsync()
        {
            var connection = this.GetDbConnectionAsync();
            var result = await connection.Table<IncomeItem>()
                  .OrderByDescending(x => x.DateOfIncome)
                  .ToListAsync();
            return result;
        }

    }
}
