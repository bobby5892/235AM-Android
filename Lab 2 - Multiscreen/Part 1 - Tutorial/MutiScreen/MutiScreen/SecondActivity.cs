﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MutiScreen
{
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.secondActivity);

            var label = FindViewById<TextView>(Resource.Id.screen2Label);
            label.Text = Intent.GetStringExtra("FirstData");

        }
    }
}