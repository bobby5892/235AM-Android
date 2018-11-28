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
    [Activity(Label = "AddComplaint")]
    public class AddComplaint : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.addComplaint);
            //Data
            SQLManager sqlManager = new SQLManager();
            CommentRepo commentRepo = new CommentRepo(sqlManager.Db);
            ComplaintRepo complaintRepo = new ComplaintRepo(sqlManager.Db);

            // Sync Between Website  - should be turned into async
            Task.Run(() => complaintRepo.Sync());
            Task.Run(() => commentRepo.Sync());

            // Bind 
            var textMultiLine = FindViewById<EditText>(Resource.Id.contentsEditText);
            var addComplaintButton = FindViewById<Button>(Resource.Id.AddComplaintButton);
            var goBackButton = FindViewById<Button>(Resource.Id.backButton);

            addComplaintButton.Click += delegate
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
            };

            goBackButton.Click += delegate
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
            };

        }
    }
}