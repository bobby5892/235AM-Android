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

namespace ListViewProject
{
    // Not used but it would make things simpler from the crazy .
    public class TideItem
    {
        public int ID;
        public string Location;
        public string Day;
        public DateTime DateTime;
        public string Time;
        public string PredictionFt;
        /// <summary>
        ///  Either HIGH or LOW
        /// </summary>
        public string TideLevel;
    }
}