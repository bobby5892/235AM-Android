using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using System.Xml;
using System.IO;
using Android.Content.Res;
using Java.IO;
using SQLite.Net;
using System.Collections.Generic;
using DataAccess_Library;
using System;
using System.Linq;
using Android;

namespace ListViewProject
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : ListActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainActivity);
            var dbPath = Path.Combine(
                 System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "tide.db3");

            // It seems you can read a file in Assets, but not write to it
            // so we'll copy our file to a read/write location
            using (Stream inStream = Assets.Open("tide.db3"))
            using (Stream outStream = System.IO.File.Create(dbPath))
                inStream.CopyTo(outStream);

            // Open the database

            var db = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), dbPath);


            //var query = db.Table<Tide>().Where(v => v.Location);
            var locations = db.Query<Tide>("SELECT Location FROM Tides GROUP BY Location");
            var dates = db.Query<Tide>("SELECT DateTime FROM Tides GROUP BY DateTime");

            /*var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, locations);
            var locationSpinner = FindViewById<Spinner>(Resource.Id.locationSpinner);
            locationSpinner.Adapter = adapter;

            string selectedLocation = "";
            locationSpinner.ItemSelected += delegate (object sender, AdapterView.ItemSelectedEventArgs e) {
                Spinner spinner = (Spinner)sender;
                selectedLocation = (string)spinner.GetItemAtPosition(e.Position);
            };

            var tideDatePicker = FindViewById<DatePicker>(Resource.Id.tideDatePicker);

            Tide firstDateTide =
                db.Get<Tide>((from s in db.Table<Tide>() select s).Min(s => s.ID));
            DateTime firstDate = firstDateTide.DateTime;
            tideDatePicker.DateTime = firstDate;
            */
            /*
            
            Button listViewButton = FindViewById<Button>(Resource.Id.buttonShowTides);
            ListView stocksListView = FindViewById<ListView>(Resource.Id.stocksListView);
            listViewButton.Click += delegate
            {
                DateTime endDate = stockDatePicker.DateTime;
                DateTime startDate = stockDatePicker.DateTime.AddDays(-14.0);
                var stocks = (from s in db.Table<Stock>()
                              where (s.Symbol == selectedSymbol)
                                  && (s.Date <= endDate)
                                  && (s.Date >= startDate)
                              select s).ToList();
                // HACK: gets around "Default constructor not found for type System.String" error
                int count = stocks.Count;
                string[] stockInfoArray = new string[count];
                for (int i = 0; i < count; i++)
                {
                    stockInfoArray[i] =
                        stocks[i].Date.ToShortDateString() + "\t\t" + stocks[i].Name + "\t\t" + stocks[i].ClosingPrice;
                }

                stocksListView.Adapter =
                    new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, stockInfoArray);
            };
               */           

        }
    }
}