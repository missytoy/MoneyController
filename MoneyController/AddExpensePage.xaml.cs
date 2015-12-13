using MoneyController.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyController
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddExpensePage : Page
    {
        public AddExpensePage()
        {
            this.InitializeComponent();

            this.ViewModel = new ExpenseViewModel();
        }

        public ExpenseViewModel ViewModel
        {
            get { return this.DataContext as ExpenseViewModel; }
            set { this.DataContext = value; }
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            //TODO: Calculate money
            this.Frame.Navigate(typeof(MainPage));
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
