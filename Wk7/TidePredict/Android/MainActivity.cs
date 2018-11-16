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

namespace TideApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        string selectedLocation = "";
        long selectedDate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
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
    }
}