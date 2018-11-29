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
        public int ComplaintID { get; set; }
        public CommentRepo CommentRepo { get; set; }
        public ComplaintRepo ComplaintRepo { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.viewComplaint);
            //Data
            SQLManager sqlManager = new SQLManager();
            CommentRepo = new CommentRepo(sqlManager.Db);
            ComplaintRepo = new ComplaintRepo(sqlManager.Db);
            // Sync Between Website  - should be turned into async
           // Task.Run(() => ComplaintRepo.Sync());
           // Task.Run(() => CommentRepo.Sync());

            var timestampLabel = FindViewById<TextView>(Resource.Id.timeStamp);
          
            var statusSwitch = FindViewById<Switch>(Resource.Id.statusComplete);
            var complaintContentsTextbox = FindViewById<EditText>(Resource.Id.contentsEditText);
            var deleteComplaintbutton = FindViewById<Button>(Resource.Id.DeleteComplaintButton);
            var goBackButton = FindViewById<Button>(Resource.Id.backButton);

            int idComplaint = Intent.GetIntExtra("ID",0);
            this.ComplaintID = idComplaint;
            var complaint = ComplaintRepo.Complaints.First(c => c.ID == idComplaint) ?? null;

            // Now fill it in
            timestampLabel.Text = complaint.Create.ToShortDateString();
         
            
            if (complaint.Completed)
            {
                statusSwitch.Text = "Completed";
                statusSwitch.Checked = true;
            }
            else
            {
                statusSwitch.Text = "InComplete";
                statusSwitch.Checked = false;
            }
            complaintContentsTextbox.Text = complaint.Contents;
         
            deleteComplaintbutton.Click += delegate
            {
               
                ComplaintRepo.DeleteComplaintOnline(ComplaintRepo.Complaints.First(c => c.ID == idComplaint));
              
                StartActivity(new Intent(this, typeof(MainActivity)));
            };
            goBackButton.Click += delegate
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
            };
            statusSwitch.Click += delegate
             {
                 ComplaintRepo.ToggleComplete(ComplaintRepo.Complaints.First(c => c.ID == idComplaint));
                 if(statusSwitch.Text == "Completed")
                 {
                     statusSwitch.Text = "Incomplete";
                 }
                 else
                 {
                     statusSwitch.Text = "Completed";
                 }
                // StartActivity(new Intent(this, typeof(MainActivity)));
             };
        }
    }
}