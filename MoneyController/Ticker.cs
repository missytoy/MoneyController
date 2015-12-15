using System;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace MoneyController
{
    public class DateTimeTaker 
    {
        public string Now
        {
            get { return DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"); }
        }
    }
}
