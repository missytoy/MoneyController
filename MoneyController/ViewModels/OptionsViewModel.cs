namespace MoneyController.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class OptionsViewModel : ViewModelBase
    {
        private bool forceLocation;
        private string forcedLatitude;
        private string forcedLongitude;
        private string forcedAccuracyInMeters;

        public OptionsViewModel()
        {            
            this.ForeceLocation = true;
            this.ForcedLatitude = "42.6509450";
            this.ForcedLongitude = "23.3792440";
            this.ForcedAccuracyInMeters = "50";
        }

        public bool ForeceLocation
        {
            get
            {
                return this.forceLocation;
            }
            set
            {
                this.forceLocation = value;
                this.RaisePropertyChange("ForeceLocation");
            }
        }

        public string ForcedLatitude
        {
            get
            {
                return this.forcedLatitude;
            }
            set
            {
                this.forcedLatitude = value;
                this.RaisePropertyChange("ForcedLatitude");
            }
        }

        public string ForcedLongitude
        {
            get
            {
                return this.forcedLongitude;
            }
            set
            {
                this.forcedLongitude = value;
                this.RaisePropertyChange("ForcedLongitude");
            }
        }

        public string ForcedAccuracyInMeters
        {
            get
            {
                return this.forcedAccuracyInMeters;
            }
            set
            {
                this.forcedAccuracyInMeters = value;
                this.RaisePropertyChange("ForcedAccuracyInMeters");
            }
        }

    }
}
