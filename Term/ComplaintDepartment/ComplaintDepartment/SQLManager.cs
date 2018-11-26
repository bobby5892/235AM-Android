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
using ComplaintDepartment.Models;

namespace ComplaintDepartment
{
    public class SQLManager
    {
        private string fileName;
        public string storagePath;
        public string fullDBPath;
        public SQLiteConnection Db { get; }
        public SQLManager()
        {
            fileName = "complaintDepartment.db3";
            storagePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            fullDBPath = storagePath + "/" + fileName;

            this.Db = new SQLiteConnection(fullDBPath);
           
           // Create Tables (won't do anything if they already exist)
            Db.CreateTable<Complaint>();
            Db.CreateTable<Comment>();
           

            
            // Need to have a valid connection

        }
        
        
       
    }
}