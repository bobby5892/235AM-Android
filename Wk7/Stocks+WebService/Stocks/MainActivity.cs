using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;
using DataAccess.DAL;
using System.Linq;

// Note: the namespace DataAccess.Android caused resolve problems, so I cahnged it
using Android;


namespace DataAccess.Droid
{
	[Activity (Label = "Stocks dB Exercise", MainLauncher = true)]
	public class Activity1 : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			string dbPath = "";
			SQLiteConnection db = null;

			// Get the path to the database that was deployed in Assets
			dbPath = Path.Combine (
				System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), "stocks.db3");

			// It seems you can read a file in Assets, but not write to it
			// so we'll copy our file to a read/write location
			//if(!File.Exists (dbPath))
			{
				using (Stream inStream = Assets.Open ("stocks.db3"))
					using (Stream outStream = File.Create (dbPath))
						inStream.CopyTo(outStream);
			}

			// Open the database
			db = new SQLiteConnection (dbPath);

			Button queryButton = FindViewById<Button> (Resource.Id.queryButton);
			TextView queryText = FindViewById<TextView> (Resource.Id.queryTextView);
			queryButton.Click += delegate 
			{
				// Query using Linq
				int count = (from s in db.Table<Stock> () select s).Count();
				queryText.Text = count.ToString();
			};

			Button listViewButton = FindViewById<Button> (Resource.Id.listViewButton);
			ListView stocksListView = FindViewById<ListView> (Resource.Id.stocksListView);
			listViewButton.Click += delegate 
			{
				//var stockNamesArray = (from stock in db.Table<Stock>() select stock.Name).ToArray ();
				var stocks = (from stock in db.Table<Stock>() select stock).ToList ();
				// HACK: gets around "Default constructor not found for type System.String" error
				// var stockNames = stocks.Select (s => s.Name).ToArray();
				// var stockNamesArray = stockNames.ToArray ();
				int count = stocks.Count;
				string[] stockInfoArray = new string[count];
				for(int i = 0; i < count; i++)
				{
					stockInfoArray[i] = 
						stocks[i].Date.ToShortDateString() + "\t\t" + stocks[i].Name + "\t\t" + stocks[i].ClosingPrice;
				}
					

				stocksListView.Adapter = 
					new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, stockInfoArray);
			};
		}

	}
}


