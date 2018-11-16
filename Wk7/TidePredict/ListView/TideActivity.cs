using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using System.Xml;
using System.IO;
using Android.Content.Res;
using Java.IO;
using System.Collections.Generic;

namespace ListViewProject
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class TideActivity : ListActivity
    {
        List<IDictionary<string, object>> dataList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
 
            var reader = new XmlTideFileParser(Assets.Open(@"9434032_annual.xml"));
            dataList = reader.TideList;

            // XmlTideFileParser xmlParser = new XmlTideFileParser(content);
            /* very  sort
                        dataList.Sort((x, y) => String.Compare((string)x[XmlVocabFileParser.POS] + (string)x[XmlVocabFileParser.SPANISH],
                      (string)y[XmlVocabFileParser.POS] + (string)y[XmlVocabFileParser.SPANISH],
                    StringComparison.Ordinal));
                   */
            
     //ListAdapter = new SimpleAdapter(this, dataList,
     ListAdapter = new TideAdapter(this, dataList,
                     Resource.Layout.customListRow,
                     new string[] { XmlTideFileParser.DATE,  XmlTideFileParser.HEIGHT, XmlTideFileParser.HI_LOW },
                     new int[] { Resource.Id.textViewFirstThing, Resource.Id.textViewSecondThing, Resource.Id.textViewThirdThing }
                 );

            ListView.FastScrollEnabled = true;

        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var tide = dataList[position].TryGetValue("pred_in_ft", out var value);
            Android.Widget.Toast.MakeText(this, ("Height: " + value.ToString()+ "ft"), Android.Widget.ToastLength.Short).Show();
        }
       


    }
}