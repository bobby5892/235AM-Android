using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;

namespace DataAccessAndroidListView
{
	[Activity (Label = "DataAccess-Android-ListView", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : ListActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Get all the stock data in a cursor
			var db = new StockDatabase(this);
			var cursor = db.ReadableDatabase.RawQuery ("SELECT * FROM Stocks", null);

			// Provide the adapter with the data
			ListAdapter = new StockListAdapter (this, cursor);

		}


	}
}


