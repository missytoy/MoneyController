namespace MoneyController
{
    using MoneyController.Models;
    using MoneyController.ViewModels;
    using SQLite.Net;
    using SQLite.Net.Async;
    using SQLite.Net.Platform.WinRT;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media.Imaging;

    public sealed partial class AnalyticsPage : Page
    {
        public AnalyticsPage()
        {
            this.InitializeComponent();
        }

        public AnalyticsPage(MainPage page)
            :this()
        {
            this.MainPageLink = page;
        }

        public MainPage MainPageLink { get; set; }

        private async void OnShowAllExpensesButtonClick(object sender, RoutedEventArgs e)
        {
            this.DataContext = new ExpensesContentViewModel();
            this.InitAsyncExpense();
            var expenseData = await this.GetAllExpensesAsync();
            (this.DataContext as ExpensesContentViewModel).ExpensesModel = expenseData.AsQueryable()
                                                                                      .Select(ExpenseViewModel.FromModel);

            this.scrollViewer.Visibility = Visibility.Visible;
            this.scrollViewerIncomes.Visibility = Visibility.Collapsed;
            this.incomeDetailsInformation.Visibility = Visibility.Collapsed;
            this.scrollViewerIncomeDetails.Visibility = Visibility.Collapsed;
            this.expenseDetailsInformation.Visibility = Visibility.Collapsed;

        }

        private async void OnAnalyticsExpensesButtonClick(object sender, RoutedEventArgs e)
        {
            this.DataContext = new ExpensesContentViewModel();
            this.InitAsyncExpense();
            var expenseData = await this.AnalyticsExpensesAsync();
            
            (this.DataContext as ExpensesContentViewModel).ExpensesModel = expenseData.AsQueryable()
                                .Select(ExpenseViewModel.FromModel);

            this.scrollViewerIncomeDetails.Visibility = Visibility.Visible;
            this.scrollViewer.Visibility = Visibility.Collapsed;
            this.scrollViewerIncomes.Visibility = Visibility.Collapsed;
            this.incomeDetailsInformation.Visibility = Visibility.Collapsed;
            this.expenseDetailsInformation.Visibility = Visibility.Collapsed;

        }

        private async void OnShowAllIncomesButtonClick(object sender, RoutedEventArgs e)
        {
            this.DataContext = new IncomeContentViewModel();
            this.InitAsyncIncome();
            var incomeData = await this.GetAllIncomesAsync();

            (this.DataContext as IncomeContentViewModel).IncomeModels = incomeData.AsQueryable()
                                                                                     .Select(IncomeViewModel.FromModel);

            this.scrollViewerIncomes.Visibility = Visibility.Visible;
            this.scrollViewer.Visibility = Visibility.Collapsed;
            this.scrollViewerIncomeDetails.Visibility = Visibility.Collapsed;
            this.incomeDetailsInformation.Visibility = Visibility.Collapsed;
            this.expenseDetailsInformation.Visibility = Visibility.Collapsed;

        }

        private async void OnAnalyticsIncomesButtonClick(object sender, RoutedEventArgs e)
        {
            this.DataContext = new IncomeContentViewModel();
            this.InitAsyncIncome();
            var incomeData = await this.AnalyticsIncomesAsync();
            
            (this.DataContext as IncomeContentViewModel).IncomeModels = incomeData.AsQueryable()
                             .Select(IncomeViewModel.FromModel);

            this.scrollViewer.Visibility = Visibility.Collapsed;
            this.scrollViewerIncomes.Visibility = Visibility.Visible;
            this.incomeDetailsInformation.Visibility = Visibility.Collapsed;
            this.scrollViewerIncomeDetails.Visibility = Visibility.Collapsed;
            this.expenseDetailsInformation.Visibility = Visibility.Collapsed;
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.MainPageLink.NavigateToMainPage();
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
        
        private void DoubleTappedOnListBox(object sender, DoubleTappedRoutedEventArgs e)
        {
            var item = (sender as ListBox).SelectedItem as IncomeViewModel;
            this.priceDetailIncome.Text = $"Price: { item.Price}"; 
            this.dateDetailIncome.Text = $"Date: {  item.DateOfIncome} ";
            this.descriptionDetailIncome.Text = item.Description.ToString();
            this.categoryDetailIncome.Text = $"Category: { item.CategoryIncomeString}";

            this.incomeDetailsInformation.Visibility = Visibility.Visible;
            this.expenseDetailsInformation.Visibility = Visibility.Collapsed;
            this.scrollViewerIncomeDetails.Visibility = Visibility.Collapsed;
            this.scrollViewer.Visibility = Visibility.Collapsed;
            this.scrollViewerIncomes.Visibility = Visibility.Collapsed;
        }

        private void DoubleTappedOnListBoxExpense(object sender, DoubleTappedRoutedEventArgs e)
        {

            var item = (sender as ListBox).SelectedItem as ExpenseViewModel;
            this.priceExpenseDetailInforamtion.Text = $"Price: { item.Price}";
            this.dateTimeExpenseDetailInforamtion.Text = $"Date: {  item.DateAndTimeOfExpence} ";
            this.descriptionExpenseDetailInforamtion.Text = item.Description.ToString();
            this.categoryExpenseDetailInforamtion.Text = $"Category: { item.CategoryExpenseString}";

            if (item.Photo.StartsWith("/Assets"))
            {
                this.imageSourceExpenseDetailInforamtion.Source = new BitmapImage(new Uri("ms-appx://" + item.Photo,UriKind.Absolute));
            }
            else
            {
                this.imageSourceExpenseDetailInforamtion.Source = new BitmapImage(new Uri(item.Photo));
            }

            this.expenseDetailsInformation.Visibility = Visibility.Visible;
            this.incomeDetailsInformation.Visibility = Visibility.Collapsed;
            this.scrollViewer.Visibility = Visibility.Collapsed;
            this.scrollViewerIncomeDetails.Visibility = Visibility.Collapsed;
            this.scrollViewerIncomes.Visibility = Visibility.Collapsed;
        }
    }
}
