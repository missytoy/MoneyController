// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyController
{
    using System;
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
    using Windows.Devices.Geolocation;
    using Helpers;
    using Windows.Media.Capture;
    using Helpers.Models;
    public sealed partial class AddExpensePage : Page
    {
        static Geolocator locator = new Geolocator();

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
            decimal price = 0;
            decimal.TryParse(this.AmountTextBox.Text, out price);
            if (price <= 0)
            {
                Notification.ShowNotification("Amount cannot be less than zero or equal to zero");
                return;
            } 
            var expenseCategoryText = ComboBoxExpense.SelectedValue == null ? "Other" : ComboBoxExpense.SelectedValue.ToString();

            //TODO:
            var photo = this.ViewModel.Photo;

            var item = new ExpenseItem
            {
                Price = price,
                Description = this.DescriptionTextBox.Text,
                DateAndTimeOfExpence = this.dataPicker.Date.DateTime,
                CategoryExpense = expenseCategoryText,
                //Gelocation = currentLocation, //LocationTextBox.Text, //delete location textbox from page and take the gelocation from phone
                Photo = photo == null ? TakeDefaultPhotoDependingOnTheCategory(expenseCategoryText) : photo //TODO: fix the button add photo and put the photo here

            };

            await this.InsertExpenseAsync(item);
            Notification.ShowNotification("New expense added");
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

        private void OnAddPhotoButtonClick(object sender, RoutedEventArgs e)
        {
            this.InitCamera();
        }

        private async void InitCamera()
        {
            var camera = new CameraCaptureUI();

            var photo = await camera.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (photo != null)
            {
                this.ViewModel.Photo = photo.Path;
            }
        }

        private async void LoadLocationsButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.ComboBoxGPS.Visibility == Visibility.Collapsed)
            {
                var accessStatus = await Geolocator.RequestAccessAsync();

                var currentLocation = string.Empty;
                var latitude = string.Empty;
                var longitude = string.Empty;
                var accuracy = string.Empty;

                if (accessStatus != GeolocationAccessStatus.Allowed)
                {
                    Notification.ShowNotification("Problem with location permissions or access");
                }
                else
                {
                    this.ComboBoxGPS.PlaceholderText = "Please Wait: Loading places from GPS..";
                    Geoposition geoposition = await locator.GetGeopositionAsync();
                    latitude = geoposition.Coordinate.Point.Position.Latitude.ToString();
                    longitude = geoposition.Coordinate.Point.Position.Longitude.ToString();
                    accuracy = geoposition.Coordinate.Accuracy.ToString();


                    var placesList = await new GoogleApiGPSHelper().GetPlaces(latitude, longitude, accuracy);

                    if (placesList.Count >= 1)
                    {
                        this.ComboBoxGPS.PlaceholderText = "Loading done: Choose place!";
                        this.ComboBoxGPS.Background = PlacesStackPanel.Background;
                        ViewModel.Places = placesList;
                        this.ComboBoxGPS.Visibility = Visibility.Visible;
                        this.LoadLocationsButton.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void ComboBoxGPS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (sender as ComboBox);
            var comboBoxText = (comboBox.SelectedValue as Place).Name;
            if (!string.IsNullOrEmpty(comboBoxText))
            {
                ViewModel.Place = comboBoxText;
            }
        }
    }
}
