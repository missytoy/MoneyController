namespace MoneyController.Helpers
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using Models;

    public class GoogleApiGPSHelper
    {
        private const string ApiKey = "AIzaSyB0X8RR2WOkk7NbNZEisvj2C7o3TAEcraI";
        private const string RadiusInMeters = "50";
        
        // TODO: get location https://msdn.microsoft.com/en-us/library/windows/desktop/mt219698.aspx

        private async Task<string> GoogleApiPlacesResult(string latitude, string longitude)
        {
            string urlBase = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?";
            string location = "location=" + latitude + "," + longitude;
            string radius = "&radius=" + RadiusInMeters;
            string key = "&key=" + ApiKey;
            string url = urlBase + location + radius + key;

            string responseString = string.Empty;
            using (var client = new HttpClient())
            {
                responseString = await client.GetStringAsync(url);
            }
            return responseString;
        }

        private static List<Place> ParseResult(string googleApiPlacesJsonString)
        {
            var places = new List<Place>();
            var jsonData = JObject.Parse(googleApiPlacesJsonString);
            
            foreach (var result in (JArray)jsonData["results"])
            {
                var place = new Place();
                place.Name = result["name"].Value<string>();
                place.IconLink = result["icon"].Value<string>();
                places.Add(place);
            }

            return places;
        }
    }


    //API key
    //Here is your API key
    //AIzaSyB0X8RR2WOkk7NbNZEisvj2C7o3TAEcraI

    //https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=-33.8670,151.1957&radius=500&types=food&name=cruise&key=YOUR_API_KEY
    // location — The latitude/longitude
    // radius — Defines the distance (in meters)
    // key: API_KEY
    //https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=42.6509450,23.3792440&radius=50&key=AIzaSyB0X8RR2WOkk7NbNZEisvj2C7o3TAEcraI

    //{
    //   "html_attributions" : [],
    //   "next_page_token" : "CoQC8wAAADfi1-8tto9ednYx4HXxVqMKGP5rOo8ypK5oGHFkSolkf1FQtERwOo-qgCxBLVMZxDfCkxpsavWdBUTwG857JO36_xKwyPil1bXmIbeue-lmGrTYOJPF24lVnVS7KWziuh8rtl1GoGZ2D9grgNUEmejtwfz5AqdH_FK85pxSxBaC1mgvnJM_4zIRNB7I-0vpGnxxPR3ggeiYN5oCt_xHO3LgRQPVagraHIg7DF80h7jRlbC9Dalro8kEwbncItEoWU6SBHxr5SH4GcBJRTbcRq4xrU_t7zagPKe0LDkw62MNDHlTyShuuNfaNx62bzorPuMtbrdzeHgE8ZevG8ZBrnwSEEOOFIvgjDVUh172P5xzZeYaFCRJmYaQKi-gw-LBN0BIE8VdWwK_",
    //   "results" : [
    //      {
    //         "geometry" : {
    //            "location" : {
    //               "lat" : 42.6569665,
    //               "lng" : 23.3766141
    //            },
    //            "viewport" : {
    //               "northeast" : {
    //                  "lat" : 42.6683831,
    //                  "lng" : 23.3846568
    //               },
    //               "southwest" : {
    //                  "lat" : 42.64807400000001,
    //                  "lng" : 23.3575182
    //               }
    //            }
    //         },
    //         "icon" : "https://maps.gstatic.com/mapfiles/place_api/icons/geocode-71.png",
    //         "id" : "b95f99035652554cfd33e54f1a0ccc73c0e15d52",
    //         "name" : "ж.к. Младост 1",
    //         "place_id" : "ChIJ0aSv9oaGqkAR26uLusMq3yI",
    //         "reference" : "CpQBiwAAAJp0KdFl8otha6OImTJgzBQSwgfJLsH6JX9zN9pOH4QOQqeIIQrf4ndzgpoHi1zMPN-baQG5VjVgPGnGFu6TImAn8h2M9G-Uae_nqKCesXkfCAEqe9pvU0vtA1gQV2u-HaR-t79JoUJiVYJamkK2TpXWZIaQS4mBjLD6Ps8HWfeOlfEmIkIHhwE4VfaH81hYZhIQtZRIubLzU5oKZ3exgSeC0RoUgr2gzCSu71JwpJSCycd3kjXHKR0",
    //         "scope" : "GOOGLE",
    //         "types" : [ "neighborhood", "political" ],
    //         "vicinity" : "София"
    //      },
    //      {
    //         "geometry" : {
    //            "location" : {
    //               "lat" : 42.65084700000001,
    //               "lng" : 23.379431
    //            }
    //         },
    //         "icon" : "https://maps.gstatic.com/mapfiles/place_api/icons/school-71.png",
    //         "id" : "fd3796688aae9e6c004d2e24a123785390ba9b8e",
    //         "name" : "Telerik Academy",
    //         "opening_hours" : {
    //            "open_now" : false,
    //            "weekday_text" : []
    //         },
    //         "photos" : [
    //            {
    //               "height" : 479,
    //               "html_attributions" : [
    //                  "\u003ca href=\"https://maps.google.com/maps/contrib/110825707792096672053/photos\"\u003eTelerik Academy\u003c/a\u003e"
    //               ],
    //               "photo_reference" : "CmRdAAAA1jZJJwoRhJfjHPRyt5HqYL-EzndCDdihA9Rs4eOJBsUGlxWbtnyw-REW5hvNDCMmpQGmqQaqGtL7U4j8q9oHTgbArP-Z1tqS97COaVTQyW6MEWgPHf_f9t74ByQ-BJ2cEhB9fbq_K2x7G4at3MNSYgPHGhTaoFzBv9xI589OWyyPtijQ16pLLA",
    //               "width" : 579
    //            }
    //         ],
    //         "place_id" : "ChIJa-QiA5SGqkARIbhz0Ykqz50",
    //         "rating" : 4.8,
    //         "reference" : "CnRjAAAAHXNJS_sMx_YrhXQckV7xKZw2Cvu7j2BO4VJIVGT-GwiG4uXNNvjOP-TL_otIOQwDBFWbgLd2Gb8fLCEYP1eNs6YoWo4Y5a2jhMtyM65H6wJ1_Ob4o05yyjyxVU9cYV8EgGdaZ0FEIMhP1ycZzU8ZgRIQUnV3k0n0vQqGiLAVSQsW0hoU-cBB2oQMCGmp4f5iANHckUtPvJ8",
    //         "scope" : "GOOGLE",
    //         "types" : [ "point_of_interest", "establishment" ],
    //         "vicinity" : "bulevard \"Aleksandar Malinov\" 31, Sofia"
    //      }
    //   ],
    //   "status" : "OK"
    //}

    //https://maps.googleapis.com/maps/api/place/nearbysearch/xml?location=42.6509450,23.3792440&radius=50&key=AIzaSyB0X8RR2WOkk7NbNZEisvj2C7o3TAEcraI

    //<PlaceSearchResponse>
    //<result><name>Telerik Academy</name><vicinity>bulevard "Aleksandar Malinov" 31, Sofia</vicinity><type>point_of_interest</type><type>establishment</type><geometry><location><lat>42.6508470</lat><lng>23.3794310</lng></location></geometry><rating>4.8</rating><icon>https://maps.gstatic.com/mapfiles/place_api/icons/school-71.png</icon><reference>CnRjAAAArOThCNT8stnVBRa7A7a1DybhDOOjiqVlEZUO3GWm60PJN2YiaDp0i_LuhEW16RyGadZCK8vg4DZHSbBs0GMaUan086chabY1dqmH3eT5NMfcrbGWIBqZn8_wFlPvkkZzsr_R5X1FoFXC25aCtRNnlxIQFpvhHTN2IF5gXvHxTpro2BoUi9aXxWaK9A70lQYgLyBADirast4</reference><id>fd3796688aae9e6c004d2e24a123785390ba9b8e</id><opening_hours><open_now>false</open_now></opening_hours><photo><photo_reference>CmRdAAAAH37k9av3ZwpppVR6syFPSXVAPzbwMlEwaiYd7GI2K8OKkpiKV_6mjfz7r2sPliixItJVPlMa1YZ4P9Q3b4LDN2B64-YmEjAhAaoR5lhlAPWC8_gLqfppf6LEJXhhy8IeEhB6GA3IPfAul53c_x54yAuJGhSqbSG5buX0FCnBIdKDBSfsqvsWWg</photo_reference><width>579</width><height>479</height><html_attribution><a href="https://maps.google.com/maps/contrib/110825707792096672053/photos">Telerik Academy</a></html_attribution></photo><place_id>ChIJa-QiA5SGqkARIbhz0Ykqz50</place_id><scope>GOOGLE</scope></result>
    //<result><name>OMV бензиностанция</name><vicinity>Alexander Malinov Blvd, Sofia</vicinity><type>gas_station</type><type>point_of_interest</type><type>establishment</type><geometry><location><lat>42.6512700</lat><lng>23.3794900</lng></location></geometry><rating>3.9</rating><icon>https://maps.gstatic.com/mapfiles/place_api/icons/gas_station-71.png</icon><reference>CoQBdAAAAJ_phANlEiSH-b-YtVoL3yyttiSo5H6f1VTzHRo9Ke8YFLt3ZTuck0laBFeJL1xqR97HbQSUNSQnkGSiFr1iOKbvF5H9WLi6ET1JYdYt4Jv7z3XQhj9GGcJNVTH6wHdbEiY2CnhWaoyPq8rCpolafsD3yWH-WrccGpumqwrOvz84EhD53q4VrGdwQTmN1U2hVUI4GhS4IG8mh73J4De6nO0g26tkSXNGdw</reference><id>782a714a3444c34fe7ecf1d237ccfc2693884665</id><place_id>ChIJPZhnAY6GqkARHqvHQLDU39U</place_id><scope>GOOGLE</scope></result>
    //</PlaceSearchResponse>

}
