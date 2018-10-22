using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace Poke
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button buttonPoke = FindViewById<Button>(Resource.Id.buttonPoke);
            Button buttonHi = FindViewById<Button>(Resource.Id.buttonSayHi);

            buttonPoke.Click += (sender, e) => {
                // StartActivity(typeof(SecondActivity));
                var second = new Intent(this, typeof(SecondActivity));
                second.PutExtra("secondText", "Screen 1 poked you");
                StartActivity(second);
            };
            buttonHi.Click += (sender, e) => {
                var second = new Intent(this, typeof(SecondActivity));
                second.PutExtra("secondText", "hello from screen 1");
                StartActivity(second);
            };
        }
    }
}