using MoneyController.ViewModels;
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
    public sealed partial class OptionsPage : Page
    {
        public OptionsPage()
        {
            this.InitializeComponent();
            this.ViewModel = new OptionsViewModel();
        }
        
        public OptionsPage(MainPage page)
            :this()
        {
            this.MainPageLink = page;
        }

        public MainPage MainPageLink { get; set; }

        public OptionsViewModel ViewModel
        {
            get { return this.DataContext as OptionsViewModel; }
            set { this.DataContext = value; }
        }
    }
}
