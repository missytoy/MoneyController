namespace MoneyController.Helpers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using Models;

    public class GoogleApiGPSHelper
    {
        private const string ApiKey = "AIzaSyB0X8RR2WOkk7NbNZEisvj2C7o3TAEcraI";
        private const string RadiusInMeters = "50";

        public async Task<ObservableCollection<Place>> GetPlaces(string latitude, string longitude, string radiusInMeters = RadiusInMeters)
        {
            var googleApiData = await GoogleApiPlacesResult(latitude, longitude, radiusInMeters);
            return ParseResult(googleApiData);
        }

        private async Task<string> GoogleApiPlacesResult(string latitude, string longitude, string radiusInMeters = RadiusInMeters)
        {
            string urlBase = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?";
            string location = "location=" + latitude + "," + longitude;
            string radius = "&radius=" + radiusInMeters;
            string key = "&key=" + ApiKey;
            string url = urlBase + location + radius + key;

            string responseString = string.Empty;
            using (var client = new HttpClient())
            {
                responseString = await client.GetStringAsync(url);
            }
            return responseString;
        }

        private static ObservableCollection<Place> ParseResult(string googleApiPlacesJsonString)
        {
            var places = new ObservableCollection<Place>();
            var jsonData = JObject.Parse(googleApiPlacesJsonString);

            foreach (var result in (JArray)jsonData["results"])
            {
                var name = result["name"].Value<string>();
                var icon = result["icon"].Value<string>();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(icon))
                {
                    var place = new Place()
                    {
                        IconLink = icon,
                        Name = name
                    };
                    places.Add(place);
                }
            }

            return places;
        }
    }
}
