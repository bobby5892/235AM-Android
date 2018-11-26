using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using SQLite;
using System.IO;
using DataAccess_Library;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using System;
using Plugin.Geolocator;
using Plugin.CurrentActivity;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using System.Threading;
using Android.Locations;
using Java.Lang;

namespace TideApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        string selectedLocation = "";
        long selectedDate;
        Location currentLocation = new Location("");
        
       
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Geolocation
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            currentLocation.Latitude = 0;
            currentLocation.Longitude = 0;
           
            
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.activity_main);


            var dbPath = Path.Combine(
               System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "tide.db3");

            // It seems you can read a file in Assets, but not write to it
            // so we'll copy our file to a read/write location
            using (Stream inStream = Assets.Open("tide.db3"))
            using (Stream outStream = System.IO.File.Create(dbPath))
                inStream.CopyTo(outStream);

            // Open the database

            var db = new SQLiteConnection(dbPath);
            // Start Grabbing Position

            //var cities = db.Query<City>("SELECT * FROM City");
            /* Temp Fix - there is something wrong with import in console that populates the Cities Table */
            var cities = new List<City>();
            cities.Add(new City(){ Location="Charleston", Longitude=43.3401, Latitude=124.3301,ID= 0 });
            cities.Add(new City() { Location = "Reedsport", Longitude=43.7023,Latitude=124.0968,ID= 1 });
            cities.Add(new City() { Location = "Florence", Longitude=43.9826, Latitude=124.0998,ID= 2});
            /* end temp fix*/
            var locations = db.Query<Tide>("SELECT Location FROM Tides GROUP BY Location").Select(l => l.Location).ToArray();
            var datesInTicks = db.Query<Tide>("SELECT DateTime FROM Tides GROUP BY DateTime").Select(l => l.DateTime).ToArray();
            Task.Run(() => this.UpdatePosition(cities));


            string[] datesInString = new string[datesInTicks.Length];

            for (int i = 0; i < datesInTicks.Length; i++)
            {
                datesInString[i] = new DateTime(datesInTicks[i]).ToShortDateString();
            }
            
            
            // Location Adapter & Spinner
            
            var locationSpinner = FindViewById<Spinner>(Resource.Id.locationSpinner);
            locationSpinner.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, locations);

            
            locationSpinner.ItemSelected += delegate (object sender, AdapterView.ItemSelectedEventArgs e) {
                Spinner spinner = (Spinner)sender;
                selectedLocation = (string)spinner.GetItemAtPosition(e.Position);
            };

            // Date Adapter & Spinner
            
            var datesSpinner = FindViewById<Spinner>(Resource.Id.tideDatePicker);
            datesSpinner.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, datesInString);

            
            datesSpinner.ItemSelected += delegate (object sender, AdapterView.ItemSelectedEventArgs e) {
                Spinner spinner = (Spinner)sender;
                selectedDate = datesInTicks[e.Position];
            };

            //Bind Elements
            Button listViewButton = FindViewById<Button>(Resource.Id.buttonShowTides);
            listViewButton.Click += (sender, e) => {
                var second = new Intent(this, typeof(ListView));
                second.PutExtra("date", selectedDate.ToString());
                second.PutExtra("location", selectedLocation);
                StartActivity(second);
            };
          //  ListView stocksListView = FindViewById<ListView>(Resource.Id.stocksListView);
        }
        public bool IsLocationAvailable()
        {
            return CrossGeolocator.Current.IsGeolocationAvailable;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
        public async void UpdatePosition(List<City> cities)
        {
            while(currentLocation.Longitude == 0) { 
                try
                {
                    var locator = CrossGeolocator.Current;
                    var position = await locator.GetLastKnownLocationAsync();
                    position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
                    currentLocation = new Location("") { Longitude = position.Longitude, Latitude = position.Latitude };
                    // Update Box
                    TextView statusBox = FindViewById<TextView>(Resource.Id.closetLocation);
                  //  statusBox.Text = "Lat:" + String.Format("{0:0.00}", currentLocation.Latitude.ToString())  + " Long:" + String.Format("{0:0.00}", currentLocation.Longitude.ToString());
                    City closestCity = null;
                    Location closestCityLoc = new Location("");
                    
                    foreach(var city in cities){
                        if(closestCity == null)
                        {
                            closestCity = city;
                            closestCityLoc = new Location("") { Longitude = city.Longitude, Latitude = city.Latitude };
                        }
                        else
                        {
                            // If distance to the city we're checking is less than the distance we are currently at
                            Location temp = new Location("") {Longitude=city.Longitude,Latitude=city.Latitude };
                            if (closestCityLoc.DistanceTo(temp) < currentLocation.DistanceTo(temp))
                            {
                                closestCity = city;
                                closestCityLoc.Longitude = city.Longitude;
                                closestCityLoc.Latitude = city.Latitude;
                                //Modified for C# - Java version didn't work
                                /* Note: Only the original thread can update UI - so this shoves the UI update back to the original thread*/
                                RunOnUiThread(new Runnable( ()=> {
                                    statusBox.Text = "Closest: " + city.Location;
                                }));
                               // name
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    if (ex.ToString() == "aerfaerf") { //do nothing
                    }
                }
                
            }
        }
    }
}