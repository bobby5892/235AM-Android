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
namespace ComplaintDepartment.Models
{
    [Table("Complaints")]
    public class Complaint
    {
        [PrimaryKey]
        public int ID { get; set; }
        public DateTime Create { get; set; }
        public string Contents { get; set; }
        public bool Completed { get; set; }
    }
}