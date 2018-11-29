using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using ComplaintDepartment.Models;
using Newtonsoft.Json;

namespace ComplaintDepartment.Repositories
{
    public class ComplaintRepo
    {
        private SQLiteConnection Db;
        private SyncManager SyncManage;
        public List<Complaint> Complaints { get { return Db.Query<Complaint>("SELECT * FROM Complaints"); } }
        public ComplaintRepo(SQLiteConnection db)
        {
            this.Db = db;
            SyncManager syncManager = new SyncManager();
            this.SyncManage = syncManager;
        }
        /* This should be turned into an async function*/
        public bool Sync()
        {
            if (this.SyncManage.HasInternet)
            {
                string json = this.SyncManage.GetJson("/Complaint/GetComplaints");
                List<Complaint> apiComplaints = JsonConvert.DeserializeObject<List<Complaint>>(json);
                /* If we were going to do this for production we would get just the ID's and then only pull a list of the ones we were for sure missing */


                // Remove any complaints that we have that we shouldnt have
                Complaints.ForEach(complaint => {
                    if (!apiComplaints.Contains(complaint))
                    {
                        this.DeleteComplaintLocal(complaint);
                    }
                });
                // Add Any Complaints we are missing
                apiComplaints.ForEach(complaint => {
                    // If Complaints does not have the complaint store it
                    if (!Complaints.Contains(complaint))
                    {
                        this.AddComplaintLocal(complaint);
                    }
                });
               
            }
            return false;
        }
        public bool AddComplaintLocal(Complaint complaint)
        {
            var status = this.Db.Insert(new Complaint()
            {
                ID = complaint.ID,
                Completed = complaint.Completed,
                Contents = complaint.Contents,
                Create = complaint.Create
            });
            if(status == 1) { return true; }
            return false;
        }
        public bool DeleteComplaintLocal(Complaint complaint)
        {
            var status =Db.Delete(complaint);
            if (status == 1) { return true; }
            return false;
        }
        public bool AddComplaintOnline(string contents)
        {
            string json = this.SyncManage.PostWeb("/Complaint/AddComplaint", ("contents=" + contents));
            this.Sync();
            if(json.Length > 2) { return true; }
            return false;
        }
        public bool DeleteComplaintOnline(Complaint complaint)
        {
            if(complaint == null)
            {
                return false;
            }
            string json = this.SyncManage.PostWeb("/Complaint/DeleteComplaintByID", ("id=" + complaint.ID));
            this.Sync();
            if (json.Length > 2) { return true; }
            return false;
        }

    }
}