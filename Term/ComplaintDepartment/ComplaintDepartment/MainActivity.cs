using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Threading.Tasks;
using ComplaintDepartment.Repositories;
using Android.Content.Res;
using Java.IO;
using System.Linq;
using System.Collections.Generic;

namespace ComplaintDepartment
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public ListView ListViewer;
        public Button ButtonAddComplaint;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //Data
            SQLManager sqlManager = new SQLManager();
            CommentRepo commentRepo = new CommentRepo(sqlManager.Db);
            ComplaintRepo complaintRepo = new ComplaintRepo(sqlManager.Db);

            // Sync Between Website
            Task.Run(() => complaintRepo.Sync());
            Task.Run(() => commentRepo.Sync());

            // Bind Items
            this.ListViewer = FindViewById<ListView>(Resource.Id.listViewer);
            this.ButtonAddComplaint = FindViewById<Button>(Resource.Id.buttonAddComplaint);

            ButtonAddComplaint.Click += delegate
            {
                //Intent Load Add ComplaintForm
            };
            // Lets build our dictionary
            List<IDictionary<string, object>> complaintsDictionary = new List<IDictionary<string, object>>();
            // first lamba key, second value

            // Lets populate our dictionary
            complaintRepo.Complaints.ForEach(complaint => {
                // Now Lets convert this to a dictionary and add it to the dictionary
                complaintsDictionary.Add(new JavaDictionary<string, object>() {
                    ["Completed"]=complaint.Completed,
                    ["Contents"]=complaint.Contents,
                    ["Create"]=complaint.Create,
                    ["ID"]=complaint.ID
                });
            });

            this.ListViewer.Adapter = new ComplaintAdapter(this, complaintsDictionary, Resource.Layout.customRow,
            new string[] { "Completed","Contents","Create"},
            new int[] { Resource.Id.textViewFirstThing, Resource.Id.textViewSecondThing, Resource.Id.textViewThirdThing }
            );
            // Enable FastScroll
            this.ListViewer.FastScrollEnabled = true;

            /* ListAdapter = new TideAdapter(this, dataList,
                     Resource.Layout.customListRow,
                     new string[] { XmlTideFileParser.DATE, XmlTideFileParser.HEIGHT, XmlTideFileParser.HI_LOW },
                     new int[] { Resource.Id.textViewFirstThing, Resource.Id.textViewSecondThing, Resource.Id.textViewThirdThing }
                 );
                 */

            //  ListView.FastScrollEnabled = true;



        }
    }
}