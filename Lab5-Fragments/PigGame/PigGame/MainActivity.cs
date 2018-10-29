using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using PigGame.Objects;
using Java.IO;
using Android.Graphics.Drawables;
using Newtonsoft.Json;
using Android.Util;
using Android.Content;

namespace PigGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
       
        EditText Player1Name;
        EditText Player2Name;
        Button newGameButton;
        Game game;
        const string DATA_KEY = "data";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            this.game = new Game();
            Player1Name = FindViewById<EditText>(Resource.Id.player1name);
            Player2Name = FindViewById<EditText>(Resource.Id.player2name);
            newGameButton = FindViewById<Button>(Resource.Id.newGameButton);

            newGameButton.Click += delegate
            {
                this.LoadSecondActivity();
            };

        }
        void LoadSecondActivity()
        {

            Intent intent = new Intent(this, typeof(SecondScreen));
            string json = JsonConvert.SerializeObject(this.game, Formatting.Indented);
            //Bundle savedInstanceState = new Bundle();
            // savedInstanceState.PutString(DATA_KEY, json);
            intent.PutExtra(DATA_KEY, json);
            this.StartActivity(intent);
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
           
            base.OnSaveInstanceState(outState);
        }
     

    }
}