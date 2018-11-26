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
    public class CommentRepo
    {
        private SQLiteConnection Db;
        private SyncManager SyncManage;
        public List<Comment> Comments { get { return Db.Query<Comment>("SELECT * FROM Comments"); } }
        public CommentRepo(SQLiteConnection db)
        {
            this.Db = db;
            SyncManager syncManager = new SyncManager();
            this.SyncManage = syncManager;
        }
        public bool Sync()
        {
            if (this.SyncManage.HasInternet)
            {
                string json = this.SyncManage.GetJson("/Complaint/GetComments");
                List<Comment> apiComments = JsonConvert.DeserializeObject<List<Comment>>(json);
                /* If we were going to do this for production we would get just the ID's and then only pull a list of the ones we were for sure missing */


                // Remove any Comments that we have that we shouldnt have
                Comments.ForEach(comment => {
                    if (!apiComments.Contains(comment))
                    {
                        this.DeleteCommentLocal(comment);
                    }
                });
                // Add Any Comments we are missing
                apiComments.ForEach(comment => {
                    // If Comments does not have the complaint store it
                    if (!Comments.Contains(comment))
                    {
                        this.AddCommentLocal(comment);
                    }
                });

            }
            return false;
        }
        public bool AddCommentLocal(Comment comment)
        {
            var status = this.Db.Insert(new Comment()
            {
                ID = comment.ID,
                ComplaintID = comment.ComplaintID,
                Contents = comment.Contents
            });
            if (status == 1) { return true; }
            return false;
        }
        public bool DeleteCommentLocal(Comment comment)
        {
            var status = Db.Delete(comment);
            if (status == 1) { return true; }
            return false;
        }
        public bool AddCommentOnline(int complaintID,string content)
        {
            string json = this.SyncManage.PostWeb("/AddComment", ("id=" + complaintID + "&content=" + content));
            this.Sync();
            if (json.Length > 2) { return true; }
            return false;
        }

    }
}