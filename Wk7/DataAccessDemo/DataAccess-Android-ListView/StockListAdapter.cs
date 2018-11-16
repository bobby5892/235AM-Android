using Android.Widget;
using Android.App;
using Android.Database;
using Android.Content;
using Android.Views;

namespace DataAccessAndroidListView
{
	public class StockListAdapter : CursorAdapter
	{
		private Activity context;

		public StockListAdapter(Activity context, ICursor cursor) 
			: base (context, cursor)
		{
			this.context = context;
		}

		public override void BindView (View view, Context context, ICursor cursor)
		{
			throw new System.NotImplementedException ();
		}

		public override View NewView (Context context, ICursor cursor, ViewGroup parent)
		{
			throw new System.NotImplementedException ();
		}
	}
}

