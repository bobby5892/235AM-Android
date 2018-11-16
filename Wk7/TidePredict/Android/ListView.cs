using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using DataAccess_Library;
namespace TideApp
{
    [Activity(Label = "ListView")]
    public class ListView : ListActivity
    {
       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var dbPath = Path.Combine(
             System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "tide.db3");

            // It seems you can read a file in Assets, but not write to it
            // so we'll copy our file to a read/write location
            using (Stream inStream = Assets.Open("tide.db3"))
            using (Stream outStream = System.IO.File.Create(dbPath))
                inStream.CopyTo(outStream);
            var db = new SQLiteConnection(dbPath);
           
            string intentLocation = Intent.GetStringExtra("location");
            string intentDate = Intent.GetStringExtra("date");
            List<string> dataList = new List<string>();


            // Not most efficent - should be using a query
            var aDataList = db.Table<Tide>();

            foreach (var x in aDataList)
            {
                if (x.Location == intentLocation && x.DateTime.ToString() == intentDate) {
                    var ax = x;
                    string toAdd = (new DateTime(x.DateTime).ToShortDateString() + "\n " + x.Day  + "\n " + x.Time +" "+ x.PredictionFt +"ft\n " +  x.TideLevel
                    );

                    dataList.Add( toAdd);
                }
            }

           
            ListAdapter = new ArrayAdapter<String>(this,
                Android.Resource.Layout.SimpleListItem1, dataList.ToArray());


            ListView.FastScrollEnabled = true;

        }



    }
}