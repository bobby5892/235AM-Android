using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace Tutorial_XML
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
            var aButton = FindViewById<Button>(Resource.Id.aButton);

            var resetButton = FindViewById<Button>(Resource.Id.resetButton);
            var aLabel = FindViewById<TextView>(Resource.Id.helloLabel);
            aButton.Click += (sender, e) => {
                aLabel.Text = "Hello from the button";
            };
            resetButton.Click += (sender, e) => {
                aLabel.Text = "Welcome to Xamarin";
            };

        }
    }
}