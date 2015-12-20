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
    using Extensions;
    public sealed partial class AddExpensePage : Page
    {
        static Geolocator locator = new Geolocator();

        public AddExpensePage()
        {
            this.InitializeComponent();
            this.InitAsync();
            this.ViewModel = new ExpenseViewModel();
        }

        public AddExpensePage(MainPage page)
            :this()
        {
            this.MainPageLink = page;
        }

        public MainPage MainPageLink { get; set; }
        
        public ExpenseViewModel ViewModel
        {
            get { return this.DataContext as ExpenseViewModel; }
            private set { this.DataContext = value; }
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

            var photo = this.ViewModel.Photo;

            var item = new ExpenseItem
            {
                Price = price,
                Description = this.DescriptionTextBox.Text ,
                DateAndTimeOfExpence = this.dataPicker.Date.DateTime,
                CategoryExpense = expenseCategoryText,
                Place = this.PlaceTextBox.Text, //LocationTextBox.Text, //delete location textbox from page and take the gelocation from phone
                Photo = photo == null ? TakeDefaultPhotoDependingOnTheCategory(expenseCategoryText) : photo //TODO: fix the button add photo and put the photo here

            };

            await this.InsertExpenseAsync(item);
            Notification.ShowNotification("New expense added");
            this.MainPageLink.NavigateToMainPage();
        }

        private string TakeDefaultPhotoDependingOnTheCategory(string categoryExpense)
        {
            switch (categoryExpense[0])
            {
                case 'F': return "/Assets/CategoryLetters/fLetter.jpg";
                case 'E': return "/Assets/CategoryLetters/eLetter.jpg";
                case 'H': return "/Assets/CategoryLetters/hLetter.jpg";
                case 'T': return "/Assets/CategoryLetters/tLetter.jpg";
                case 'B': return "/Assets/CategoryLetters/bLetter.jpg";
                case 'L': return "/Assets/CategoryLetters/lLetter.jpg";
                case 'P': return "/Assets/CategoryLetters/pLetter.jpg";
                case 'S': return "/Assets/CategoryLetters/sLetter.jpg";
                case 'R': return "/Assets/CategoryLetters/rLetter.jpg";
                case 'C': return "/Assets/CategoryLetters/cLetter.jpg";
                case 'I': return "/Assets/CategoryLetters/iLetter.jpg";
                case 'G': return "/Assets/CategoryLetters/gLetter.jpg";
                case 'O': return "/Assets/CategoryLetters/oLetter.jpg"; 
                default: return "/Assets/CategoryLetters/oLetter.jpg";
            }
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
                    this.ComboBoxGPS.IsEnabled = false;
                    this.LoadLocationsButton.Visibility = Visibility.Collapsed;
                    this.ComboBoxGPS.Visibility = Visibility.Visible;
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
                        this.ViewModel.Places = placesList;
                        this.ComboBoxGPS.IsEnabled = true;
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
                this.ViewModel.Place = comboBoxText;
            }
        }
    }
}
