﻿// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyController
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Models;
    using MoneyController.ViewModels;

    using SQLite.Net;
    using SQLite.Net.Async;
    using SQLite.Net.Platform.WinRT;

    using Windows.Storage;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using System.Text;
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddExpensePage : Page
    {
        public AddExpensePage()
        {
            this.InitializeComponent();
            this.InitAsync();
            this.ViewModel = new ExpenseViewModel();
        }

        public ExpenseViewModel ViewModel
        {
            get { return this.DataContext as ExpenseViewModel; }
            set { this.DataContext = value; }
        }

        private async void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            var price = 0;
            int.TryParse(this.AmountTextBox.Text, out price);
            
            var expenseCategoryText = ComboBoxExpense.SelectedValue == null ? "Other" : ComboBoxExpense.SelectedValue.ToString();


            var item = new ExpenseItem
            {
                Price = price,
                Description = this.DescriptionTextBox.Text,
                DateAndTimeOfExpence = DateTime.Now,
                CategoryExpense = expenseCategoryText,
                Gelocation = LocationTextBox.Text, //delete location textbox from page and take the gelocation from phone
                Photo = TakeDefaultPhotoDependingOnTheCategory(expenseCategoryText) //TODO: fix the button add photo and put the photo here

            };

            await this.InsertExpenseAsync(item);

            this.Frame.Navigate(typeof(MainPage));
        }

        private string TakeDefaultPhotoDependingOnTheCategory(string categoryExpense)
        {
            switch (categoryExpense[0])
            {
                case 'F': return "/Assets/CategoryLetters/fLetter";
                case 'E': return "/Assets/CategoryLetters/eLetter";
                case 'H': return "/Assets/CategoryLetters/hLetter";
                case 'T': return "/Assets/CategoryLetters/tLetter";
                case 'B': return "/Assets/CategoryLetters/bLetter";
                case 'L': return "/Assets/CategoryLetters/lLetter";
                case 'P': return "/Assets/CategoryLetters/pLetter";
                case 'S': return "/Assets/CategoryLetters/sLetter";
                case 'R': return "/Assets/CategoryLetters/rLetter";
                case 'C': return "/Assets/CategoryLetters/cLetter";
                case 'I': return "/Assets/CategoryLetters/iLetter";
                case 'O': return "/Assets/CategoryLetters/oLetter";
                default: return "/Assets/CategoryLetters/oLetter";
            }
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
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
            await connection.CreateTableAsync<ExpenseItem>();
        }

        private async Task<int> InsertExpenseAsync(ExpenseItem item)
        {
            var connection = this.GetDbConnectionAsync();
            var result = await connection.InsertAsync(item);
            return result;
        }
    }
}
