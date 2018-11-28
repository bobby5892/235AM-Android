using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ComplaintDepartment.Repositories;

namespace ComplaintDepartment
{
    [Activity(Label = "ViewComplaint")]
    public class ViewComplaint : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            //Data
            SQLManager sqlManager = new SQLManager();
            CommentRepo commentRepo = new CommentRepo(sqlManager.Db);
            ComplaintRepo complaintRepo = new ComplaintRepo(sqlManager.Db);

            // Sync Between Website  - should be turned into async
            Task.Run(() => complaintRepo.Sync());
            Task.Run(() => commentRepo.Sync());

            var timestampLabel = FindViewById<TextView>(Resource.Id.timeStamp);
            var completedStatusLabel = FindViewById<TextView>(Resource.Id.timeStamp);
            var statusSwitch = FindViewById<Switch>(Resource.Id.statusComplete);
            var complaintContentsTextbox = FindViewById<EditText>(Resource.Id.contentsEditText);
            var deleteComplaintbutton = FindViewById<Button>(Resource.Id.DeleteComplaintButton);
            var goBackButton = FindViewById<Button>(Resource.Id.backButton);

            deleteComplaintbutton.Click += delegate
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
            };
            goBackButton.Click += delegate
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
            };
            statusSwitch.Click += delegate
             {

             };
        }
    }
}