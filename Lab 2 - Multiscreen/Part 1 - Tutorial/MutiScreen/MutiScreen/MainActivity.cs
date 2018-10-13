using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace MutiScreen
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Use UI created in Main.axml
            SetContentView(Resource.Layout.activity_main);
            var showSecond = FindViewById<Button>(Resource.Id.secondActivity);

            showSecond.Click += (sender, e) => {
                var second = new Intent(this, typeof(SecondActivity));
                second.PutExtra("FirstData", "Data from FirstActivity");
                StartActivity(second);
            };
            

        }
    }
}