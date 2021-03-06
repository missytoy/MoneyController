﻿using MoneyController.Helpers;
using MoneyController.Models;
using MoneyController.ViewModels;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace MoneyController
{
    public sealed partial class AddIncomePage : Page
    {
        public AddIncomePage()
        {
            this.InitializeComponent();
            this.InitAsync();
            this.ViewModel = new IncomeViewModel();
        }

        public AddIncomePage(MainPage page)
            :this()
        {
            this.MainPageLink = page;
        }

        public MainPage MainPageLink { get; set; }

        public IncomeViewModel ViewModel
        {
            get { return this.DataContext as IncomeViewModel; }
            set { this.DataContext = value; }
        }

        private async void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            decimal price = 0;
            decimal.TryParse(this.AmountIncomeTextBox.Text, out price);
            if (price <= 0)
            {
                Notification.ShowNotification("Amount cannot be less than zero or equal to zero");
                return;
            }

            var incomeCategoryText = ComboBoxIncome.SelectedValue == null ? "Other" : ComboBoxIncome.SelectedValue.ToString();

            var item = new IncomeItem
            {
                Price = price,
                Description = this.DescriptionIncomeTextBox.Text ,
                DateOfIncome = this.dataPicker.Date.DateTime,
                IncomeCategory = incomeCategoryText
            };

            await this.InsertIncomeAsync(item);
            Notification.ShowNotification("New income added");
            this.MainPageLink.AddIncomePagePage = new AddIncomePage(this.MainPageLink);
            this.MainPageLink.NavigateToMainPage();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.MainPageLink.AddIncomePagePage = new AddIncomePage(this.MainPageLink);
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

        private async void InitAsync()
        {
            var connection = this.GetDbConnectionAsync();
            await connection.CreateTableAsync<IncomeItem>();
        }

        private async Task<int> InsertIncomeAsync(IncomeItem item)
        {
            var connection = this.GetDbConnectionAsync();
            var result = await connection.InsertAsync(item);
            return result;
        }

        private async Task<List<IncomeItem>> GetAllIncomesAsync()
        {
            var connection = this.GetDbConnectionAsync();
            var result = await connection.Table<IncomeItem>().ToListAsync();
            return result;
        }
    }
}
