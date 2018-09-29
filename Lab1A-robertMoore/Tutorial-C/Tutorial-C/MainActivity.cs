using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace Tutorial_C
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Create the user interface in code
            var layout = new LinearLayout(this);
            layout.Orientation = Orientation.Vertical;
            var aLabel = new TextView(this);
            //aLabel.Text = "Hello, Xamarin.Android";
            aLabel.SetText(Resource.String.helloLabelText);
            var aButton = new Button(this);
            //aButton.Text = "Say Hello";
            aButton.SetText(Resource.String.helloButtonText);
            aButton.Click += (sender, e) => {
                aLabel.Text = "Hello from the button";
            };
            var aButtonReset = new Button(this);
            //aButton.Text = "Say Hello";
            aButtonReset.SetText(Resource.String.resetButtonText);
            aButtonReset.Click += (sender, e) => {
                aLabel.Text = "Hello Xamarin.Android";
            };
            
            layout.AddView(aButton);
            layout.AddView(aButtonReset);
            layout.AddView(aLabel);
            SetContentView(layout);
        }
    }
}