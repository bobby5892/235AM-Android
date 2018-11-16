// Demo of using SQLite-net ORM
// Brian Bird, 5/20/13
// Converted to an exercise starter and completed
// By Brian Bird 5/12/16
using System;
using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;
using DataAccess_Library;
using System.Linq;

// Note: the namespace DataAccess.Android caused resolve problems, so I cahnged it
namespace DataAccess.Droid
{
    [Activity (Label = "Tide Viewer", MainLauncher = true)]
	public class Activity1 : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            /* ------ copy and open the dB file using the SQLite-Net ORM ------ */

            string dbPath = "";
			SQLiteConnection db = null;

			// Get the path to the database that was deployed in Assets
			dbPath = Path.Combine (
				System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), "stocks.db3");

			// It seems you can read a file in Assets, but not write to it
			// so we'll copy our file to a read/write location
			using (Stream inStream = Assets.Open ("stocks.db3"))
				using (Stream outStream = File.Create (dbPath))
					inStream.CopyTo(outStream);

			// Open the database
			db = new SQLiteConnection (dbPath);

			/* ------ Spinner initialization ------ */

			// Initialize the adapter for the spinner with stock symbols
			var distinctStocks = db.Table<Stock> ().GroupBy (s => s.Symbol).Select (s => s.First ());
			var stockSymbols = distinctStocks.Select (s => s.Symbol).ToList ();
			var adapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, stockSymbols);

			var stockSpinner = FindViewById <Spinner> (Resource.Id.stockSpinner);
			stockSpinner.Adapter = adapter;

			// Event handler for selected spinner item
			string selectedSymbol = "";
			stockSpinner.ItemSelected += delegate(object sender, AdapterView.ItemSelectedEventArgs e) {
				Spinner spinner = (Spinner)sender;
				selectedSymbol = (string)spinner.GetItemAtPosition(e.Position);
			};

			/* ------- DatePicker initialization ------- */

			var stockDatePicker = FindViewById<DatePicker> (Resource.Id.stockDatePicker);

			Stock firstDateStock = 
				db.Get<Stock>((from s in db.Table<Stock>() select s).Min(s => s.ID));
			DateTime firstDate = firstDateStock.Date;
			stockDatePicker.DateTime = firstDate;

			/* ------- Query for selected stock prices -------- */

			Button listViewButton = FindViewById<Button> (Resource.Id.listViewButton);
			ListView stocksListView = FindViewById<ListView> (Resource.Id.stocksListView);
			listViewButton.Click += delegate 
			{
				DateTime endDate = stockDatePicker.DateTime;
				DateTime startDate = stockDatePicker.DateTime.AddDays(-14.0);
				var stocks = (from s in db.Table<Stock>() 
					where (s.Symbol == selectedSymbol) 
						&& (s.Date <= endDate) 
						&& (s.Date >= startDate)
					select s).ToList ();
				// HACK: gets around "Default constructor not found for type System.String" error
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


