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

namespace TideApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        string selectedLocation = "";
        long selectedDate;
        public struct myPosition
        {
            public double Longitutde;
            public double Latitude;
        }
        myPosition currentLocation = new myPosition();
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Geolocation
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            currentLocation.Latitude = 0;
            currentLocation.Longitutde = 0;
           
            
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
            Task.Run(() => this.UpdatePosition(db));
            var locations = db.Query<Tide>("SELECT Location FROM Tides GROUP BY Location").Select(l => l.Location).ToArray();

            var datesInTicks = db.Query<Tide>("SELECT DateTime FROM Tides GROUP BY DateTime").Select(l => l.DateTime).ToArray();
            


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
        public double getDistance(double x1, double x2, double y1, double y2)
        {
            return Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2);
        }
        public async void UpdatePosition(SQLiteConnection db)
        {
            while(currentLocation.Longitutde == 0) { 
                try
                {
                    var locator = CrossGeolocator.Current;
                    var position = await locator.GetLastKnownLocationAsync();
                    position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
                    currentLocation.Latitude =  position.Latitude;
                    currentLocation.Longitutde = position.Longitude;

                    // Update Box
                    TextView statusBox = FindViewById<TextView>(Resource.Id.closetLocation);
                    statusBox.Text = "Lat:" + currentLocation.Latitude.ToString() + " Long:" + currentLocation.Longitutde.ToString();
                    City closestCity = null;
                    var cities = db.Query<City>("SELECT * FROM City");
                    foreach(var city in cities){
                        if(closestCity == null)
                        {
                            closestCity = city;
                        }
                        else
                        {
                            // If distance to the city we're checking is less than the distance we are currently at
                            if(getDistance(currentLocation.Latitude, currentLocation.Longitutde, closestCity.Latitude, closestCity.Longitude) > getDistance(currentLocation.Latitude, currentLocation.Longitutde, city.Latitude, city.Longitude))
                            {
                                closestCity = city;
                                currentLocation.Longitutde = city.Longitude;
                                currentLocation.Latitude = city.Latitude;
                                statusBox.Text = city.Location; // name
                            }



                        }
                    }

                }
                catch (Exception ex)
                {
                    if (ex.ToString() == "aerfaerf") { //do nothing
                    }
                }
                
            }
        }
    }
}