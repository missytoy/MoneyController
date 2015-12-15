namespace MoneyController
{
    using System;

    public class DateTimeTaker 
    {
        public string Now
        {
            get { return DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"); }
        }
    }
}
