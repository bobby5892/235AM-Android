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

namespace Poke
{
    [Activity(Label = "secondActivity", ParentActivity = typeof(MainActivity))]
    public class SecondActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.secondActivity);
            // Stack overflow fix for no action bar
           // Window.RequestFeature(WindowFeatures.ActionBar); // Doesn't work

            //ActionBar.SetDisplayShowHomeEnabled(true); Errors on use ActionBar is null

            var label = FindViewById<TextView>(Resource.Id.screen2Message);
            label.Text = Intent.GetStringExtra("secondText") ?? "Data not available";
            // Create your application here
        }
    }
}